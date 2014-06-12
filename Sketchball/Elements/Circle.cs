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
    /// Represents an arbitrary circle usually drawn by the user.
    /// </summary>
    [DataContract]
    class Circle : CustomElement
    {
        [DataMember]
        private int radius;
        private static readonly Size size = new Size(100, 100);

        public Circle() : base()
        {
        }

        public Circle(double x0, double y0, double radius)
            : base()
        {
            X = x0;
            Y = y0;
            this.radius = (int)radius;

            Color = System.Drawing.Color.Black;
        }

        protected override void Init()
        {
            base.Init();

            //set up of bounding box
            BoundingCircle bc = new BoundingCircle(this.radius, new Vector(0, 0));
            this.BoundingContainer.AddBoundingBox(bc);
        }

        protected override Geometry Geometry
        {
            get { return new EllipseGeometry(new Point(radius, radius), radius, radius); }
        }



        protected override Size BaseSize
        {
            get { return new Size(radius*2, radius*2); }
        }

    }
}
