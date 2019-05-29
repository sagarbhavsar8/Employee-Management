using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AddToMyCart.ViewModels;
using AddToMyCart.ServiceLayer;
using AddToMyWebCart.Common;
using AddToMyWebCart.CustomFilter;
using System.Text;

namespace AddToMyWebCart.Controllers
{

    public class HomeController : Controller
    {
        ICalendarsService cs;
        ISetPrioritiesService sps;

        public HomeController(ICalendarsService cs, ISetPrioritiesService sps)
        {
            this.cs = cs;
            this.sps = sps;
        }

        public Dictionary<string, object> getMonthDetails(int month = 0, int year = 0)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            string[] Days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            if (month == 0)
                month = DateTime.Now.Month;
            if (year == 0)
                year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year, month, 1);
            int DayNumbOfWeek = Array.IndexOf(Days, firstDay.DayOfWeek.ToString());
            string CurrentMonthName = Common.Common.getMonthName(month);
            int daysinmonth = DateTime.DaysInMonth(year, month);
            dict.Add("DayNumbOfWeek", DayNumbOfWeek);
            dict.Add("CurrentMonthName", CurrentMonthName);
            dict.Add("daysinmonth", daysinmonth);
            dict.Add("CurrentYear", year);
            dict.Add("currentDate", new DateTime(year, month, 1));
            return dict;
        }

        [UserAuthorizationFilter]
        public ActionResult Index(int month = 0, int year = 0)
        {
            var setMonth = getMonthDetails(month, year);
            ViewBag.DayNumbStarts = setMonth["DayNumbOfWeek"];
            ViewBag.DaysCount = setMonth["daysinmonth"];
            ViewBag.MonthName = setMonth["CurrentMonthName"];
            ViewBag.CurrentYear = setMonth["CurrentYear"];
            //ViewBag.CurrentMonthCount = DateTime.Now.Month;
            DateTime currentDate = Convert.ToDateTime(setMonth["currentDate"]);
            Session["PreviousMonth"] = currentDate.AddMonths(-1).Month;
            Session["NextMonth"] = currentDate.AddMonths(1).Month;
            Session["PreviousYear"] = (currentDate.Month == 1) ? currentDate.AddYears(-1).Year : currentDate.Year;
            Session["NextYear"] = (currentDate.Month == 12) ? currentDate.AddYears(1).Year : currentDate.Year;
            List<CalendarViewModel> cvm = cs.GetCalendarByUserIDandDate(Convert.ToInt32(Session["CurrentUserID"]), currentDate);

            var headline = cs.GetOneWeekTasks(Convert.ToInt32(Session["CurrentUserID"]));

            if (headline.Count > 0)
            {
                StringBuilder createStr = new StringBuilder();
                createStr.Append("| ");
                foreach (var item in headline)
                {
                    createStr.Append(item.CalendarText + " on " + item.CalendarDateTime.ToShortDateString() + " | ");
                }
                Session["CreateHeadline"] = createStr;
            }
            else
                Session["CreateHeadline"] = "";
            return View(cvm);
        }

        public ActionResult AddReminder()
        {
            CalendarViewModel cvm = new CalendarViewModel();
            List<SetPriorityViewModel> svm = sps.GetAllPriorities();
            ViewBag.priorities = svm;
            return PartialView("AddReminderPartial", cvm);
        }


        public ActionResult SaveReminder(AddCalendarViewModel cvm)
        {
            CalendarViewModel newcvm = new CalendarViewModel();

            int setUserID = Convert.ToInt32(cvm.UserID);
            int setPriorityID = Convert.ToInt32(cvm.PriorityID);
            DateTime calendarDatetime = DateTime.ParseExact(cvm.CalendarDateTime, "MM/dd/yyyy", null);

            newcvm.UserID = setUserID;
            newcvm.CalendarText = cvm.CalendarText;
            newcvm.PriorityID = setPriorityID;
            newcvm.CalendarDateTime = calendarDatetime;

            if (ModelState.IsValid)
            {
                cs.InsertCalendar(newcvm);
            }
            return RedirectToAction("Index");
        }
    }
}