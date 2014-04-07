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
        private Keys Trigger = Keys.Space;
        private float Power = 0;
        private Vector2 MaxVelocity = new Vector2(-200f, -1500f);

        private bool Charging = false;
        private Ball Ball = null;
        private Glide Tweener = new Glide();

        public StartingRamp() : base()
        {

            bounceFactor = 0.5f;

            Width = 585 - 520 + 50;
            Height = 590 - 210;

            // Vertical line left
            boundingContainer.addBoundingBox(new BoundingLine(new Vector2(0, 0), new Vector2(478 - 405, 590 - 210)));
            
            // Horizontal line
            boundingContainer.addBoundingBox(new BoundingLine(new Vector2(478 - 405, 590 - 210), new Vector2(520 - 405, 585 - 210)));
            
            // Vertical line right
            //boundingContainer.addBoundingBox(new BoundingLine(new Vector2(Width, 0), new Vector2(Width, Height)));

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

        public override void Draw(System.Drawing.Graphics g)
        {
            g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((el) =>
            {
                el.drawDEBUG(g, Pens.Orange);
            });
            g.TranslateTransform(X, Y);

            g.DrawString(Power + "", new Font("Arial", 12, FontStyle.Regular), Brushes.Red, 0f, 0f);

            g.DrawRectangle(Pens.Red, 0, 0, Width, Height);
        }

        public void IntroduceBall(Ball ball) {
            Ball = ball;

            Ball.X = X + Ball.Width / 4;
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
