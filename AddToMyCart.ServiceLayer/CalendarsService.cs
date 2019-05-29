using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddToMyCart.Repositories;
using AddToMyCart.DomainModels;
using AddToMyCart.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;

namespace AddToMyCart.ServiceLayer
{

    public interface ICalendarsService
    {
        List<CalendarViewModel> GetCalendars();
        List<CalendarViewModel> GetCalendarbyUserID(int UserID);
        void InsertCalendar(CalendarViewModel cvm);
        List<CalendarViewModel> GetCalendarByUserIDandDate(int UserID, DateTime CalendarDate);
        List<Calendar> GetOneWeekTasks(int UserID);
    }

    public class CalendarsService : ICalendarsService
    {
        ICalendarsRepository cr;

        public CalendarsService()
        {
            cr = new CalendarsRepository();
        }

        public List<CalendarViewModel> GetCalendarbyUserID(int UserID)
        {
            List<Calendar> calendars = cr.GetCalendarByUserID(UserID);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Calendar, CalendarViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CalendarViewModel> cvm = mapper.Map<List<Calendar>, List<CalendarViewModel>>(calendars);
            return cvm;
        }

        public List<CalendarViewModel> GetCalendarByUserIDandDate(int UserID, DateTime CalendarDate)
        {
            List<Calendar> calendars = cr.GetCalendarByUserIDandDate(UserID, CalendarDate);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Calendar, CalendarViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CalendarViewModel> cvm = mapper.Map<List<Calendar>, List<CalendarViewModel>>(calendars);
            return cvm;
        }

        public List<CalendarViewModel> GetCalendars()
        {
            List<Calendar> calendars = cr.GetCalendars();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Calendar, CalendarViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CalendarViewModel> cvm = mapper.Map<List<Calendar>, List<CalendarViewModel>>(calendars);
            return cvm;
        }

        public List<Calendar> GetOneWeekTasks(int UserID)
        {
            return cr.GetOneWeekTasks(UserID);
        }

        public void InsertCalendar(CalendarViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CalendarViewModel, Calendar>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Calendar c = mapper.Map<CalendarViewModel, Calendar>(cvm);
            cr.InsertCalendar(c);
        }

    }
}
