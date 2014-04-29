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

        private bool dragging = false;
        private Point startPoint;
        private Vector2 startVector;


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
                Point loc = Control.PointToPinball(e.Location);

                PinballElement element = FindElement(loc);
                if (element != null)
                {
                    // Select
                    SelectedElement = element;
                    dragging = true;
                    startPoint = loc;
                    startVector = element.getLocation();
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




        protected override void Draw(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            Rectangle boundingRect = new Rectangle();
           
        }

       

    }
}
