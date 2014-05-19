using Sketchball.Collision;
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

        private static readonly Size size = new Size(200, 200);

        protected override Size BaseSize
        {
            get { return size; }
        }


        public TriangleTMP()
        {
           // turnaround();
        }

        protected override void Init()
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

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            g.DrawRectangle(Pens.Green, 0, Height / 10 * 9, Width, Height / 10 * 2);
        }
    }
}
