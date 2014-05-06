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
using Sketchball.Elements;


namespace Sketchball
{
   
    public partial class SelectionForm : Form
    {
        private PrivateFontCollection fontCollection = new PrivateFontCollection();
        private Form childForm = null;

        public SelectionForm()
        {
            InitializeComponent();

        }

        void picBGame_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
          //  this.picBGame.Image = global::Sketchball.Properties.Resources.btnDown;
        }

        private void picBGame_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
           
        }


        private void picBEditor_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           // this.picBEditor.Image = global::Sketchball.Properties.Resources.btnDown;
        }

        private void picBEditor_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
           // this.picBEditor.Image = global::Sketchball.Properties.Resources.btnup;
            
        }

        private void picBGame_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select Pinball machine";
            fDialog.Filter = "Pinball machine files|*.pmf";
            fDialog.CheckFileExists = true;
            fDialog.CheckPathExists = true;

            //DialogResult result = fDialog.ShowDialog();

            //if (result == DialogResult.OK) // Test result.
            //{
            //    //TODO
            //    MessageBox.Show("machine load not implemented\n I want a cookie!");
                PinballControl2 b = new PinballControl2();
                Sketchball.Elements.PinballMachine pbm = b.getMachine();

                OpenGame(new PinballMachine());
            //}
            
        }

        private void picBEditor_Click(object sender, EventArgs e)
        {
            OpenEditor();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void CloseManagedForm()
        {
            if (childForm != null)
            {
                childForm.FormClosed -= onChildClose;
                childForm.Close();
            }

            this.Show();
        }

        public void OpenEditor(PinballMachine pbm = null)
        {
            CloseManagedForm();
            Hide();

            if (pbm == null)
                childForm = new EditorForm(this);
            else
                childForm = new EditorForm(pbm, this);
            
            childForm.Show();
            childForm.FormClosed += onChildClose;
        }

        public void OpenGame(PinballMachine pbm)
        {
            CloseManagedForm();

            this.Hide();

            childForm = new PlayForm(pbm, this);
            childForm.Show();
            childForm.FormClosed += onChildClose;
        }

        private void onChildClose(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

    }
}
