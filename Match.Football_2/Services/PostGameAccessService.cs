using Arneb.Common.Common;
using Arneb.Common.Services;
using Arneb.WebJobs.Live.scaffolding.repositories;
using System;
using System.Configuration;
using System.Net.Http;
using Arneb.Common;
using System.Linq;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Arneb.WebJobs.Live.Models.GeniusService;
using Arneb.WebJobs.Live.Models.LiveServices;
using FluentScheduler;
using Arneb.WebJobs.Live.Models.Api;
using System.Text;

namespace Arneb.WebJobs.Live.Services
{
    public class PostGameAccessService
    {
        /// <summary>
        /// Finalizacion del partido.
        /// </summary>
        public async Task FinishGame(string messageNotification, int matchId,string local, string visitante)
        {
            try
            {

                var apiURL = ConfigurationManager.AppSettings["restAPI"];
                LogService.AddLog(Notifications.OutputConsole("Finalizó el juego."));

                var _key = ApiRestServices<IEnumerable<Params>>.Get(String.Concat(ConfigurationManager.AppSettings["restAPI"], "api/Parameters/GetAll"))
                                                          .GetAwaiter()
                                                          .GetResult()
                                                          .Where(x => x.ParamKey == Common.Constants.Parameters.KeyApiNotificationPush.ToString())
                                                          .Select(x => x.Value)
                                                          .SingleOrDefault();

                var _KeyDecrypted = NotificationService.DecryptPass(_key);

                var _keyEncrypted = NotificationService.EncryptPass(NotificationService.EncryptPass(String.Concat(NotificationService.EncryptPass("@bR@s8@skET2017"), _KeyDecrypted)));

                var result = await NotificationService.SendNotification(new Common.ViewModel.NotificationPushViewModel
                {
                    Key = _keyEncrypted,
                    Message = messageNotification
                }, $"{ConfigurationManager.AppSettings["restAPI"]}api/NotificationPush");
                var Data = new UpdateStatus
                {
                    MatchID = matchId,
                    Status = Constants.MatchStatus.COMPLETE,
                    ScoreVisitor = visitante,
                    ScoreLocal = local
                };
                await ApiRestServices<string>.Put($"{apiURL}api/Calendar/", Data);
                //Despues de enviar la notificacion push debe genera una tarea que corra 1min despues. para
                if (!JobManager.RunningSchedules.Any(x => x.Name == "SaveBoxScoreMessageData"))
                {
                    IJob job = new Job(matchId);
                    JobManager.AddJob(job, s => s.WithName("SaveBoxScoreMessageData").NonReentrant().ToRunEvery(4).Minutes());
                }
                await LogService.SaveLog();
            }
            catch (Exception e)
            {
                LogService.AddLog(Notifications.OutputConsole($"Ocurrió un error al enviar la notificación: {e.Message}"));
            }
            finally
            {
                await LogService.SaveLog();
            }

        }

        //public void TestFinish(string messageNotification, int matchId)
        //{
        //    var listJob = JobManager.RunningSchedules;
        //    IJob job = new Job(matchId);
        //    JobManager.AddJob(job, s => s.WithName("PrimeraVez").NonReentrant().ToRunOnceIn(15).Seconds());
        //}

        #region Class for fluensheduler Method async
        /// <summary>
        /// Clase para tareas que sean asyc fluensheduler
        /// </summary>
        public class Job : IJob,IDisposable
        {
            private int matchId;

            public Job(int _mactId)
            {
                matchId = _mactId;
            }
            public async void Execute()
            {
               await SaveBoxScoreMessageData(matchId);
            }

            public void Dispose()
            {
               // Console.WriteLine($"Disposing job {_job}");
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public void ResumenGame()
        {


            LogService.AddLog(Notifications.OutputConsole("Resumen del juego."));


        }

        /// <summary>
        /// Guarda el objeto que contiene a los dos equipos del partido finalizado en un DocumentDB en Azure
        /// </summary>
        /// <param name="matchId">Es el número de juego del partido provisto por Genius</param>
        /// <returns></returns>
        public static async Task SaveBoxScoreMessageData(int matchId)
        {
            try
            {
                #if DEBUG
                //esta condición evita que se lance una llamada a la api de genius
                //la cual da como resultado un error (status 401 "unauthorized")
                //debido a que la key de test(ak=fb3f05fc50daa647a2dba428c9e32bfb) es erronea 
                if (matchId == 36475)
                {
                    return;
                }                   
                #endif
                var _context = new DbObrasDev01Context();
                ParamsRepository _rep = new ParamsRepository(_context);
                JObject TestData;

                var _listParams = _rep.GetAll().ToList();
                var _competitionId = _listParams.Where(x => x.ParamKey == Constants.Parameters.CompetitionId.ToString())
                    .Select(s => s.Value)
                    .SingleOrDefault();
               
                var ak = "?ak=" + ConfigurationManager.AppSettings["appKeyGenius"];
                var _geniusApi = ConfigurationManager.AppSettings["geniusAPI"] + matchId + ak + "&periodNumber=0&limit=100";
                var response = await ApiRestServices<IDictionary<string,PagedResultRest<PersonMatch>>>.Get(_geniusApi);

                if (response["response"].Data.Count!=0)
                {
                    PostGameMessage test = CompleteMatchWithTeamsAndPlayers(response["response"].Data.ToList());
                    var _result = JsonConvert.SerializeObject(test);
                    TestData = JObject.Parse(_result);

                    // aca es donde se realiza el guardado en documentDB en Azure
                    DocumentClient _client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endPointUri"]), ConfigurationManager.AppSettings["authKeyString"]);
                    //borro el ultimo registro generado
                    //await _client.DeleteDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri("BoxScore", "Obras-Dev"));
                    await _client.CreateDatabaseIfNotExistsAsync(new Database { Id = "BoxScore" });
                    await _client.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri("BoxScore"), new DocumentCollection { Id = "Obras-Dev" });                            
                    await _client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("BoxScore", "Obras-Dev"), TestData);
                    //remover la tarea para que no se ejecuten cada 30min
                    JobManager.RemoveJob("SaveBoxScoreMessageData");
                }
                else
                {
                    LogService.AddLog(Notifications.OutputConsole("La respuesto desde la API de Genius fue correcta, pero no se obtuvo datos."));                           
                }

                await LogService.SaveLog();   
            }
            catch (Exception e)
            {
                LogService.AddLog(Notifications.OutputConsole($"Ocurrió un error: {e.Message}"));
            }
            finally
            {
                await LogService.SaveLog();
            }
        }        

        #region HelperMethods
        /// <summary>
        /// Método para completar armar el modelo PostGameMessage
        /// </summary>
        /// <param name = "playerList" >Lista obtenida de la API de Genius</param>
        private static PostGameMessage CompleteMatchWithTeamsAndPlayers(List<PersonMatch> playerList)
        {
            try
            {
                PostGameMessage _postGameMessage = new PostGameMessage();
                TeamMatch _firstTeam = new TeamMatch();
                TeamMatch _secondTeam = new TeamMatch();
                foreach (var item in playerList)
                {
                    
                    TeamMatch _team = new TeamMatch();
                    if (_postGameMessage.Teams.Count < 2)
                    {
                        if (_postGameMessage.Teams.Count == 0)                            
                        {
                            _postGameMessage.MatchId = item.MatchId;
                            _team.TeamId = item.TeamId;
                            _team.TeamName = item.TeamName;
                            _postGameMessage.Teams.Add(_team);
                        }
                        else
                        {
                            if (_postGameMessage.Teams[0].TeamName != item.TeamName)
                            {
                                _team.TeamId = item.TeamId;
                                _team.TeamName = item.TeamName;
                                _postGameMessage.Teams.Add(_team);
                            }
                        }
                    }

                    MatchPlayers _matchPlayer = new MatchPlayers();
                    _matchPlayer.Assists = item.Assists;
                    _matchPlayer.FamilyName = item.FamilyName;
                    _matchPlayer.FieldGoalsAttempted = item.FieldGoalsAttempted;
                    _matchPlayer.FieldGoalsMade = item.FieldGoalsMade;
                    _matchPlayer.IsStarter = item.IsStarter;
                    _matchPlayer.Minutes = item.Minutes;
                    _matchPlayer.PlayingPosition = item.PlayingPosition;
                    _matchPlayer.Points = item.Points;
                    _matchPlayer.ReboundsTotal = item.ReboundsTotal;
                    _matchPlayer.ShirtNumber = item.ShirtNumber;
                    _matchPlayer.Blocks = item.Blocks;
                    _matchPlayer.Steals = item.Steals;
                    _matchPlayer.ThreePointersAttempted = item.ThreePointersAttempted;
                    _matchPlayer.ThreePointersMade = item.ThreePointersMade;
                    _matchPlayer.FreeThrowsAttempted = item.FreeThrowsAttempted;
                    _matchPlayer.FreeThrowsMade = item.FreeThrowsMade;
                    _matchPlayer.Turnovers = item.Turnovers;
                    _matchPlayer.FoulsPersonal = item.FoulsPersonal;
                    _matchPlayer.PlusMinusPoints = item.PlusMinusPoints;

                    if (item.TeamName == _postGameMessage.Teams[0].TeamName)
                    {
                            
                        if (item.IsStarter == 1)
                        {
                            _firstTeam.PlayersMatchStarter.Add(_matchPlayer);
                        }
                        else
                        {
                            _firstTeam.PlayersMatchBench.Add(_matchPlayer);
                        }
                    }
                    else
                    {
                        if (item.IsStarter == 1)
                        {
                            _secondTeam.PlayersMatchStarter.Add(_matchPlayer);
                        }
                        else
                        {
                            _secondTeam.PlayersMatchBench.Add(_matchPlayer);
                        }
                    }
                    
                }
                _postGameMessage.Teams[0].PlayersMatchStarter = _firstTeam.PlayersMatchStarter;
                _postGameMessage.Teams[0].PlayersMatchBench = _firstTeam.PlayersMatchBench;

                _postGameMessage.Teams[1].PlayersMatchStarter = _secondTeam.PlayersMatchStarter;
                _postGameMessage.Teams[1].PlayersMatchBench = _secondTeam.PlayersMatchBench;
                return _postGameMessage;
            }
            catch (Exception e)
            {
                //log error
                LogService.AddLog(Notifications.OutputConsole($"Ocurrió un error: {e.Message}"));
                throw;
            }
        }

#endregion
    }
}
