using GlideTween;
using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    /// <summary>
    /// Allowed origins for rotation. Powers of 2 to make them flags.
    /// </summary>
    public enum RotationOrigin
    {
        TopLeft = 1,
        TopCenter = 2,
        TopRight = 4,
        
        MiddleLeft = 8,
        MiddleCenter = 16,
        MiddleRight = 32,

        BottomLeft = 64,
        BottomCenter = 128,
        BottomRight = 256
    }


    /// <summary>
    /// Central element class that is inherited by all elements on the play field.
    /// </summary>
    [DataContract]
    public abstract class PinballElement : ICloneable
    {
        // Determines how many pixels around the click position will be taken into account on click.
        private const int SELECTION_PADDING = 2;

        #region Properties
        /// <summary>
        /// Gets or sets the current location of the element in internal coordinates.
        /// </summary>
        [DataMember]
        public Vector2 Location = new Vector2();

        /// <summary>
        /// Gets the original size of the element.
        /// </summary>
        [Browsable(false)]
        protected abstract Size BaseSize { get;}

        /// <summary>
        /// Gets the original width of the element.
        /// </summary>
        [Browsable(false)]
        protected int BaseWidth { get { return BaseSize.Width; } }


        /// <summary>
        /// Gets the original height of the element.
        /// </summary>
        [Browsable(false)]
        protected int BaseHeight { get { return BaseSize.Height; } }

        /// <summary>
        /// Gets or sets the effective width of the element.
        /// </summary>
        [Category("Position"), Browsable(false)]
        public int Width { get { return (int)(BaseWidth * Scale); } set { Scale = Scale / Width * value; } }

        /// <summary>
        /// Gets or sets the effective height of the element.
        /// </summary>
        [Category("Position"), Browsable(false)]
        public int Height { get { return (int)(BaseHeight * Scale); } set { Scale = Scale / Height * value; } }

        /// <summary>
        /// Gets or sets the X position of the element.
        /// </summary>
        [Category("Position")]
        public float X { get { return Location.X; } set { Location.X = value; } }
       
        /// <summary>
        /// Gets or sets the Y position of the element.
        /// </summary>
        [Category("Position")]
        public float Y { get { return Location.Y; } set { Location.Y = value; } }

        
        /// <summary>
        /// Gets or sets how much the element is rotated (in degrees)
        /// </summary>
        [DataMember]
        [Category("Position"), DisplayName("Rotation"), Description("The rotation of this element in degrees.")]
        public float BaseRotation
        {
            get { return _baseRotation; }
            set
            {
                _baseRotation = value;
                RebuildMatrix();
            }
        }
        private float _baseRotation = 0;


        /// <summary>
        /// Gets or sets the origin for rotation.
        /// </summary>
        [DataMember]
        [Category("Position")]
        public RotationOrigin Origin { get; set; }


        /// <summary>
        /// Sets or gets the scale of this element.
        /// </summary>
        [Category("Position"), DefaultValue(1.0f)]
        public float Scale
        {
            get { return _scale; }
            set
            {
                if (_scale != 0)
                {
                    _scale = value;
                    RebuildMatrix();
                }
            }
        }
        [DataMember]
        private float _scale = 1.0f;

        /// <summary>
        /// Gets whether or not this element reflects the ball.
        /// </summary>
        [Browsable(false)]
        public bool pureIntersection{ get; protected set;}

        /// <summary>
        /// Gets or sets the pinball machine this element is attached to. Don't tinker with this property.
        /// </summary>
        [DataMember]
        [Browsable(false)]
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
        private PinballMachine _machine = null;
        
        /// <summary>
        /// Gets or sets the bounce factor of the element.
        /// </summary>
        [DataMember]
        [Category("Behavior"), Description("Determines how the ball will be reflected.")]
        public float BounceFactor
        {
            get
            {
                return _bounceFactor;
            }
            set
            {
                if (value < 1) value = 1;
                if (value > 5) value = 5;
                _bounceFactor = value;
            }
        }
        private float _bounceFactor = 1f;

        /// <summary>
        /// Gets the bounding container of this element.
        /// </summary>
        [Browsable(false)]
        public BoundingContainer boundingContainer { get; private set; }


        [DataMember]
        public int Value { get; set; }

        #endregion

        public PinballElement() : this(0, 0)
        {
        }

        public PinballElement(float X, float Y)
        {
            this.X = X;
            this.Y = Y;

            Origin = RotationOrigin.MiddleCenter;

            init();
        }

        private void init()
        {
            Transform = new Matrix();
            pureIntersection = false;
            boundingContainer = new BoundingContainer(this);

            Init();
        }

        [OnDeserializing]
        private void OnDeserialized(StreamingContext context)
        {
            init();
        }


        public void Draw(Graphics g)
        {
            var state = g.Save();

            g.MultiplyTransform(Transform, MatrixOrder.Prepend);

            OnDraw(g);

            g.Restore(state);

            g.TranslateTransform(-X, -Y);
            // Debug draw
            if (Properties.Settings.Default.Debug)
            {
                foreach (var box in boundingContainer.boundingBoxes)
                {
                    box.drawDEBUG(g, Pens.Red);
                }
            }
            g.TranslateTransform(X, Y);
        }

        public Matrix Transform { get; private set; }
        private void RebuildMatrix()
        {
            var vOrigin = GetRotationOrigin();
            var origin = new PointF(vOrigin.X, vOrigin.Y);

            Transform = new Matrix();
            Transform.RotateAt(BaseRotation, origin);
            Transform.Scale(Scale, Scale);

            Sync();
        }

        /// <summary>
        /// Returns the exact coordinates of the origin where the element is to be rotated.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetRotationOrigin()
        {
            var top = (RotationOrigin.TopLeft | RotationOrigin.TopCenter | RotationOrigin.TopRight);
            var middle = (RotationOrigin.MiddleLeft | RotationOrigin.MiddleCenter | RotationOrigin.MiddleRight);
            //var bottom = (RotationOrigin.BottomLeft | RotationOrigin.BottomCenter | RotationOrigin.BottomRight);
 
            var left = (RotationOrigin.TopLeft | RotationOrigin.MiddleLeft | RotationOrigin.BottomLeft);
            var center = (RotationOrigin.TopCenter | RotationOrigin.MiddleCenter | RotationOrigin.BottomCenter);
            //var right = (RotationOrigin.TopRight | RotationOrigin.MiddleRight | RotationOrigin.BottomRight);


            Vector2 origin = new Vector2();
            if ( (Origin & top) != 0)
            {
                origin.Y = 0;
            } else if ( (Origin & middle) != 0 ) {
                origin.Y = Height / 2f;
            } else {
                origin.Y = Height;
            }

            if ((Origin & left) != 0)
            {
                origin.X = 0;
            }
            else if ((Origin & center) != 0)
            {
                origin.X = Width / 2f;
            }
            else
            {
                origin.X = Width;
            }

            return origin;
        }

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
            element.init();

            OnClone(element);
            return element;
        }


        private void Sync()
        {
            boundingContainer.Sync();
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


        public Rectangle GetBounds()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }


#region Implementables

        protected abstract void Init();
        protected abstract void OnDraw(Graphics g);

        public virtual void Update(long delta) { }


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

        public virtual void notifyIntersection(Ball b)
        {
            //placeholder
        }

#endregion

        
        
    }
}
