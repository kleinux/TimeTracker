using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeTracker.Database;
using TimeTracker.Entities;

namespace TimeTracker
{
    public partial class MainWindow : Form
    {
        User m_user;
        public MainWindow()
        {
            InitializeComponent();
            DatabaseContext.ConfigureMigrations();
            using (var db = new DatabaseContext())
            {
                var username = Environment.UserName;
                m_user = db.Users.FirstOrDefault(fn => fn.UserName == username);
                if (m_user == null)
                {
                    m_user = new User { UserName = username };
                    db.Users.Add(m_user);
                    db.SaveChanges();
                }
            }
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Hide();
        }

        void MenuOpeningHandler(object sender, CancelEventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                var last = db.TimedEvents.FirstOrDefault(fn => fn.End == null && fn.User.UserId == m_user.UserId);
                if (last == null)
                {
                    mniClockInOut.Text = "Clock In";
                    mniEditNote.Enabled = false;
                }
                else
                {
                    mniClockInOut.Text = "Clock Out";
                    mniEditNote.Enabled = true;
                }
            }
        }

        void ClockInOutHandler(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                var last = db.TimedEvents.FirstOrDefault(fn => fn.End == null && fn.User.UserId == m_user.UserId);
                if (last == null)
                {
                    last = new TimedEvent { Start = DateTime.Now, User = m_user };
                    db.TimedEvents.Add(last);
                }
                else
                {
                    last.End = DateTime.Now;
                }
                db.SaveChanges();
            }
        }

        void EditNoteHandler(object sender, EventArgs e)
        {

        }
    }
}
