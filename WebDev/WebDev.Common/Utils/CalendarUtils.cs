using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDev.Common.DataObjects;
//using WebDev.Common.DataObjects;

namespace WebDev.Common.Utils
{
    public class CalendarUtils
    {
        public static string CreateICalendar(DataObjects.ICSCalendar eventInfo)
        {
            StringBuilder sb = new StringBuilder();
            //Calendar
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//Compnay Inc//Product Application//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:REQUEST");
            //Event
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine("DTSTART:" + ConvertToUniversalTime(eventInfo.EventStartDateTime));
            sb.AppendLine("DTEND:" + ConvertToUniversalTime(eventInfo.EventEndDateTime));
            sb.AppendLine("DTSTAMP:" + ConvertToUniversalTime(eventInfo.EventTimeStamp));
            sb.AppendLine($"ORGANIZER;CN={eventInfo.OrganizerName}:mailto:{eventInfo.OrganizerEmail}");
            sb.AppendLine("UID:" + eventInfo.UID);
            //To add attendee information
            //sb.AppendLine("ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=niranjan.kala@gmail.com;X-NUM-GUESTS=0:mailto:niranjan.kala@gmail.com");
            sb.AppendLine("CREATED:" + ConvertToUniversalTime(eventInfo.EventCreatedDateTime));
            sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + eventInfo.EventDescription);
            sb.AppendLine("LAST-MODIFIED:" + ConvertToUniversalTime(eventInfo.EventLastModifiedTimeStamp));
            sb.AppendLine("LOCATION:" + eventInfo.EventLocation);
            sb.AppendLine("SEQUENCE:0");
            sb.AppendLine("STATUS:CONFIRMED");
            sb.AppendLine("SUMMARY:" + eventInfo.EventSummary);
            sb.AppendLine("TRANSP:OPAQUE");
            //Alarm
            sb.AppendLine("BEGIN:VALARM");
            sb.AppendLine("TRIGGER:" + String.Format("-PT{0}M", eventInfo.AlarmTrigger));
            sb.AppendLine("REPEAT:" + eventInfo.AlarmRepeat);
            sb.AppendLine("DURATION:" + String.Format("PT{0}M", eventInfo.AlarmDuration));
            sb.AppendLine("ACTION:DISPLAY");
            sb.AppendLine("DESCRIPTION:" + eventInfo.AlarmDescription);
            sb.AppendLine("END:VALARM");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");
            return sb.ToString();
        }

        private static List<string> CreateICalendars(List<ICSCalendar> iCals)
        {
            List<string> calendars = new List<string>();
            foreach(DataObjects.ICSCalendar cal in iCals)
            {

                //Repeat daily for 5 days
                var rrule = new RecurrencePattern(FrequencyType.Daily, 1) { Count = 1 };

                var e = new CalendarEvent()
                {

                    Start = new CalDateTime(cal.EventStartDateTime.ToUniversalTime()),
                    End = new CalDateTime(cal.EventEndDateTime.ToUniversalTime()),
                    DtStamp = new CalDateTime(cal.EventTimeStamp.ToUniversalTime()),
                    Organizer = new Organizer() { CommonName = $"{cal.OrganizerName}", Value = new Uri($"mailto:{cal.OrganizerEmail}") },
                    Uid = cal.UID,
                    Created = new CalDateTime(cal.EventCreatedDateTime.ToUniversalTime()),
                    Description = cal.EventDescription,
                    LastModified = new CalDateTime(cal.EventLastModifiedTimeStamp.ToUniversalTime()),
                    Location = cal.EventLocation,
                    Sequence = 0,
                    Status = "CONFIRMED",
                    Summary = cal.EventSummary,
                    Transparency = "OPAQUE"
                    //RecurrenceRules = new List<RecurrencePattern> { rrule },

                };
                e.Alarms.Add(new Alarm()
                {
                    Summary = cal.AlarmDescription,
                    Trigger = new Trigger(TimeSpan.FromMinutes(cal.AlarmTrigger)),
                    Duration = TimeSpan.FromMinutes(cal.AlarmDuration),
                    Repeat = cal.AlarmRepeat,
                    Action = AlarmAction.Display
                });

                //                e.Attendees = new List<Attendee>() { new Attendee
                //{
                //    CommonName = cal.,
                //    Rsvp = true,
                //    Value = new Uri("mailto:niranjan.kala@gmail.com")
                //}
                //            };

                var calendar = new Calendar();
                calendar.ProductId = "//Compnay Inc//Product Application//EN";
                calendar.Version = "2.0";
                calendar.Method = "REQUEST";
                calendar.Events.Add(e);

                var serializer = new CalendarSerializer();
                var serializedCalendar = serializer.SerializeToString(calendar);               
                calendars.Add(serializedCalendar);
            }
            return calendars;
        }        
        
        
        public static string ConvertToUniversalTime(DateTime dt)
        {
            string DateFormat = "yyyyMMddTHHmmssZ";
            return dt.ToString(DateFormat);
        }

        public static string CreateAddToCalendarUrl(ICSCalendar calendarInfo, string calendar)
        {
            StringBuilder urlBuilder = new StringBuilder();
            if (calendar == "google")
            {
                //return "http://www.google.com/calendar/event?action=TEMPLATE&text=[event-title]&dates=[start-custom format='Ymd\\THi00\\Z']/[end-custom format='Ymd\\THi00\\Z']&details=[description]&location=[location]&trp=false&sprop=&sprop=name:"
                urlBuilder.Append("http://www.google.com/calendar/event?action=TEMPLATE&");
                //urlBuilder.Append("https://calendar.google.com/calendar/r/eventedit?");
                urlBuilder.Append($"text={ System.Web.HttpUtility.UrlEncode(calendarInfo.EventSummary)}");
                urlBuilder.Append($"&dates={ConvertToUniversalTime(calendarInfo.EventStartDateTime)}/{ConvertToUniversalTime(calendarInfo.EventEndDateTime)}");
                urlBuilder.Append($"&details={System.Web.HttpUtility.UrlEncode(calendarInfo.EventDescription)}");

                urlBuilder.Append($"&location={System.Web.HttpUtility.UrlEncode(calendarInfo.EventLocation)}");
            }
            else if (calendar == "outlook")
            {
                urlBuilder.Append("");
            }
            return urlBuilder.ToString();
        }

        //private static List<string> CreateICalendars(List<iCalendar> iCals)
        //{
        //    List<string> calendars = new List<string>();
        //    foreach (iCalendar cal in iCals)
        //    {

        //        //Repeat daily for 5 days
        //        var rrule = new RecurrencePattern(FrequencyType.Daily, 1) { Count = 1 };

        //        var e = new CalendarEvent
        //        {

        //            Start = new CalDateTime(cal.EventStartDateTime.ToUniversalTime()),
        //            End = new CalDateTime(cal.EventEndDateTime.ToUniversalTime()),
        //            DtStamp = new CalDateTime(cal.EventTimeStamp.ToUniversalTime()),
        //            Organizer = new Organizer() { CommonName = $"{cal.OrganizerName}", Value = new Uri($"mailto:{cal.OrganizerEmail}") },
        //            Uid = cal.UID,
        //            Created = new CalDateTime(cal.EventCreatedDateTime.ToUniversalTime()),
        //            Description = cal.EventDescription,
        //            LastModified = new CalDateTime(cal.EventLastModifiedTimeStamp.ToUniversalTime()),
        //            Location = cal.EventLocation,
        //            Sequence = 0,
        //            Status = "CONFIRMED",
        //            Summary = cal.EventSummary,
        //            Transparency = "OPAQUE"
        //            //RecurrenceRules = new List<RecurrencePattern> { rrule },

        //        };
        //        e.Alarms.Add(new Alarm()
        //        {
        //            Summary = cal.AlarmDescription,
        //            Trigger = new Trigger(TimeSpan.FromMinutes(cal.AlarmTrigger)),
        //            Duration = TimeSpan.FromMinutes(cal.AlarmDuration),
        //            Repeat = cal.AlarmRepeat,
        //            Action = AlarmAction.Display
        //        });

        //        //                e.Attendees = new List<Attendee>() { new Attendee
        //        //{
        //        //    CommonName = cal.,
        //        //    Rsvp = true,
        //        //    Value = new Uri("mailto:niranjan.kala@gmail.com")
        //        //}
        //        //            };

        //        var calendar = new Calendar();
        //        calendar.ProductId = "//Compnay Inc//Product Application//EN";
        //        calendar.Version = "2.0";
        //        calendar.Method = "REQUEST";
        //        calendar.Events.Add(e);

        //        var serializer = new CalendarSerializer();
        //        var serializedCalendar = serializer.SerializeToString(calendar);

        //        calendars.Add(serializedCalendar);
        //    }
        //    return calendars;
        //}

    }
}
