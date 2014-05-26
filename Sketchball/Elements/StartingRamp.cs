using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GlideTween;
using System.Windows;
using System.Windows.Media;

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

        private static readonly Size size = new Size(276, 1934);
        //private static readonly Size size = new Size((int)(276 /5f), (int)(1934/5f));

        protected override Size BaseSize { get { return size; } }
        private readonly int PencilPullback = 50;
        private readonly int PencilOffsetY = -100;

        private static System.Drawing.Image PencilImage = Booster.OptimizeImage(Properties.Resources.Rampe_pencil, 200);
        private static ImageSource RampImageS = Booster.OptimizeWpfImage("Rampe.png");
        private static ImageSource PencilImageS = Booster.OptimizeWpfImage("Rampe_pencil.png");



        public StartingRamp()
        {
        }

        protected override void Init()
        {

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

            bL4.BounceFactor = 0.5f;
            bL1.BounceFactor = 0.5f;
            bLP.BounceFactor = 0.2f;

            this.boundingContainer.AddBoundingBox(bL1);
            this.boundingContainer.AddBoundingBox(bL2);
            this.boundingContainer.AddBoundingBox(bL3);
            this.boundingContainer.AddBoundingBox(bL4);
            this.boundingContainer.AddBoundingBox(bL5);

            this.boundingContainer.AddBoundingBox(bL21);
            this.boundingContainer.AddBoundingBox(bL22);
            this.boundingContainer.AddBoundingBox(bL23);
            this.boundingContainer.AddBoundingBox(bL24);
            this.boundingContainer.AddBoundingBox(bL25);
            this.boundingContainer.AddBoundingBox(bL26);

            this.boundingContainer.AddBoundingBox(bLP);


            Scale = 1 / 5f;
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

        protected override void OnDraw(DrawingContext g)
        {
            g.DrawImage(RampImageS, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
            g.DrawImage(PencilImageS, new System.Windows.Rect(86f / 276 * BaseWidth, (1800f + PencilOffsetY - 5) / 1934 * BaseHeight + Power * PencilPullback, PencilImage.Width, PencilImage.Height));
        }

        public void IntroduceBall(Ball ball) {
            Ball = ball;

            Ball.X = X + (float)Ball.Width / 2;
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
