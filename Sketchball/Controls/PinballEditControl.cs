using Sketchball.Editor;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Controls
{
    public class PinballEditControl : PinballControl
    {
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
                        PinballMachine.Remove(SelectedElement);
                        PinballMachine.Add(SelectedElement);
                    }
                    Invalidate();
                }
            }
        }

        public delegate void SelectionChangedHandler(PinballElement prevElement, PinballElement newElement);
        public event SelectionChangedHandler SelectionChanged;

        private float _scaleFactor = 1.0f;
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
            SuspendLayout();
            Width = (int)(PinballMachine.Width * ScaleFactor);
            Height = (int)(PinballMachine.Height * ScaleFactor);
            ResumeLayout();
            Invalidate();
        }

        public PinballMachine PinballMachine { get; private set; }

        public PinballEditControl()
            : base()
        {
            PinballMachine = new PinballMachine();

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
           // SetStyle(ControlStyles.UserPaint, true);

            SelectionPen = new Pen(Color.Black, 1);
            SelectionPen.DashStyle = DashStyle.Dash;
            
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
            change.Do();

            History.Add(change);
        }

        protected override void ConfigureGDI(Graphics g)
        {
            base.ConfigureGDI(g);
        }

        protected override void Draw(Graphics g)
        {
            //Brush brush = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.Gray, Color.LightGray);
            /*Brush brush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray, Color.DarkGray);
            g.FillRectangle(brush, 0, 0, base.Width, base.Height);
            */

            var state = g.Save();
            g.Transform = Transform;
            PinballMachine.Draw(g);
            g.Restore(state);

            // Draw selection
            // Border should always look the same, therefore we need to restore the gstate first and then use editor coordinates
            if (SelectedElement != null)
            {
                var bounds = SelectedElement.GetBounds();
                var origin = SelectedElement.GetRotationOrigin();


                bounds.Location = PointToEditor(bounds.Location);
                bounds.Width = (int)LengthToEditor(bounds.Width);
                bounds.Height = (int)LengthToEditor(bounds.Height);
                origin.X = LengthToEditor(origin.X);
                origin.Y = LengthToEditor(origin.Y);


                g.TranslateTransform(bounds.X + origin.X, bounds.Y + origin.Y);
                g.RotateTransform(SelectedElement.BaseRotation);
                g.TranslateTransform(-bounds.X - origin.X, -bounds.Y - origin.Y);

                g.DrawRectangle(SelectionPen, bounds);
            }

        }

        private Matrix Transform
        {
            get
            {
                Matrix m  = new Matrix();
                m.Translate(15, 15);
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
 
            m.TransformPoints(pArray);
            
            return pArray[0];
        }

        public Vector2 PointToPinball(Vector2 p)
        {
            var point = PointToPinball(new Point((int)p.X, (int)p.Y));
            return new Vector2(point.X, point.Y);
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
            m.TransformPoints(pArray);

            return pArray[0];
        }

        public Vector2 PointToEditor(Vector2 p)
        {
            var point = PointToEditor(new Point((int)p.X, (int)p.Y));
            return new Vector2(point.X, point.Y);
        }

        /// <summary>
        /// Takes a float from the pinball coordinate system and converts it into the editor coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public float LengthToEditor(float val)
        {
            return val * ScaleFactor;
        }

        /// <summary>
        /// Takes a float from the editor coordinate system and converts it into the pinball coordinate system.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public float LengthToPinball(float val) {
            return val / ScaleFactor;
        }

        public void LoadMachine(PinballMachine machine) {
            PinballMachine = machine;
            History.Clear();

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
    }
}
