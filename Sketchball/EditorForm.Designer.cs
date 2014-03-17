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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitToMenueAndField = new System.Windows.Forms.SplitContainer();
            this.SplitToNameAndTools = new System.Windows.Forms.SplitContainer();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.ToolsTab = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.PlayFieldEditor = new Sketchball.Controls.PinballEditControl();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).BeginInit();
            this.MainSplitToMenueAndField.Panel1.SuspendLayout();
            this.MainSplitToMenueAndField.Panel2.SuspendLayout();
            this.MainSplitToMenueAndField.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitToNameAndTools)).BeginInit();
            this.SplitToNameAndTools.Panel1.SuspendLayout();
            this.SplitToNameAndTools.Panel2.SuspendLayout();
            this.SplitToNameAndTools.SuspendLayout();
            this.ToolsTab.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(813, 27);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openStageToolStripMenuItem,
            this.saveStageToolStripMenuItem,
            this.toolStripSeparator1,
            this.playToolStripMenuItem,
            this.playgroundToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.newToolStripMenuItem.Text = "New";
            // 
            // openStageToolStripMenuItem
            // 
            this.openStageToolStripMenuItem.Name = "openStageToolStripMenuItem";
            this.openStageToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openStageToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openStageToolStripMenuItem.Text = "Open Stage...";
            // 
            // saveStageToolStripMenuItem
            // 
            this.saveStageToolStripMenuItem.Name = "saveStageToolStripMenuItem";
            this.saveStageToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveStageToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.saveStageToolStripMenuItem.Text = "Save Stage";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Enabled = false;
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.playToolStripMenuItem.Text = "Play...";
            // 
            // playgroundToolStripMenuItem
            // 
            this.playgroundToolStripMenuItem.Name = "playgroundToolStripMenuItem";
            this.playgroundToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.playgroundToolStripMenuItem.Text = "Playground";
            this.playgroundToolStripMenuItem.Click += new System.EventHandler(this.playgroundToolStripMenuItem_Click);
            // 
            // MainSplitToMenueAndField
            // 
            this.MainSplitToMenueAndField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitToMenueAndField.Location = new System.Drawing.Point(0, 0);
            this.MainSplitToMenueAndField.Name = "MainSplitToMenueAndField";
            // 
            // MainSplitToMenueAndField.Panel1
            // 
            this.MainSplitToMenueAndField.Panel1.Controls.Add(this.SplitToNameAndTools);
            // 
            // MainSplitToMenueAndField.Panel2
            // 
            this.MainSplitToMenueAndField.Panel2.Controls.Add(this.splitContainer1);
            this.MainSplitToMenueAndField.Size = new System.Drawing.Size(1119, 652);
            this.MainSplitToMenueAndField.SplitterDistance = 302;
            this.MainSplitToMenueAndField.TabIndex = 1;
            // 
            // SplitToNameAndTools
            // 
            this.SplitToNameAndTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitToNameAndTools.Location = new System.Drawing.Point(0, 0);
            this.SplitToNameAndTools.Name = "SplitToNameAndTools";
            this.SplitToNameAndTools.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitToNameAndTools.Panel1
            // 
            this.SplitToNameAndTools.Panel1.Controls.Add(this.TitleLabel);
            this.SplitToNameAndTools.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer2_Panel1_Paint);
            // 
            // SplitToNameAndTools.Panel2
            // 
            this.SplitToNameAndTools.Panel2.Controls.Add(this.ToolsTab);
            this.SplitToNameAndTools.Size = new System.Drawing.Size(302, 652);
            this.SplitToNameAndTools.SplitterDistance = 103;
            this.SplitToNameAndTools.TabIndex = 0;
            // 
            // TitleLabel
            // 
            this.TitleLabel.AccessibleName = "TitleLabel";
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.BackColor = System.Drawing.Color.Transparent;
            this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(0, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Padding = new System.Windows.Forms.Padding(15);
            this.TitleLabel.Size = new System.Drawing.Size(308, 93);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "Sketchball";
            this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TitleLabel.Click += new System.EventHandler(this.TitleLabel_Click);
            // 
            // ToolsTab
            // 
            this.ToolsTab.Controls.Add(this.tabPage3);
            this.ToolsTab.Controls.Add(this.tabPage4);
            this.ToolsTab.Controls.Add(this.tabPage1);
            this.ToolsTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ToolsTab.Location = new System.Drawing.Point(0, 0);
            this.ToolsTab.Name = "ToolsTab";
            this.ToolsTab.SelectedIndex = 0;
            this.ToolsTab.Size = new System.Drawing.Size(302, 545);
            this.ToolsTab.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(294, 519);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Bumpers";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.flowLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(294, 519);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Slingshots";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(295, 293);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(294, 519);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Holes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mainMenuStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PlayFieldEditor);
            this.splitContainer1.Size = new System.Drawing.Size(813, 652);
            this.splitContainer1.SplitterDistance = 27;
            this.splitContainer1.TabIndex = 1;
            // 
            // PlayFieldEditor
            // 
            this.PlayFieldEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PlayFieldEditor.Location = new System.Drawing.Point(0, 0);
            this.PlayFieldEditor.Name = "PlayFieldEditor";
            this.PlayFieldEditor.SelectedElement = null;
            this.PlayFieldEditor.Size = new System.Drawing.Size(813, 621);
            this.PlayFieldEditor.TabIndex = 1;
            this.PlayFieldEditor.Text = "pinballEditControl1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 652);
            this.Controls.Add(this.MainSplitToMenueAndField);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.MainSplitToMenueAndField.Panel1.ResumeLayout(false);
            this.MainSplitToMenueAndField.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).EndInit();
            this.MainSplitToMenueAndField.ResumeLayout(false);
            this.SplitToNameAndTools.Panel1.ResumeLayout(false);
            this.SplitToNameAndTools.Panel1.PerformLayout();
            this.SplitToNameAndTools.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitToNameAndTools)).EndInit();
            this.SplitToNameAndTools.ResumeLayout(false);
            this.ToolsTab.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveStageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.SplitContainer MainSplitToMenueAndField;
        private System.Windows.Forms.ToolStripMenuItem playgroundToolStripMenuItem;
        private System.Windows.Forms.SplitContainer SplitToNameAndTools;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.TabControl ToolsTab;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.PinballEditControl PlayFieldEditor;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

    }
}

