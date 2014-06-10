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
    /// <summary>
    /// Represents a tool used to draw circles.
    /// </summary>
    public class CircleTool : Tool
    {
        // State-keeping vars
        private Vector center;
        private double radius;
        private bool drawing = false;
        
        /// <summary>
        /// Instantiates a new circle tool.
        /// </summary>
        /// <param name="control">Control where this tool operates on.</param>
        public CircleTool(PinballEditControl control)
            : base(control)
        {
            Label = "Circle";
            Icon = Properties.Resources.circle_outline_512;
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            // -> Start drawing

            var pos = e.GetPosition(Editor);
            this.center = new Vector(pos.X, pos.Y);
            this.radius = 0;

            this.drawing = true;
            this.Editor.Invalidate();
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            // -> Stop drawing

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
            // -> Change radius if drawing
            if (this.drawing)
            {
                var position = e.GetPosition(Editor);
                this.radius = (new Vector(position.X, position.Y) - this.center).Length;
                this.Editor.Invalidate();
            }
        }

        protected override void Draw(object sender, DrawingContext g)
        {
            // -> Preview circle if drawing
            if (this.drawing)
            {
                g.DrawEllipse(null, new Pen(Brushes.Black,1), new Point(this.center.X, this.center.Y), this.radius, this.radius);
            }
        }
    }
}
