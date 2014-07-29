using System.Collections.Generic;

namespace Hudl.Models.Events
{
    public class ConcertModel
    {
        public Dictionary<int, EventsModel> Events;
        public Dictionary<int, VenueModel> Venue;

        public ConcertModel()
        {
            Events = new Dictionary<int, EventsModel>();
            Venue = new Dictionary<int, VenueModel>();
        }
    }
}
