using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Entities;

namespace TimeTracker.Database
{
    public static class ExtensionMethods
    {
        public static IQueryable<TimedEvent> GetTimes(this User me, DatabaseContext context)
        {
            return context.TimedEvents.Where(fn => fn.User.UserId == me.UserId);
        }
    }
}
