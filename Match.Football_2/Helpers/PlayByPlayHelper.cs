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
    public static class PlayByPlayHelper
    {

        /// <summary>
        /// Método para completar el playbyplay del partido con la información proveniente de los servicios de Genius
        /// </summary>
        /// <param name="action">Modelo de Action proveniente de los servicios de Genius</param>
        public static void CompletePlayByPlay(Models.GeniusService.Action action)
        {
            try
            {
                
                if(action.Pno != 0)
                {
                    var actionPlayer = CreateActionPlayer(action);
                    PlayByPlayMessage.Instance.AllActions.Add(actionPlayer);
                }
               
            }
            catch (Exception e)
            {

                Notifications.OutputConsole("completePlayByPlay: "+e.Message);
            }

        }


        private static ActionPlayer CreateActionPlayer(Models.GeniusService.Action action)
        {
            List<Models.LiveServices.BoxScorePlayer> Players = new List<Models.LiveServices.BoxScorePlayer>();
            Models.LiveServices.BoxScorePlayer playerAction;

            if (action.TeamNumber == 1)
            {
                Players.AddRange(BoxScoreMessage.Instance.FirstTeam.InitialPlayers);
                Players.AddRange(BoxScoreMessage.Instance.FirstTeam.BenchPlayers);
                playerAction = Players.FirstOrDefault(x => x.Pno == action.Pno);
            }
            else
            {
                Players.AddRange(BoxScoreMessage.Instance.SecondTeam.InitialPlayers);
                Players.AddRange(BoxScoreMessage.Instance.SecondTeam.BenchPlayers);
                playerAction = Players.FirstOrDefault(x => x.Pno == action.Pno);
            }
           
            var actionPlayer = new ActionPlayer();
            actionPlayer.Period = action.Period;
            actionPlayer.ActionNumber = action.ActionNumber;
            actionPlayer.ActionType = action.ActionType;
            actionPlayer.Clock = action.Clock;
            actionPlayer.Pno = action.Pno;
            actionPlayer.Score1 = action.Score1;
            actionPlayer.Score2 = action.Score2;
            actionPlayer.SubType = action.SubType;
            actionPlayer.Success = action.Success;
            actionPlayer.TeamNumber = action.TeamNumber;
            actionPlayer.FamilyName = playerAction.FamilyName;
            actionPlayer.ShirtNumber = playerAction.ShirtNumber;
            actionPlayer.Position = playerAction.Position;
            actionPlayer.PeriodType = action.PeriodType;
            actionPlayer.X = action.X;
            actionPlayer.Y = action.Y;
            actionPlayer.Side = action.Side;
            actionPlayer.Area = action.Area;
            return actionPlayer;
        }

    }
}
