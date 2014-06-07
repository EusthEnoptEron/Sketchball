using Sketchball.Editor;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Sketchball.Controls
{
    public class PinballEditControl : PinballControl
    {
        private const int PADDING = 15;

        public readonly History History = new History();
        private Pen SelectionPen;

        private PinballElement _selectedElement = null;
        public PinballElement SelectedElement { 
            get {
                return _selectedElement;
            }
            set {
                var prevElement = _selectedElement;
                _selectedElement = value;

                if (prevElement != _selectedElement)
                {
                    RaiseSelectionChanged(prevElement);

                    if (SelectedElement != null)
                    {
                        // Re-Add the element to bring it to front
                        PinballMachine.BringToFront(SelectedElement);
                    }
                    Invalidate();
                }
            }
        }

        public void Invalidate()
        {
            InvalidateVisual();
        }

        public delegate void SelectionChangedHandler(PinballElement prevElement, PinballElement newElement);
        public event SelectionChangedHandler SelectionChanged;

        private float _scaleFactor = 1.0f;

        public event EventHandler<DrawingContext> Paint;

        public float ScaleFactor
        {
            get
            {
                return _scaleFactor;
            }
            set
            {
                _scaleFactor = value;
                UpdateSize();
            }
        }

        private void UpdateSize()
        {
            Width = (int)(PinballMachine.Width * ScaleFactor) + PADDING;
            Height = (int)(PinballMachine.Height * ScaleFactor) + PADDING;

          //  Invalidate();
        }

        public PinballMachine PinballMachine { get; private set; }


        public PinballEditControl()
            : base()
        {
            PinballMachine = new PinballMachine();

            SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
            //Effect = new DropShadowEffect();

            SelectionPen = new Pen(Brushes.Black, 1);
            SelectionPen.DashStyle = DashStyles.Dash;
            //SelectionPen.DashStyle = DashStyle.Dash;
           
            UpdateSize();

            History.Change += () => { Invalidate(); };
        }

        /// <summary>
        /// Adds a new element to the pinball machine AND keeps track of it.
        /// </summary>
        /// <param name="element"></param>
        public void AddElement(PinballElement element)
        {
            IChange change = new CreationChange(PinballMachine.DynamicElements, element);
            change.Do();

            History.Add(change);
        }


        /// <summary>
        /// Removes an element from the pinball machine AND keeps track of it.
        /// </summary>
        /// <param name="element"></param>
        public void RemoveElement(PinballElement element)
        {
            IChange change = new DeletionChange(PinballMachine.DynamicElements, element);

            if (element is WormholeExit)
            {
                List<IChange> changes = new List<IChange>();
                changes.Add(change);

                var exit = element as WormholeExit;
                foreach (var entry in exit.Entries)
                {
                    changes.Add(new DeletionChange(PinballMachine.DynamicElements, entry));
                }

                IChange mainChange = new CompoundChange(changes);
                mainChange.Do();
                History.Add(mainChange);
            }
            else
            {
                change.Do();

                History.Add(change);
            }
        }

        protected override void Draw(DrawingContext g)
        {
            // Clear rectangle (needed, because otherwise it will not react to click events)
           // g.DrawRectangle(Brushes.White, null, new Rect(0, 0, Width, Height));


            g.PushTransform(new MatrixTransform(Transform));
            PinballMachine.Draw(g);
            g.Pop();

            // Draw selection
            // Border should always look the same, therefore we use editor coordinates for everything
            if (SelectedElement != null && SelectedElement.World != null)
            {
                var bounds = SelectedElement.GetBounds();
                var origin = SelectedElement.GetRotationOrigin();

                var point = new Point(bounds.Location.X, bounds.Location.Y);
                var pRes = PointToEditor(point);
                bounds.Location = new Point(pRes.X, pRes.Y);
                bounds.Width = LengthToEditor(bounds.Width);
                bounds.Height = LengthToEditor(bounds.Height);
                origin.X = LengthToEditor(origin.X);
                origin.Y = LengthToEditor(origin.Y);


                g.PushTransform(new RotateTransform(SelectedElement.BaseRotation, bounds.X + origin.X, bounds.Y + origin.Y));
                {
                    g.DrawRectangle(null, SelectionPen, new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height));
                }
                g.Pop();

                if (SelectedElement is Wormhole)
                {
                    List<Wormhole> targets = new List<Wormhole>();

                    if (SelectedElement is WormholeEntry)
                    {
                        var entry = SelectedElement as WormholeEntry;
                        if (entry.WormholeExit != null) targets.Add(entry.WormholeExit);
                    }
                    else if (SelectedElement is WormholeExit)
                    {
                        var exit = SelectedElement as WormholeExit;
                        targets.AddRange(exit.Entries);
                    }

                    foreach (var wormhole in targets)
                    {
                        var exitVector = PointToEditor(wormhole.Location) + new Vector(wormhole.Width, wormhole.Height) * ScaleFactor * 0.5;
                        var exitPoint = new Point(exitVector.X, exitVector.Y);
                        g.DrawLine(SelectionPen, exitPoint, new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2));
                    }
                }
            }

            if (Paint != null)
                Paint(this, g);

        }

        private Matrix Transform
        {
            get
            {
                Matrix m  = new Matrix();
                m.Translate(PADDING, PADDING);
                m.Scale(ScaleFactor, ScaleFactor);
                //m.Translate((Width / ScaleFactor.X - World.Width * ScaleFactor.X ) / 2, 15);

                return m;
            }
        }

        /// <summary>
        /// Computes the location of the specified client point into pinball coordinates. 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Point PointToPinball(Point p)
        {
            Point[] pArray = new Point[] { p };

            Matrix m = Transform;
            m.Invert();
 
            m.Transform(pArray);
            
            return pArray[0];
        }

        public Vector PointToPinball(Vector p)
        {
            var point = PointToPinball(new Point((int)p.X, (int)p.Y));
            return new Vector(point.X, point.Y);
        }

        /// <summary>
        /// Computes the location of the specified pinball point into editor coordinates. 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Point PointToEditor(Point p)
        {
            Point[] pArray = new Point[] { p };
            Matrix m = Transform;
            m.Transform(pArray);

            return pArray[0];
        }

        public Vector PointToEditor(Vector p)
        {
            var point = PointToEditor(new Point(p.X, p.Y));
            return new Vector(point.X, point.Y);
        }

        /// <summary>
        /// Takes a float from the pinball coordinate system and converts it into the editor coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public double LengthToEditor(double val)
        {
            return val * ScaleFactor;
        }
        
        public float LengthToEditor(float val)
        {
            return (float)LengthToEditor((double)val);
        }

        /// <summary>
        /// Takes a float from the editor coordinate system and converts it into the pinball coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public double LengthToPinball(double val)
        {
            return val / ScaleFactor;
        }

        public float LengthToPinball(float val)
        {
            return (float)LengthToPinball((double)val);
        }

        public void LoadMachine(PinballMachine machine) {
            PinballMachine = machine;
            History.Clear();
            SelectedElement = null;

            Invalidate();
        }

        private void RaiseSelectionChanged(PinballElement prev)
        {
            var listeners = SelectionChanged;
            if (listeners != null)
            {
                listeners(prev, SelectedElement);
            }
        }

        public System.Drawing.Point PointToPinball(System.Drawing.Point point)
        {
            var p = PointToPinball(new Point(point.X, point.Y));
            return new System.Drawing.Point((int)p.X, (int)p.Y);
        }
    }
}
