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
using System.Runtime.Serialization;

namespace Sketchball.Elements
{

    [DataContract]
    public class StartingRamp : PinballElement
    {
        private float Power = 0;
        private Keys Trigger = Keys.Space;
        private Vector MaxVelocity = new Vector(0, -2000);

        private bool Charging = false;
        private Glide Tweener = new Glide();

        private static readonly Size size = new Size(210, 1128);

        protected override Size BaseSize { get { return size; } }
        private readonly int PencilPullback = 50;
        private readonly int PencilOffsetY = -90;

        private static System.Drawing.Image pencilImage = Booster.OptimizeImage(Properties.Resources.Rampe_pencil, 70);
        private ImageSource pencilImageWpf;

        private BoundingLine powerLine;

        protected override void InitResources()
        {
            Image = Booster.OptimizeWpfImage("Rampe.png");
            pencilImageWpf = Booster.OptimizeWpfImage("Rampe_pencil.png");
        }

        public StartingRamp()
        {
        }

        protected override void Init()
        {

            Vector p1 = new Vector(21,1128);
            Vector p2 = new Vector(16, 233);
            Vector p3 = new Vector(35, 158);
            Vector p4 = new Vector(58, 230);
            Vector p5 = new Vector(68, 1128);

            Vector p21 = new Vector(149, 1128);
            Vector p22 = new Vector(131, 231);
            Vector p23 = new Vector(102, 26);
            Vector p24 = new Vector(121, 12);
            Vector p25 = new Vector(154, 230);
            Vector p26 = new Vector(175, 1128);

            Vector pPs = new Vector(65, this.BaseSize.Height + PencilOffsetY-20);
            Vector pPe = new Vector(152, this.BaseSize.Height + PencilOffsetY-20);


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

            powerLine = new BoundingLine(pPs, pPe);

            bL4.BounceFactor = 0.5f;
            bL1.BounceFactor = 0.5f;
            powerLine.BounceFactor = 0.2f;

            this.BoundingContainer.AddBoundingBox(bL1);
            this.BoundingContainer.AddBoundingBox(bL2);
            this.BoundingContainer.AddBoundingBox(bL3);
            this.BoundingContainer.AddBoundingBox(bL4);
            this.BoundingContainer.AddBoundingBox(bL5);

            this.BoundingContainer.AddBoundingBox(bL21);
            this.BoundingContainer.AddBoundingBox(bL22);
            this.BoundingContainer.AddBoundingBox(bL23);
            this.BoundingContainer.AddBoundingBox(bL24);
            this.BoundingContainer.AddBoundingBox(bL25);
            this.BoundingContainer.AddBoundingBox(bL26);

            this.BoundingContainer.AddBoundingBox(powerLine);


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

        protected override void OnDraw(DrawingContext g)
        {
            g.DrawImage(Image, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
            g.DrawImage(pencilImageWpf, new System.Windows.Rect(96f / 276 * BaseWidth, (1800f + PencilOffsetY - 5) / 1934 * BaseHeight + Power * PencilPullback, pencilImage.Width, pencilImage.Height));
        }

        public void IntroduceBall(Ball ball) {
            ball.X = X + ball.Width * 0.85;
            ball.Y = 1.6 * Y;
        }

        public override void Update(long delta)
        {
            base.Update(delta);
            Tweener.Update(delta / 1000f);
           
            if (!Charging && Power > 0)
            {
                // SHOOT!
                powerLine.move(new Vector(0, -20));
                var dummy = new Vector();
                foreach (var el in World.Balls)
                {
                    Ball ball = (Ball)el;
                    foreach (var boundingBox in ball.BoundingContainer.BoundingBoxes)
                    {
                        if (powerLine.Intersect(boundingBox, out dummy))
                        {
                            ball.Velocity += Power * MaxVelocity;
                            ball.Location.Y -= Power * PencilPullback * Scale;
                            break;
                        }
                    }
                }
                powerLine.move(new Vector(0, 20));
                powerLine.Sync(Transform);
                Power = 0;
            }
        }

        private void Charge(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Trigger)
            {
                Charging = true;
                Tweener.Tween(this, new { Power = 1 }, 1f).OnUpdate(delegate {
                    Matrix m = Matrix.Identity;
                    m *= Transform;
                    m.Translate(0, Power * PencilPullback * Scale);

                    powerLine.Sync(m);
                });
            }
        }

        private void Discharge(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Trigger)
            {
                Charging = false;
                Tweener.Cancel();
            }
        }


        public bool Contains(Ball ball)
        {
            return GetBounds().Contains(ball.GetBounds());
        }

    }
}
