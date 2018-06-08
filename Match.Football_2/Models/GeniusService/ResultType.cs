using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class ResultType
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
