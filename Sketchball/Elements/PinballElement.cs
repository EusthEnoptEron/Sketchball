using GlideTween;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [Serializable]
    public abstract class PinballElement
    {
        public int Width = 100;
        public int Height = 100;
        protected Glide Tweener = new Glide();

        public static Vector2 G = new Vector2(0, 0.8f);

        public PinballMachine World { get; internal set; }

        private Vector2 PrivateAcceleration = new Vector2();
        public Vector2 Acceleration
        {
            get
            {
                if (AffectedByGravity)
                    return PrivateAcceleration + World.Gravity;
                else
                    return PrivateAcceleration;
            }
            set
            {
                if (AffectedByGravity)
                    PrivateAcceleration = value - World.Gravity;
                else
                    PrivateAcceleration = value;
            }
        }

        public virtual Boolean AffectedByGravity { get; protected set; }


        private Vector2 _v0 = new Vector2();
        private float t = 0;
        public Vector2 V0 {
            get { return _v0;  } 
            set { _v0 = value; t = 0; }
        }
        public Vector2 Velocity { get; private set; }

        public Vector2 Location = new Vector2();
        public float X { get { return Location.X; } set { Location.X = value; } }
        public float Y { get { return Location.Y; } set { Location.Y = value; } }
       

        public virtual void Update(long delta)
        {  
            t += delta;
            Velocity = V0 + t * Acceleration;
            Location.X += (Velocity.X * delta / 1000f);
            Location.Y += (Velocity.Y * delta / 1000f);
        }

        public abstract void Draw(Graphics g);

        public virtual bool Contains(Point point)
        {
            throw new NotImplementedException();
        }
    }
}
