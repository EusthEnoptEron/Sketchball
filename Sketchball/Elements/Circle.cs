using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{
    class Circle: PinballElement
    {
        private int radius;
        private static readonly Size size = new Size(100, 100);

        public Circle() : base()
        {
        }

        public Circle(float x0, float y0, float radius) : base()
        {
            Width = (int)(2 * radius);
            Height = (int)(2 * radius);

            X = x0;
            Y = y0;
            this.radius = (int)radius;
        }

        protected override void Init()
        {
            //set up of bounding box
            BoundingCircle bc = new BoundingCircle(this.radius, new Vector2(0, 0));
            this.boundingContainer.AddBoundingBox(bc);
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            g.DrawEllipse(null, new Pen(Brushes.Red, 1), new Point(radius, radius), radius, radius);
        }


        protected override Size BaseSize
        {
            get { return size; }
        }

    }
}
