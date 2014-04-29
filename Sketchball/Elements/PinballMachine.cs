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
                foreach (PinballElement el in StaticElements)
                    yield return el;
                foreach (PinballElement el in DynamicElements)
                    yield return el;
                foreach (PinballElement el in Balls)
                    yield return el;
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
            DynamicElements = new ElementCollection(this);
            Balls = new ElementCollection(this);

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

        /// <summary>
        /// Draws the pinball elements and all its components.
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g)
        {
            GraphicsState gsave = g.Save();
            try
            {
                g.IntersectClip(new Rectangle(0, 0, Width, Height));

               // g.DrawRectangle(Pens.Black, 0, 0, Width - 1, Height - 1);

                // Draw contours


                foreach (PinballElement element in Elements)
                {
                    GraphicsState gstate = g.Save();

                    g.TranslateTransform(element.X, element.Y);
                    element.Draw(g);

                    g.Restore(gstate);
                }

            }
            finally
            {
                g.Restore(gsave);
            }
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
        /// Disposes the pinball machine and frees all ressources used by it.
        /// </summary>
        public void Dispose()
        {
            DynamicElements.Clear();
            Balls.Clear();
        }
        
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

        internal static PinballMachine FromStream(Stream input)
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
            return true;
        }
    }

}
