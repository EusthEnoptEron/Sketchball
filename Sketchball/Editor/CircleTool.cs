using Sketchball.Controls;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Editor
{
    public class CircleTool : Tool
    {
        private Vector2 center;
        private float radius;
        private bool drawing = false;
        

        public CircleTool(PinballEditControl control)
            : base(control)
        {
            Label = "Circle";
            Icon = Properties.Resources.circle_outline_512;
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            this.center = new Vector2(e.X, e.Y);
            this.radius = 0;

            this.drawing = true;
            this.Control.Refresh();
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.radius = (new Vector2(e.X, e.Y) - this.center).Length();
            var center = Control.PointToPinball(this.center);

            //Create Circle
            Circle c = new Circle(center.X - this.radius, center.Y - this.radius, this.radius);
            this.Control.AddElement(c);

            this.drawing = false;
            this.Control.Refresh();
        }

        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.drawing)
            {
                this.radius = (new Vector2(e.X, e.Y) - this.center).Length();
                this.Control.Refresh();
            }
        }

        protected override void Draw(object sender, PaintEventArgs e)
        {
            if (this.drawing)
            {
                e.Graphics.DrawEllipse(System.Drawing.Pens.Black, this.center.X-this.radius, this.center.Y-this.radius, (int)(this.radius * 2), ((int)this.radius * 2));
            }
        }
    }
}
