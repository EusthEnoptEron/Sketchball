using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Sketchball.Collision;
using Sketchball.Elements;

namespace Sketchball.Editor
{
    public class LineTool : Tool
    {
        private Vector2 startPos;
        private Vector2 actualPos;
        private bool drawing = false;
        

        public LineTool(PinballEditControl control)
            : base(control)
        {
            Icon = Properties.Resources.LineTool;
            Label = "Line tool";
            
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.startPos = new Vector2(e.X, e.Y);
            this.actualPos = new Vector2(e.X, e.Y);
            this.drawing = true;
            this.Control.Refresh();
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.actualPos = new Vector2(e.X, e.Y);

            //Create Line
            var start = Control.PointToPinball(startPos);
            var end = Control.PointToPinball(actualPos);
            Line l = new Line(start.X, start.Y, end.X, end.Y);
            this.Control.PinballMachine.DynamicElements.Add(l);

            this.drawing = false;
            this.Control.Refresh();
        }

        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.drawing)
            {
                this.actualPos.X = e.X;
                this.actualPos.Y = e.Y;

                this.Control.Refresh();
            }
        }

        protected override void Draw(object sender, PaintEventArgs e)
        {
            if (this.drawing)
            {
                e.Graphics.DrawLine(System.Drawing.Pens.Black, startPos.X, startPos.Y, actualPos.X, actualPos.Y);
            }
        }
    }
}
