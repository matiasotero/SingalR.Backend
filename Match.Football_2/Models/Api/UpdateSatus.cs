using static Arneb.Common.Constants;

namespace Arneb.WebJobs.Live.Models.Api
{
    public class UpdateStatus
    {
        public int MatchID { get; set; }
        public MatchStatus Status { get; set; }
        public string ScoreLocal { get; set; }
        public string ScoreVisitor { get; set; }
    }
}
