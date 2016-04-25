using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Database.Migrations;
using TimeTracker.Entities;

namespace TimeTracker.Database
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext() : base("name=TimeTracker")
        {

        }
        public static void ConfigureMigrations()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());

        }
        public DbSet<User> Users { get; set; }
        public DbSet<TimedEvent> TimedEvents { get; set; }
    }
}
