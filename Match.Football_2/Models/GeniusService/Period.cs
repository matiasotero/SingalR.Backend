using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
   public class Period
   {
        [JsonProperty("current")]
        public int Current { get; set; }

        [JsonProperty("periodType")]
        public string PeriodType { get; set; }
    }
}
