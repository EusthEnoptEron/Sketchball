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

    /// <summary>
    /// Represents a starting ramp. This ramp is not usually placed by the user but by the layout.
    /// </summary>
    [DataContract]
    public class StartingRamp : PinballElement
    {
        private static readonly Size size = new Size(210, 1128);
        
        // Current power [0..1]
        private float power = 0;

        // Key that loads the ramp
        private Keys trigger = Keys.Space;

        // State variable
        private bool charging = false;

        // Animation helper
        private Glide tweener = new Glide();


        protected override Size BaseSize { get { return size; } }
        
        private readonly int pencilPullback = 50;
        private readonly int pencilOffsetY = -90;

        
        private static System.Drawing.Image pencilImage = Booster.OptimizeImage(Properties.Resources.Rampe_pencil, 70);
        private ImageSource pencilImageWpf;
        private BoundingLine powerLine;

        public StartingRamp()
        {
        }

        protected override void InitResources()
        {
            Image = Booster.LoadImage("Rampe.png");
            pencilImageWpf = Booster.LoadImage("Rampe_pencil.png");
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

            Vector pPs = new Vector(65, this.BaseSize.Height + pencilOffsetY-20);
            Vector pPe = new Vector(152, this.BaseSize.Height + pencilOffsetY-20);


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
            BoundingLine subPowerLine = new BoundingLine(pPs + new Vector(0, pencilPullback), pPe + new Vector(0, pencilPullback));


            bL4.BounceFactor = 0.5f;
            bL1.BounceFactor = 0.5f;
            powerLine.BounceFactor = 0.2f;
            subPowerLine.BounceFactor = 0.2f;

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
            // Makes sure that the ball doesn't fall through
            BoundingContainer.AddBoundingBox(subPowerLine);


            Scale = 1 / 2f;
        }

        protected override void EnterGame(PinballGameMachine machine)
        {
            // Bind key event
            machine.Input.KeyDown += Charge;
            machine.Input.KeyUp += Discharge;
        }

        protected override void LeaveGame(PinballGameMachine machine)
        {
            // Unbind key event
            machine.Input.KeyDown -= Charge;
            machine.Input.KeyUp -= Discharge;
        }

        protected override void OnDraw(DrawingContext g)
        {
            g.DrawImage(Image, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
            g.DrawImage(pencilImageWpf, new System.Windows.Rect(96f / 276 * BaseWidth, (1800f + pencilOffsetY - 5) / 1934 * BaseHeight + power * pencilPullback, pencilImage.Width, pencilImage.Height));
        }

        /// <summary>
        /// Positions a ball so that it will fall into the ramp.
        /// </summary>
        /// <param name="ball"></param>
        public void IntroduceBall(Ball ball) {
            ball.X = X + ball.Width * 1.5;
            ball.Y = 2 * Y;
        }


        public override void Update(double delta)
        {
            // Update animations
            base.Update(delta);
            tweener.Update((float)delta);
           
            // We need to discharge
            if (!charging && power > 0)
            {
                // 1. Calculate the velocity needed to get the ball out there
                double t = 1;
                // v * t = s - (a*t^2)/2
                Vector targetDistance = -new Vector(0, Height*2);
                Vector maxVelocity = (targetDistance - 0.5 * World.Acceleration * t * t) / t;

                //Vector maxVelocity = -World.Acceleration * 2;
                maxVelocity.X = 0;

                // SHOOT!
                // We use a little trick here: the powerline is raised by a little and if it then intersects with the ball,
                // the ball will be bounced off.
                powerLine.move(new Vector(0, -20));
                var dummy = new Vector();
                foreach (var el in World.Balls)
                {
                    Ball ball = (Ball)el;
                    foreach (var boundingBox in ball.BoundingContainer.BoundingBoxes)
                    {
                        if (powerLine.Intersect(boundingBox, out dummy))
                        {
                            ball.Velocity += power * maxVelocity;
                            ball.Location.Y -= power * pencilPullback * Scale;
                            break;
                        }
                    }
                }
                powerLine.move(new Vector(0, 20));
                powerLine.Sync(Transform);
                power = 0;
            }
        }

        // OnKeyDown
        private void Charge(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == trigger)
            {
                charging = true;
                tweener.Tween(this, new { power = 1 }, 1f).OnUpdate(delegate {
                    Matrix m = Matrix.Identity;
                    m *= Transform;
                    m.Translate(0, power * pencilPullback * Scale);

                    powerLine.Sync(m);
                });
            }
        }

        // OnKeyUp
        private void Discharge(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == trigger)
            {
                charging = false;
                tweener.Cancel();
            }
        }

        /// <summary>
        /// Checks if the ramp contains a ball currently.
        /// </summary>
        /// <param name="ball"></param>
        /// <returns>Whether or not the ball is in this ramp.</returns>
        public bool Contains(Ball ball)
        {
            return GetBounds().Contains(ball.GetBounds());
        }

    }
}
