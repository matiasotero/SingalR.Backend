using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arneb.Common.Services;
using System.Configuration;
using Arneb.WebJobs.Live.Models.LiveServices;

namespace Arneb.WebJobs.Live.Services
{
    public class ShotChartPostGameService
    {
        public static async Task SaveShotChart(int matchId)
        {
            var x = ConfigurationManager.AppSettings["shotChartAPI"];

            await ApiRestServices<string>.Get($"{x}api/shotchart/generate/{matchId}");


        }

        public static async Task SaveShotChartPeriod(int matchId, List<ActionPlayer> actions, int period, string periodType)
        {

            var scperiod = new ShotChartPeriod();
            scperiod.MatchId = matchId;
            scperiod.Actions = actions;
            if(periodType.ToUpper() == "OVERTIME")
            {
                scperiod.Period = 5;
            }
            else
            {
                scperiod.Period = period;
            }
            

            var x = ConfigurationManager.AppSettings["shotChartAPI"];

            await ApiRestServices<string>.Post($"{x}api/shotchart/generate", scperiod);

        }

    }

    public class ShotChartPeriod
    {
        public int MatchId { get; set; }
        public List<ActionPlayer> Actions { get; set; }
        public int Period { get; set; }
    }
}
