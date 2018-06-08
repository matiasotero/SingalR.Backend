using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public class TeamDetail
    {
        public string TeamName { get; set; }

        public string ExternalId { get; set; }

        public int TeamId { get; set; }
    }
}
