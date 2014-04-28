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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveStageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playgroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSplitToMenueAndField = new System.Windows.Forms.SplitContainer();
            this.elementPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RedoTool = new System.Windows.Forms.PictureBox();
            this.SelectionTool = new System.Windows.Forms.PictureBox();
            this.CircleTool = new System.Windows.Forms.PictureBox();
            this.LineTool = new System.Windows.Forms.PictureBox();
            this.undoTool = new System.Windows.Forms.PictureBox();
            this.menuPanel = new System.Windows.Forms.Panel();
            this.PlayFieldEditor = new Sketchball.Controls.PinballEditControl();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).BeginInit();
            this.MainSplitToMenueAndField.Panel1.SuspendLayout();
            this.MainSplitToMenueAndField.Panel2.SuspendLayout();
            this.MainSplitToMenueAndField.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RedoTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CircleTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineTool)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.undoTool)).BeginInit();
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
            this.TitleLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.TitleLabel.Click += new System.EventHandler(this.TitleLabel_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1119, 24);
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
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
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
            this.MainSplitToMenueAndField.Size = new System.Drawing.Size(1119, 624);
            this.MainSplitToMenueAndField.SplitterDistance = 302;
            this.MainSplitToMenueAndField.TabIndex = 1;
            // 
            // elementPanel
            // 
            this.elementPanel.Location = new System.Drawing.Point(7, 183);
            this.elementPanel.Name = "elementPanel";
            this.elementPanel.Size = new System.Drawing.Size(292, 438);
            this.elementPanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RedoTool);
            this.panel2.Controls.Add(this.SelectionTool);
            this.panel2.Controls.Add(this.CircleTool);
            this.panel2.Controls.Add(this.LineTool);
            this.panel2.Controls.Add(this.undoTool);
            this.panel2.Location = new System.Drawing.Point(0, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(304, 78);
            this.panel2.TabIndex = 1;
            // 
            // RedoTool
            // 
            this.RedoTool.BackgroundImage = global::Sketchball.Properties.Resources.Redo_icon;
            this.RedoTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RedoTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RedoTool.Location = new System.Drawing.Point(232, 0);
            this.RedoTool.Name = "RedoTool";
            this.RedoTool.Size = new System.Drawing.Size(41, 39);
            this.RedoTool.TabIndex = 7;
            this.RedoTool.TabStop = false;
            this.RedoTool.MouseHover += new System.EventHandler(this.RedoTool_MouseHover);
            // 
            // SelectionTool
            // 
            this.SelectionTool.BackgroundImage = global::Sketchball.Properties.Resources.Very_Basic_Cursor_icon;
            this.SelectionTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SelectionTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectionTool.Location = new System.Drawing.Point(138, 0);
            this.SelectionTool.Name = "SelectionTool";
            this.SelectionTool.Size = new System.Drawing.Size(41, 39);
            this.SelectionTool.TabIndex = 6;
            this.SelectionTool.TabStop = false;
            this.SelectionTool.MouseHover += new System.EventHandler(this.SelectionTool_MouseHover);
            // 
            // CircleTool
            // 
            this.CircleTool.BackgroundImage = global::Sketchball.Properties.Resources.circle_outline_512;
            this.CircleTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CircleTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CircleTool.Location = new System.Drawing.Point(91, 0);
            this.CircleTool.Name = "CircleTool";
            this.CircleTool.Size = new System.Drawing.Size(41, 39);
            this.CircleTool.TabIndex = 5;
            this.CircleTool.TabStop = false;
            this.CircleTool.MouseHover += new System.EventHandler(this.CircleTool_MouseHover);
            // 
            // LineTool
            // 
            this.LineTool.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LineTool.BackgroundImage")));
            this.LineTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LineTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LineTool.Location = new System.Drawing.Point(44, 0);
            this.LineTool.Name = "LineTool";
            this.LineTool.Size = new System.Drawing.Size(41, 39);
            this.LineTool.TabIndex = 4;
            this.LineTool.TabStop = false;
            this.LineTool.MouseHover += new System.EventHandler(this.pictureBox2_MouseHover);
            // 
            // undoTool
            // 
            this.undoTool.BackgroundImage = global::Sketchball.Properties.Resources.Undo_icon;
            this.undoTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.undoTool.Cursor = System.Windows.Forms.Cursors.Hand;
            this.undoTool.Location = new System.Drawing.Point(185, 0);
            this.undoTool.Name = "undoTool";
            this.undoTool.Size = new System.Drawing.Size(41, 39);
            this.undoTool.TabIndex = 3;
            this.undoTool.TabStop = false;
            this.undoTool.MouseHover += new System.EventHandler(this.undoTool_MouseHover);
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
            this.PlayFieldEditor.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDrop);
            this.PlayFieldEditor.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter);
            this.PlayFieldEditor.DragOver += new System.Windows.Forms.DragEventHandler(this.DragOver);
            this.PlayFieldEditor.DragLeave += new System.EventHandler(this.DragLeave);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1119, 652);
            this.Controls.Add(this.MainSplitToMenueAndField);
            this.Controls.Add(this.menuPanel);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "EditorForm";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditorForm_FormClosed);
            this.Load += new System.EventHandler(this.EditorForm_Load);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.MainSplitToMenueAndField.Panel1.ResumeLayout(false);
            this.MainSplitToMenueAndField.Panel1.PerformLayout();
            this.MainSplitToMenueAndField.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitToMenueAndField)).EndInit();
            this.MainSplitToMenueAndField.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RedoTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectionTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CircleTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineTool)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.undoTool)).EndInit();
            this.menuPanel.ResumeLayout(false);
            this.menuPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveStageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playgroundToolStripMenuItem;
        private System.Windows.Forms.SplitContainer MainSplitToMenueAndField;
        private Controls.PinballEditControl PlayFieldEditor;
        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox undoTool;
        private System.Windows.Forms.PictureBox RedoTool;
        private System.Windows.Forms.PictureBox SelectionTool;
        private System.Windows.Forms.PictureBox CircleTool;
        private System.Windows.Forms.PictureBox LineTool;
        private System.Windows.Forms.FlowLayoutPanel elementPanel;

    }
}

