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
    /// <summary>
    /// Represents an arbitrary line typically drawn by the user.
    /// </summary>
    [DataContract]
    public class Line : CustomElement
    {
        [DataMember]
        Point p1;
        
        [DataMember]
        Point p2;


        public Line()
            : this(100, 0, 0, 0)
        {
        }

        private Size size;
        protected override Size BaseSize
        {
            get { return size; }
        }

        /// <summary>
        /// Creates a new line from (x0, y0) to (x1, y1)
        /// </summary>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        public Line(double x0, double y0, double x1, double y1)
            : base()
        {
            X = Math.Min(x0, x1);
            Y = Math.Min(y0, y1);

            p1 = new Point(x0 - X, y0 - Y);
            p2 = new Point(x1 - X, y1 - Y);

            RegenerateBounds();
        }

        protected override void Init()
        {
            base.Init();

            double x0 = p1.X;
            double x1 = p2.X;
            double y0 = p1.Y;
            double y1 = p2.Y;

            size = new Size((int)Math.Abs(x1 - x0), (int)Math.Abs(y1 - y0));

            //set up of bounding box
            BoundingLine bL = new BoundingLine(new Vector(x0, y0), new Vector(x1, y1));
            this.BoundingContainer.AddBoundingBox(bL);
        }

        protected override Geometry Geometry
        {
            get { return new LineGeometry(p1, p2); }
        }
    }
}
