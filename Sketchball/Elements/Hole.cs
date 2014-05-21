﻿using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Hole : PinballElement
    {
        private static Image image = Booster.OptimizeImage(Properties.Resources.hole, 50, 50);
        public Hole()
            : base()
        {
            Width = 50;
            Height = 50;

            this.setLocation(new Vector2(100, 100));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, System.Drawing.Pens.Red);
            });
            g.TranslateTransform(X, Y);
            g.DrawImage(image, 0, 0, Width, Height);
        }

        protected override void InitBounds()
        {
            BoundingCircle bC = new BoundingCircle(25, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
        }
    }
}
