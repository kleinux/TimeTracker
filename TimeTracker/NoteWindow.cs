using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeTracker
{
    public sealed partial class NoteWindow : Form
    {
        public string Note => txtNote?.Text;

        public NoteWindow(string note)
        {
            InitializeComponent();
            txtNote.Text = note;
        }

        void AcceptHandler(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        void CancelHandler(object sender, EventArgs e)
        {
            Close();
        }
    }
}
