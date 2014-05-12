using GlideTween;
using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public enum RotationOrigin
    {
        TopLeft,
        TopCenter,
        TopRight,
        
        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight
    }

    [DataContract]
    public abstract class PinballElement : ICloneable
    {
        [DataMember]
        private Size _size = new Size(100, 100);

        [DataMember]
        public Vector2 Location = new Vector2();

        [Category("Position")]
        public int Width { get { return _size.Width; } set { _size.Width = value; } }

        [Category("Position")]
        public int Height { get { return _size.Height; } set { _size.Height = value; } }

        [Category("Position")]
        public float X { get { return Location.X; } set { Location.X = value; } }
       
        [Category("Position")]
        public float Y { get { return Location.Y; } set { Location.Y = value; } }

        [DataMember]
        [Category("Position"), DisplayName("Rotation"), Description("The rotation of this element in degrees.")]
        public float BaseRotation { get; set; }

        [DataMember]
        [Category("Position")]
        [DefaultValue(typeof(RotationOrigin), "RotationOrigin.TopLeft")]
        public RotationOrigin Origin { get; set; }

        private PinballMachine _machine = null;
        private const int SELECTION_PADDING = 2;

        [DataMember]
        public PinballMachine World { 
            get {
                return _machine;
            }
 			internal set
            {
                if (_machine != null && _machine is PinballGameMachine)
                {
                    LeaveMachine((PinballGameMachine)_machine);
                }
                _machine = value;

                if (_machine != null && _machine is PinballGameMachine)
                {
                    EnterMachine((PinballGameMachine)_machine);
                }
            }
        }



        //Collision detection stuff
        private BoundingContainer _boundingContainer = null;

        /// <summary>
        /// Lazy-loading bounding container.
        /// </summary>
        public BoundingContainer boundingContainer
        {
            get
            {
                if (_boundingContainer == null)
                {
                    InitBoundingContainer();
                }
                return _boundingContainer;
            }
            private set
            {
                _boundingContainer = boundingContainer;
            }
        }

        public PinballElement() : this(0, 0)
        {
        }

        public PinballElement(float X, float Y)
        {
            Origin = RotationOrigin.TopLeft;
            this.X = X;
            this.Y = Y;
        }

        private void InitBoundingContainer()
        {
            _boundingContainer = new BoundingContainer(this);
            InitBounds();
        }

        protected abstract void InitBounds();

        [DataMember]
        public int Value { get; set; }

        public virtual void Update(long delta) {}

        public abstract void Draw(Graphics g);

        public virtual bool Contains(Point point)
        {
            using (Bitmap bm = new Bitmap(World.Width, World.Height))
            {
                using (Graphics g = Graphics.FromImage(bm))
                {
                    g.TranslateTransform(X, Y);
                    Draw(g);
                }

                for (int dx = -SELECTION_PADDING; dx <= SELECTION_PADDING; dx++)
                {
                    int x = point.X + dx;
                    int y = 0;

                    if (x < 0 || x >= bm.Width) continue;
                    for (int dy = -SELECTION_PADDING; dy <= SELECTION_PADDING; dy++)
                    {
                        y = point.Y + dy;
                        if (y < 0 || y >= bm.Height) continue;

                        Color pixel = bm.GetPixel(x, y);
                        if (pixel.A > 0) return true;
                    }
                }
            }
            return false;
        }

        public object Clone()
        {
            PinballElement element = (PinballElement)MemberwiseClone();
            element._boundingContainer = null;

            OnClone(element);
            return element;
        }

        /// <summary>
        /// Does additional stuff when an element is cloned.
        /// </summary>
        /// <param name="element"></param>
        protected virtual void OnClone(PinballElement element)
        {
        }

        /// <summary>
        /// Sets up event listeners when a new machine is entered.
        /// </summary>
        /// <param name="machine"></param>
  		protected virtual void EnterMachine(PinballGameMachine machine)
        {
        }

        /// <summary>
        /// Removes event listeners when a machine is left
        /// </summary>
        /// <param name="machine"></param>
        protected virtual void LeaveMachine(PinballGameMachine machine)
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


        public Rectangle Shape
        {
            get
            {
                return new Rectangle(0, 0, Width, Height);
            }
        }
    }
}
