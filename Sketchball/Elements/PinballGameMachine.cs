using Sketchball.Collision;
using Sketchball.GameComponents;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    /// <summary>
    /// Represents a pinball machine with additional game logic, i.e. physics.
    /// </summary>
    public class PinballGameMachine : PinballMachine
    {
        public delegate void CollisionEventHandler(PinballElement sender);
        public delegate void GameOverEventHandler();

        /// <summary>
        /// Occurs then the machine detects a collision.
        /// </summary>
        public event CollisionEventHandler Collision;
        /// <summary>
        /// Occurs when the ball gets thrown out of the field.
        /// </summary>
        public event GameOverEventHandler GameOver;

        private BoundingRaster boundingRaster;
        private List<Ball> killedBalls = new List<Ball>();
        internal readonly InputManager Input = InputManager.Instance();
        internal readonly SoundManager Sfx = new SoundManager();

        /// <summary>
        /// No more elements can be added after this function call 
        /// </summary>
        public void prepareForLaunch()
        {
            LinkedList<IBoundingBox> anis = this.boundingRaster.getAnimatedObjects();
            this.boundingRaster = new BoundingRaster((int)Math.Ceiling(Width * 1f / Ball.Size.Width), (int)Math.Ceiling(Height * 1f / Ball.Size.Width), Width, Height);
            foreach (IBoundingBox b in anis)
            {
                this.boundingRaster.AddAnimatedObject(b);
            }
            this.boundingRaster.takeOverBoundingBoxes(StaticElements);
            this.boundingRaster.takeOverBoundingBoxes(DynamicElements);
        }


        public PinballGameMachine(PinballMachine machine) : base(machine.Layout.Clone() as IMachineLayout)
        {
            this.boundingRaster = new BoundingRaster((int)Math.Ceiling(Width / 60f), (int)Math.Ceiling(Height / 60f), Width, Height);

            // Copy constructor
            foreach (PinballElement element in machine.DynamicElements)
            {
                DynamicElements.Add((PinballElement)element.Clone());
            }
            Angle = machine.Angle;
            Gravity = machine.Gravity;
        }
       
        public void Update(double elapsed)
        {
            foreach (PinballElement element in Elements)
            {
                element.Update(elapsed);
            }


            foreach (var el in Balls)
            {
                Ball ball = el as Ball;
                if (Properties.Settings.Default.Debug && (ball.Y + ball.Height) > Height)
                {
                    ball.Y = (Height - ball.Height);
                    ball.Velocity *= -0.5f;
                }
                if (ball.Y > Height)
                {
                    KillBall(ball);
                }
            }

            handleCollision();

            // Handle balls that should be removed
            foreach (var ball in killedBalls)
            {
                Balls.Remove(ball);

                var handlers = GameOver;
                if (handlers != null)
                    GameOver();
            }
            killedBalls.Clear();
        }

        /// <summary>
        /// Removes a ball from the field and fires a GameOver event.
        /// </summary>
        /// <param name="ball"></param>
        public void KillBall(Ball ball)
        {
            killedBalls.Add(ball);
        }

        private void handleCollision()
        {
            List<CollisionResult> hits = new List<CollisionResult>(20);
            foreach (Ball b in this.Balls)
            {
                hits.Add(boundingRaster.HandleCollision(b));
            }

            foreach (CollisionResult result in hits)
            {
                analyzeCollisions(result);
            }
        }

        private void analyzeCollisions(CollisionResult result)
        {
            foreach (PinballElement element in result)
            {
                raiseCollision(element);
            }
        }

        public bool HasBall()
        {
            //throw new NotImplementedException();
            return Balls.Count > 0;
        }

        public void IntroduceBall()
        {
            Ball ball = new Ball();

            Ramp.IntroduceBall(ball);
          
            this.Balls.Add(ball);
        }

        private void raiseCollision(PinballElement element)
        {
            var handlers = Collision;
            if (handlers != null)
            {
                handlers(element);
            }
        }

    }
}
