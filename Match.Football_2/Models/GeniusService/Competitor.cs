
namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public  class Competitor
    {
        public string competitorType { get; set; }
        public string competitorName { get; set; }
        public int competitorId { get; set; }
        public string linkDetailCompetitor { get; set; }
        public string scoreString { get; set; }
        public string scoreSecondaryString { get; set; }
        public string completionStatus { get; set; }
        public int resultPlacing { get; set; }
        public bool isDrawn { get; set; }
        public bool isHomeCompetitor { get; set; }
        public int teamId { get; set; }
        public string teamName { get; set; }
        public string teamNameInternational { get; set; }
        public string teamNickname { get; set; }
        public string teamNicknameInternational { get; set; }
        public string teamCode { get; set; }
        public string teamCodeInternational { get; set; }
        public string website { get; set; }
        public string internationalReference { get; set; }
        public string externalId { get; set; }
        public int clubId { get; set; }
        public string clubName { get; set; }
        public string clubNameInternational { get; set; }
        public string linkDetailClub { get; set; }

    }
}
