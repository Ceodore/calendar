using System.Web.Mvc;
using Hudl.Models.Events;
using Hudl.Services.Calendar;
using Hudl.Services.Events;
using Hudl.ViewModel.DefaultViewModel;

namespace Hudl.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventsBuilder _eventsBuilder;
        private readonly ICalendarBuilder _calendarBuilder;

        public HomeController(IEventsBuilder eventsBuilder, ICalendarBuilder calendarBuilder)
        {
            _eventsBuilder = eventsBuilder;
            _calendarBuilder = calendarBuilder;
        }

        public ActionResult Index()
        {
            var defaultViewModel = new DefaultViewModel
            {
                Concerts = _eventsBuilder.Getevents(),
                Calendar = _calendarBuilder.GetCalendar()
            };
            
            return View("Index", defaultViewModel);
        }

        public void AddEvent(EventsModel concert, VenueModel venue)
        {
            _eventsBuilder.Addevents(concert, venue);
        }
    }
}
