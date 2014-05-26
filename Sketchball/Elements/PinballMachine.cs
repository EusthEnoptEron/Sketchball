using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{

    [DataContract(IsReference=true)]
    public class PinballMachine : ICloneable, IDisposable
    {

        // 500px = 1m
        public const float PIXELS_TO_METERS_RATIO = 500f / 1;
        
        [DataMember]
        public ElementCollection DynamicElements { get; private set; }
        public ElementCollection StaticElements { get; private set; }
        public ElementCollection Balls { get; private set; }

        public IEnumerable<PinballElement> Elements
        {
            get
            {
                lock (StaticElements)
                {
                    foreach (PinballElement el in StaticElements)
                        yield return el;
                }
                lock(DynamicElements) {
                    foreach (PinballElement el in DynamicElements)
                        yield return el;
                }
                lock(Balls) {
                    foreach (PinballElement el in Balls)
                        yield return el;
                }
            }
        }

        [DataMember]
        public IMachineLayout Layout { get; private set; }

        // -------  DERIVED PROPERTIES            
        protected StartingRamp Ramp { get { return Layout.Ramp; } }
        public int Width { get { return Layout.Width; } }
        public int Height { get { return Layout.Height; } }


        [DataMember]
        public float Gravity = 9.81f;

           
        /// <summary>
        /// Tilt of the pinball machine in radians.
        /// </summary>
        [DataMember]
        public float Angle = (float)(Math.PI / 180 * 10);

       
        public PinballMachine() : this(new DefaultLayout()) {}

        public PinballMachine(IMachineLayout layout)
        {
            Layout = layout;
            Init();
        }

        private void Init()
        {
            StaticElements = new ElementCollection(this);
            Balls = new ElementCollection(this);
            if (DynamicElements == null) DynamicElements = new ElementCollection(this);

            Layout.Apply(this);
        }


        /// <summary>
        /// Gets the calculated acceleration based on the gravity and the angle.
        /// </summary>
        public Vector2 Acceleration
        {
            get
            {
                return new Vector2(0, (float)Math.Sin(Angle) * Gravity * PIXELS_TO_METERS_RATIO);
            }
        }

        public virtual void Draw(System.Windows.Media.DrawingContext g)
        {
            g.PushClip(new System.Windows.Media.RectangleGeometry(new System.Windows.Rect(0, 0, Width, Height)));
            g.DrawRectangle(System.Windows.Media.Brushes.White, null, new System.Windows.Rect(0, 0, Width, Height));

            // Draw contours
            //TODO take away red border
           System.Windows.Media.Pen pen = new System.Windows.Media.Pen(System.Windows.Media.Brushes.LightGray, 1);
            for (int y = 0; y <= Height; y += 10)
            {
                g.DrawLine(pen, new System.Windows.Point(0, y), new System.Windows.Point(Width, y));
            }

            for (int x = 0; x <= Width; x += 10)
            {
                g.DrawLine(pen, new System.Windows.Point(x, 0), new System.Windows.Point(x, Height));
            }



            foreach (PinballElement element in Elements)
            {
                g.PushTransform(new System.Windows.Media.TranslateTransform(element.X, element.Y));
                element.Draw(g);
                g.Pop();
            }

            g.Pop();
        }


        public void Add(PinballElement element)
        {
            DynamicElements.Add(element);
        }

        public bool Remove(PinballElement element)
        {
            return DynamicElements.Remove(element);
        }


        public object Clone()
        {
            PinballMachine machine = new PinballMachine(Layout);

            machine.Angle = Angle;
            machine.Gravity = Gravity;

            // Clone elements
            // (clone static ones just to be sure their settings are OK)
            machine.StaticElements = new ElementCollection(machine);
            foreach (PinballElement element in StaticElements)
            {
                machine.StaticElements.Add((PinballElement)element.Clone());
            }

            machine.DynamicElements = new ElementCollection(machine);
            foreach (PinballElement element in DynamicElements)
            {
                machine.DynamicElements.Add((PinballElement)element.Clone());
            }
            
            return machine;
        }

        public void addBall(Ball b)
        {
            this.Balls.Add(b);
        }


        /// <summary>
        /// Disposes the pinball machine and frees all resources used by it.
        /// </summary>
        public void Dispose()
        {
            StaticElements.Clear();
            DynamicElements.Clear();
            Balls.Clear();
        }


#region Serialization

        public void Save(string path)
        {
            using (var stream = File.OpenWrite(path))
            {
                Save(stream);
            }
        }

        public void Save(Stream output)
        {
            NetDataContractSerializer serializer = new NetDataContractSerializer();
            serializer.WriteObject(output, this);
        }

        public static PinballMachine FromFile(string path)
        {
            PinballMachine pbm;
            using (var stream = File.OpenRead(path))
            {
                pbm = FromStream(stream);
            }
            return pbm;
        }

        public static PinballMachine FromStream(Stream input)
        {
            NetDataContractSerializer serializer = new NetDataContractSerializer();
            return (PinballMachine)serializer.ReadObject(input);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Init();
        }


        internal bool IsValid()
        {
            foreach(PinballElement p in this.Elements)
            {
                if (p.GetType() == typeof(WormholeEntry))
                {
                    if (((WormholeEntry)p).WormholeExit == null)
                    {
                        return false;
                    }
                    else
                    {
                        bool isthere = false;
                        foreach (PinballElement p2 in this.Elements)
                        {
                            if (p2.Equals(((WormholeEntry)p).WormholeExit))
                            {
                                isthere = true;
                            }
                        }
                        if (!isthere)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

#endregion

    }

}
