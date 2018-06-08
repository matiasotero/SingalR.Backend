using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class BoxScore
    {
        [JsonProperty("teamNumber")]
        public int TeamNumber { get; set; }

        [JsonProperty("total")]
        public BoxScoreTotal Players { get; set; }
    }
}
