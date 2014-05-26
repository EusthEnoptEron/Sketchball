using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlideTween;

namespace Sketchball.Elements
{
    public class StartingRamp : PinballElement
    {
        private float Power = 0;
        private Keys Trigger = Keys.Space;
        private Vector2 MaxVelocity = new Vector2(5, -1000f);

        private Ball Ball = null;
        private bool Charging = false;
        private Glide Tweener = new Glide();

        private static readonly Size size = new Size(210, 1128);

        protected override Size BaseSize { get { return size; } }
        private readonly int PencilPullback = 50;
        private readonly int PencilOffsetY = -100;


        private static Image PencilImage = Booster.OptimizeImage(Properties.Resources.Rampe_pencil, 86);
        private static Image RampImage = Booster.OptimizeImage(Properties.Resources.Rampe, 500);




        public StartingRamp()
        {
        }

        protected override void Init()
        {

            Vector2 p1 = new Vector2(21,1128);
            Vector2 p2 = new Vector2(16, 233);
            Vector2 p3 = new Vector2(35, 158);
            Vector2 p4 = new Vector2(58, 230);
            Vector2 p5 = new Vector2(68, 1128);

            Vector2 p21 = new Vector2(149, 1128);
            Vector2 p22 = new Vector2(131, 231);
            Vector2 p23 = new Vector2(102, 26);
            Vector2 p24 = new Vector2(121, 12);
            Vector2 p25 = new Vector2(154, 230);
            Vector2 p26 = new Vector2(175, 1128);

            Vector2 pPs = new Vector2(65, this.BaseSize.Height + PencilOffsetY);
            Vector2 pPe = new Vector2(152, this.BaseSize.Height + PencilOffsetY);


            BoundingLine bL1 = new BoundingLine(p1, p2);
            BoundingLine bL2 = new BoundingLine(p2, p3);
            BoundingLine bL3 = new BoundingLine(p3, p4);
            BoundingLine bL4 = new BoundingLine(p4, p5);
            BoundingLine bL5 = new BoundingLine(p5, p1);

            BoundingLine bL21 = new BoundingLine(p21, p22);
            BoundingLine bL22 = new BoundingLine(p22, p23);
            BoundingLine bL23 = new BoundingLine(p23, p24);
            BoundingLine bL24 = new BoundingLine(p24, p25);
            BoundingLine bL25 = new BoundingLine(p25, p26);
            BoundingLine bL26 = new BoundingLine(p26, p21);

            BoundingLine bLP = new BoundingLine(pPs, pPe);

            bL4.bounceFactor = 0.5f;
            bL1.bounceFactor = 0.5f;
            bLP.bounceFactor = 0.2f;

            this.boundingContainer.addBoundingBox(bL1);
            this.boundingContainer.addBoundingBox(bL2);
            this.boundingContainer.addBoundingBox(bL3);
            this.boundingContainer.addBoundingBox(bL4);
            this.boundingContainer.addBoundingBox(bL5);

            this.boundingContainer.addBoundingBox(bL21);
            this.boundingContainer.addBoundingBox(bL22);
            this.boundingContainer.addBoundingBox(bL23);
            this.boundingContainer.addBoundingBox(bL24);
            this.boundingContainer.addBoundingBox(bL25);
            this.boundingContainer.addBoundingBox(bL26);

            this.boundingContainer.addBoundingBox(bLP);


            Scale = 1 / 2f;
        }

        protected override void EnterMachine(PinballGameMachine machine)
        {
            // Bind event
            machine.Input.KeyDown += Charge;
            machine.Input.KeyUp += Discharge;
        }

        protected override void LeaveMachine(PinballGameMachine machine)
        {
            // Unbind event
            machine.Input.KeyDown -= Charge;
            machine.Input.KeyUp -= Discharge;
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {

            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, Pens.Red);
            });
            g.DrawImage(RampImage, 0, 0, BaseWidth, BaseHeight);
            g.DrawImage(PencilImage, 64, (1128 + PencilOffsetY -15)  + Power * PencilPullback);
        } 

        public void IntroduceBall(Ball ball) {
            Ball = ball;

            Ball.X = X + Ball.Width / 2;
            Ball.Y = Y;
        }

        public override void Update(long delta)
        {
            base.Update(delta);
            Tweener.Update(delta / 1000f);

            if (!Charging && Power > 0 )
            {
                if (Active)
                {
                    // SHOOT!
                    Ball.Velocity += Power * MaxVelocity;
                    Power = 0;
                }
            }
        }

        public bool Active
        {
            get
            {
                // TODO: make better check!
                return Ball != null;
            }
        }

        private void Charge(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Trigger && Active)
            {
                Charging = true;
                Tweener.Tween(this, new { Power = 1 }, 1f);
            }
        }

        private void Discharge(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Trigger && Active)
            {
                Charging = false;
                Tweener.Cancel();
            }
        }
    }
}
