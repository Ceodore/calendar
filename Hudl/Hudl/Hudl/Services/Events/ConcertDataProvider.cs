using System;
using System.Collections.Generic;
using System.Data;
using Hudl.Models;
using Hudl.Models.Events;

namespace Hudl.Services.Events
{
    public interface IEventsDataProvider
    {
        IDataReader Getevents();
        int GetLocationId(string name);
        int GeteventsId(string locationId);

        bool AddLocation(VenueModel venue);
        bool Addevents(string locationId, EventsModel events);
    }

    public class EventsDataProvider : IEventsDataProvider
    {
        private readonly IConnectionFactory _connectionFactory;
        public EventsDataProvider(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IDataReader Getevents()
        {
            var events = new List<EventsModel>();
            var connection = _connectionFactory.GetConnection();
            const string query = "Select c.eventID, c.locationID, c.artist, c.description, c.date, c.startTime, c.endTime, l.name, l.address, l.phone, l.email from dbo.events as c join dbo.Locations as l on c.locationId = l.locationId";
            using (connection = Open(ref connection))
            {
                var command = connection.CreateCommand();
                command.CommandText = query;
                var reader = command.ExecuteReader();
                var data = new DataTable();
                data.Load(reader);
                return data.CreateDataReader();
            }
        }
        public int GetLocationId(string name)
        {
            var connection = _connectionFactory.GetConnection();
            const string queryLocation = "Select locationID from dbo.Locations where name = @venue";

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = queryLocation;
            CreateParam(ref command, "venue", name);
            var result = command.ExecuteScalar();

            if (result == null || string.IsNullOrWhiteSpace(result.ToString())) return -1;
            return Convert.ToInt32(result);

        }
        public int GeteventsId(string locationId)
        {
            var connection = _connectionFactory.GetConnection();
            const string queryLocation = "Select Max(eventsID) from dbo.events where locationID = @locationId";

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = queryLocation;
            CreateParam(ref command, "locationId", locationId);
            var result = command.ExecuteScalar();

            if (result == null || string.IsNullOrWhiteSpace(result.ToString())) return -1;
            return Convert.ToInt32(result);
        }

        public bool AddLocation(VenueModel venue)
        {
            var connection = _connectionFactory.GetConnection();
            const string queryLocation = "Insert into dbo.Locations (name, address, phone, email) values (@name, @address, @phone, @email)";

            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = queryLocation;
            CreateParam(ref command, "name", venue.Name);
            CreateParam(ref command, "address", venue.Address);
            CreateParam(ref command, "phone", venue.PhoneNumber);
            CreateParam(ref command, "email", venue.EmailAddress);
            var result = command.ExecuteNonQuery();

            return !Convert.ToInt32(result).Equals(0);
        }
        public bool Addevents(string locationId, EventsModel events)
        {
            var connection = _connectionFactory.GetConnection();
            const string queryevents = "Insert into dbo.events (locationID, artist, description, date, startTime, endTime) values (@locationID, @artist, @description, @date, @startTime, @endTime)";

            connection.Open();
            var command = connection.CreateCommand();
            CreateParam(ref command, "locationID", locationId);
            CreateParam(ref command, "artist", events.Artist);
            CreateParam(ref command, "description", events.Description);
            CreateParam(ref command, "date", events.Date);
            CreateParam(ref command, "startTime", events.StartTime);
            CreateParam(ref command, "endTime", events.EndTime);
            var result = command.ExecuteNonQuery();

            return !Convert.ToInt32(result).Equals(0);
        }

        private IDbConnection Open(ref IDbConnection connection)
        {
            connection.Open();
            return connection;
        }
        private void CreateParam(ref IDbCommand command, string name, string value)
        {
            var param = command.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            command.Parameters.Add(param);
        }
    }
}
