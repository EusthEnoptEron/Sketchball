using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Elements
{
    public class StartingRamp : PinballElement
    {
        private Keys Trigger = Keys.Space;
        private float Power = 0;
        private Vector2 MaxVelocity = new Vector2(0, 100f);

        private bool Charging = false;
        private Ball Ball = null;

        public StartingRamp() : base()
        {
            Width = 100;
            Height = 300;
        }

        protected override void EnterMachine(PinballMachine machine)
        {
            // Bind event
            machine.Input.KeyDown += Charge;
            machine.Input.KeyUp += Uncharge;
        }

        protected override void LeaveMachine(PinballMachine machine)
        {
            // Unbind event
            machine.Input.KeyDown -= Charge;
            machine.Input.KeyUp -= Uncharge;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawRectangle(Pens.Red, 0, 0, Width, Height);
        }

        public void IntroduceBall(Ball ball) {
            Ball = ball;

            Ball.X = X;
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
                Tweener.Tween(this, new { Power = 1 }, 2f);
            }
        }

        private void Uncharge(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Trigger && Active)
            {
                Charging = false;
                Tweener.Cancel();
            }
        }
    }
}
