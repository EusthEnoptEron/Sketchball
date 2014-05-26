using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{

    [DataContract]
    public class Line : PinballElement
    {
        [DataMember]
        Point p1, p2;


        public Line()
            : this(100, 0, 0, 0)
        {
        }

        private Size size;
        protected override Size BaseSize
        {
            get { return size; }
        }

        public Line(double x0, double y0, double x1, double y1)
            : base()
        {
            X = Math.Min(x0, x1);
            Y = Math.Min(y0, y1);

            p1 = new Point(x0 - X, y0 - Y);
            p2 = new Point(x1 - X, y1 - Y);
        }

        protected override void Init()
        {
            float x0 = (float)p1.X;
            float x1 = (float)p2.X;
            float y0 = (float)p1.Y;
            float y1 = (float)p2.Y;

            size = new Size((int)Math.Abs(x1 - x0), (int)Math.Abs(y1 - y0));

            //set up of bounding box
            BoundingLine bL = new BoundingLine(new Vector(x0, y0), new Vector(x1, y1));
            this.boundingContainer.AddBoundingBox(bL);
        }


        protected override void OnDraw(DrawingContext g)
        {
            g.DrawLine(new Pen(Brushes.Black, 1), new Point(p1.X, p1.Y), new Point(p2.X, p2.Y));

        }
    }
}
