using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class WeeklyWorkSchedule
    {
        private TimeSpan startTime { get; set; }
        private TimeSpan endTime { get; set; }

        [XmlIgnore]
        public TimeSpan StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        [XmlIgnore]
        public TimeSpan EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        [XmlElement("StartTime")]
        public long TimeSinceLastEventTicksStart
        {
            get { return startTime.Ticks; }
            set { startTime = new TimeSpan(value); }
        }

        [XmlElement("EndTime")]
        public long TimeSinceLastEventTicksEnd
        {
            get { return endTime.Ticks; }
            set { endTime = new TimeSpan(value); }
        }




        public override string ToString()
        {

            return ((StartTime.Hours<10)?  "0" : "")  + StartTime.Hours.ToString() + ":" + ((StartTime.Minutes < 10) ? "0" : "") + StartTime.Minutes.ToString() + " - " // ido
                  + ((EndTime.Hours < 10) ? "0" : "") + EndTime.Hours.ToString() + ":" + ((EndTime.Minutes < 10) ? "0" : "") + EndTime.Minutes.ToString();
        }
    }
}
