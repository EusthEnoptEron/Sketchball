using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Sketchball.Collision;
using System.Runtime.Serialization;

namespace Sketchball.Elements
{

    [DataContract]
    public class Flipper : AnimatedObject
    {
        [DataMember]
        public Keys Trigger;
        protected Keys DebugTrigger;

        public float RotationRange = (float)(Math.PI / 180 * 60);
        private bool Animating = false;

        public Flipper()  : base()
        {

            Width = 100;
            Height = 150;


  //          this.rotate((float)Math.PI*2, Origin, 0);

            // 0, Height / 10 * 9, Width , Height / 10 * 2 )

            this.setLocation(new Vector2(0, 0));
        }

        protected override void InitBounds()
        {
            int y1 = Height / 10 * 8;
            int recHeight = Height / 10 * 2;
            //set up of bounding box
            boundingContainer.AddPolygon(
                0, y1,
                Width, y1,
                Width, y1 + recHeight,
                0, y1 + recHeight,
                0, y1
            );

            //BoundingLine bL1 = new BoundingLine(new Vector2(0, y1), new Vector2(Width, y1));
            //BoundingLine bL2 = new BoundingLine(new Vector2(Width, y1), new Vector2(Width, y1 + recHeight));
            //BoundingLine bL3 = new BoundingLine(new Vector2(Width, y1 + recHeight), new Vector2(0, y1 + recHeight));
            //BoundingLine bL4 = new BoundingLine(new Vector2(0, y1 + recHeight), new Vector2(0, y1));

            //this.boundingContainer.addBoundingBox(bL1);
            //this.boundingContainer.addBoundingBox(bL2);
            //this.boundingContainer.addBoundingBox(bL3);
            //this.boundingContainer.addBoundingBox(bL4);
        }

 
        public override void Update(long delta)
        {
            base.Update(delta);
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);


            g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, Pens.Red);
            });
            g.TranslateTransform(X, Y);

            g.DrawRectangle(Pens.Green, 0, Height / 10 * 8, Width, Height / 10 * 2);
            //g.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);
        }

        public override bool Contains(Point point)
        {
            Rectangle rect = new Rectangle((int)X, (int)Y, Width, Height);
            return rect.Contains(point);
        }

        protected override void EnterMachine(PinballGameMachine machine)
        {
            machine.Input.KeyDown += OnKeyDown;
        }


        protected override void LeaveMachine(PinballGameMachine machine)
        {
            machine.Input.KeyDown -= OnKeyDown;
        }

        protected virtual Vector2 Origin
        {
            get
            {
                return new Vector2(0, this.Height);
            }
        }


        void OnKeyDown(object sender, KeyEventArgs e)
        {
            if ( (e.KeyCode == Trigger ||e.KeyCode == DebugTrigger) && !Animating)
            {

                var speed = e.KeyCode == Trigger ? 0.1f : 4f;

                Animating = true;

                Action endRot = () => {
                    this.rotate(-Rotation, Origin, 0.1f, () => { Animating = false; }); 
                };

                this.rotate(RotationRange, Origin, speed, endRot);
            }
        }

    }
    
    [DataContract]
    public class LeftFlipper : Flipper
    {
        public LeftFlipper()
        {
            Trigger = Keys.A;
            DebugTrigger = Keys.Q;
        }
    }

    [DataContract]
    public class RightFlipper : Flipper
    {

        protected override Vector2 Origin
        {
            get
            {
                return new Vector2(Width, Height);
            }
        }

        public RightFlipper()
        {
            Trigger = Keys.D;
            DebugTrigger = Keys.E;
            RotationRange = - RotationRange;
        }
    }
}
