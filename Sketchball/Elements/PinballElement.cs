using GlideTween;
using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [DataContract]
    public abstract class PinballElement : ICloneable
    {
        [DataMember]
        public int Width = 100;
        [DataMember]
        public int Height = 100;

        public float X { get { return Location.X; } set { Location.X = value; } }
        public float Y { get { return Location.Y; } set { Location.Y = value; } }

        [DataMember]
        public Vector2 Location = new Vector2();

        private PinballMachine _machine = null;

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
        public int Value { get; protected set; }

        public virtual void Update(long delta) {}

        public abstract void Draw(Graphics g);

        public virtual bool Contains(Point point)
        {
            var p = new Point(point.X - (int)X, point.Y - (int)Y);
            return Shape.Contains(p);
            return false;
            //throw new NotImplementedException();
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


        #region test
        public Rectangle Shape
        {
            get
            {
                var offset = new Point(Width, Height);

                using (Bitmap bm = new Bitmap((int)offset.X + Width, (int)offset.Y + Height))
                using (Graphics g = Graphics.FromImage(bm))
                {
                    g.TranslateTransform(offset.X, offset.Y);
                    Draw(g);

                    var res = TrimBitmap(bm);
                    res.X -= offset.X;
                    res.Y -= offset.Y;
                    return res;
                }
                

            }
        }

        static Rectangle TrimBitmap(Bitmap source)
        {
            Rectangle srcRect = default(Rectangle);
            System.Drawing.Imaging.BitmapData data = null;
            try
            {
                data = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                byte[] buffer = new byte[data.Height * data.Stride];
                Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);

                int xMin = int.MaxValue,
                    xMax = int.MinValue,
                    yMin = int.MaxValue,
                    yMax = int.MinValue;

                bool foundPixel = false;

                // Find xMin
                for (int x = 0; x < data.Width; x++)
                {
                    bool stop = false;
                    for (int y = 0; y < data.Height; y++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            xMin = x;
                            stop = true;
                            foundPixel = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Image is empty...
                if (!foundPixel)
                    return new Rectangle();

                // Find yMin
                for (int y = 0; y < data.Height; y++)
                {
                    bool stop = false;
                    for (int x = xMin; x < data.Width; x++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            yMin = y;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Find xMax
                for (int x = data.Width - 1; x >= xMin; x--)
                {
                    bool stop = false;
                    for (int y = yMin; y < data.Height; y++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            xMax = x;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                // Find yMax
                for (int y = data.Height - 1; y >= yMin; y--)
                {
                    bool stop = false;
                    for (int x = xMin; x <= xMax; x++)
                    {
                        byte alpha = buffer[y * data.Stride + 4 * x + 3];
                        if (alpha != 0)
                        {
                            yMax = y;
                            stop = true;
                            break;
                        }
                    }
                    if (stop)
                        break;
                }

                srcRect = Rectangle.FromLTRB(xMin, yMin, xMax, yMax);
            }
            finally
            {
                if (data != null)
                    source.UnlockBits(data);
            }

            return srcRect;
        }
#endregion
    }
}
