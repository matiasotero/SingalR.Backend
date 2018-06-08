using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class PersonMatch
    {
        [JsonProperty("matchId")]
        public int MatchId { get; set; }

        [JsonProperty("shirtNumber")]
        public int? ShirtNumber { get; set; }

        [JsonProperty("isStarter")]
        public int? IsStarter { get; set; }

        [JsonProperty("teamName")]
        public string TeamName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("teamId")]
        public int TeamId { get; set; }

        [JsonProperty("playingPosition")]
        public string PlayingPosition { get; set; }

        [JsonProperty("sMinutes")]
        public decimal Minutes { get; set; }

        [JsonProperty("sPoints")]
        public int Points { get; set; }

        [JsonProperty("sFieldGoalsAttempted")]
        public int FieldGoalsAttempted { get; set; }

        [JsonProperty("sFieldGoalsMade")]
        public int FieldGoalsMade { get; set; }

        [JsonProperty("sBlocks")]
        public int Blocks { get; set; }

        [JsonProperty("sSteals")]
        public int Steals { get; set; }

        [JsonProperty("sAssists")]
        public int Assists { get; set; }

        [JsonProperty("sReboundsTotal")]
        public int ReboundsTotal { get; set; }

        [JsonProperty("sThreePointersAttempted")]
        public int ThreePointersAttempted { get; set; }

        [JsonProperty("sThreePointersMade")]
        public int ThreePointersMade { get; set; }

        [JsonProperty("sTurnovers")]
        public int Turnovers { get; set; }

        [JsonProperty("sPlusMinusPoints")]
        public int PlusMinusPoints { get; set; }

        [JsonProperty("sFoulsPersonal")]
        public int FoulsPersonal { get; set; }

        [JsonProperty("sFreeThrowsAttempted")]
        public int FreeThrowsAttempted { get; set; }

        [JsonProperty("sFreeThrowsMade")]
        public int FreeThrowsMade { get; set; }

    }
}
