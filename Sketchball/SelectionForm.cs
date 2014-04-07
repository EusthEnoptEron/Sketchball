﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;


namespace Sketchball
{
   
    public partial class SelectionForm : Form
    {
        private PrivateFontCollection fontCollection = new PrivateFontCollection();

        public SelectionForm()
        {
            System.String path = (Path.Combine(Application.ExecutablePath, "..", "Resources", "Courgette-Regular.ttf"));
            this.fontCollection.AddFontFile(path);

            InitializeComponent();

            this.btnGameLabel.Font = new System.Drawing.Font(fontCollection.Families[0], 30);
            this.btnEditorLabel.Font = new System.Drawing.Font(fontCollection.Families[0], 30); 
        }

        void picBGame_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.picBGame.Image = global::Sketchball.Properties.Resources.btnDown;
        }

        private void picBGame_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.picBGame.Image = global::Sketchball.Properties.Resources.btnup;
            this.Visible = false;
        }


        private void picBEditor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.picBEditor.Image = global::Sketchball.Properties.Resources.btnDown;
        }

        private void picBEditor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.picBEditor.Image = global::Sketchball.Properties.Resources.btnup;
            this.Visible = false;
        }
        

    }
}
