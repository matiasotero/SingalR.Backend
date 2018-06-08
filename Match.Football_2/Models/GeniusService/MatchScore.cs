using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class MatchScore
    {
        [JsonProperty("teamNumber")]
        public int TeamNumber { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }
    }
}
