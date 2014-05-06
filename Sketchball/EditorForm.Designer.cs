namespace Sketchball
{
    partial class EditorForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPBMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.openPBMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.savePBMButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.playButton = new System.Windows.Forms.ToolStripMenuItem();
            this.playgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitToMenueAndField = new System.Windows.Forms.SplitContainer();
            this.elementPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.PlayFieldEditor = new Sketchball.Controls.PinballEditControl();
            this.dragThumb = new System.Windows.Forms.PictureBox();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).BeginInit();
            this.MainSplitToMenueAndField.Panel1.SuspendLayout();
            this.MainSplitToMenueAndField.Panel2.SuspendLayout();
            this.MainSplitToMenueAndField.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dragThumb)).BeginInit();
            this.menuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 100);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AccessibleName = "TitleLabel";
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(-4, 3);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Padding = new System.Windows.Forms.Padding(15);
            this.TitleLabel.Size = new System.Drawing.Size(308, 93);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Sketchball";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1119, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPBMButton,
            this.openPBMButton,
            this.savePBMButton,
            this.toolStripSeparator1,
            this.playButton,
            this.playgroundToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newPBMButton
            // 
            this.newPBMButton.Name = "newPBMButton";
            this.newPBMButton.ShortcutKeyDisplayString = "";
            this.newPBMButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newPBMButton.Size = new System.Drawing.Size(155, 22);
            this.newPBMButton.Text = "New";
            this.newPBMButton.Click += new System.EventHandler(this.newPBMButton_Click);
            // 
            // openPBMButton
            // 
            this.openPBMButton.Name = "openPBMButton";
            this.openPBMButton.ShortcutKeyDisplayString = "";
            this.openPBMButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openPBMButton.Size = new System.Drawing.Size(155, 22);
            this.openPBMButton.Text = "Open...";
            this.openPBMButton.Click += new System.EventHandler(this.openPBMButton_Click);
            // 
            // savePBMButton
            // 
            this.savePBMButton.Name = "savePBMButton";
            this.savePBMButton.ShortcutKeyDisplayString = "";
            this.savePBMButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.savePBMButton.Size = new System.Drawing.Size(155, 22);
            this.savePBMButton.Text = "Save";
            this.savePBMButton.Click += new System.EventHandler(this.savePBMButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // playButton
            // 
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(155, 22);
            this.playButton.Text = "Play...";
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // playgroundToolStripMenuItem
            // 
            this.playgroundToolStripMenuItem.Name = "playgroundToolStripMenuItem";
            this.playgroundToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.playgroundToolStripMenuItem.Text = "Playground";
            this.playgroundToolStripMenuItem.Click += new System.EventHandler(this.playgroundToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // MainSplitToMenueAndField
            // 
            this.MainSplitToMenueAndField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitToMenueAndField.Location = new System.Drawing.Point(0, 28);
            this.MainSplitToMenueAndField.Name = "MainSplitToMenueAndField";
            // 
            // MainSplitToMenueAndField.Panel1
            // 
            this.MainSplitToMenueAndField.Panel1.Controls.Add(this.elementPanel);
            this.MainSplitToMenueAndField.Panel1.Controls.Add(this.panel2);
            this.MainSplitToMenueAndField.Panel1.Controls.Add(this.TitleLabel);
            // 
            // MainSplitToMenueAndField.Panel2
            // 
            this.MainSplitToMenueAndField.Panel2.Controls.Add(this.PlayFieldEditor);
            this.MainSplitToMenueAndField.Size = new System.Drawing.Size(1119, 668);
            this.MainSplitToMenueAndField.SplitterDistance = 309;
            this.MainSplitToMenueAndField.TabIndex = 1;
            // 
            // elementPanel
            // 
            this.elementPanel.AutoScroll = true;
            this.elementPanel.Location = new System.Drawing.Point(7, 138);
            this.elementPanel.Name = "elementPanel";
            this.elementPanel.Size = new System.Drawing.Size(292, 483);
            this.elementPanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.toolBar);
            this.panel2.Location = new System.Drawing.Point(0, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(307, 33);
            this.panel2.TabIndex = 1;
            // 
            // toolBar
            // 
            this.toolBar.AutoSize = false;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolBar.Size = new System.Drawing.Size(307, 25);
            this.toolBar.Stretch = true;
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolStrip1";
            // 
            // PlayFieldEditor
            // 
            this.PlayFieldEditor.AllowDrop = true;
            this.PlayFieldEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayFieldEditor.Location = new System.Drawing.Point(0, 0);
            this.PlayFieldEditor.Name = "PlayFieldEditor";
            this.PlayFieldEditor.SelectedElement = null;
            this.PlayFieldEditor.Size = new System.Drawing.Size(813, 624);
            this.PlayFieldEditor.TabIndex = 2;
            this.PlayFieldEditor.Text = "pinballEditControl1";
            this.PlayFieldEditor.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.PlayFieldEditor.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.PlayFieldEditor.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.PlayFieldEditor.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.PlayFieldEditor.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.OnGiveFeedback);
            this.PlayFieldEditor.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.OnQueryContinueDrag);
            // 
            // dragThumb
            // 
            this.dragThumb.BackColor = System.Drawing.Color.Transparent;
            this.dragThumb.Location = new System.Drawing.Point(1016, 599);
            this.dragThumb.Name = "dragThumb";
            this.dragThumb.Size = new System.Drawing.Size(100, 50);
            this.dragThumb.TabIndex = 3;
            this.dragThumb.TabStop = false;
            this.dragThumb.Visible = false;
            // 
            // menuPanel
            // 
            this.menuPanel.Controls.Add(this.mainMenuStrip);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(1119, 28);
            this.menuPanel.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Pinball machine files|*.pmf";
            this.openFileDialog.Title = "Select Pinball machine";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "pmf";
            this.saveFileDialog.Filter = "Pinball machine files|*.pmf";
            // 
            // PlayFieldEditor
            // 
            this.PlayFieldEditor.AllowDrop = true;
            this.PlayFieldEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
           | System.Windows.Forms.AnchorStyles.Right| System.Windows.Forms.AnchorStyles.Bottom)));
            //this.PlayFieldEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayFieldEditor.Location = new System.Drawing.Point(0, 0);
            this.PlayFieldEditor.Name = "PlayFieldEditor";
            this.PlayFieldEditor.SelectedElement = null;
            this.PlayFieldEditor.Size = new System.Drawing.Size(806, 668);
            this.PlayFieldEditor.TabIndex = 2;
            this.PlayFieldEditor.Text = "pinballEditControl1";
            this.PlayFieldEditor.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.PlayFieldEditor.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.PlayFieldEditor.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.PlayFieldEditor.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.PlayFieldEditor.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.OnGiveFeedback);
            this.PlayFieldEditor.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.OnQueryContinueDrag);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 696);
            this.Controls.Add(this.dragThumb);
            this.Controls.Add(this.MainSplitToMenueAndField);
            this.Controls.Add(this.menuPanel);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "EditorForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.MainSplitToMenueAndField.Panel1.ResumeLayout(false);
            this.MainSplitToMenueAndField.Panel1.PerformLayout();
            this.MainSplitToMenueAndField.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).EndInit();
            this.MainSplitToMenueAndField.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dragThumb)).EndInit();
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPBMButton;
        private System.Windows.Forms.ToolStripMenuItem openPBMButton;
        private System.Windows.Forms.ToolStripMenuItem savePBMButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem playButton;
        private System.Windows.Forms.ToolStripMenuItem playgroundToolStripMenuItem;
        private System.Windows.Forms.SplitContainer MainSplitToMenueAndField;
        private Controls.PinballEditControl PlayFieldEditor;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel elementPanel;
        private System.Windows.Forms.PictureBox dragThumb;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;

    }
}

