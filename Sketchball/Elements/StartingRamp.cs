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
        private float factor = 1f / 5;
        private float Power = 0;
        private Keys Trigger = Keys.Space;
        private Vector2 MaxVelocity = new Vector2(5, -1000f);

        private Ball Ball = null;
        private bool Charging = false;
        private Glide Tweener = new Glide();

        private static readonly Size size = new Size(276, 1934);

        protected override Size BaseSize { get { return size; } }
        private readonly int PencilPullback = 50;
        private readonly int PencilOffsetY = -100;





        public StartingRamp()
        {
        }

        protected override void Init()
        {
            // Vertical line left
            BoundingLine bl1 = new BoundingLine(new Vector2(0, 0), new Vector2(478 - 405, 590 - 210-5));
            bl1.bounceFactor = 0.5f;
            boundingContainer.addBoundingBox(bl1);

            Vector2 p1 = new Vector2(7,1874);
            Vector2 p2 = new Vector2(7, 403);
            Vector2 p3 = new Vector2(44, 270);
            Vector2 p4 = new Vector2(85, 403);
            Vector2 p5 = new Vector2(85, 1874);

            Vector2 p21 = new Vector2(222, 1843);
            Vector2 p22 = new Vector2(224, 405);
            Vector2 p23 = new Vector2(176, 45);
            Vector2 p24 = new Vector2(215, 16);
            Vector2 p25 = new Vector2(267, 397);
            Vector2 p26 = new Vector2(264, 1888);

            Vector2 pPs = new Vector2(84, 1800 + PencilOffsetY);
            Vector2 pPe = new Vector2(223, 1800 + PencilOffsetY);

            //Factors here
       
            p1 *= factor;
            p2 *= factor;
            p3 *= factor;
            p4 *= factor;
            p5 *= factor;

            p21 *= factor;
            p22 *= factor;
            p23 *= factor;
            p24 *= factor;
            p25 *= factor;
            p26 *= factor;

            pPs *= factor;
            pPe *= factor;

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
            g.DrawImage(Booster.OptimizeImage(Properties.Resources.Rampe,Width,Height), 0, 0, Width, Height);
            g.DrawImage(Booster.OptimizeImage(Properties.Resources.Rampe_pencil, (int)(115f / 276 * Width), (int)(273f / 1934 * Height)), 86f / 276 * Width, (1800f + PencilOffsetY - 5) / 1934 * Height + Power * PencilPullback, (int)(115f / 276 * Width), (int)(273f / 1934 * Height));
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

            if (!Charging && Power > 0 && Active)
            {
                // SHOOT!
                Ball.Velocity += Power * MaxVelocity;
                Power = 0;
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
