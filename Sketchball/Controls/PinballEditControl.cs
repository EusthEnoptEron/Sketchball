using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Controls
{
    class PinballEditControl : PinballControl
    {
        private History History = new History();
        public PinballElement SelectedElement { get; set; }

        private bool dragging = false;
        private Change translation;

        private Vector2 startVector;
        private Point startPoint;

        public PinballEditControl()
            : base()
        {
            // Optimize control for performance
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
           // SetStyle(ControlStyles.UserPaint, true);

            World.Add(new Flipper() { X = World.Width / 2, Y = Height / 2 } );

            MouseDown += StartDrag;
            MouseMove += DoDrag;
            MouseUp += StopDrag;
        }

        void StopDrag(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging && e.Button == MouseButtons.Left)
            {
                dragging = false;
                SelectedElement.Location = startVector + new Vector2(e.X - startPoint.X, e.Y - startPoint.Y);

                History.Add(new TranslationChange(SelectedElement, SelectedElement.Location - startVector));
            }
        }

        void DoDrag(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (dragging)
            {
                SelectedElement.Location = startVector + new Vector2(e.X - startPoint.X, e.Y - startPoint.Y);
                Invalidate();
            }
        }

        void StartDrag(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PinballElement element = FindElement(e.Location);
                if (element != null)
                {
                    // Select
                    SelectedElement = element;
                    dragging = true;
                    startPoint = e.Location;
                    startVector = element.Location;
                }
            }            
        }

        private PinballElement FindElement(Point location)
        {
            foreach (PinballElement element in World)
            {
                if (element.Contains(location))
                {
                    return element;
                }
            }

            return null;
        }

    }
}
