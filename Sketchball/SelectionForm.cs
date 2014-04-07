using System;
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
using Sketchball.Controls;


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

            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select Pinball machine";
            fDialog.Filter = "Pinball machine files|*.pmf";
            fDialog.CheckFileExists = true;
            fDialog.CheckPathExists = true;

            DialogResult result = fDialog.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                //TODO
                MessageBox.Show("machine load not implemented\n I want a cookie!");
                PinballControl2 b = new PinballControl2();
               
                Sketchball.Elements.PinballMachine pbm = b.getMachine();
                Form f = new PlayForm(pbm,this);                
                f.ShowDialog();
            }
            else
            {
                this.Visible = true;
            }
           
        }


        private void picBEditor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.picBEditor.Image = global::Sketchball.Properties.Resources.btnDown;
        }

        private void picBEditor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.picBEditor.Image = global::Sketchball.Properties.Resources.btnup;
            this.Visible = false;

            Form f = new EditorForm(this); 
            f.ShowDialog();
        }
        

    }
}
