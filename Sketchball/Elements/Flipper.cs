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

    class Flipper : PinballElement
    {

        private int Rotation = 0;
        
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

        private void GoUp()
        {
           Tweener.Tween(this, new { Rotation = 90 }, 1).Ease(GlideTween.Ease.QuintInOut).OnComplete(GoDown);
        }

        private void GoDown()
        {
            Tweener.Tween(this, new { Rotation = 0 }, 2).Ease(GlideTween.Ease.BackOut).OnComplete(GoUp);
        }
        void Flipper_KeyUp(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Space)
            {
                Timer t = new Timer();
                t.Interval = 10;
                t.Tick += (o, i) =>
                {
                    if (Rotation < 90)
                    {
                        Rotation += 10;
                    }
                    else
                    {
                        t.Stop();
                        Rotation = 0;
                    }
                };
                t.Start();
            }
        }
    

        public override void Update(long delta)
        {
            base.Update(delta);
            Tweener.Update(delta / 1000f);
            /*
            if (Rotation < 90)
            {
                Rotation += (int)(90.0 / 1000 * (int)delta);
            }
            else
            {
                Rotation = 0;
            }*/
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.TranslateTransform(0, Height);
            g.RotateTransform(-Rotation);
            g.TranslateTransform(0, -Height);

            g.DrawRectangle(Pens.Green, 0, Height / 10 * 9, Width , Height / 10 * 2 );
        }

        public override bool Contains(Point point)
        {
            Rectangle rect = new Rectangle((int)X, (int)Y, Width, Height);
            return rect.Contains(point);
        }

        protected override void EnterMachine(PinballMachine machine)
        {
        }

        protected override void LeaveMachine(PinballMachine machine)
        {
        }

    }
}
