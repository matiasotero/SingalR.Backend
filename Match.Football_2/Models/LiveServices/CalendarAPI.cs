using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Arneb.WebJobs.Live.Models.LiveServices
{
    public class CalendarAPI
    {
        [JsonProperty("idCalendar")]
        public int IdCalendar { get; set; }

        [JsonProperty("localTeamId")]
        public int? LocalTeamId { get; set; }

        [JsonProperty("visitingTeamId")]
        public int? VisitingTeamId { get; set; }

        [JsonProperty("scoreLocal")]
        public string ScoreLocal { get; set; }

        [JsonProperty("scoreVisitor")]
        public string ScoreVisitor { get; set; }

        [JsonProperty("stadium")]
        public string Stadium { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("idMatch")]
        public int? IdMatch { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("liveStream")]
        public bool LiveStream { get; set; }

        [JsonProperty("localTeam")]
        public virtual Teams LocalTeam { get; set; }

        [JsonProperty("VisitingTeam")]
        public virtual Teams VisitingTeam { get; set; }
    }

    public class Teams
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("idExternal")]
        public int IdExternal { get; set; }
    }
}

