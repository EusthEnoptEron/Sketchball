using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            turnaround();
        }

        protected override void Init()
        {
            //set up of bounding box
            BoundingLine bL1 = new BoundingLine(new Vector2(0, 200), new Vector2(100, 0));
            BoundingLine bL2 = new BoundingLine(new Vector2(100, 0), new Vector2(200, 200));
            BoundingLine bL3 = new BoundingLine(new Vector2(200, 200), new Vector2(0, 200));

            this.boundingContainer.AddBoundingBox(bL1);
            this.boundingContainer.AddBoundingBox(bL2);
            this.boundingContainer.AddBoundingBox(bL3);

            bL1.AssignToContainer(this.boundingContainer);
            bL2.AssignToContainer(this.boundingContainer);
            bL3.AssignToContainer(this.boundingContainer);
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

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            base.OnDraw(g);
        }
    }
}
