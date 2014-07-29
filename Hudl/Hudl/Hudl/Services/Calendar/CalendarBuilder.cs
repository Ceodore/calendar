using Hudl.Models.Calendar;

namespace Hudl.Services.Calendar
{
    public interface ICalendarBuilder
    {
        CalendarModel GetCalendar();
    }

    public class CalendarBuilder : ICalendarBuilder
    {
        private readonly ICalendarDataProvider _calendarDataProvider;

        public CalendarBuilder(ICalendarDataProvider calendarDataProvider)
        {
            _calendarDataProvider = calendarDataProvider;
        }

        public CalendarModel GetCalendar()
        {
            var calendar = new CalendarModel
            {
                Days = _calendarDataProvider.GetDays(),
                First = (int)_calendarDataProvider.MonthStart(),
                DaysInMonth = _calendarDataProvider.DaysInMonth(),
                MonthYear = _calendarDataProvider.MonthYear()
            };

            return calendar;
        }
    }
}