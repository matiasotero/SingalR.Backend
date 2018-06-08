using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class TeamDetail
    {
        [JsonProperty("teamName")]
        public string TeamName { get; set; }

        [JsonProperty("externalId")]
        public string ExternalId { get; set; }

        [JsonProperty("teamId")]
        public int TeamId { get; set; }
    }
}
