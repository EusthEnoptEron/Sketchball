﻿using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Bumper : PinballElement
    {
        public Bumper():base()
        {
            Width = 30;
            Height = 30;
            
        
            this.setLocation(new Vector2(0, 100));
 
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            /* g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, Pens.Red);
            });
            g.TranslateTransform(X, Y);*/
            g.DrawImage(Properties.Resources.Bumper, 0, 0, Width, Height);
        }

        protected override void InitBounds()
        {
            BoundingCircle bC = new BoundingCircle(15, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
        }
    }
}
