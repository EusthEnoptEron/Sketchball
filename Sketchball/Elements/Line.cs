﻿using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Line : PinballElement
    {
        PointF p1, p2;


        public Line()
            : base()
        {
            Width = 100;
            Height = 0;


            this.setLocation(new Vector2(100, 200));


            //set up of bounding box
            BoundingLine bL = new BoundingLine(new Vector2(0, 0), new Vector2(this.Width, this.Height));
            this.boundingContainer.addBoundingBox(bL);

        }
        public Line(float x0, float y0, float x1, float y1) : base()
        {
            X = Math.Min(x0, x1);
            Y = Math.Min(y0, y1);
            Width = (int)Math.Abs(x1 - x0);
            Height = (int)Math.Abs(y1 - y0);


            p1 = new PointF(x0, y0);
            p2 = new PointF(x1, y1);

            //set up of bounding box
            BoundingLine bL = new BoundingLine(new Vector2(x0 - X,y0 - Y),  new Vector2(x1 - X, y1 - Y));
            this.boundingContainer.addBoundingBox(bL);

        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.TranslateTransform(-X, -Y);

            g.DrawLine(Pens.Red, p1, p2);

            g.TranslateTransform(X, Y);
        }
    }
}