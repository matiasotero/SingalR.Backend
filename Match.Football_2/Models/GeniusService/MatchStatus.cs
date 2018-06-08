using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class MatchStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("scores")]
        public IEnumerable<MatchScore> Score { get; set; }

        [JsonProperty("clock")]
        public string Clock { get; set; }

        [JsonProperty("period")]
        public Period Period { get; set; }
    }
}
