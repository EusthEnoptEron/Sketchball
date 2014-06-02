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
            BoundingLine bL1 = new BoundingLine(new Vector(0, 200), new Vector(100, 0));
            BoundingLine bL2 = new BoundingLine(new Vector(100, 0), new Vector(200, 200));
            BoundingLine bL3 = new BoundingLine(new Vector(200, 200), new Vector(0, 200));

            this.BoundingContainer.AddBoundingBox(bL1);
            this.BoundingContainer.AddBoundingBox(bL2);
            this.BoundingContainer.AddBoundingBox(bL3);

            bL1.AssignToContainer(this.BoundingContainer);
            bL2.AssignToContainer(this.BoundingContainer);
            bL3.AssignToContainer(this.BoundingContainer);
            turnaround();

            
        }

        private void turnaround()
        {
            Vector drawCenter = new Vector(100, 0);
          
            this.Rotate(((360 / 180f * Math.PI)), drawCenter, 5f, new Action(turnaround));
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
