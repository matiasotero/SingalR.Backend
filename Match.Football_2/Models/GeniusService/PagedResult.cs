using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class PagedResult<T> where T : class
    {
        [JsonProperty("teams")]
        public IEnumerable<T> Result { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<T> ResultPBP { get; set; }

    }
}
