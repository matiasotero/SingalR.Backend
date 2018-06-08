using Arneb.Common.Common;
using Arneb.WebJobs.Live.Models.GeniusService;
using Arneb.WebJobs.Live.Models.LiveServices;
using Arneb.WebJobs.Live.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arneb.WebJobs.Live.Helpers
{
    public class BoxScoreHelper
    {
        /// <summary>
        /// Método para completar el Scoring del partido con la información proveniente de los servicios de Genius
        /// </summary>
        /// <param name="matchStatus">Modelo de MachStatus proveniente de los servicios de Genius</param>
        public static void CompleteMatchStatus(Models.GeniusService.MatchStatus matchStatus)
        {
            try
            {
                foreach (var s in matchStatus.Score)
                {
                    if (s.TeamNumber == 1)
                    {
                        Models.LiveServices.MatchScore.Instance.ScoreLocal = s.Score.ToString();
                    }
                    else
                    {
                        Models.LiveServices.MatchScore.Instance.ScoreVisiting = s.Score.ToString();
                    }
                }

                Models.LiveServices.MatchScore.Instance.Clock = matchStatus.Clock;
                Models.LiveServices.MatchScore.Instance.CurrentPeriod = matchStatus.Period.Current;
                Models.LiveServices.MatchScore.Instance.PeriodType = matchStatus.Period.PeriodType;
            }
            catch (Exception e)
            {
                Notifications.OutputConsole("complete macthstatus "+e.Message);
            }
        }

        /// <summary>
        /// Método para completar el equipo, detectando si cada jugador es titular o suplente
        /// </summary>
        /// <param name="players">Lista del Modelo de jugadores proveniente de los servicios de Genius</param>
        /// <param name="teamNumber">Número de equipo proveniente de los servicios de Genius - Número interno, valores posibles: 1 o 2</param>
        public static void CompleteRoster(IEnumerable<Arneb.WebJobs.Live.Models.GeniusService.Player> players, int teamNumber)
        {
            if (BoxScoreMessage.Instance.FirstTeam.InitialPlayers.Count <= 0 || BoxScoreMessage.Instance.SecondTeam.InitialPlayers.Count <= 0)
            {
                foreach (var x in players)
                {
                    var player = new Models.LiveServices.BoxScorePlayer();
                    player.Pno = x.Pno;
                    player.Position = x.Position;
                    player.ShirtNumber = x.ShirtNumber;
                    player.FamilyName = x.FamilyName;
                    if (teamNumber == 1)
                    {
                        if (x.Starter == 1)
                        {
                            BoxScoreMessage.Instance.FirstTeam.InitialPlayers.Add(player);
                        }
                        else
                        {
                            BoxScoreMessage.Instance.FirstTeam.BenchPlayers.Add(player);
                        }
                    }
                    else
                    {
                        if (x.Starter == 1)
                        {
                            BoxScoreMessage.Instance.SecondTeam.InitialPlayers.Add(player);
                        }
                        else
                        {
                            BoxScoreMessage.Instance.SecondTeam.BenchPlayers.Add(player);
                        }
                    }

                }
            }

        }

        /// <summary>
        /// Método para completar el boxscore del equipo por jugador con la información proveniente de los servicios de Genius
        /// </summary>
        /// <param name="boxScore">Modelo de BoxScore proveniente de los servicios de Genius</param>
        public static void CompleteBoxScore(Models.GeniusService.BoxScore boxScore)
        {
            try
            {
                if (boxScore.TeamNumber == 1)
                {
                    foreach (var p in boxScore.Players.Players)
                    {
                        foreach (var x in BoxScoreMessage.Instance.FirstTeam.InitialPlayers)
                        {
                            if (p.PNO == x.Pno)
                            {
                                x.Assists = p.Assists;
                                x.FieldGoalsAttempted = p.FieldGoalsAttempted;
                                x.FieldGoalsMade = p.FieldGoalsMade;
                                x.Minutes = p.Minutes;
                                x.Points = p.Points;
                                x.ReboundsTotal = p.ReboundsTotal;
                                x.Blocks = p.Blocks;
                                x.Steals = p.Steals;
                                x.Turnovers = p.Turnovers;
                                x.FoulsPersonal = p.FoulsPersonal;
                                x.PlusMinusPoints = p.PlusMinusPoints;
                                x.ThreePointersAttempted = p.ThreePointersAttempted;
                                x.ThreePointersMade = p.ThreePointersMade;
                                x.FreeThrowsAttempted = p.FreeThrowsAttempted;
                                x.FreeThrowsMade = p.FreeThrowsMade;
                            }
                        }

                        foreach (var x in BoxScoreMessage.Instance.FirstTeam.BenchPlayers)
                        {
                            if (p.PNO == x.Pno)
                            {
                                x.Assists = p.Assists;
                                x.FieldGoalsAttempted = p.FieldGoalsAttempted;
                                x.FieldGoalsMade = p.FieldGoalsMade;
                                x.Minutes = p.Minutes;
                                x.Points = p.Points;
                                x.ReboundsTotal = p.ReboundsTotal;
                                x.Blocks = p.Blocks;
                                x.Steals = p.Steals;
                                x.Turnovers = p.Turnovers;
                                x.FoulsPersonal = p.FoulsPersonal;
                                x.PlusMinusPoints = p.PlusMinusPoints;
                                x.ThreePointersAttempted = p.ThreePointersAttempted;
                                x.ThreePointersMade = p.ThreePointersMade;
                                x.FreeThrowsAttempted = p.FreeThrowsAttempted;
                                x.FreeThrowsMade = p.FreeThrowsMade;
                            }
                        }
                    }

                }
                else
                {
                    foreach (var p in boxScore.Players.Players)
                    {
                        foreach (var x in BoxScoreMessage.Instance.SecondTeam.InitialPlayers)
                        {
                            if (p.PNO == x.Pno)
                            {
                                x.Assists = p.Assists;
                                x.FieldGoalsAttempted = p.FieldGoalsAttempted;
                                x.FieldGoalsMade = p.FieldGoalsMade;
                                x.Minutes = p.Minutes;
                                x.Points = p.Points;
                                x.ReboundsTotal = p.ReboundsTotal;
                                x.Blocks = p.Blocks;
                                x.Steals = p.Steals;
                                x.Turnovers = p.Turnovers;
                                x.FoulsPersonal = p.FoulsPersonal;
                                x.PlusMinusPoints = p.PlusMinusPoints;
                                x.ThreePointersAttempted = p.ThreePointersAttempted;
                                x.ThreePointersMade = p.ThreePointersMade;
                                x.FreeThrowsAttempted = p.FreeThrowsAttempted;
                                x.FreeThrowsMade = p.FreeThrowsMade;
                            }
                        }

                        foreach (var x in BoxScoreMessage.Instance.SecondTeam.BenchPlayers)
                        {
                            if (p.PNO == x.Pno)
                            {
                                x.Assists = p.Assists;
                                x.FieldGoalsAttempted = p.FieldGoalsAttempted;
                                x.FieldGoalsMade = p.FieldGoalsMade;
                                x.Minutes = p.Minutes;
                                x.Points = p.Points;
                                x.ReboundsTotal = p.ReboundsTotal;
                                x.Blocks = p.Blocks;
                                x.Steals = p.Steals;
                                x.Turnovers = p.Turnovers;
                                x.FoulsPersonal = p.FoulsPersonal;
                                x.PlusMinusPoints = p.PlusMinusPoints;
                                x.ThreePointersAttempted = p.ThreePointersAttempted;
                                x.ThreePointersMade = p.ThreePointersMade;
                                x.FreeThrowsAttempted = p.FreeThrowsAttempted;
                                x.FreeThrowsMade = p.FreeThrowsMade;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Notifications.OutputConsole("Complete boxscore "+e.Message);
            }

        }

        /// <summary>
        /// Método para completar los dos equipos que juegan el partido actual
        /// </summary>
        /// <param name="team">Modelo de equipo proveniente de los servicios de Genius</param>
        public static void CompleteTeam(Team team)
        {
            try
            {
                if (team.TeamNumber == 1)
                {
                    BoxScoreMessage.Instance.FirstTeam.TeamNumber = team.TeamNumber;
                    BoxScoreMessage.Instance.FirstTeam.TeamDetails.TeamName = team.Detail.TeamName;
                    BoxScoreMessage.Instance.FirstTeam.TeamDetails.ExternalId = team.Detail.ExternalId;
                    BoxScoreMessage.Instance.FirstTeam.TeamDetails.TeamId = team.Detail.TeamId;
                    CompleteRoster(team.Players, team.TeamNumber);

                }
                else
                {
                    BoxScoreMessage.Instance.SecondTeam.TeamNumber = team.TeamNumber;
                    BoxScoreMessage.Instance.SecondTeam.TeamDetails.TeamName = team.Detail.TeamName;
                    BoxScoreMessage.Instance.SecondTeam.TeamDetails.ExternalId = team.Detail.ExternalId;
                    BoxScoreMessage.Instance.SecondTeam.TeamDetails.TeamId = team.Detail.TeamId;
                    CompleteRoster(team.Players, team.TeamNumber);

                }
            }
            catch (Exception e)
            {
                //log error
                Notifications.OutputConsole("Completeteams "+e.Message);
            }
        }
    }
}
