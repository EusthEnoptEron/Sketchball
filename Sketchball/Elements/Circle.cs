using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.boundingContainer.addBoundingBox(bc);
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            g.TranslateTransform(-X, -Y);

            g.DrawEllipse(Pens.Red, (int)this.Location.X, (int)this.Location.Y, (int)(this.radius * 2), ((int)this.radius * 2));

            g.TranslateTransform(X, Y);
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            g.DrawEllipse(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Red, 1), new System.Windows.Point(radius, radius), radius, radius);

        }

        protected override Size BaseSize
        {
            get { return size; }
        }

    }
}
