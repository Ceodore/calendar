using System;
using Hudl.Models.Events;

namespace Hudl.Services.Events
{
    public interface IEventsBuilder
    {
        ConcertModel Getevents();
        bool Addevents(EventsModel events, VenueModel venue);
    }

    public class EventsBuilder : IEventsBuilder
    {
        private readonly IEventsDataProvider _eventsDataProvider;

        public EventsBuilder(IEventsDataProvider eventsDataProvider)
        {
            _eventsDataProvider = eventsDataProvider;
        }

        public ConcertModel Getevents()
        {
            var concertModel = new ConcertModel();

            var reader = _eventsDataProvider.Getevents();
            int i = 0;

            while (reader.Read())
            {
                var events = new EventsModel
                {
                    EventsId = reader.GetInt32(0),
                    VenueId = reader.GetInt32(1),
                    Artist = reader.GetString(2),
                    Description = reader.GetString(3),
                    Date = reader.GetString(4),
                    StartTime = reader.GetString(5),
                    EndTime = reader.GetString(6),
                };
                var venue = new VenueModel
                {
                    LocationId = reader.GetInt32(1),
                    Name = reader.GetString(7),
                    Address = reader.GetString(8),
                    PhoneNumber = reader.GetString(9),
                    EmailAddress = reader.GetString(10)
                };
                concertModel.Events.Add(i, events);
                concertModel.Venue.Add(i, venue);

                i += 1;
            }

            return concertModel;
        }

        public bool Addevents(EventsModel events, VenueModel venue)
        {
            var locationId = GetLocationId(venue);
            var eventsId = Newevents(locationId, events);

            return true;
        }

        private int Newevents(int locationId, EventsModel events)
        {
            var eventsAdded = _eventsDataProvider.Addevents(locationId.ToString(), events);
            int eventsId = -1;
            if (eventsAdded) { eventsId = _eventsDataProvider.GeteventsId(locationId.ToString()); }
            else { Console.WriteLine("Could Not Add events!!!"); }

            return eventsId;
        }
        private int GetLocationId(VenueModel venue)
        {
            var locationId = _eventsDataProvider.GetLocationId(venue.Name);
            if (locationId.Equals(-1))
            {
                var locationAdded = _eventsDataProvider.AddLocation(venue);
                if (locationAdded){ locationId = _eventsDataProvider.GetLocationId(venue.Name); }
                else { Console.WriteLine("Could Not Add Location!!!"); }
            }
            return locationId;
        }
    }
}