namespace Sketchball
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.playButton = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitToMenueAndField = new System.Windows.Forms.SplitContainer();
            this.elementPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.fieldAndPropertySplitter = new System.Windows.Forms.SplitContainer();
            this.playFieldPanel = new Sketchball.Editor.NoScrollOnFocusPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.machineInspector = new Sketchball.Controls.ElementInspector();
            this.label1 = new System.Windows.Forms.Label();
            this.elementInspector = new Sketchball.Controls.ElementInspector();
            this.mainView = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.zoomBar = new Sketchball.Controls.ToolStripTrackBarItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).BeginInit();
            this.MainSplitToMenueAndField.Panel1.SuspendLayout();
            this.MainSplitToMenueAndField.Panel2.SuspendLayout();
            this.MainSplitToMenueAndField.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldAndPropertySplitter)).BeginInit();
            this.fieldAndPropertySplitter.Panel1.SuspendLayout();
            this.fieldAndPropertySplitter.Panel2.SuspendLayout();
            this.fieldAndPropertySplitter.SuspendLayout();
            this.mainView.BottomToolStripPanel.SuspendLayout();
            this.mainView.ContentPanel.SuspendLayout();
            this.mainView.TopToolStripPanel.SuspendLayout();
            this.mainView.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolStripMenuItem1});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1119, 26);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPBMButton,
            this.openPBMButton,
            this.savePBMButton,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.playButton});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(40, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newPBMButton
            // 
            this.newPBMButton.Name = "newPBMButton";
            this.newPBMButton.ShortcutKeyDisplayString = "";
            this.newPBMButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newPBMButton.Size = new System.Drawing.Size(218, 22);
            this.newPBMButton.Text = "New";
            this.newPBMButton.Click += new System.EventHandler(this.onNewMachine);
            // 
            // openPBMButton
            // 
            this.openPBMButton.Name = "openPBMButton";
            this.openPBMButton.ShortcutKeyDisplayString = "";
            this.openPBMButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openPBMButton.Size = new System.Drawing.Size(218, 22);
            this.openPBMButton.Text = "Open...";
            this.openPBMButton.Click += new System.EventHandler(this.onOpenMachine);
            // 
            // savePBMButton
            // 
            this.savePBMButton.Name = "savePBMButton";
            this.savePBMButton.ShortcutKeyDisplayString = "";
            this.savePBMButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.savePBMButton.Size = new System.Drawing.Size(218, 22);
            this.savePBMButton.Text = "Save";
            this.savePBMButton.Click += new System.EventHandler(this.onSaveMachine);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.onSaveAsMachine);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(215, 6);
            // 
            // playButton
            // 
            this.playButton.Name = "playButton";
            this.playButton.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.playButton.Size = new System.Drawing.Size(218, 22);
            this.playButton.Text = "Play";
            this.playButton.Click += new System.EventHandler(this.onPlayClicked);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripMenuItem1.Image = global::Sketchball.Properties.Resources.control_play;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(60, 22);
            this.toolStripMenuItem1.Text = "Play";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.onPlayClicked);
            // 
            // MainSplitToMenueAndField
            // 
            this.MainSplitToMenueAndField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitToMenueAndField.Location = new System.Drawing.Point(0, 0);
            this.MainSplitToMenueAndField.Name = "MainSplitToMenueAndField";
            // 
            // MainSplitToMenueAndField.Panel1
            // 
            this.MainSplitToMenueAndField.Panel1.Controls.Add(this.elementPanel);
            this.MainSplitToMenueAndField.Panel1.Controls.Add(this.panel2);
            this.MainSplitToMenueAndField.Panel1.Controls.Add(this.TitleLabel);
            this.MainSplitToMenueAndField.Panel1.Resize += new System.EventHandler(this.onLeftPanelResize);
            // 
            // MainSplitToMenueAndField.Panel2
            // 
            this.MainSplitToMenueAndField.Panel2.AutoScroll = true;
            this.MainSplitToMenueAndField.Panel2.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.MainSplitToMenueAndField.Panel2.Controls.Add(this.fieldAndPropertySplitter);
            this.MainSplitToMenueAndField.Size = new System.Drawing.Size(1119, 640);
            this.MainSplitToMenueAndField.SplitterDistance = 288;
            this.MainSplitToMenueAndField.TabIndex = 1;
            // 
            // elementPanel
            // 
            this.elementPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementPanel.AutoScroll = true;
            this.elementPanel.Location = new System.Drawing.Point(7, 138);
            this.elementPanel.Name = "elementPanel";
            this.elementPanel.Size = new System.Drawing.Size(276, 499);
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
            this.panel2.Size = new System.Drawing.Size(286, 33);
            this.panel2.TabIndex = 1;
            // 
            // toolBar
            // 
            this.toolBar.AutoSize = false;
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolBar.Size = new System.Drawing.Size(286, 25);
            this.toolBar.Stretch = true;
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "toolStrip1";
            // 
            // fieldAndPropertySplitter
            // 
            this.fieldAndPropertySplitter.BackColor = System.Drawing.SystemColors.Control;
            this.fieldAndPropertySplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fieldAndPropertySplitter.Location = new System.Drawing.Point(0, 0);
            this.fieldAndPropertySplitter.Name = "fieldAndPropertySplitter";
            // 
            // fieldAndPropertySplitter.Panel1
            // 
            this.fieldAndPropertySplitter.Panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.fieldAndPropertySplitter.Panel1.Controls.Add(this.playFieldPanel);
            // 
            // fieldAndPropertySplitter.Panel2
            // 
            this.fieldAndPropertySplitter.Panel2.Controls.Add(this.label2);
            this.fieldAndPropertySplitter.Panel2.Controls.Add(this.machineInspector);
            this.fieldAndPropertySplitter.Panel2.Controls.Add(this.label1);
            this.fieldAndPropertySplitter.Panel2.Controls.Add(this.elementInspector);
            this.fieldAndPropertySplitter.Size = new System.Drawing.Size(827, 640);
            this.fieldAndPropertySplitter.SplitterDistance = 628;
            this.fieldAndPropertySplitter.TabIndex = 3;
            // 
            // playFieldPanel
            // 
            this.playFieldPanel.AutoScroll = true;
            this.playFieldPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playFieldPanel.Location = new System.Drawing.Point(0, 0);
            this.playFieldPanel.Name = "playFieldPanel";
            this.playFieldPanel.Size = new System.Drawing.Size(628, 640);
            this.playFieldPanel.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(3, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Element Config";
            // 
            // machineInspector
            // 
            this.machineInspector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.machineInspector.Location = new System.Drawing.Point(6, 32);
            this.machineInspector.Name = "machineInspector";
            this.machineInspector.Size = new System.Drawing.Size(177, 197);
            this.machineInspector.TabIndex = 2;
            this.machineInspector.ToolbarVisible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pinball Machine Config";
            // 
            // elementInspector
            // 
            this.elementInspector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementInspector.Location = new System.Drawing.Point(6, 269);
            this.elementInspector.Name = "elementInspector";
            this.elementInspector.Size = new System.Drawing.Size(177, 368);
            this.elementInspector.TabIndex = 0;
            this.elementInspector.ToolbarVisible = false;
            // 
            // mainView
            // 
            // 
            // mainView.BottomToolStripPanel
            // 
            this.mainView.BottomToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // mainView.ContentPanel
            // 
            this.mainView.ContentPanel.Controls.Add(this.MainSplitToMenueAndField);
            this.mainView.ContentPanel.Size = new System.Drawing.Size(1119, 640);
            this.mainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainView.Location = new System.Drawing.Point(0, 0);
            this.mainView.Name = "mainView";
            this.mainView.Size = new System.Drawing.Size(1119, 696);
            this.mainView.TabIndex = 2;
            this.mainView.Text = "toolStripContainer2";
            // 
            // mainView.TopToolStripPanel
            // 
            this.mainView.TopToolStripPanel.Controls.Add(this.mainMenuStrip);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomBar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.MaximumSize = new System.Drawing.Size(0, 30);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(0, 30);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // zoomBar
            // 
            this.zoomBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.zoomBar.Name = "zoomBar";
            this.zoomBar.Size = new System.Drawing.Size(104, 45);
            this.zoomBar.Text = "toolStripTrackBarItem1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Pinball machine files|*.pmf";
            this.openFileDialog.Title = "Select Pinball machine";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "pmf";
            this.saveFileDialog.FileName = "Untitled.pmf";
            this.saveFileDialog.Filter = "Pinball machine files|*.pmf";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 696);
            this.Controls.Add(this.mainView);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "EditorForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.MainSplitToMenueAndField.Panel1.ResumeLayout(false);
            this.MainSplitToMenueAndField.Panel1.PerformLayout();
            this.MainSplitToMenueAndField.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).EndInit();
            this.MainSplitToMenueAndField.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.fieldAndPropertySplitter.Panel1.ResumeLayout(false);
            this.fieldAndPropertySplitter.Panel2.ResumeLayout(false);
            this.fieldAndPropertySplitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fieldAndPropertySplitter)).EndInit();
            this.fieldAndPropertySplitter.ResumeLayout(false);
            this.mainView.BottomToolStripPanel.ResumeLayout(false);
            this.mainView.ContentPanel.ResumeLayout(false);
            this.mainView.TopToolStripPanel.ResumeLayout(false);
            this.mainView.TopToolStripPanel.PerformLayout();
            this.mainView.ResumeLayout(false);
            this.mainView.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
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
        private System.Windows.Forms.SplitContainer MainSplitToMenueAndField;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel elementPanel;
        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.SplitContainer fieldAndPropertySplitter;
        private Controls.ElementInspector elementInspector;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.ToolStripTrackBarItem zoomBar;
        private System.Windows.Forms.Label label2;
        private Controls.ElementInspector machineInspector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer mainView;
        private Editor.NoScrollOnFocusPanel playFieldPanel;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

    }
}

