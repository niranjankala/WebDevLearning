using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDev.Common.DataObjects
{
    public class ICSCalendar
    {
        public string CalendarName { get; set; }
        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }
        public DateTime EventTimeStamp { get; set; }
        public DateTime EventCreatedDateTime { get; set; }
        public DateTime EventLastModifiedTimeStamp { get; set; }
        public string UID { get; set; }
        /// <summary>
        /// Contains plain or HTML formated event description
        /// </summary>
        public string EventDescription { get; set; }
        public string EventLocation { get; set; }
        /// <summary>
        /// Event Name
        /// </summary>
        public string EventSummary { get; set; }

        public string OrganizerName { get; set; }
        public string OrganizerEmail { get; set; }
        public int AlarmTrigger { get; set; }
        public int AlarmRepeat { get; set; }
        /// <summary>
        /// Alarm duration in minutes
        /// </summary>
        public int AlarmDuration { get; set; }
        public string AlarmDescription { get; set; }
        public ICSCalendar()
        {
            EventTimeStamp = DateTime.Now;
            EventCreatedDateTime = EventTimeStamp;
            EventLastModifiedTimeStamp = EventTimeStamp;
        }
    }
}
