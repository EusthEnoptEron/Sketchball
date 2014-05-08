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
        public PinballElement SelectedElement { get; set; }

        public float ScaleFactor = 1.0f;

        public PinballMachine PinballMachine { get; private set; }

        public PinballEditControl()
            : base()
        {
            PinballMachine = new PinballMachine();

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
           // SetStyle(ControlStyles.UserPaint, true);


            History.Change += History_Change;
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

        void History_Change()
        {
            Invalidate();
        }

        protected override void ConfigureGDI(Graphics g)
        {
            base.ConfigureGDI(g);
        }

        protected override void Draw(Graphics g)
        {
            //Brush brush = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.Gray, Color.LightGray);
            Brush brush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray, Color.DarkGray);
            g.FillRectangle(brush, 0, 0, base.Width, base.Height);

            var state = g.Save();
            g.Transform = Transform;

            PinballMachine.Draw(g);
            g.Restore(state);
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
    }
}
