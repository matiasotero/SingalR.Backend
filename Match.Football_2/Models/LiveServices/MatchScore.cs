using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public sealed class MatchScore
    {
        private static MatchScore instance = null;
        private static readonly object padlock = new object();

        public static MatchScore Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MatchScore();
                    }
                    return instance;
                }
            }
        }

        public string ScoreLocal { get; set; }

        public string ScoreVisiting { get; set; }

        public string Clock { get; set; }

        public string PeriodType { get; set; }

        public int CurrentPeriod { get; set; }

        public string CurrentPeriodFormat { get {

                return CurrentPeriod==1 ? "Primer cuarto":
                       CurrentPeriod==2 ? "Segundo cuarto":
                       CurrentPeriod==3 ? "Tercer cuarto":
                       CurrentPeriod==4 ? "Cuarto cuarto":
                       "";
                            } }
    }
}
