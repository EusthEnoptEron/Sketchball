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
    public abstract class PinballElement : ICloneable
    {
        public int Width = 100;
        public int Height = 100;
        protected Glide Tweener = new Glide();


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

                EnterMachine(_machine);
            }
        }

        public Vector2 Location = new Vector2();
        public float X { get { return Location.X; } set { Location.X = value; } }
        public float Y { get { return Location.Y; } set { Location.Y = value; } }

        public int Value { get; protected set; }

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
            PinballElement element = (PinballElement)MemberwiseClone();
            return element;
        }

        protected virtual void EnterMachine(PinballMachine machine)
        {
        }

        protected virtual void LeaveMachine(PinballMachine machine)
        {
        }
    }
}
