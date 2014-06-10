using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sketchball.Collision;
using Sketchball.Elements;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace Sketchball.Editor
{

    /// <summary>
    /// Represents a tool that can draw multiple lines, i.e. a shammed polygon.
    /// </summary>
    public class MultiLineTool : Tool
    {
        private Vector startPos;
        private Vector actualPos;
        private bool drawing = false;


        public MultiLineTool(PinballEditControl control)
            : base(control)
        {
            Icon = Properties.Resources.MultiLineTool;
            Label = "Multi Line tool";
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (drawing == false)
            {
                var pos = e.GetPosition(Editor);
                this.startPos = new Vector(pos.X, pos.Y);
                this.actualPos = new Vector(pos.X, pos.Y);
                this.drawing = true;
                this.Editor.Invalidate();
            }
        }

        protected override void OnSelect()
        {
            Editor.PreviewMouseDoubleClick += OnMouseDoubleClick;
        }

        protected override void OnUnselect()
        {
            Editor.PreviewMouseDoubleClick -= OnMouseDoubleClick;
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {

            var pos = e.GetPosition(Editor);
            this.actualPos = new Vector(pos.X, pos.Y);

            var end = Editor.PointToPinball(actualPos);
            var start = Editor.PointToPinball(startPos);

            //Create Line
            if (start.X != end.X && start.Y != end.Y)
            {
                Line l = new Line(start.X, start.Y, end.X, end.Y);
                this.Editor.AddElement(l);
            }

            this.startPos = actualPos;
            this.Editor.Invalidate();
        }

        protected void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
           // OnMouseUp(sender, e);
            this.drawing = false;

            // Set as handled, otherwise MouseDown will be triggered.
            e.Handled = true;
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
