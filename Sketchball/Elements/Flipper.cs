using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Sketchball.Collision;

namespace Sketchball.Elements
{

    class Flipper : AnimatedObject
    {
        
        public Flipper()  : base()
        {
            Width = 50;
            Height = 50;
            GoUp();
            // 0, Height / 10 * 9, Width , Height / 10 * 2 )
            int y1 = Height / 10 * 9;
            int recHeight =  Height / 10 * 2 ;

            this.setLocation(new Vector2(0, 0));

            //set up of bounding box
            BoundingLine bL1 = new BoundingLine(new Vector2(0, y1), new Vector2(Width, y1));
            BoundingLine bL2 = new BoundingLine(new Vector2(Width, y1), new Vector2(Width, y1+recHeight));
            BoundingLine bL3 = new BoundingLine(new Vector2(Width, y1 + recHeight), new Vector2(0, y1 + recHeight));
            BoundingLine bL4 = new BoundingLine(new Vector2(0, y1 + recHeight), new Vector2(0, y1));

            this.boundingContainer.addBoundingBox(bL1);
            this.boundingContainer.addBoundingBox(bL2);
            this.boundingContainer.addBoundingBox(bL3);
            this.boundingContainer.addBoundingBox(bL4);

            bL1.assigneToContainer(this.boundingContainer);
            bL2.assigneToContainer(this.boundingContainer);
            bL3.assigneToContainer(this.boundingContainer);
            bL4.assigneToContainer(this.boundingContainer);

        }

        public void GoUp()
        {
            Vector2 drawCenter = this.getLocation() + new Vector2(0, this.Height);
            Action endRot = new Action(() => { this.rotate((float)(90 / 180f * Math.PI), drawCenter, 1f); });
            this.rotate((-(float)(90 / 180f * Math.PI)), drawCenter, 1f, endRot);
        }

 
        public override void Update(long delta)
        {
            base.Update(delta);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);
            g.DrawRectangle(Pens.Green, 0, Height / 10 * 9, Width , Height / 10 * 2 );
        }

        public override bool Contains(Point point)
        {
            Rectangle rect = new Rectangle((int)X, (int)Y, Width, Height);
            return rect.Contains(point);
        }

        protected override void EnterMachine(PinballGameMachine machine)
        {
        }

        protected override void LeaveMachine(PinballGameMachine machine)
        {
        }

    }
}
