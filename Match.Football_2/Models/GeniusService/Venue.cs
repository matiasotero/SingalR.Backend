using System;
using System.Collections.Generic;
using System.Text;

namespace Arneb.WebJobs.Live.Models.GeniusService
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string VenueNameInternational { get; set; }
        public string VenueNickname { get; set; }
        public string venueNicknameInternational { get; set; }
        public string SurfaceName { get; set; }
        public string LocationName { get; set; }
        public string Website { get; set; }
        public string TicketURL { get; set; }
        public string ExternalId { get; set; }
        public string LinkDetailVenue { get; set; }

    }
}
