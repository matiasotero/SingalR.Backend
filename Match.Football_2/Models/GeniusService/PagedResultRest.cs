using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class PagedResultRest<T> where T : class
    {
        [JsonProperty("data")]
        public IList<T> Data { get; set; }

    }
}
