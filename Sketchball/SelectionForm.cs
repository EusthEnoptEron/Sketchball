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
   
    /// <summary>
    /// Entry form that gives the user some choices and acts as the parent window of all following windows.
    /// </summary>
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
            fDialog.InitialDirectory = new DirectoryInfo(Path.Combine(Application.ExecutablePath, "..", "Machines")).FullName;
            fDialog.CheckFileExists = true;
            fDialog.CheckPathExists = true;

            DialogResult result = fDialog.ShowDialog();

            if (result == DialogResult.OK) // Test result.
            {
                PinballMachine pbm = PinballMachine.FromFile(fDialog.FileName);
                
                if (pbm.IsValid())
                {
                    OpenGame(pbm, fDialog.FileName);
                }
                else
                {
                    MessageBox.Show("The pinball machine you provided is not valid: " + pbm.LastProblem.Message, "Invalid machine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private void picBEditor_Click(object sender, EventArgs e)
        {
            OpenEditor();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Closes the currently managed form.
        /// </summary>
        public void CloseManagedForm()
        {
            if (childForm != null)
            {
                childForm.FormClosed -= onChildClose;
                childForm.Close();
            }

            this.Show();
        }

        /// <summary>
        /// CLoses the currently managed form  and opens an editor.
        /// </summary>
        /// <param name="pbm"></param>
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


        /// <summary>
        /// Closes the currently managed form and opens a game.
        /// </summary>
        /// <param name="pbm"></param>
        /// <param name="fileName"></param>
        public void OpenGame(PinballMachine pbm, string fileName)
        {
            CloseManagedForm();

            this.Hide();

            childForm = new PlayForm(pbm, this);
            ((PlayForm)childForm).ActivateScoreTracking(fileName);

            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
            
            childForm.FormClosed += onChildClose;
        }

        private void onChildClose(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

    }
}
