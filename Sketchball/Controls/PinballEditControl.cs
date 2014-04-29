﻿using Sketchball.Elements;
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
        private History History = new History();
        public PinballElement SelectedElement { get; set; }

        private bool dragging = false;

        private Vector2 startVector;
        private Point startPoint;

        public Vector2 ScaleFactor = new Vector2(1,1);

        public PinballMachine PinballMachine;

        public PinballEditControl()
            : base()
        {
            PinballMachine = new PinballMachine();

            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
           // SetStyle(ControlStyles.UserPaint, true);

            //MouseDown += StartDrag;
            //MouseMove += DoDrag;
            //MouseUp += StopDrag;
        }

        void StopDrag(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging && e.Button == MouseButtons.Left)
            {

                Point loc = PointToPinball(e.Location);
                dragging = false;
                SelectedElement.setLocation(startVector + new Vector2(loc.X - startPoint.X, loc.Y - startPoint.Y));

                History.Add(new TranslationChange(SelectedElement, SelectedElement.getLocation() - startVector));
            }
        }

        //void DoDrag(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (dragging)
        //    {
        //        Point loc = PointToPinball(e.Location);
        //        SelectedElement.Location = (startVector + new Vector2(loc.X - startPoint.X, loc.Y - startPoint.Y));
        //        Invalidate();
        //    }
        //}

        /*
        void StartDrag(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point loc = PointToPinball(e.Location);

                PinballElement element = FindElement(loc);
                if (element != null)
                {
                    // Select
                    SelectedElement = element;
                    dragging = true;
                    startPoint = loc;
                    startVector = element.getLocation();
                }
            }            
        }

       */
        protected override void ConfigureGDI(Graphics g)
        {
            base.ConfigureGDI(g);
        }

        protected override void Draw(Graphics g)
        {
            //Brush brush = new HatchBrush(HatchStyle.WideDownwardDiagonal, Color.Gray, Color.LightGray);
            Brush brush = new HatchBrush(HatchStyle.DarkDownwardDiagonal, Color.Gray, Color.DarkGray);
            g.FillRectangle(brush, 0, 0, base.Width, base.Height);
            g.Transform = Transform;

            PinballMachine.Draw(g);
        }

        private Matrix Transform
        {
            get
            {
                Matrix m  = new Matrix();
                m.Translate(15, 15);
                m.Scale(ScaleFactor.X, ScaleFactor.Y);
                //m.Translate((Width / ScaleFactor.X - World.Width * ScaleFactor.X ) / 2, 15);

                return m;
            }
        }
        private Matrix Transform2
        {
            get
            {
                Matrix m = new Matrix();
                m.Scale(1/ScaleFactor.X, 1/ScaleFactor.Y);
                m.Translate(-15, -15);                
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
    }
}
