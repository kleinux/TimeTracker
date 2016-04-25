using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Entities;

namespace TimeTracker.Testing
{
    [TestClass]
    public class TimingTests
    {
        [TestMethod]
        public void EightHourDayNoBreaks()
        {
            var time = new TimedEvent
            {
                Start = new DateTime(2016, 3, 1, 8, 0, 0),
                End = new DateTime(2016, 3, 1, 16, 0, 0)
            };
            var duration = time.Duration();
            Assert.AreEqual(new TimeSpan(8, 0, 0), duration, "Time should be eight hours");
        }
        [TestMethod]
        public void EightHourDayWithBreak()
        {
            var time1 = new TimedEvent
            {
                Start = new DateTime(2016, 3, 1, 8, 0, 0),
                End = new DateTime(2016, 3, 1, 12, 0, 0)
            };
            var time2 = new TimedEvent
            {
                Start = new DateTime(2016, 3, 1, 12, 30, 0),
                End = new DateTime(2016, 3, 1, 16, 30, 0)
            };
            var duration = TimedEvent.Duration(new[] { time1, time2 });
            Assert.AreEqual(new TimeSpan(8, 0, 0), duration, "Time should be eight hours");
        }
    }
}
