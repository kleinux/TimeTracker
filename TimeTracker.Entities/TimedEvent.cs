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

        public TimeSpan Duration()
        {
            if (End == null)
                return TimeSpan.Zero;
            return End.GetValueOrDefault() - Start;
        }

        public static TimeSpan Duration(IEnumerable<TimedEvent> events)
        {
            var time = TimeSpan.Zero;
            foreach (var item in events)
                time += item.Duration();
            return time;
        }
    }
}
