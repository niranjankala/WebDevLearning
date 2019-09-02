using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDev.Common.DataObjects;

namespace WebDev.AspNETMVC.Controllers
{
    public class EventController : Controller
    {
        // GET: Events
        public ActionResult Index()
        {
            return View();

        }

        public ActionResult icsCalendar(int eventId, string user, string cal)
        {
            ICSCalendar calInfo = CreateEventICSInfo(eventId, user, cal);
                

            string iCal = WebDev.Common.Utils.CalendarUtils.CreateICalendar(calInfo);
            byte[] calendarBytes = System.Text.Encoding.UTF8.GetBytes(iCal);  //iCal is the calendar string
            return File(calendarBytes, "text/calendar", "event.ics");
        }

        private ICSCalendar CreateEventICSInfo(int eventId, string user, string cal)
        {
            ICSCalendar calInfo = new ICSCalendar();
            CalendarEventInfo eventInfo = GetCalendarEventInfo(eventId, user);            
            calInfo.EventStartDateTime = eventInfo.StartTime ?? DateTime.UtcNow;
            calInfo.EventEndDateTime = eventInfo.EndTime ?? DateTime.UtcNow;
            calInfo.OrganizerName = "Rich See";
            calInfo.OrganizerEmail = "RichSee@d-alchemy.com";
            calInfo.EventDescription = eventInfo.Description; //Text to display in event info
            calInfo.EventSummary = eventInfo.Title;
            calInfo.AlarmTrigger = 30;
            calInfo.AlarmRepeat = 2;
            calInfo.AlarmDuration = 15;
            calInfo.AlarmDescription = eventInfo.Title;
            return calInfo;
        }

        private CalendarEventInfo GetCalendarEventInfo(int eventId, string user)
        {
            return new CalendarEventInfo()
            {
                StartTime = DateTime.UtcNow.AddDays(2),
                EndTime = DateTime.UtcNow.AddDays(2).AddHours(2),
                Title = "Deep dive into C#",
                Description = "Some plain or html text"
            };
        }
    }
}