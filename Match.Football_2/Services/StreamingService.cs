using Arneb.Common;
using Arneb.Common.Common;
using Arneb.Common.Services;
using Arneb.WebJobs.Live.Helpers;
using Arneb.WebJobs.Live.Models;
using Arneb.WebJobs.Live.Models.Api;
using Arneb.WebJobs.Live.Models.GeniusService;
using Arneb.WebJobs.Live.Models.LiveServices;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Arneb.WebJobs.Live.Services
{
    public class StreamingService
    {
        #region variables
           private static  Dictionary<int,string> _DictionaryQuarter = new Dictionary<int, string> {
               { 1, "1er cuarto" },
               { 2, "2do cuarto" },
               { 3, "3er cuarto" },
               { 4, "4to cuarto" },
               { 5, "5to cuarto" },
               { 6, "6to cuarto" }
           };

        private static int maxReconection = 100;
        private static int canReconection;

        #endregion
 

        /// <summary>
        /// Método para procesar la información del partido en tiempo real y enviarla a la API con SignalR
        /// </summary>
        public static void ProcessGame(int matchID, string statusGame)
        {
            //Para validar que no se repitan cuartos
            bool[] _quarterNoFinished = { false, true, true,true,true };
            bool[] _quarterOtNoFinished = { false, true, true, true, true };
            canReconection = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    var ak = ConfigurationManager.AppSettings["appKeyGenius"];
                    var _urlApiGenius = ConfigurationManager.AppSettings["urlApiGenius"];
                    var stream = client.GetStreamAsync(_urlApiGenius+ matchID + "?format=json&types=te,st,box,ac&ak=" + ak).GetAwaiter().GetResult();                    
                    var Data = new UpdateStatus
                    {
                        MatchID = matchID,
                        Status = Common.Constants.MatchStatus.IN_PROGRESS,
                        ScoreLocal = null,
                        ScoreVisitor = null
                    };

                    ApiRestServices<string>.Put($"{ConfigurationManager.AppSettings["restAPI"]}api/Calendar/", Data).Wait();

                    using (var reader = new StreamReader(stream))
                    {
                        var _key = ApiRestServices<IEnumerable<Params>>.Get(String.Concat(ConfigurationManager.AppSettings["restAPI"], "api/Parameters/GetAll"))
                                     .GetAwaiter()
                                     .GetResult()
                                     .Where(x => x.ParamKey == Common.Constants.Parameters.KeyApiNotificationPush.ToString())
                                     .Select(x => x.Value)
                                     .SingleOrDefault();
                        var _KeyDecrypted = NotificationService.DecryptPass(_key);
                        var _keyEncrypted = NotificationService.EncryptPass(NotificationService.EncryptPass(String.Concat(NotificationService.EncryptPass("@bR@s8@skET2017"), _KeyDecrypted)));
                        var _transmitting = true;
                        var sendStartNotification = statusGame!= "IN_PROGRESS";
                        var _firstData = true;
                        var liveAPIFromConfig = ConfigurationManager.AppSettings["liveAPI"];
                        var liveHubNameFromConfig = ConfigurationManager.AppSettings["liveHubName"];
                        var connection = new HubConnection(liveAPIFromConfig);
                        var myHub = connection.CreateHubProxy(liveHubNameFromConfig);
                        
                        string _currentPeriodFormat = "";

                        connection.Start().ContinueWith(task =>
                        {
                            if (task.IsFaulted)
                            {
                                Notifications.OutputConsole($"There was an error opening the connection:{0} { task.Exception.GetBaseException()}");
                            }
                            else
                            {
                                Notifications.OutputConsole("ConnectedToHub");
                            }
                        }).Wait();

                        while (_transmitting)
                        {

                                if (!_firstData)
                                {
                                    SendMatchInfoToSignalR(myHub, connection);
                                }

                                var line = reader.ReadLineAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                                var typeResult = JsonConvert.DeserializeObject<ResultType>(line);
                                switch (typeResult.Type)
                                {
                                    case "status":
                                        SendMatchInfoToSignalR(myHub, connection);
                                        var status = JsonConvert.DeserializeObject<MatchStatus>(line);
                                        if (status.Type == "status")
                                        {
                                            if (status.Status.ToLower() == "finished" || status.Status.ToLower() == "complete")
                                            {
                                                SendMatchInfoToSignalR(myHub, connection);
                                                reader.Close();
                                                reader.Dispose();
                                                _transmitting = false;

                                                var postGameAccessService = new PostGameAccessService();
                                                postGameAccessService.FinishGame($"Finalizó el juego: {BoxScoreMessage.Instance.FirstTeam.TeamDetails.TeamName} {Models.LiveServices.MatchScore.Instance.ScoreLocal} vs {BoxScoreMessage.Instance.SecondTeam.TeamDetails.TeamName} {Models.LiveServices.MatchScore.Instance.ScoreVisiting}",
                                                    matchID,
                                                    Models.LiveServices.MatchScore.Instance.ScoreLocal,
                                                    Models.LiveServices.MatchScore.Instance.ScoreVisiting
                                                    ).Wait();
                                                PlayByPlayPostGameService.SavePlayByPlay(matchID, PlayByPlayMessage.Instance).Wait();
                                                ShotChartPostGameService.SaveShotChartPeriod(matchID, PlayByPlayMessage.Instance.AllActions, status.Period.Current, status.Period.PeriodType).Wait();

                                                myHub.Invoke("FinishGame").Wait();
                                                connection.Stop();
                                                _firstData = true;

                                            }
                                            if (status.Status.ToLower() == "inprogress" || status.Status.ToUpper() == "IN_PROGRESS" || status.Status.ToLower() == "periodbreak")
                                            {
                                                SendMatchInfoToSignalR(myHub, connection);

                                                if (sendStartNotification)
                                                {
                                                    var _message = $"El juego acaba de comenzar: {BoxScoreMessage.Instance.FirstTeam.TeamDetails.TeamName} vs {BoxScoreMessage.Instance.SecondTeam.TeamDetails.TeamName}";
                                                    myHub.Invoke("StartGame").Wait();

                                                    NotificationService.SendNotification(new Common.ViewModel.NotificationPushViewModel
                                                    {
                                                        Key = _keyEncrypted,
                                                        Message = _message
                                                    }, $"{ConfigurationManager.AppSettings["restAPI"]}api/NotificationPush").Wait();
                                                    sendStartNotification = false;
                                                    Notifications.OutputConsole(_message);
                                                }

                                                if (status.Status.ToLower() == "periodbreak")
                                                {
                                                    _currentPeriodFormat = _DictionaryQuarter[status.Period.Current];
                                                    bool _sendNotification = true;

                                                    switch (status.Period.PeriodType.ToUpper())
                                                    {
                                                        case "OVERTIME":
                                                            _currentPeriodFormat += " del suplementario";
                                                            _sendNotification = _quarterOtNoFinished[status.Period.Current];
                                                            _quarterOtNoFinished[status.Period.Current] = false;
                                                            break;
                                                        case "REGULAR":
                                                            _sendNotification = _quarterNoFinished[status.Period.Current];
                                                            _quarterNoFinished[status.Period.Current] = false;
                                                            break;
                                                        default:
                                                            break;
                                                    }

                                                    if (_sendNotification)
                                                    {
                                                        string _scoreBreak = BoxScoreMessage.Instance.FirstTeam.TeamDetails.TeamName + ": " + status.Score.Where(x => x.TeamNumber == 1).Select(r => r.Score).SingleOrDefault() + " ";
                                                        _scoreBreak += BoxScoreMessage.Instance.SecondTeam.TeamDetails.TeamName + ": " + status.Score.Where(x => x.TeamNumber == 2).Select(r => r.Score).SingleOrDefault();

                                                        var _message = $"Finalizó el {_currentPeriodFormat }  {_scoreBreak}";
                                                        Notifications.OutputConsole(Environment.NewLine + _message + "    *****************" + Environment.NewLine);

                                                        NotificationService.SendNotification(new Common.ViewModel.NotificationPushViewModel
                                                        {
                                                            Key = _keyEncrypted,
                                                            Message = _message
                                                        }, $"{ConfigurationManager.AppSettings["restAPI"]}api/NotificationPush").Wait();

                                                        ShotChartPostGameService.SaveShotChartPeriod(matchID, PlayByPlayMessage.Instance.AllActions, status.Period.Current, status.Period.PeriodType).Wait();

                                                    }

                                                }

                                                UpdatePartialScoreAsync(status, matchID).Wait();
                                                SendMatchInfoToSignalR(myHub, connection);
                                                _firstData = false;

                                            Notifications.OutputConsole(Environment.NewLine + status.Status + " Cuarto: " + status.Period.Current + "  Periodo " + status.Period.PeriodType);

#if DEBUG
                                                foreach (var j in status.Score)
                                                {
                                                    Notifications.OutputConsole($"Equipo: {j.TeamNumber} Puntaje: {j.Score}");
                                                }
#endif
                                            }
                                        }
                                        BoxScoreHelper.CompleteMatchStatus(status);
                                        SendMatchInfoToSignalR(myHub, connection);
                                        break;
                                    case "teams":
                                        SendMatchInfoToSignalR(myHub, connection);
                                        var teams = JsonConvert.DeserializeObject<PagedResult<Models.GeniusService.Team>>(line);
                                        var team = teams.Result.ToList().FirstOrDefault();
                                        var team2 = teams.Result.ToList().Skip(1).FirstOrDefault();
                                        BoxScoreHelper.CompleteTeam(team);
                                        BoxScoreHelper.CompleteTeam(team2);
                                        SendMatchInfoToSignalR(myHub, connection);
                                        break;
                                    case "boxscore":
                                        SendMatchInfoToSignalR(myHub, connection);
                                        var boxScore = JsonConvert.DeserializeObject<PagedResult<Models.GeniusService.BoxScore>>(line);
                                        var boxScoreTeam1 = boxScore.Result.ToList().FirstOrDefault();
                                        var boxScoreTeam2 = boxScore.Result.ToList().Skip(1).FirstOrDefault();
                                        BoxScoreHelper.CompleteBoxScore(boxScoreTeam1);
                                        BoxScoreHelper.CompleteBoxScore(boxScoreTeam2);
                                        SendMatchInfoToSignalR(myHub, connection);
                                        break;
                                    case "action":
                                        SendMatchInfoToSignalR(myHub, connection);
                                        var action = JsonConvert.DeserializeObject<Models.GeniusService.Action>(line);
                                        PlayByPlayHelper.CompletePlayByPlay(action);
                                        SendMatchInfoToSignalR(myHub, connection);
                                        break;
                                    default:
                                        SendMatchInfoToSignalR(myHub, connection);
                                        break;

                                }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Notifications.OutputConsole("[Error] "+e.Message + e.InnerException);
                LogService.SaveLog().Wait();
            }
        }

#region HelperMethods
        /// <summary>
        /// Método para actualiar el Scoring del partido con la información proveniente de los servicios de Genius en la base de datos
        /// </summary>
        /// <param name="matchStatus">Modelo de MachStatus proveniente de los servicios de Genius</param>
        /// <param name="matchId">Id de Partido</param>
        private static async Task UpdatePartialScoreAsync(Models.GeniusService.MatchStatus matchStatus, int matchId)
        {
            try
            {
                var localScore = Models.LiveServices.MatchScore.Instance.ScoreLocal;
                var visitingScore = Models.LiveServices.MatchScore.Instance.ScoreVisiting;
                var save = false;

                foreach (var s in matchStatus.Score)
                {
                    if (s.TeamNumber == 1)
                    {
                       if(Models.LiveServices.MatchScore.Instance.ScoreLocal != s.Score.ToString())
                         {
                            localScore = s.Score.ToString();
                            save = true;
                         }
                                
                    }
                    else
                    {
                        if (Models.LiveServices.MatchScore.Instance.ScoreVisiting != s.Score.ToString())
                        {
                            visitingScore = s.Score.ToString();
                            save = true;
                        }
                    }
                }

                if(save)
                {
                    var Data = new UpdateStatus
                    {
                        MatchID = matchId,
                        Status = Constants.MatchStatus.IN_PROGRESS,
                        ScoreVisitor = visitingScore,
                        ScoreLocal = localScore
                    };

                    await ApiRestServices<string>.Put($"{ConfigurationManager.AppSettings["restAPI"]}api/Calendar/", Data);
                }

            }
            catch (Exception e)
            {
                Notifications.OutputConsole("UpdatePartialScoreAsync "+e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hubProxy"></param>
        /// <param name="connection"></param>
        private static void SendMatchInfoToSignalR(IHubProxy hubProxy, HubConnection connection)
        {
            if(connection.State == ConnectionState.Connected)
            {
                hubProxy.Invoke("UpdateScoring", Models.LiveServices.MatchScore.Instance).Wait();
                hubProxy.Invoke("UpdateBoxScore", BoxScoreMessage.Instance).Wait();
                hubProxy.Invoke("UpdatePlayByPlay", PlayByPlayMessage.Instance).Wait();
            }
           
        }
#endregion
    }
}