using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class Team
    {
        [JsonProperty("players")]
        public IEnumerable<Player> Players { get; set; }

        [JsonProperty("detail")]
        public TeamDetail Detail { get; set; }

        [JsonProperty("teamNumber")]
        public int TeamNumber { get; set; }
    }

    public class Player
    {

        [JsonProperty("pno")]
        public int Pno { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("starter")]
        public int Starter { get; set; }

        [JsonProperty("shirtNumber")]
        public string ShirtNumber { get; set; }

        [JsonProperty("playingPosition")]
        public string Position { get; set; }
    }
}
