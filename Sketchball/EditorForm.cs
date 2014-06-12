using Sketchball.Controls;
using Sketchball.Editor;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using Vector = System.Windows.Vector;

namespace Sketchball
{

    /// <summary>
    /// The form that contains all editing functionality.
    /// </summary>
    public partial class EditorForm : Form
    {
        internal class DragState
        {
            internal bool Active = false;
            internal PinballElement Element = null;
        }

        private PinballEditControl PlayFieldEditor;
        private WPFContainer EditorContainer;

        private SelectionForm selectionForm;
        private ToolTip tt = new ToolTip();

        private Tool _currentTool = null;
        private string _fileName;

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

            PlayFieldEditor = new PinballEditControl();
            EditorContainer = new WPFContainer(PlayFieldEditor);
            playFieldPanel.Controls.Add(EditorContainer);

            TitleLabel.Font = new Font(FontManager.Courgette, 40);

            // Set up menu
            editToolStripMenuItem.DropDownItems.Add(new UndoItem(PlayFieldEditor.History));
            editToolStripMenuItem.DropDownItems.Add(new RedoItem(PlayFieldEditor.History));

            // Set up left panel
            populateElementPanel();
            populateToolPanel();

            // Set up playfield and element inspector
            PlayFieldEditor.History.Change += () => { elementInspector.Refresh(); machineInspector.Refresh(); };
            elementInspector.PropertyValueChanged += onElementPropertyChanged;
            machineInspector.PropertyValueChanged += onMachinePropertyChanged;
            machineInspector.SelectedObject = PlayFieldEditor.PinballMachine;

            // Set up zoom bar
            zoomBar.Trackbar.Minimum = 5;
            zoomBar.Trackbar.Maximum = 20;
            zoomBar.Trackbar.Value = 10;
            zoomBar.Trackbar.ValueChanged += (sender, e) => { PlayFieldEditor.ScaleFactor = zoomBar.Trackbar.Value / 10f; };

            PlayFieldEditor.MouseWheel += onMouseWheel;

            FileName = null;

            // PlayFieldEditor
            //EditorContainer.AllowDrop = true;
            PlayFieldEditor.AllowDrop = true;
            EditorContainer.Location = new System.Drawing.Point(3, 3);
            EditorContainer.TabIndex = 2;
         

            PlayFieldEditor.SelectionChanged += new Sketchball.Controls.PinballEditControl.SelectionChangedHandler(this.onSelectionChanged);

            PlayFieldEditor.Drop += this.onDragDrop;
            PlayFieldEditor.DragEnter += this.onDragEnter;
            PlayFieldEditor.DragOver += this.onDragOver;
            PlayFieldEditor.DragLeave += this.onDragLeave;
            PlayFieldEditor.QueryContinueDrag += this.onQueryContinueDrag;

            
            //PlayFieldEditor.Background = System.Windows.Media.Brushes.White;
        }

        public EditorForm(SelectionForm selectionForm)
            : this()
        {
            this.selectionForm = selectionForm;
        }

        public EditorForm(PinballMachine pbm, SelectionForm selectionForm)
            : this()
        {
            this.selectionForm = selectionForm;
            loadMachine(pbm);
        }


        private void populateToolPanel()
        {
            // List of available tools
            Tool[] tools = new Tool[] { 
                new SelectionTool(PlayFieldEditor),
                new LineTool(PlayFieldEditor), 
                new MultiLineTool(PlayFieldEditor), 
                new CircleTool(PlayFieldEditor)
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
            //elementPanel.Controls.Add(new ElementControl(new TriangleTMP(), "TriDebgu", font));

            foreach (Control c in elementPanel.Controls)
            {
                c.MouseDown += onStartDragAndDrop;
            }

        }


        #region Event Handlers

        // Adds changes on the machine to the history.
        private void onMachinePropertyChanged(object s, PropertyValueChangedEventArgs e)
        {
            IChange change = new PropertyChange(PlayFieldEditor.PinballMachine, e.ChangedItem.PropertyDescriptor.Name, e.ChangedItem.Value, e.OldValue);
            PlayFieldEditor.History.Add(change);
        }

        // Adds changes on the selected element to the history.
        private void onElementPropertyChanged(object s, PropertyValueChangedEventArgs e)
        {
            PlayFieldEditor.Invalidate();

            IChange change = new PropertyChange((PinballElement)elementInspector.SelectedObject, e.ChangedItem.PropertyDescriptor.Name, e.ChangedItem.Value, e.OldValue);
            PlayFieldEditor.History.Add(change);

        }

        // Scrolls the playfield editor in the proper direction
        void onMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            e.Handled = true;
            //((HandledMouseEventArgs)e).Handled = true;

            // 15 = font size
            int numberOfPixelsToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120 * 15;

            if (Control.ModifierKeys == Keys.Control)
            {
                int newValue = zoomBar.Trackbar.Value + (e.Delta > 0 ? 1 : -1);
                newValue = Math.Max(zoomBar.Trackbar.Minimum, Math.Min(zoomBar.Trackbar.Maximum, newValue));
                zoomBar.Trackbar.Value = newValue;
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
                int newScroll = playFieldPanel.HorizontalScroll.Value - numberOfPixelsToMove;
                //newScroll = Math.Max(playFieldPanel.HorizontalScroll.Minimum, Math.Min(playFieldPanel.HorizontalScroll.Maximum, newScroll));
                playFieldPanel.AutoScrollPosition = new Point(newScroll, playFieldPanel.VerticalScroll.Value);
            }
            else
            {
                int newScroll = playFieldPanel.VerticalScroll.Value - numberOfPixelsToMove;
                //newScroll = Math.Max(playFieldPanel.VerticalScroll.Minimum, Math.Min(playFieldPanel.VerticalScroll.Maximum, newScroll));
                playFieldPanel.AutoScrollPosition = new Point(playFieldPanel.HorizontalScroll.Value, newScroll);
            }
        }


 
        void onCreatorElementChosen(object sender, MouseEventArgs e)
        {
            ElementControl element = (ElementControl)sender;
            element.DoDragDrop("", DragDropEffects.Copy | DragDropEffects.Move);
        }


        private void onStartDragAndDrop(object sender, MouseEventArgs e)
        {
            initDragDrop((ElementControl)sender);
        }


        private void onDragDrop(object sender, System.Windows.DragEventArgs e)
        {
            PlayFieldEditor.PinballMachine.Remove(dragState.Element);
            PlayFieldEditor.AddElement(dragState.Element);
        }

        private void onDragEnter(object sender, System.Windows.DragEventArgs e)
        {
            e.Effects = System.Windows.DragDropEffects.Move;

            PlayFieldEditor.PinballMachine.Add(dragState.Element);
        }

        private void onDragLeave(object sender, EventArgs e)
        {
            PlayFieldEditor.PinballMachine.Remove(dragState.Element);
        }

        private void onDragOver(object sender, System.Windows.DragEventArgs e)
        {
            var pos = e.GetPosition(PlayFieldEditor);
            var pinballPoint = PlayFieldEditor.PointToPinball(pos);
            dragState.Element.Location = new Vector((pinballPoint.X - dragState.Element.Width / 2),
                                                     (pinballPoint.Y - dragState.Element.Height / 2));
            PlayFieldEditor.Invalidate();
        }

        private void onQueryContinueDrag(object sender, System.Windows.QueryContinueDragEventArgs e)
        {
            if (!dragState.Active)
            {
                // Don't allow drag'n'drop from outside
                e.Action = System.Windows.DragAction.Cancel;
            }
        }

        private void initDragDrop(ElementControl control)
        {
            dragState.Active = true;
            dragState.Element = control.GetInstance();

            EditorContainer.DoDragDrop(new object(), DragDropEffects.All);
            
            dragState.Active = false;
            dragState.Element = null;
        }


        // Opens a machine if valid.
        private void onOpenMachine(object sender, EventArgs e)
        {
            if (mayOmitChanges())
            {
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    PinballMachine pbm = PinballMachine.FromFile(openFileDialog.FileName);

                    if (pbm.IsValid())
                    {
                        loadMachine(pbm);
                        FileName = openFileDialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show("The pinball machine you provided is not valid: " + pbm.LastProblem.Message, "Invalid machine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Loads a machine
        private void loadMachine(PinballMachine pbm)
        {
            PlayFieldEditor.LoadMachine(pbm);
            machineInspector.SelectedObject = pbm;
        }

      
        // Shows the problem of the last IsValid() operation. Could also visualize the problem on the playfield in a 2nd step
        private void showProblem()
        {
            var problem = PlayFieldEditor.PinballMachine.LastProblem;
            if (problem != null)
            {
                MessageBox.Show("The pinball machine you provided is not valid. " + PlayFieldEditor.PinballMachine.LastProblem.Message,
                                "Invalid machine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Saves the machine.
        private void onSaveMachine(object sender, EventArgs e)
        {
            if (PlayFieldEditor.PinballMachine.IsValid())
            {

                if (FileName == null)
                {
                    var result = saveFileDialog.ShowDialog();
                    if (result != DialogResult.OK) return;
                    else FileName = saveFileDialog.FileName;
                }

                saveMachine(FileName);
            }
            else showProblem();
        }

        // Saves the machine with a new name.
        private void onSaveAsMachine(object sender, EventArgs e)
        {
            if (PlayFieldEditor.PinballMachine.IsValid())
            {
                if (FileName != null)
                {
                    FileInfo info = new FileInfo(FileName);
                    saveFileDialog.InitialDirectory = info.DirectoryName;
                    saveFileDialog.FileName = info.Name;
                }
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    saveMachine(saveFileDialog.FileName);
                }
            }
            else showProblem();
        }

        // Saves the machine to a file.
        private void saveMachine(string filename)
        {
            FileName = filename;
            PlayFieldEditor.PinballMachine.Save(FileName);
            PlayFieldEditor.History.ClearStatus();
        }


        // Creates a new machine.
        private void onNewMachine(object sender, EventArgs e)
        {
            if (mayOmitChanges())
            {
                loadMachine(new PinballMachine());
                FileName = null;
            }

        }

        // Checks whether it's okay to omit changes if there are any.
        private bool mayOmitChanges()
        {
            if (PlayFieldEditor.History.HasChanged())
            {
                var result = MessageBox.Show("There are unsaved changes, do you want to continue?", "Unsaved changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                return result == DialogResult.Yes;
            }
            else
            {
                return true;
            }
        }

        // Start the game if valid.
        private void onPlayClicked(object sender, EventArgs e)
        {
            if (PlayFieldEditor.PinballMachine.IsValid())
            {
                using (var form = new PlayForm(PlayFieldEditor.PinballMachine))
                {
                    form.WindowState = FormWindowState.Maximized;
                    form.ShowDialog();
                }
            }
            else showProblem();
        }

        // Makes sure that the elementInspector always has the right element.
        private void onSelectionChanged(PinballElement prevElement, PinballElement newElement)
        {
            if (newElement != null)
            {
                elementInspector.SelectedObject = newElement;
            }
            else
            {
                elementInspector.SelectedObject = null;
            }
        }

        // Makes sure that the title font is correctly sized.
        private void onLeftPanelResize(object sender, EventArgs e)
        {
            SizeF extent;
            var space = MainSplitToMenueAndField.Panel1.Size;
            space.Width -= TitleLabel.Padding.Left * 2;

            TitleLabel.Font = AppropriateFont(8, 40, space, TitleLabel.Text, TitleLabel.Font, out extent);
        }

        #endregion


        /// <summary>
        /// Finds an appropriate font size for a given size. Taken from:
        /// http://tech.pro/tutorial/691/csharp-tutorial-font-scaling
        /// </summary>
        /// <param name="minFontSize"></param>
        /// <param name="maxFontSize"></param>
        /// <param name="layoutSize"></param>
        /// <param name="s"></param>
        /// <param name="f"></param>
        /// <param name="extent"></param>
        /// <returns></returns>
        private static Font AppropriateFont(float minFontSize,
                                    float maxFontSize, Size layoutSize, string s, Font f, out SizeF extent)
        {
            if (maxFontSize == minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);

            extent = TextRenderer.MeasureText(s, f);

            if (maxFontSize <= minFontSize)
                return f;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = f.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;

            f = new Font(f.FontFamily, newSize, f.Style);
            extent = TextRenderer.MeasureText(s, f);

            return f;
        }

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

            PlayFieldEditor.Dispose();
            base.Dispose(disposing);
        }


    }

}
