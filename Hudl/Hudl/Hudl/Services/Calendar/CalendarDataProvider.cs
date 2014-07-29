using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Hudl.Services.Calendar
{
    public interface ICalendarDataProvider
    {
        List<string> GetDays();
        DayOfWeek MonthStart();
        int DaysInMonth();
        string MonthYear();
    }
    public class CalendarDataProvider : ICalendarDataProvider
    {
        public List<string> GetDays()
        {
            return new List<string> {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"};
        }

        public DayOfWeek MonthStart()
        {
            return DayOfWeek.Tuesday;
        }

        public int DaysInMonth()
        {
            int numDays;
            var month = 7;//DateTime.Now.Month;

            switch (month)
            {
                case 11:
                case 9:
                case 6:
                case 4:
                    numDays = 30;
                    break;
                case 2:
                    numDays = DateTime.Now.Year % 4 == 0 ? 29 : 28; //plus a bunch of extra rules...
                    break;
                default:
                    numDays = 31;
                    break;
            }
            return numDays;
        }

        public string MonthYear()
        {
            var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(7);//DateTime.Now.Month);
            return month + " " + DateTime.Now.Year.ToString();
        }
    }
}