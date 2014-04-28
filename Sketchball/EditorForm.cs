using Sketchball.Controls;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    public partial class EditorForm : Form
    {

        private SelectionForm selectionForm;
        private ToolTip tt = new ToolTip();
        public EditorForm()
        {
            InitializeComponent();
            PlayFieldEditor.ScaleFactor *= 1.1f;


            TitleLabel.Font = new Font(FontManager.Courgette, 40);
            Font tabFont = new Font(FontManager.Courgette, 14);
            
       
        }

        public EditorForm(SelectionForm selectionForm) : this()
        {
            this.selectionForm = selectionForm;
        }

        void element_MouseDown(object sender, MouseEventArgs e)
        {
            ElementControl element = (ElementControl)sender;
            element.DoDragDrop("Test", DragDropEffects.Copy | DragDropEffects.Move);
        }

        private void playgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            PinballControl2 pinball = new PinballControl2();
               
            f.Controls.Add(pinball);
            pinball.Dock = DockStyle.Fill;
            f.Width = 500;
            f.Height = 500;
            f.ShowDialog();
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
           
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void EditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.selectionForm.Visible = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SplitToNameAndTools_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            
            tt.SetToolTip(this.LineTool, "Line Tool");
        }

        private void CircleTool_MouseHover(object sender, EventArgs e)
        {
            tt.SetToolTip(this.CircleTool, "Circle Tool");
        }

        private void SelectionTool_MouseHover(object sender, EventArgs e)
        {
            tt.SetToolTip(this.SelectionTool, "Selection");
        }

        private void undoTool_MouseHover(object sender, EventArgs e)
        {
            tt.SetToolTip(this.undoTool, "Undo");
        }

        private void RedoTool_MouseHover(object sender, EventArgs e)
        {
            tt.SetToolTip(this.RedoTool, "Redo");
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        
      
    }
}
