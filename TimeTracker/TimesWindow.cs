using System;
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
            dtStart.Value = dtEnd.Value.AddDays(-7d);
            FindTimesHandler(null, null);
        }

        IEnumerable<TimedEvent> Times() => (IEnumerable<TimedEvent>)bs_times.DataSource;

        void FindTimesHandler(object sender, EventArgs e)
        {
            if (bs_times.DataSource != null)
            {
                LinkUser();
            }
            var start = dtStart.Value;
            var end = dtEnd.Value.AddDays(1d);
            m_list = new ObservableCollection<TimedEvent>(m_user.GetTimes(m_db).Where(fn => fn.Start >= start && fn.Start < end));
            m_list.CollectionChanged += ListChangedHandler;
            grid.DataSource = m_list.ToBindingList();

        }

        void ListChangedHandler(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (var time in e.NewItems.Cast<TimedEvent>())
                        time.Start = DateTime.Now;
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

        void LinkUser()
        {
            foreach (var time in Times())
                time.User = m_user;
            m_db.SaveChanges();
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
            lblHours.Text = $"Time: {TimedEvent.Duration(Times())}";
        }
    }
}
