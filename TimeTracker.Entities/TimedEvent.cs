using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.Entities
{
    public class TimedEvent
    {
        public int TimedEventId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Note { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Duration {
            get
            {
                var ts = ComputeDuration(true);
                var sb = new StringBuilder();
                if (ts.Days > 0)
                    sb.Append(ts.Days).Append(' ');
                sb.Append(ts.Hours).Append(':').Append(ts.Minutes.ToString("00"));
                return sb.ToString();
            }
        }

        public TimeSpan ComputeDuration(bool countNoEnd = false)
        {
            if (End != null)
            return End.GetValueOrDefault() - Start;
            if (countNoEnd)
                return DateTime.Now - Start;
            return TimeSpan.Zero;
        }

        public static TimeSpan ComputeDuration(IEnumerable<TimedEvent> events, bool countNoEnd = false)
        {
            var time = TimeSpan.Zero;
            foreach (var item in events)
                time += item.ComputeDuration(countNoEnd);
            return time;
        }
    }
}
