﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class TimesWindow : Form
    {
        DatabaseContext m_db;
        User m_user;
        ObservableCollection<TimedEvent> m_list;

        public TimesWindow(User user)
        {
            m_db = new DatabaseContext();
            m_db.Users.Attach(user);
            m_user = user;
            InitializeComponent();
            grid.AutoGenerateColumns = false;
            var today = DateTime.Today;
            dtEnd.Value = today.AddDays(-(today.DayOfWeek - DayOfWeek.Sunday)).Date.AddDays(6d);
            dtStart.Value = dtEnd.Value.AddDays(-6d);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FindTimesHandler(null, null);
        }

        void LinkUser()
        {
            foreach (var time in Times())
                time.User = m_user;
            m_db.SaveChanges();
        }

        IEnumerable<TimedEvent> Times() => (IEnumerable<TimedEvent>)bs_times.DataSource;

        TimedEvent Started() => Times().FirstOrDefault(fn => fn.End == null);

        async void FindTimesHandler(object sender, EventArgs e)
        {
            if (bs_times.DataSource != null)
            {
                LinkUser();
            }
            var start = dtStart.Value;
            var end = dtEnd.Value.AddDays(1d);
            Enabled = false;
            m_list = new ObservableCollection<TimedEvent>(await m_user.GetTimes(m_db).Where(fn => fn.Start >= start && fn.Start < end).ToArrayAsync());
            Enabled = true;
            m_list.CollectionChanged += ListChangedHandler;
            bs_times.DataSource = m_list.ToBindingList();

        }

        void ListChangedHandler(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    m_db.TimedEvents.RemoveRange(e.OldItems.Cast<TimedEvent>());
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        void GridDoubleClickHandler(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == colNote.Index)
            {
                var time = (TimedEvent)grid.Rows[e.RowIndex].DataBoundItem;
                using (var form = new NoteWindow(time.Note))
                    if (form.ShowDialog(this) == DialogResult.OK)
                        time.Note = form.Note;
            }
        }

        void SaveHandler(object sender, EventArgs e)
        {
            LinkUser();
            m_db.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }

        void CloseHandler(object sender, EventArgs e)
        {
            Close();
        }

        void AddingNewTimeHandler(object sender, AddingNewEventArgs e)
        {
        }

        void bs_times_CurrentItemChanged(object sender, EventArgs e)
        {
            var selection = grid.SelectedCells.Cast<DataGridViewCell>().Select(fn => fn.RowIndex).Distinct().ToArray();
            var adding = TimeSpan.Zero;
            foreach (var index in selection)
            {
                var ev = (TimedEvent)grid.Rows[index].DataBoundItem;
                adding += ev.ComputeDuration();
            }
            string msg = "";
            if (selection.Length > 1)
                msg = $" Selection: {(int)adding.TotalHours}:{adding.Minutes:00}";
            var time = TimedEvent.ComputeDuration(Times(), true);
            lblHours.Text = $"Time: {(int)time.TotalHours}:{time.Minutes:00}{msg}";
            if (Started() != null)
                btnStartStop.Text = "Stop Timing";
            else
                btnStartStop.Text = "Start Timing";
        }

        void NewTimeHandler(object sender, EventArgs e)
        {
            var started = Started();
            if (started != null)
                started.End = DateTime.Now;
            else
                m_list.Add(m_db.TimedEvents.Add(new TimedEvent { Start = DateTime.Now, User = m_user }));
            bs_times.ResetBindings(false);
        }

        void SelectionChangedHandler(object sender, EventArgs e)
        {

        }
    }
}
