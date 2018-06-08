using Arneb.WebJobs.Live.Models.GeniusService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public class PostGameMessage
    {
        public PostGameMessage()
        {
            Teams = new List<TeamMatch>();
        }
        [JsonProperty("id")]
        public string Id { get { return MatchId.ToString(); } }
        public int MatchId { get; set; }
        public List<TeamMatch> Teams { get; set; }
    }

    public class TeamMatch
    {
        public TeamMatch()
        {
            PlayersMatchStarter = new List<MatchPlayers>();
            PlayersMatchBench = new List<MatchPlayers>();
        }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public List<MatchPlayers> PlayersMatchStarter { get; set; }
        public List<MatchPlayers> PlayersMatchBench { get; set; }

    }

    public class MatchPlayers
    {
        public int MatchId { get; set; }

        public int? ShirtNumber { get; set; }

        public int? IsStarter { get; set; }

        public string TeamName { get; set; }

        public string FamilyName { get; set; }

        public int TeamId { get; set; }

        public string PlayingPosition { get; set; }

        public decimal Minutes { get; set; }

        public int Points { get; set; }

        public int FieldGoalsAttempted { get; set; }

        public int FieldGoalsMade { get; set; }

        public int Blocks { get; set; }

        public int Steals { get; set; }

        public int Assists { get; set; }

        public int ReboundsTotal { get; set; }

        public int ThreePointersAttempted { get; set; }

        public int ThreePointersMade { get; set; }

        public int Turnovers { get; set; }

        public int PlusMinusPoints { get; set; }

        public int FoulsPersonal { get; set; }

        public int FreeThrowsAttempted { get; set; }

        public int FreeThrowsMade { get; set; }
    }
}
