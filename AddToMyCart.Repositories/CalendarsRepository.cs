using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.DomainModels;

namespace AddToMyCart.Repositories
{
    public interface ICalendarsRepository
    {
        List<Calendar> GetCalendars();
        List<Calendar> GetCalendarByUserID(int UserID);
        void InsertCalendar(Calendar calendar);
        List<Calendar> GetCalendarByUserIDandDate(int UserID, DateTime CalendarDate);
        List<Calendar> GetOneWeekTasks(int UserID);
    }

    public class CalendarsRepository : ICalendarsRepository
    {
        AddToMyWebCartDbContext db;

        public CalendarsRepository()
        {
            db = new AddToMyWebCartDbContext();
        }

        public List<Calendar> GetCalendars()
        {
            return db.Calendars.ToList();
        }

        public List<Calendar> GetCalendarByUserID(int UserID)
        {
            List<Calendar> calendar = db.Calendars.Where(c => c.UserID == UserID).ToList();
            return calendar;
        }

        public void InsertCalendar(Calendar calendar)
        {
            db.Calendars.Add(calendar);
            db.SaveChanges();
        }

        public List<Calendar> GetCalendarByUserIDandDate(int UserID, DateTime CalendarDate)
        {
            List<Calendar> calendar = db.Calendars.Where(c => c.CalendarDateTime.Month == CalendarDate.Month && c.UserID == UserID).ToList();
            return calendar;
        }

        public List<Calendar> GetOneWeekTasks(int UserID)
        {
            DateTime CurrentDate = DateTime.Now.Date;
            DateTime NextWeek = DateTime.Now.AddDays(7);
            List<Calendar> calendar = db.Calendars.Where(c => c.CalendarDateTime >= CurrentDate && c.CalendarDateTime <= NextWeek && c.UserID == UserID).OrderBy(o => o.CalendarDateTime).ToList();
            return calendar;
        }
    }
}
