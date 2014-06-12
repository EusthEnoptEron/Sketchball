using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Control = System.Windows.Forms.Control;

namespace Sketchball.Editor
{

    /// <summary>
    /// The omnipotent tool for selecting elements.
    /// </summary>
    class SelectionTool : Tool
    {   
        enum SelectionState { Idle, Dragging, Shaping }

        private Vector startVector;
        private const int MIN_LENGTH = 20;

        private struct ShapeData
        {
            public Point origin;
            public Point startPoint;
            public Vector initialPoint;
            public double initialScale;
            public double initialRotation;

        }

        private ShapeData shapeData = new ShapeData();


        private SelectionState State = SelectionState.Idle;
        private Point currentPoint;


        private Vector delta;
       
        private TranslationChange posChange = null;
        private PinballElement SelectedElement
        {
            get
            {
                return Editor.SelectedElement;
            }
            set
            {
                Editor.SelectedElement = value;
            }
        }


        private PinballMachine Machine {
            get
            {
                return Editor.PinballMachine;
            }
        }

        protected override void OnSelect()
        {
            Editor.KeyDown += OnDelete;
        }


        protected override void OnUnselect()
        {
            Editor.KeyDown -= OnDelete;

            SelectedElement = null;
        }

        public SelectionTool(Sketchball.Controls.PinballEditControl Control)
            : base(Control)
        {
            this.Icon = Properties.Resources.Very_Basic_Cursor_icon;
            this.Label = "Select";
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            // Only act if we're idle
            if (State == SelectionState.Idle)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (SelectedElement != null && Control.ModifierKeys == System.Windows.Forms.Keys.Control)
                    {
                        State = SelectionState.Shaping;

                        var pos = Editor.PointToEditor(SelectedElement.Location + SelectedElement.GetRotationOrigin());
                        shapeData.origin = new Point(pos.X, pos.Y);


                        shapeData.startPoint = e.GetPosition(Editor);
                        double factor = (shapeData.startPoint - shapeData.origin).Length / MIN_LENGTH;
                        if (factor < 1)
                        {
                            shapeData.startPoint += (shapeData.startPoint - shapeData.origin) * (1 / factor);
                        }

                        currentPoint = new Point(shapeData.startPoint.X, shapeData.startPoint.Y);

                        shapeData.initialPoint = SelectedElement.Location;
                        shapeData.initialScale = SelectedElement.Scale;
                        shapeData.initialRotation = SelectedElement.BaseRotation;
                    }
                    else
                    {
                        State = SelectionState.Dragging;

                        var pos = e.GetPosition(Editor);
                        Point loc = Editor.PointToPinball(pos);

                        PinballElement element = FindElement(loc);
                        if (element != null)
                        {
                            // Select
                            SelectedElement = element;
                            this.delta = new Vector(pos.X, pos.Y) - Editor.PointToEditor(SelectedElement.Location);
                            startVector = SelectedElement.Location;
                        }
                        else
                        {
                            SelectedElement = null;
                        }
                    }
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
            if (SelectedElement == null) State = SelectionState.Idle;
            
            if (State == SelectionState.Dragging)
            {
                var pos = e.GetPosition(Editor);
                var newPos = Editor.PointToPinball(new Vector(pos.X, pos.Y) - delta);

                SelectedElement.Location = newPos;

                posChange = new TranslationChange(SelectedElement, newPos - startVector);
                
                Editor.Invalidate();
            }
            else if (State == SelectionState.Shaping)
            {
                var pos = e.GetPosition(Editor);
                currentPoint = new Point(pos.X, pos.Y);

                SelectedElement.Scale = ((currentPoint - shapeData.origin).Length / (shapeData.startPoint - shapeData.origin).Length) * shapeData.initialScale;

                var newLoc = Editor.PointToPinball(shapeData.origin) - SelectedElement.GetRotationOrigin();
                SelectedElement.Location = new Vector(newLoc.X, newLoc.Y);
                SelectedElement.BaseRotation = shapeData.initialRotation + Vector.AngleBetween(shapeData.startPoint - shapeData.origin, currentPoint - shapeData.origin);

                Editor.Invalidate();
            }
            
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {

            if (State == SelectionState.Dragging && posChange != null)
            {
                Editor.History.Add(posChange);
                posChange = null;
            }
            if (State == SelectionState.Shaping && currentPoint != shapeData.startPoint)
            {
                var translation = new TranslationChange(SelectedElement, SelectedElement.Location - shapeData.initialPoint);
                var scale = new PropertyChange(SelectedElement, "Scale", SelectedElement.Scale, shapeData.initialScale);
                var rotation = new PropertyChange(SelectedElement, "BaseRotation", SelectedElement.BaseRotation, shapeData.initialRotation);
                Editor.History.Add(new CompoundChange(new IChange[] { translation, scale, rotation }));
            }

            State = SelectionState.Idle;

            Editor.Invalidate();
        }


        protected override void Draw(object sender, System.Windows.Media.DrawingContext g)
        {
            if (State == SelectionState.Shaping)
            {
                g.DrawLine(new Pen(Brushes.Gray, 1), shapeData.origin, currentPoint);
                g.DrawEllipse(Brushes.CornflowerBlue, new Pen(Brushes.DarkBlue, 1), shapeData.origin, 2, 2);
                g.DrawEllipse(Brushes.CornflowerBlue, new Pen(Brushes.DarkBlue, 1), currentPoint, 5, 5);
            }
        }

        private void OnDelete(object sender, KeyEventArgs e)
        {
            if (State == SelectionState.Idle && e.Key == System.Windows.Input.Key.Delete)
            {
                if (SelectedElement != null)
                {
                    Editor.RemoveElement(SelectedElement);
                }
            }
        }
             
    }
}
