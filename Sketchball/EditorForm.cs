using Sketchball.Controls;
using Sketchball.Editor;
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

        private Tool _currentTool = null;
        private Tool CurrentTool
        {
            get
            {
                return _currentTool;
            }
            set
            {
                _currentTool = value;
            }
        }

        private DragState dragState = new DragState();

        public EditorForm()
        {
            InitializeComponent();
            PlayFieldEditor.ScaleFactor *= 2f;


            TitleLabel.Font = new Font(FontManager.Courgette, 40);
            Font tabFont = new Font(FontManager.Courgette, 14);

        }

        public EditorForm(SelectionForm selectionForm) : this()
        {
         
            this.selectionForm = selectionForm;
        }

        public EditorForm(PinballMachine pbm, SelectionForm selectionForm) : this()
        {

            // TODO: Complete member initialization
            this.selectionForm = selectionForm;
            PlayFieldEditor.PinballMachine = pbm;
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
            populateElementPanel();
            PlayFieldEditor.Controls.Add(dragThumb);
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


        private void populateElementPanel()
        {
            Font font = new Font("Arial", 10, FontStyle.Regular);
            elementPanel.Controls.Add(new ElementControl(new Flipper(), "Flipper (left)", font));
            elementPanel.Controls.Add(new ElementControl(new Flipper() { Rotation = 0.1f }, "Flipper (right)", font));


            foreach (Control c in elementPanel.Controls)
            {
                c.MouseDown += StartDragAndDrop;
            }
        }

        private void StartDragAndDrop(object sender, MouseEventArgs e)
        {
            InitDragDrop((ElementControl)sender);
        }


        private void DragDrop(object sender, DragEventArgs e)
        {
            
            PlayFieldEditor.PinballMachine.Add(dragState.Element);
            PlayFieldEditor.Invalidate();
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            dragThumb.Visible = true;
        }

        private void DragLeave(object sender, EventArgs e)
        {
            dragThumb.Visible = false;
        }

        private void DragOver(object sender, DragEventArgs e)
        {
            dragThumb.Location = PlayFieldEditor.PointToClient(new Point(e.X + 1, e.Y + 1));

            var pinballPoint = PlayFieldEditor.PointToPinball(dragThumb.Location);
            dragState.Element.Location = new Vector2(pinballPoint.X, pinballPoint.Y);
            dragThumb.Visible = true;
        }

        private void OnQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (!dragState.Active)
            {
                // Don't allow drag'n'drop from outside
                e.Action = DragAction.Cancel;
            }
        }

        private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
        }

        private void InitDragDrop(ElementControl control)
        {
            dragState.Active = true;
            dragState.Element = control.GetInstance();

            dragThumb.Image = control.GetImage();

            PlayFieldEditor.DoDragDrop(new object(), DragDropEffects.All);
            
            dragThumb.Visible = false;


            dragState.Active = false;
            dragState.Element = null;
        }

        internal class DragState
        {
            internal bool Active = false;
            internal PinballElement Element = null;
        }
      
    }
}
