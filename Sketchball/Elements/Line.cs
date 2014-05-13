using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{

    [DataContract]
    public class Line : PinballElement
    {
        [DataMember]
        PointF p1, p2;


        public Line()
            : this(0, 0, 100, 0)
        {
        }

        public Line(float x0, float y0, float x1, float y1) : base()
        {
            X = Math.Min(x0, x1);
            Y = Math.Min(y0, y1);
            Width = (int)Math.Abs(x1 - x0);
            Height = (int)Math.Abs(y1 - y0);


            p1 = new PointF(x0 - X, y0 - Y);
            p2 = new PointF(x1 - X, y1 - Y);
        }

        protected override void InitBounds()
        {
            var x0 = p1.X;
            var x1 = p2.X;
            var y0 = p1.Y;
            var y1 = p2.Y;

            //set up of bounding box
            BoundingLine bL = new BoundingLine(new Vector2(x0, y0), new Vector2(x1, y1));
            this.boundingContainer.addBoundingBox(bL);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawLine(Pens.Red, p1, p2);
        }
    }
}
