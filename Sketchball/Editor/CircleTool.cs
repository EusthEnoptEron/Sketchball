using Sketchball.Controls;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sketchball.Editor
{
    public class CircleTool : Tool
    {
        private Vector center;
        private double radius;
        private bool drawing = false;
        

        public CircleTool(PinballEditControl control)
            : base(control)
        {
            Label = "Circle";
            Icon = Properties.Resources.circle_outline_512;
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(Editor);
            this.center = new Vector(pos.X, pos.Y);
            this.radius = 0;

            this.drawing = true;
            this.Editor.Invalidate();
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            var position = e.GetPosition(Editor);

            this.radius = Editor.LengthToPinball((new Vector(position.X, position.Y) - this.center).Length);
            var center = Editor.PointToPinball(this.center);
            

            //Create Circle
            Circle c = new Circle(center.X - this.radius,  center.Y - this.radius, this.radius);
            this.Editor.AddElement(c);

            this.drawing = false;
            this.Editor.Invalidate();
        }

        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.drawing)
            {
                var position = e.GetPosition(Editor);
                this.radius = (new Vector(position.X, position.Y) - this.center).Length;
                this.Editor.Invalidate();
            }
        }

        protected override void Draw(object sender, DrawingContext g)
        {
            if (this.drawing)
            {
                g.DrawEllipse(null, new Pen(Brushes.Black,1), new Point(this.center.X, this.center.Y), this.radius, this.radius);
            }
        }
    }
}
