using GlideTween;
using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [Serializable]
    public abstract class PinballElement : ICloneable
    {
        public int Width = 100;
        public int Height = 100;
        protected Glide Tweener = new Glide();

        public float bounceFactor { get; set; }

        private PinballMachine _machine = null;
        public PinballMachine World { 
            get {
                return _machine;
            }
 			internal set
            {
                if (_machine != null)
                {
                    LeaveMachine(_machine);
                }
                _machine = value;

                if (_machine != null)
                {
                    EnterMachine(_machine);
                }
            }
        }

        private Vector2 Location= new Vector2();


        //Collision detection stuff
        public BoundingContainer boundingContainer{get;private set;}

        public PinballElement()
        {
            this.boundingContainer =  new BoundingContainer(this);
            this.bounceFactor = 0.9f;
        }

        public int Value { get; protected set; }

        public virtual void Update(long delta)
        
        }

        public abstract void Draw(Graphics g);

        public virtual bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            PinballElement element = (PinballElement)MemberwiseClone();
            return element;
        }

  		 protected virtual void EnterMachine(PinballMachine machine)
        {
        }

        protected virtual void LeaveMachine(PinballMachine machine)
        {
        }

        public BoundingContainer getBoundingContainer()
        {
            return this.boundingContainer;
        }

        public Vector2 getLocation()
        {
            return this.Location;
        }

        public void setLocation(Vector2 newLoc)
        {
            this.Location.X = newLoc.X;
            this.Location.Y = newLoc.Y;
        }

        public Vector2 reflectManipulation(Vector2 newDirection, int energy = 0)
        {
            return newDirection* bounceFactor;
        }
    }
}
