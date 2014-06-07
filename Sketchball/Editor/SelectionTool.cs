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
    class SelectionTool : Tool
    {   
        enum SelectionState { Idle, Dragging, Resizing, Rotating }

        private Vector startVector;
        private const int MIN_LENGTH = 20;

        private Point origin;
        private Point startPoint;
        private Vector initialPoint;
        private double initialScale;
        private double initialRotation;


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
                        State = SelectionState.Resizing;

                        var pos = Editor.PointToEditor(SelectedElement.Location + SelectedElement.GetRotationOrigin());
                        origin = new Point(pos.X, pos.Y);


                        startPoint = e.GetPosition(Editor);
                        double factor = (startPoint - origin).Length / MIN_LENGTH;
                        if (factor < 1)
                        {
                            startPoint += (startPoint - origin) * (1 / factor);
                        }

                        currentPoint = new Point(startPoint.X, startPoint.Y);

                        initialPoint = SelectedElement.Location;
                        initialScale = SelectedElement.Scale;
                        initialRotation = SelectedElement.BaseRotation;
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
            else if (State == SelectionState.Resizing || State == SelectionState.Rotating)
            {
                var pos = e.GetPosition(Editor);
                currentPoint = new Point(pos.X, pos.Y);

                SelectedElement.Scale = ((currentPoint - origin).Length / (startPoint - origin).Length) * initialScale;

                var newLoc = Editor.PointToPinball(origin) - SelectedElement.GetRotationOrigin();
                SelectedElement.Location = new Vector(newLoc.X, newLoc.Y);
                SelectedElement.BaseRotation = initialRotation + Vector.AngleBetween(startPoint - origin, currentPoint - origin);

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
            if (State == SelectionState.Resizing && currentPoint != startPoint)
            {
                var translation = new TranslationChange(SelectedElement, SelectedElement.Location - initialPoint);
                var scale = new PropertyChange<PinballElement>(SelectedElement, "Scale", SelectedElement.Scale, initialScale);
                var rotation = new PropertyChange<PinballElement>(SelectedElement, "BaseRotation", SelectedElement.BaseRotation, initialRotation);
                Editor.History.Add(new CompoundChange(new IChange[] { translation, scale, rotation }));
            }

            State = SelectionState.Idle;

            Editor.Invalidate();
        }


        protected override void Draw(object sender, System.Windows.Media.DrawingContext g)
        {
            if (State == SelectionState.Resizing)
            {
                g.DrawLine(new Pen(Brushes.Gray, 1), origin, currentPoint);
            }
        }

        private void OnDelete(object sender, KeyEventArgs e)
        {
            if (State == SelectionState.Idle && e.Key == System.Windows.Input.Key.Delete)
            {
                if (SelectedElement != null)
                {
    
                    if (SelectedElement.GetType() == typeof(WormholeEntry))
                    {
                        WormholeEntry we = ((WormholeEntry)(SelectedElement));
                        if(we.WormholeExit!=null)
                        {
                            Editor.RemoveElement(we.WormholeExit);
                            if (Wormhole.WormholeExitPending == we.WormholeExit)
                            {
                                Wormhole.WormholeExitPending = null;
                            }
                        }

                        if (Wormhole.WormholeEntryPending == we)
                        {
                            Wormhole.WormholeEntryPending = null;
                        }
                    }

                    if (SelectedElement.GetType() == typeof(WormholeExit))
                    {
                        WormholeExit we = ((WormholeExit)(SelectedElement));
                        if (we.WormholeEntry != null)
                        {
                            Editor.RemoveElement(we.WormholeEntry);
                            if (Wormhole.WormholeEntryPending == we.WormholeEntry)
                            {
                                Wormhole.WormholeEntryPending = null;
                            }
                        }

                        if (Wormhole.WormholeExitPending == we)
                        {
                            Wormhole.WormholeExitPending = null;
                        }
                    }
                       
                    
                    Editor.RemoveElement(SelectedElement);
                }
            }
        }
             
    }
}
