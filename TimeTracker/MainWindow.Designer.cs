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
            this.mniToggle = new System.Windows.Forms.ToolStripMenuItem();
            this.mniNewTimeNote = new System.Windows.Forms.ToolStripMenuItem();
            this.mniEditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.editTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mniToggle,
            this.mniNewTimeNote,
            this.mniEditNote,
            this.editTimesToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(158, 142);
            this.menu.Opening += new System.ComponentModel.CancelEventHandler(this.MenuOpeningHandler);
            // 
            // mniToggle
            // 
            this.mniToggle.Name = "mniToggle";
            this.mniToggle.Size = new System.Drawing.Size(157, 22);
            this.mniToggle.Text = "Start/Stop Time";
            this.mniToggle.Click += new System.EventHandler(this.ToggleStartStopHandler);
            // 
            // mniNewTimeNote
            // 
            this.mniNewTimeNote.Name = "mniNewTimeNote";
            this.mniNewTimeNote.Size = new System.Drawing.Size(157, 22);
            this.mniNewTimeNote.Text = "New Time Note";
            this.mniNewTimeNote.Click += new System.EventHandler(this.NewTimeNoteHandler);
            // 
            // mniEditNote
            // 
            this.mniEditNote.Name = "mniEditNote";
            this.mniEditNote.Size = new System.Drawing.Size(157, 22);
            this.mniEditNote.Text = "Edit Note";
            this.mniEditNote.Click += new System.EventHandler(this.EditNoteHandler);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitHandler);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // editTimesToolStripMenuItem
            // 
            this.editTimesToolStripMenuItem.Name = "editTimesToolStripMenuItem";
            this.editTimesToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.editTimesToolStripMenuItem.Text = "Edit Times";
            this.editTimesToolStripMenuItem.Click += new System.EventHandler(this.EditTimesHandler);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 130);
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
        private System.Windows.Forms.ToolStripMenuItem mniToggle;
        private System.Windows.Forms.ToolStripMenuItem mniNewTimeNote;
        private System.Windows.Forms.ToolStripMenuItem mniEditNote;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTimesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

