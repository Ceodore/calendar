using Hudl.Models.Events;
using Hudl.Services;
using Hudl.Services.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Hudl.UnitTests.Services.Events
{
    [TestClass]
    public class EventsBuilderTests
    {
        [TestClass]
        public class AddeventsTests
        {
            private Mock<IEventsDataProvider> _eventsDataProviderMock;

            [TestInitialize]
            public void Initialize()
            {
                _eventsDataProviderMock = new Mock<IEventsDataProvider>();
            }

            [TestMethod]
            public void Addevents_Valid()
            {
                var events = new EventsModel()
                {
                    Date = "2014-07-18",
                    Artist = "Uhhhhh",
                    Description = "I don't think they host events here anymore?",
                    EndTime = "22:30:00",
                    StartTime = "15:30:00"
                };
                var venue = new VenueModel()
                {
                    Name = "Lancaster events Center",
                    Address = "4100 N 84th St, Lincoln, NE 68507",
                    PhoneNumber = "4024416545",
                    EmailAddress = "sbulling@lancastereventscenter.com"
                };

                var connectionFactory = new ConnectionFactory();

                var eventsDataProvider = new EventsDataProvider(connectionFactory);
                var eventsBuilder = new EventsBuilder(eventsDataProvider);
                eventsBuilder.Addevents(events, venue);

            }
        }
    }
}
