using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class PinballGameMachine : PinballMachine
    {
        public delegate void CollisionEventHandler(PinballElement sender);
        public delegate void GameOverEventHandler();

        public event CollisionEventHandler Collision;
        public event GameOverEventHandler GameOver;

        private List<PinballElement> FallenBalls = new List<PinballElement>();

        private BoundingRaster boundingRaster;


        /// <summary>
        /// No more elements can be added after this function call 
        /// </summary>
        public void prepareForLaunch()
        {
            this.boundingRaster = new BoundingRaster(Width / 60, Height / 60, Width, Height);
            this.boundingRaster.takeOverBoundingBoxes(this.Elements);
        }

        internal readonly InputManager Input = InputManager.Instance();

        public PinballGameMachine(PinballMachine machine) : base(machine.Bounds)
        {
            this.boundingRaster = new BoundingRaster(Width / 60, Height / 60, Width, Height);

            // Copy constructor
            foreach (PinballElement element in machine.Elements)
            {
                if(!(element is StartingRamp))
                    Elements.Add((PinballElement)element.Clone());
            }
            Angle = machine.Angle;
            Gravity = machine.Gravity;

        }
       
        public void Update(long elapsed)
        {
            bool hasGameOver = false;

            foreach (PinballElement element in Elements)
            {
                element.Update(elapsed);
            }


            for (int i = Balls.Count - 1; i >= 0; i--)
            {
                Balls[i].Update(elapsed);
                if (Balls[i].Y > Height)
                {
                    Balls.RemoveAt(i);
                    hasGameOver = true;
                }
            }

            handleCollision();

            if (hasGameOver)
                GameOver();
        }

        public void handleCollision()
        {
            foreach (Ball b in this.Balls)
            {
                this.boundingRaster.handleCollision(b);
            }
        }

        public void debugDraw(Graphics g)
        {
            g.DrawEllipse(Pens.Orange, this.boundingRaster.hitPointDebug.X, this.boundingRaster.hitPointDebug.Y, 2, 2);
        }

        internal void addAnimatedObject(PinballElement tr)
        {
            foreach (BoundingBox b in tr.boundingContainer.getBoundingBoxes())
            {
                this.boundingRaster.addAnimatedObject(b);
            }
        }

        internal bool HasBall()
        {
            //throw new NotImplementedException();
            return Balls.Count > 0;
        }

        internal void IntroduceBall()
        {
            Ball ball = new Ball();

            Ramp.IntroduceBall(ball);
            this.Balls.Add(ball);
        }

    }
}
