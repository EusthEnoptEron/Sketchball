
namespace Sketchball
{
    partial class SelectionForm
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
            this.CloseButton = new System.Windows.Forms.PictureBox();
            this.picBEditor = new System.Windows.Forms.PictureBox();
            this.picBGame = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBEditor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBGame)).BeginInit();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.CloseButton.BackgroundImage = global::Sketchball.Properties.Resources.Exit;
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CloseButton.InitialImage = null;
            this.CloseButton.Location = new System.Drawing.Point(323, 23);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(72, 69);
            this.CloseButton.TabIndex = 2;
            this.CloseButton.TabStop = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // picBEditor
            // 
            this.picBEditor.BackColor = System.Drawing.Color.Transparent;
            this.picBEditor.BackgroundImage = global::Sketchball.Properties.Resources.EditorSchrift2;
            this.picBEditor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picBEditor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBEditor.Location = new System.Drawing.Point(136, 253);
            this.picBEditor.Name = "picBEditor";
            this.picBEditor.Size = new System.Drawing.Size(168, 111);
            this.picBEditor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBEditor.TabIndex = 1;
            this.picBEditor.TabStop = false;
            this.picBEditor.Click += new System.EventHandler(this.picBEditor_Click);
            // 
            // picBGame
            // 
            this.picBGame.BackColor = System.Drawing.Color.Transparent;
            this.picBGame.BackgroundImage = global::Sketchball.Properties.Resources.PlaySchrift1;
            this.picBGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBGame.Location = new System.Drawing.Point(113, 82);
            this.picBGame.Name = "picBGame";
            this.picBGame.Size = new System.Drawing.Size(145, 129);
            this.picBGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBGame.TabIndex = 0;
            this.picBGame.TabStop = false;
            this.picBGame.Click += new System.EventHandler(this.picBGame_Click);
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImage = global::Sketchball.Properties.Resources.BackgroundScharf;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(532, 440);
            this.ControlBox = false;
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.picBEditor);
            this.Controls.Add(this.picBGame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "SelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectionForm";
            this.TransparencyKey = System.Drawing.Color.Gray;
            ((System.ComponentModel.ISupportInitialize)(this.CloseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBEditor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBGame)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.PictureBox picBGame;
        private System.Windows.Forms.PictureBox picBEditor;
        private System.Windows.Forms.PictureBox CloseButton;
    }
}