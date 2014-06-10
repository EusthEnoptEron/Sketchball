using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sketchball.Collision;
using Sketchball.Elements;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows;

namespace Sketchball.Editor
{

    /// <summary>
    /// Represents a tool that can draw line segments.
    /// </summary>
    public class LineTool : Tool
    {
        private Vector startPos;
        private Vector actualPos;
        private bool drawing = false;
        

        public LineTool(PinballEditControl control)
            : base(control)
        {
            Icon = Properties.Resources.LineTool;
            Label = "Line tool";
            
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {

            var pos = e.GetPosition(Editor);
            this.startPos = new Vector(pos.X, pos.Y);
            this.actualPos = new Vector(pos.X, pos.Y);
            this.drawing = true;
            this.Editor.Invalidate();
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(Editor);
            this.actualPos = new Vector(pos.X, pos.Y);

            //Create Line
            var start = Editor.PointToPinball(startPos);
            var end = Editor.PointToPinball(actualPos);
            Line l = new Line(start.X, start.Y, end.X, end.Y);
            this.Editor.AddElement(l);

            this.drawing = false;
            this.Editor.Invalidate();
        }

        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.drawing)
            {
                var pos = e.GetPosition(Editor);
                this.actualPos.X = pos.X;
                this.actualPos.Y = pos.Y;

                this.Editor.Invalidate();
            }
        }

        protected override void Draw(object sender, DrawingContext g)
        {
            if (this.drawing)
            {
                g.DrawLine(new Pen(Brushes.Black, 1), new Point(startPos.X, startPos.Y), new Point(actualPos.X, actualPos.Y));
            }
        }
    }
}
