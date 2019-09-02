using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDev.AspNETMVC.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="user">user idendentification</param>
        /// <param name="cal">outlook, google, ical</param>
        /// <returns></returns>
        public ActionResult icsCalendar(string eventId, string user, string cal)
        {
            return View();
        }
    }
}