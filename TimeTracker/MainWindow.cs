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
using TimeTracker.Properties;

namespace TimeTracker
{
    public partial class MainWindow : Form
    {
        User m_user;
        public MainWindow()
        {
            InitializeComponent();
            Visible = false;
            Open();
        }
        async void Open()
        {
            notify.ContextMenuStrip = null;
            notify.Icon = Resources.IconPurple;
            notify.Text = "Starting Time Tracker...";
            DatabaseContext.ConfigureMigrations();
            using (var db = new DatabaseContext())
            {
                var username = Environment.UserName;
                m_user = await db.Users.FirstOrDefaultAsync(fn => fn.UserName == username);
                if (m_user == null)
                {
                    m_user = new User { UserName = username };
                    db.Users.Add(m_user);
                    await db.SaveChangesAsync();
                }
                UpdateIconState(db);
            }
            notify.ContextMenuStrip = menu;
            notify.Text = "";
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Hide();
        }

        void UpdateIconState(DatabaseContext db)
        {
            var timing = m_user.GetTimes(db).FirstOrDefault(fn => fn.End == null);
            if (timing == null)
            {
                mniToggle.Text = "Start Timing";
                mniEditNote.Enabled = false;
                notify.Icon = Resources.IconBlue;
            }
            else
            {
                mniToggle.Text = "Stop Timing";
                mniEditNote.Enabled = true;
                notify.Icon = Resources.IconGreen;
            }
        }

        void MenuOpeningHandler(object sender, CancelEventArgs e)
        {
        }

        void ToggleStartStopHandler(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                db.Users.Attach(m_user);
                var last = m_user.GetTimes(db).FirstOrDefault(fn => fn.End == null);
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
                UpdateIconState(db);
            }
        }

        void EditNoteHandler(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                var last = m_user.GetTimes(db).OrderByDescending(fn => fn.Start).FirstOrDefault();
                if (last == null)
                    return;
                using (var form = new NoteWindow(last.Note))
                {
                    if (form.ShowDialog(this) != DialogResult.OK)
                        return;
                    last.Note = form.Note;
                    db.SaveChanges();
                }
            }
        }

        void ExitHandler(object sender, EventArgs e)
        {
            Close();
        }

        private void NewTimeNoteHandler(object sender, EventArgs e)
        {
            using (var form = new NoteWindow(""))
                if (form.ShowDialog(this) == DialogResult.OK)
                    using (var db = new DatabaseContext())
                    {
                        var now = DateTime.Now;
                        var last = m_user.GetTimes(db).OrderByDescending(fn => fn.Start).FirstOrDefault();
                        if (last.End == null)
                            last.End = now;
                        db.TimedEvents.Add(new TimedEvent { Start = now, UserId = m_user.UserId, Note = form.Note });
                        db.SaveChanges();
                    }
        }
    }
}
