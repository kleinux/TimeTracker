namespace TimeTracker
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniClockInOut = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewClockNote = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.menu;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Visible = true;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniClockInOut,
            this.mniNewClockNote,
            this.mniEditNote});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(161, 70);
            this.menu.Opening += new System.ComponentModel.CancelEventHandler(this.MenuOpeningHandler);
            // 
            // mniClockInOut
            // 
            this.mniClockInOut.Name = "mniClockInOut";
            this.mniClockInOut.Size = new System.Drawing.Size(160, 22);
            this.mniClockInOut.Text = "Clock In";
            this.mniClockInOut.Click += new System.EventHandler(this.ClockInOutHandler);
            // 
            // mniNewClockNote
            // 
            this.mniNewClockNote.Name = "mniNewClockNote";
            this.mniNewClockNote.Size = new System.Drawing.Size(160, 22);
            this.mniNewClockNote.Text = "New Clock Note";
            // 
            // mniEditNote
            // 
            this.mniEditNote.Name = "mniEditNote";
            this.mniEditNote.Size = new System.Drawing.Size(160, 22);
            this.mniEditNote.Text = "Edit Note";
            this.mniEditNote.Click += new System.EventHandler(this.EditNoteHandler);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 198);
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Time Tracker";
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem mniClockInOut;
        private System.Windows.Forms.ToolStripMenuItem mniNewClockNote;
        private System.Windows.Forms.ToolStripMenuItem mniEditNote;
    }
}

