
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
            this.picBGame = new System.Windows.Forms.PictureBox();
            this.picBEditor = new System.Windows.Forms.PictureBox();
            this.btnGameLabel = new System.Windows.Forms.Label();
            this.btnEditorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBEditor)).BeginInit();
            this.SuspendLayout();
            // 
            // picBGame
            // 
            this.picBGame.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBGame.Image = global::Sketchball.Properties.Resources.btnup;
            this.picBGame.Location = new System.Drawing.Point(12, 12);
            this.picBGame.Name = "picBGame";
            this.picBGame.Size = new System.Drawing.Size(260, 97);
            this.picBGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBGame.TabIndex = 0;
            this.picBGame.TabStop = false;
            this.picBGame.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBGame_MouseDown);
            this.picBGame.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBGame_MouseUp);
            // 
            // picBEditor
            // 
            this.picBEditor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBEditor.Image = global::Sketchball.Properties.Resources.btnup;
            this.picBEditor.Location = new System.Drawing.Point(12, 134);
            this.picBEditor.Name = "picBEditor";
            this.picBEditor.Size = new System.Drawing.Size(260, 97);
            this.picBEditor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBEditor.TabIndex = 1;
            this.picBEditor.TabStop = false;
            this.picBEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBEditor_MouseDown);
            this.picBEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBEditor_MouseUp);
            // 
            // btnGameLabel
            // 
            this.btnGameLabel.AutoSize = true;
            this.btnGameLabel.BackColor = System.Drawing.Color.White;
            this.btnGameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGameLabel.Location = new System.Drawing.Point(44, 36);
            this.btnGameLabel.Name = "btnGameLabel";
            this.btnGameLabel.Size = new System.Drawing.Size(190, 46);
            this.btnGameLabel.TabIndex = 2;
            this.btnGameLabel.Text = "Lets play!";
            // 
            // btnEditorLabel
            // 
            this.btnEditorLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEditorLabel.AutoSize = true;
            this.btnEditorLabel.BackColor = System.Drawing.Color.White;
            this.btnEditorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditorLabel.Location = new System.Drawing.Point(70, 160);
            this.btnEditorLabel.Name = "btnEditorLabel";
            this.btnEditorLabel.Size = new System.Drawing.Size(125, 46);
            this.btnEditorLabel.TabIndex = 3;
            this.btnEditorLabel.Text = "Editor";
            this.btnEditorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 243);
            this.Controls.Add(this.btnEditorLabel);
            this.Controls.Add(this.btnGameLabel);
            this.Controls.Add(this.picBEditor);
            this.Controls.Add(this.picBGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "SelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectionForm";
            ((System.ComponentModel.ISupportInitialize)(this.picBGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBEditor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.PictureBox picBGame;
        private System.Windows.Forms.PictureBox picBEditor;
        private System.Windows.Forms.Label btnGameLabel;
        private System.Windows.Forms.Label btnEditorLabel;
    }
}