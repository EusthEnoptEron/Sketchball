﻿using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    class TriangleTMP: AnimatedObject
    {
        public TriangleTMP()
        {
            Width = 200;
            Height = 200;


            this.setLocation(new Vector2(0, 100));
            turnaround();
        }

        protected override void InitBounds()
        {
            //set up of bounding box
            BoundingLine bL1 = new BoundingLine(new Vector2(0, 200), new Vector2(100, 0));
            BoundingLine bL2 = new BoundingLine(new Vector2(100, 0), new Vector2(200, 200));
            BoundingLine bL3 = new BoundingLine(new Vector2(200, 200), new Vector2(0, 200));

            this.boundingContainer.addBoundingBox(bL1);
            this.boundingContainer.addBoundingBox(bL2);
            this.boundingContainer.addBoundingBox(bL3);

            bL1.assigneToContainer(this.boundingContainer);
            bL2.assigneToContainer(this.boundingContainer);
            bL3.assigneToContainer(this.boundingContainer);
            turnaround();

            
        }

        private void turnaround()
        {
            Vector2 drawCenter = new Vector2(100, 0);
          
            this.rotate(((float)(360 / 180f * Math.PI)), drawCenter, 5f, new Action(turnaround));
        }


        public override void Update(long delta)
        {
            base.Update(delta);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            Point p1 = new Point(0, 200);
            Point p2 = new Point(100, 0);
            Point p3 = new Point(200, 200);
            base.Draw(g);
            g.DrawLine(Pens.Red, p1, p2);
            g.DrawLine(Pens.Red, p2, p3);
            g.DrawLine(Pens.Red, p3, p1);
        }
    }
}
