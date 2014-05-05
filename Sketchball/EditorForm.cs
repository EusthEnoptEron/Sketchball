﻿using Sketchball.Controls;
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

            //PlayFieldEditor.ScaleFactor *= 1f;

            TitleLabel.Font = new Font(FontManager.Courgette, 40);

            populateElementPanel();
            populateToolPanel();
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

        private void populateToolPanel()
        {
            // List of available tools
            Tool[] tools = new Tool[] { 
                new SelectionTool(PlayFieldEditor),
                new LineTool(PlayFieldEditor), new CircleTool(PlayFieldEditor) };


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
