﻿using Sketchball.Controls;
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
                var pos = e.GetPosition(Control);
                this.startPos = new Vector(pos.X, pos.Y);
                this.actualPos = new Vector(pos.X, pos.Y);
                this.drawing = true;
                this.Control.Invalidate();
            }
        }

        protected override void OnSelect()
        {
            Control.PreviewMouseDoubleClick += OnMouseDoubleClick;
        }

        protected override void OnUnselect()
        {
            Control.PreviewMouseDoubleClick -= OnMouseDoubleClick;
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {

            var pos = e.GetPosition(Control);
            this.actualPos = new Vector(pos.X, pos.Y);

            var end = Control.PointToPinball(actualPos);
            var start = Control.PointToPinball(startPos);

            //Create Line
            if (start.X != end.X && start.Y != end.Y)
            {
                Line l = new Line(start.X, start.Y, end.X, end.Y);
                this.Control.AddElement(l);
            }

            this.startPos = actualPos;
            this.Control.Invalidate();
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
                var pos = e.GetPosition(Control);
                this.actualPos.X = pos.X;
                this.actualPos.Y = pos.Y;

                this.Control.Invalidate();
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
