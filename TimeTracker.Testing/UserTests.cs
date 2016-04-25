using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Database;
using TimeTracker.Entities;

namespace TimeTracker.Testing
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void CreateUser()
        {
            using (var db = new DatabaseContext())
            {
                var user = new User { UserName = "bob", DisplayName = "Bob Test" };
                db.Users.Add(user);
                db.SaveChanges();
                Assert.AreNotEqual(user.UserId, 0, "UserId should contain a value after saving");
            }
        }
    }
}
