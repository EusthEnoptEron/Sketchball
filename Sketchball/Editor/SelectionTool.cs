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

        public PinballElement SelectedElement { get; set; }

       
        private Point startPoint;
        private Vector2 startVector;
        private bool mouseIsDown = false;
        private Vector2 delta;
       
        private TranslationChange posChange = null;


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
                    this.delta = new Vector2(e.X - SelectedElement.X, e.Y - SelectedElement.Y);
                    startVector = SelectedElement.Location;
                   
                    Control.Invalidate();
                }
                else
                {
                    SelectedElement = null;
                    Control.Invalidate();
                }
            }
        }

        private PinballElement FindElement(Point location)
        {
            foreach (PinballElement element in Machine.DynamicElements)
            {
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
                var newPos = new Vector2(e.X, e.Y) - delta;

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

        protected override void Draw(object sender, PaintEventArgs e)
        {
            if(SelectedElement != null) {
                Pen pen = new Pen(Color.Black, 1);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                pen.DashPattern = new float[] { 5f, 4f };
                
                var origin = Control.PointToEditor(new Point((int)SelectedElement.Location.X, (int)SelectedElement.Location.Y));
                e.Graphics.DrawRectangle(pen, origin.X, origin.Y, SelectedElement.Width, SelectedElement.Height);
            }
        }

       

    }
}
