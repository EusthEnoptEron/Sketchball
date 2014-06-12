using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{
    /// <summary>
    /// Represents the default implementation of IMachineLayout.
    /// </summary>
    [DataContract]
    public class DefaultLayout : IMachineLayout
    {

        private StartingRamp _ramp;
        private List<PinballElement> _elements;


        private class Frame : PinballElement
        {
            private static readonly Size size = new Size(997, 1385);

            internal Frame() : base(0, 0) {}

            protected override void Init()
            {
                var totalWidth = (int)this.Width;
                int totalHeight = (int)this.Height;

                Vector p1 = new Vector(316, 1344);
                Vector p2 = new Vector(107, 1207);
                Vector p3 = new Vector(88, 361);
                Vector p4 = new Vector(125, 215);
                Vector p5 = new Vector(184, 127);
                Vector p6 = new Vector(262, 61);
                Vector p7 = new Vector(417, 26);
                Vector p8 = new Vector(601, 29);
                Vector p9 = new Vector(733, 55);
                Vector p10 = new Vector(832, 133);
                Vector p11 = new Vector(876, 199);
                Vector p12 = new Vector(898, 267);
                Vector p13 = new Vector(931, 487);
                Vector p14 = new Vector(952, 1385);

                Vector p15 = new Vector(799, 1233);
                Vector p16 = new Vector(569, 1348);



                BoundingLine bL1 = new BoundingLine(p1, p2);
                BoundingLine bL2 = new BoundingLine(p2, p3);
                BoundingLine bL3 = new BoundingLine(p3, p4);
                BoundingLine bL4 = new BoundingLine(p4, p5);
                BoundingLine bL5 = new BoundingLine(p5, p6);
                BoundingLine bL6 = new BoundingLine(p6, p7);
                BoundingLine bL7 = new BoundingLine(p7, p8);
                BoundingLine bL8 = new BoundingLine(p8, p9);
                BoundingLine bL9 = new BoundingLine(p9, p10);
                BoundingLine bL10 = new BoundingLine(p10, p11);
                BoundingLine bL11 = new BoundingLine(p11, p12);
                BoundingLine bL12 = new BoundingLine(p12, p13);
                BoundingLine bL13 = new BoundingLine(p13, p14);

                BoundingLine bL14 = new BoundingLine(p15, p16);

                this.BoundingContainer.AddBoundingBox(bL1);
                this.BoundingContainer.AddBoundingBox(bL2);
                this.BoundingContainer.AddBoundingBox(bL3);
                this.BoundingContainer.AddBoundingBox(bL4);
                this.BoundingContainer.AddBoundingBox(bL5);
                this.BoundingContainer.AddBoundingBox(bL6);
                this.BoundingContainer.AddBoundingBox(bL7);
                this.BoundingContainer.AddBoundingBox(bL8);
                this.BoundingContainer.AddBoundingBox(bL9);
                this.BoundingContainer.AddBoundingBox(bL10);
                this.BoundingContainer.AddBoundingBox(bL11);
                this.BoundingContainer.AddBoundingBox(bL12);
                this.BoundingContainer.AddBoundingBox(bL13);
                this.BoundingContainer.AddBoundingBox(bL14);

                Scale = 1 / 2f;
            }

            protected override Size BaseSize
            {
                get { return size; }
            }

            protected override void InitResources()
            {
                Image = Booster.OptimizeWpfImage("TableSlim.png");
            }
        }

        public DefaultLayout()
        {
            Init();
        }

       
        private void Init()
        {
            _elements = new List<PinballElement>();
            _elements.Add(new Frame());

            // Add ramp
            _ramp = new StartingRamp();
            _elements.Add(_ramp);

            _ramp.X = Width - _ramp.Width -4;
            _ramp.Y = Height - _ramp.Height;


            // Add flippers
            Flipper lflipper = new LeftFlipper() { X = 148, Y = Height - 100 };
            _elements.Add(lflipper);

            Flipper rflipper = new RightFlipper() { X = 227, Y = Height - 100 };
            _elements.Add(rflipper);

            //Add Hole
            Hole h = new WideHole() { X = 148-40, Y = Height - 15 };
            h.Scale = .4f;
            _elements.Add(h);
        }

        public int Width
        {
            get { return 997 / 2; }
        }

        public int Height
        {
            get { return 1385/2; }
        }

        public StartingRamp Ramp
        {
            get { return _ramp; }
        }
        /// <summary>
        /// Initializes the machine with a layout.
        /// !!! Only use once on a machine !!!
        /// </summary>
        /// <param name="machine"></param>
        public void Apply(PinballMachine machine)
        {
            machine.StaticElements.Clear();
            foreach (PinballElement el in _elements) machine.StaticElements.Add(el);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            Init();
        }
    }
}
