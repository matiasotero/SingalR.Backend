using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class BoxScoreTotal
    {
        [JsonProperty("players")]
        public IEnumerable<BoxScorePlayer> Players { get; set; }
    }
}
