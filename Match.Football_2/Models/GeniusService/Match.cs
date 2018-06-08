using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class Match
    {
        [JsonProperty("leagueId")]
        public int LeagueId { get; set; }
        [JsonProperty("matchId")]
        public int MatchId { get; set; }
        [JsonProperty("competitionId")]
        public int CompetitionId { get; set; }
        [JsonProperty("venueId")]
        public int VenueId { get; set; }
        [JsonProperty("poolNumber")]
        public int PoolNumber { get; set; }
        [JsonProperty("roundNumber")]
        public string RoundNumber { get; set; }
        [JsonProperty("roundDescription")]
        public string RoundDescription { get; set; }
        [JsonProperty("matchNumber")]
        public int MatchNumber { get; set; }
        [JsonProperty("matchStatus")]
        public string MatchStatus { get; set; }
        [JsonProperty("matchName")]
        public string MatchName { get; set; }
        [JsonProperty("phaseName")]
        public string PhaseName { get; set; }
        public int extraPeriodsUsed { get; set; }
        public string matchTime { get; set; }
        public string matchTimeUTC { get; set; }
        public string enddate { get; set; }
        public string timeActual { get; set; }
        public string timeEndActual { get; set; }
        public int durationActual { get; set; }
        public int temperature { get; set; }
        public int attendance { get; set; }
        public int duration { get; set; }
        public string weather { get; set; }
        public string twitterHashtag { get; set; }
        public int liveStream { get; set; }
        public string matchType { get; set; }
        public string keywords { get; set; }
        public string ticketURL { get; set; }
        public string externalId { get; set; }
        public int nextMatchId { get; set; }
        public int placeIfWon { get; set; }
        public int placeIfLost { get; set; }
        public string updated { get; set; }
        public string linkDetail { get; set; }
        public string linkDetailLeague { get; set; }
        public Venue venue { get; set; }
        public string leagueName { get; set; }
        public string leagueNameInternational { get; set; }
        public string competitionName { get; set; }
        public string competitionNameInternational { get; set; }
        public IList<Competitor> competitors { get; set; }

    }
}

