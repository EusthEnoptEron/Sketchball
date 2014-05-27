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
    public class PinballGameMachine : PinballMachine
    {
        public delegate void CollisionEventHandler(PinballElement sender);
        public delegate void GameOverEventHandler();

        public event CollisionEventHandler Collision;
        public event GameOverEventHandler GameOver;

        private BoundingRaster boundingRaster;

        internal readonly InputManager Input = InputManager.Instance();

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


        public PinballGameMachine(PinballMachine machine) : base(machine.Layout)
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
       
        public void Update(long elapsed)
        {
            bool hasGameOver = false;

            foreach (PinballElement element in Elements)
            {
                element.Update(elapsed);
            }


            for (int i = Balls.Count - 1; i >= 0; i--)
            {
                if (Properties.Settings.Default.Debug && (Balls[i].Y + Balls[i].Height) > Height)
                {
                    Balls[i].Y = (Height - Balls[i].Height);
                    ((Ball)Balls[i]).Velocity *= -0.5f;
                }
                if (Balls[i].Y > Height)
                {
                    Balls.RemoveAt(i);
                    hasGameOver = true;
                }
            }

            handleCollision();

            if (hasGameOver)
            {
                var handlers = GameOver;
                if(handlers != null)
                    GameOver();
            }
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
