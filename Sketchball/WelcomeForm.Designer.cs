﻿namespace Sketchball
{
    partial class WelcomeForm
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
            this.startGameButton = new System.Windows.Forms.Button();
            this.startEditorButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(12, 12);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(132, 23);
            this.startGameButton.TabIndex = 0;
            this.startGameButton.Text = "Start a Game";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // startEditorButton
            // 
            this.startEditorButton.Location = new System.Drawing.Point(12, 41);
            this.startEditorButton.Name = "startEditorButton";
            this.startEditorButton.Size = new System.Drawing.Size(132, 23);
            this.startEditorButton.TabIndex = 1;
            this.startEditorButton.Text = "Open Editor";
            this.startEditorButton.UseVisualStyleBackColor = true;
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 277);
            this.Controls.Add(this.startEditorButton);
            this.Controls.Add(this.startGameButton);
            this.Name = "WelcomeForm";
            this.Text = "WelcomeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Button startEditorButton;
    }
}