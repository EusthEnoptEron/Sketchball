using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Editor
{
    class SelectionTool : Tool
    {   
        private Point startPoint;
        private Vector2 startVector;
        private bool mouseIsDown = false;
        private Vector2 delta;
       
        private TranslationChange posChange = null;
        private PinballElement SelectedElement
        {
            get
            {
                return Control.SelectedElement;
            }
            set
            {
                Control.SelectedElement = value;
            }
        }

        private PropertyGrid propertyGrid;

        private PinballMachine Machine {
            get
            {
                return Control.PinballMachine;
            }
        }

        public SelectionTool(Sketchball.Controls.PinballEditControl Control)
            : base(Control)
        {
            this.Icon = Properties.Resources.Very_Basic_Cursor_icon;
            this.Label = "Select";
        }


        protected override void OnSelect()
        {
            Control.Controls.Add(propertyGrid);
        }

        protected override void OnUnselect()
        {
            Control.Controls.Remove(propertyGrid);
        }

        protected override void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = true;
                Point loc = Control.PointToPinball(e.Location);

                PinballElement element = FindElement(loc);
                if (element != null)
                {
                    // Select
                    SelectedElement = element;
                    this.delta = new Vector2(e.X, e.Y) - Control.PointToEditor(SelectedElement.Location);
                    startVector = SelectedElement.Location;
                }
                else
                {
                    SelectedElement = null;
                }
            }
        }

        private PinballElement FindElement(Point location)
        {
            // Elements with a higher index are drawn *above* those with a lower one
            // -> we need to iterate downward
            for (int i = Machine.DynamicElements.Count - 1; i >= 0; --i)
            {
                var element = Machine.DynamicElements[i];
                if (element.Contains(location))
                {
                    return element;
                }
            }

            return null;
        }

        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            
            if (mouseIsDown && SelectedElement != null)
            {
                var newPos = Control.PointToPinball(new Vector2(e.X, e.Y) - delta);

                SelectedElement.Location = newPos;

                posChange = new TranslationChange(SelectedElement, newPos - startVector);
                
                Control.Invalidate(); 
            }
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (posChange != null)
            {
                Control.History.Add(posChange);
                posChange = null;
            }

            mouseIsDown = false;
            delta = Vector2.Zero;


        }

    }
}
