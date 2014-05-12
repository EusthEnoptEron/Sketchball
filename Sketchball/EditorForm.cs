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
        private string _fileName;
       
        private WormholeEntry lastWormholeEntry = null;
        /// <summary>
        /// Gets or sets the current filename of the pinball machine file.
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                var name = _fileName;
                if (name == null) name = "Untitled machine";
                
                Text = name + " - Pinball Machine Editor";
            }
        }

        private Tool CurrentTool
        {
            get
            {
                return _currentTool;
            }
            set
            {
                if (_currentTool != null)
                {
                    _currentTool.Leave();
                    Tools[_currentTool].Checked = false;
                }

                _currentTool = value;

                _currentTool.Enter();
                Tools[_currentTool].Checked = true;
            }
        }

        private Dictionary<Tool, ToolStripButton> Tools = new Dictionary<Tool, ToolStripButton>();

        private DragState dragState = new DragState();

        public EditorForm()
        {
            InitializeComponent();

            TitleLabel.Font = new Font(FontManager.Courgette, 40);

            // Set up menu
            editToolStripMenuItem.DropDownItems.Add(new UndoItem(PlayFieldEditor.History));
            editToolStripMenuItem.DropDownItems.Add(new RedoItem(PlayFieldEditor.History));

            // Set up left panel
            populateElementPanel();
            populateToolPanel();

            // Set up playfield and element inspector
            PlayFieldEditor.History.Change += () => { elementInspector.Refresh(); };
            elementInspector.PropertyValueChanged += (sender, e) => { PlayFieldEditor.Refresh(); };
            fieldAndPropertySplitter.Panel2Collapsed = true;

            // Set up zoom bar
            zoomBar.Trackbar.Minimum = 5;
            zoomBar.Trackbar.Maximum = 20;
            zoomBar.Trackbar.Value = 10;
            zoomBar.Trackbar.ValueChanged += (sender, e) => { PlayFieldEditor.ScaleFactor = zoomBar.Trackbar.Value / 10f; };
            
            FileName = null;
        }

 

        public EditorForm(SelectionForm selectionForm) : this()
        {
            this.selectionForm = selectionForm;
        }

        public EditorForm(PinballMachine pbm, SelectionForm selectionForm) : this()
        {

            // TODO: Complete member initialization
            this.selectionForm = selectionForm;
            PlayFieldEditor.LoadMachine(pbm);
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

        private void populateToolPanel()
        {
            // List of available tools
            Tool[] tools = new Tool[] { 
                new SelectionTool(PlayFieldEditor),
                new LineTool(PlayFieldEditor), new MultiLineTool(PlayFieldEditor), new CircleTool(PlayFieldEditor)
            };


            // Initiate all tools and connect them with a button
            foreach (var tool in tools)
            {
                ToolStripButton button = new ToolStripButton(tool.Icon);
                button.ToolTipText = tool.Label;
                button.Click += (s, e) => { CurrentTool = tool; };

                toolBar.Items.Add(button);
                Tools.Add(tool, button);
            }

            if (tools.Length > 0)
            {
                CurrentTool = tools[0];
            }

            // Add undo / redo buttons
            toolBar.Items.Add(new RedoButton(PlayFieldEditor.History) { Alignment = ToolStripItemAlignment.Right });
            toolBar.Items.Add(new UndoButton(PlayFieldEditor.History) { Alignment = ToolStripItemAlignment.Right });
        }

        private void populateElementPanel()
        {
            Font font = new Font("Arial", 10, FontStyle.Regular);
            elementPanel.Controls.Add(new ElementControl(new LeftFlipper(), "Flipper (left)", font));
            elementPanel.Controls.Add(new ElementControl(new RightFlipper(), "Flipper (right)", font));     //new RightFlipper() { Rotation = 0.1f }
            elementPanel.Controls.Add(new ElementControl(new SlingshotLeft(), "Slingshot (left)", font));
            elementPanel.Controls.Add(new ElementControl(new SlingshotRight(), "Slingshot (right)", font));
            elementPanel.Controls.Add(new ElementControl(new Hole(), "Hole", font));
            elementPanel.Controls.Add(new ElementControl(new Bumper(), "Bumper", font));
            elementPanel.Controls.Add(new ElementControl(new WormholeEntry(), "Wormhole (entry)", font));
            elementPanel.Controls.Add(new ElementControl(new WormholeExit(), "Wormhole (exit)", font));

            foreach (Control c in elementPanel.Controls)
            {
                c.MouseDown += StartDragAndDrop;
            }

            PlayFieldEditor.Controls.Add(dragThumb);
        }

        private void EditorForm_Load(object sender, EventArgs e)
        {
          
        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }



        private void StartDragAndDrop(object sender, MouseEventArgs e)
        {
            InitDragDrop((ElementControl)sender);
        }


        private void OnDragDrop(object sender, DragEventArgs e)
        {
            PlayFieldEditor.AddElement(dragState.Element);
            if( dragState.Element.GetType() == typeof(WormholeEntry))
            {
                lastWormholeEntry = (WormholeEntry)dragState.Element;
            }

            if (dragState.Element.GetType() == typeof(WormholeExit))
            {
                if (lastWormholeEntry != null)
                {
                    lastWormholeEntry.WormholeExit = (WormholeExit)dragState.Element;
                }
                lastWormholeEntry = null;
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            dragThumb.Visible = true;
        }

        private void OnDragLeave(object sender, EventArgs e)
        {
            dragThumb.Visible = false;
        }

        private void OnDragOver(object sender, DragEventArgs e)
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

        private void openPBMButton_Click(object sender, EventArgs e)
        {
            if (MayOmitChanges())
            {
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PinballMachine pbm = PinballMachine.FromFile(openFileDialog.FileName);

                    if (pbm.IsValid())
                    {
                        PlayFieldEditor.LoadMachine(pbm);
                        FileName = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show("The pinball machine you provided is not valid.", "Invalid machine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void savePBMButton_Click(object sender, EventArgs e)
        {
            if (PlayFieldEditor.PinballMachine.IsValid())
            {

                if (FileName == null)
                {
                    var result = saveFileDialog.ShowDialog();
                    if (result != DialogResult.OK) return;
                    else FileName = saveFileDialog.FileName;
                }

                PlayFieldEditor.PinballMachine.Save(FileName);
            }
            else
            {
                MessageBox.Show("The pinball machine you provided is not valid.", "Invalid machine", MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }
        }

        private void newPBMButton_Click(object sender, EventArgs e)
        {
            if (MayOmitChanges())
            {
                PlayFieldEditor.LoadMachine(new PinballMachine());
                FileName = null;
            }

        }

        private bool MayOmitChanges()
        {
            if (PlayFieldEditor.History.HasChanged())
            {
                var result = MessageBox.Show("There are unsaved changes, do you want to continue?", "Unsaved changes!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return result == DialogResult.OK;
            }
            else
            {
                return true;
            }
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (PlayFieldEditor.PinballMachine.IsValid())
            {
                var form = new PlayForm(PlayFieldEditor.PinballMachine);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("The pinball machine you provided is not valid.", "Invalid machine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PlayFieldEditor_SelectionChanged(PinballElement prevElement, PinballElement newElement)
        {
            if (newElement != null)
            {
                elementInspector.SelectedObject = newElement;
                fieldAndPropertySplitter.Panel2Collapsed = false;
            }
            else
            {
                elementInspector.SelectedObject = null;
                fieldAndPropertySplitter.Panel2Collapsed = true;
            }
        }
      
    }

}
