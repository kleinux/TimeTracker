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
            Visible = false;
            Open();
        }
        async void Open()
        {
            notify.ContextMenuStrip = null;
            notify.ShowBalloonTip(0, "Starting", "Time Tracker is opening the database", ToolTipIcon.Info);
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
            }
            notify.ContextMenuStrip = menu;
            notify.ShowBalloonTip(0, "Started", "Time Tracker Is open", ToolTipIcon.Info);
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
                var last = m_user.GetTimes(db).FirstOrDefault(fn => fn.End == null);
                if (last == null)
                {
                    mniToggle.Text = "Start Timing";
                    mniEditNote.Enabled = false;
                }
                else
                {
                    mniToggle.Text = "Stop Timing";
                    mniEditNote.Enabled = true;
                }
            }
        }

        void ToggleStartStopHandler(object sender, EventArgs e)
        {
            using (var db = new DatabaseContext())
            {
                var last = m_user.GetTimes(db).FirstOrDefault(fn => fn.End == null);
                if (last == null)
                {
                    last = new TimedEvent { Start = DateTime.Now, UserId = m_user.UserId };
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
