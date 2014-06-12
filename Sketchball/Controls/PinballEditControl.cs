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
    /// <summary>
    /// Is to the editor what GameView is to the game. Contains a pinball machine and
    ///     1.) Draws it
    ///     2.) Provides a history and an API to make modifications to the PBM.
    /// </summary>
    public class PinballEditControl : PinballControl
    {
        // Padding from the left and top border
        private const int PADDING = 15;

        // Pen used to draw selections
        private Pen selectionPen;


        #region Events
        public delegate void SelectionChangedHandler(PinballElement prevElement, PinballElement newElement);

        /// <summary>
        /// Occurs when the selection changes.
        /// </summary>
        public event SelectionChangedHandler SelectionChanged;

        /// <summary>
        /// Occurs when the control is drawn.
        /// </summary>
        public event EventHandler<DrawingContext> Paint;

        #endregion


        #region Properties
        /// <summary>
        /// Gets the history of changes.
        /// </summary>
        public History History { get; private set; }

        /// <summary>
        /// Gets or sets the currently selected element. null = no element selected
        /// </summary>
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
        private PinballElement _selectedElement = null;

       
        /// <summary>
        /// Gets or sets the current scale factor.
        /// </summary>
        public float ScaleFactor
        {
            get
            {
                return _scaleFactor;
            }
            set
            {
                if (value != 0)
                {
                    _scaleFactor = value;
                    UpdateSize();
                }
            }
        }
        private float _scaleFactor = 1.0f;


       /// <summary>
       /// Gets the pinball machine currently in the making. To start anew, use <see cref="LoadMachine(PinballMachine machine)"/>.
       /// </summary>
        public PinballMachine PinballMachine { get; private set; }

        #endregion

        public PinballEditControl()
            : base()
        {
            PinballMachine = new PinballMachine();
            History = new Sketchball.History();

            SetValue(RenderOptions.BitmapScalingModeProperty, BitmapScalingMode.HighQuality);
            //Effect = new DropShadowEffect();

            selectionPen = new Pen(Brushes.Black, 1);
            selectionPen.DashStyle = DashStyles.Dash;
            //SelectionPen.DashStyle = DashStyle.Dash;
           
            UpdateSize();

            History.Change += () => { Invalidate(); };
        }


        // Keeps the size equal to the machine.
        private void UpdateSize()
        {
            Width = (int)(PinballMachine.Width * ScaleFactor) + PADDING;
            Height = (int)(PinballMachine.Height * ScaleFactor) + PADDING;
        }


        /// <summary>
        /// Shortcut for InvalidateVisual() to maintain the naming scheme of GDI+. 
        /// </summary>
        public void Invalidate()
        {
            InvalidateVisual();
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
            // This is done by the machine already, so we can comment this out now.
            // // g.DrawRectangle(Brushes.White, null, new Rect(0, 0, Width, Height));


            g.PushTransform(new MatrixTransform(Transform));
            PinballMachine.Draw(g);
            g.Pop();

            // Draw selection
            // Border should always look the same (same thickness), therefore we have to use editor coordinates for everything
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
                    g.DrawRectangle(null, selectionPen, new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height));
                }
                g.Pop();

                // If we're dealing with a wormhole, let's draw the connections between the elements.
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
                        g.DrawLine(selectionPen, exitPoint, new Point(bounds.X + bounds.Width / 2, bounds.Y + bounds.Height / 2));
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
                
                // First scale, then translate
                m.Scale(ScaleFactor, ScaleFactor);
                m.Translate(PADDING, PADDING);

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

        /// <summary>
        /// Computes the location of the specified client point into pinball coordinates. 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Computes the location of the specified pinball point into editor coordinates. 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Vector PointToEditor(Vector p)
        {
            var point = PointToEditor(new Point(p.X, p.Y));
            return new Vector(point.X, point.Y);
        }

        /// <summary>
        /// Takes a double from the pinball coordinate system and converts it into the editor coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public double LengthToEditor(double val)
        {
            return val * ScaleFactor;
        }

        /// <summary>
        /// LEGACY FUNCTION: Takes a float from the pinball coordinate system and converts it into the editor coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public float LengthToEditor(float val)
        {
            return (float)LengthToEditor((double)val);
        }

        /// <summary>
        /// Takes a double from the editor coordinate system and converts it into the pinball coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public double LengthToPinball(double val)
        {
            return val / ScaleFactor;
        }

        /// <summary>
        /// LEGACY FUNCTION: Takes a double from the editor coordinate system and converts it into the pinball coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public float LengthToPinball(float val)
        {
            return (float)LengthToPinball((double)val);
        }

        /// <summary>
        /// Loads a fresh machine into the editor.
        /// </summary>
        /// <param name="machine">Machine to be loaded.</param>
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
