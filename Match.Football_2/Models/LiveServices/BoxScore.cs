using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public class BoxScore
    {
        public BoxScore()
        {
            InitialPlayers = new List<BoxScorePlayer>();
            BenchPlayers = new List<BoxScorePlayer>();
            TeamDetails = new TeamDetail();
        }

        public int TeamNumber { get; set; }

        public TeamDetail TeamDetails { get; set; }

        public List<BoxScorePlayer> InitialPlayers { get; set; }

        public List<BoxScorePlayer> BenchPlayers { get; set; }
    }
}
