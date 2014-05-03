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

        public Circle() : base()
        {
            Width = 100;
            Height = 100;

            this.setLocation(new Vector2(0, 0));


            //set up of bounding box
            BoundingLine bL = new BoundingLine(new Vector2(0, 0), new Vector2(this.Width, this.Height));
            this.boundingContainer.addBoundingBox(bL);

        }

        public Circle(float x0, float y0, float radius)
        {
            X = x0;
            Y = y0;
            Width = (int)(2 * radius);
            Height = (int)(2 * radius);
            this.radius = (int)radius;

            //set up of bounding box
            BoundingCircle bc = new BoundingCircle(this.radius, new Vector2(0,0));
            this.boundingContainer.addBoundingBox(bc);

        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.TranslateTransform(-X, -Y);

            g.DrawEllipse(Pens.Red, (int)this.Location.X, (int)this.Location.Y, (int)(this.radius * 2), ((int)this.radius * 2));

            g.TranslateTransform(X, Y);
        }
    }
}
