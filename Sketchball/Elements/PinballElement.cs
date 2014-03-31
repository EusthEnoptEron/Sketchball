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

        public float bounceFactor { get; set; }

        public PinballMachine World { get; internal set; }

        private Vector2 Location= new Vector2();

        public float X { get { return Location.X; } set { Location.X = value; } }
        public float Y { get { return Location.Y; } set { Location.Y = value; } }

        //Collision detection stuff
        public BoundingContainer boundingContainer{get;private set;}

        public PinballElement()
        {
            this.boundingContainer =  new BoundingContainer(this);
            this.bounceFactor = 0.9f;
        }
       

        public virtual void Update(long delta)
        {
        }

        public abstract void Draw(Graphics g);

        public virtual bool Contains(Point point)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            PinballElement element = (PinballElement)base.MemberwiseClone();
            return element;
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
