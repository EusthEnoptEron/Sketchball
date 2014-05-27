using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    [DataContract]
    public class DefaultLayout : IMachineLayout
    {

        private StartingRamp _ramp;
        private List<PinballElement> _elements;


        private class Frame : PinballElement
        {
            private static readonly Size size = new Size(997, 1385);
            private static Image image = Booster.OptimizeImage(Properties.Resources.TableSlim, size.Width, size.Height);
       


            internal Frame() : base(0, 0) {}

            protected override void Init()
            {
                var totalWidth = this.Width;
                int totalHeight = this.Height;

                Vector2 p1 = new Vector2(316, 1344);
                Vector2 p2 = new Vector2(107, 1207);
                Vector2 p3 = new Vector2(88, 361);
                Vector2 p4 = new Vector2(125, 215);
                Vector2 p5 = new Vector2(184, 127);
                Vector2 p6 = new Vector2(262, 61);
                Vector2 p7 = new Vector2(417, 26);
                Vector2 p8 = new Vector2(601, 29);
                Vector2 p9 = new Vector2(733, 55);
                Vector2 p10 = new Vector2(832, 133);
                Vector2 p11 = new Vector2(876, 199);
                Vector2 p12 = new Vector2(898, 267);
                Vector2 p13 = new Vector2(931, 487);
                Vector2 p14 = new Vector2(952, 1385);

                Vector2 p15 = new Vector2(799, 1233);
                Vector2 p16 = new Vector2(569, 1348);



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

                this.boundingContainer.addBoundingBox(bL1);
                this.boundingContainer.addBoundingBox(bL2);
                this.boundingContainer.addBoundingBox(bL3);
                this.boundingContainer.addBoundingBox(bL4);
                this.boundingContainer.addBoundingBox(bL5);
                this.boundingContainer.addBoundingBox(bL6);
                this.boundingContainer.addBoundingBox(bL7);
                this.boundingContainer.addBoundingBox(bL8);
                this.boundingContainer.addBoundingBox(bL9);
                this.boundingContainer.addBoundingBox(bL10);
                this.boundingContainer.addBoundingBox(bL11);
                this.boundingContainer.addBoundingBox(bL12);
                this.boundingContainer.addBoundingBox(bL13);
                this.boundingContainer.addBoundingBox(bL14);

                Scale = 1 / 2f;
            }

            protected override void OnDraw(Graphics g)
            {
                g.DrawImage(image, 0, 0, size.Width, size.Height);

            }

            protected override Size BaseSize
            {
                get { return size; }
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

            _ramp.X = Width - _ramp.Width-4 ;
            _ramp.Y = Height - _ramp.Height;


            // Add flippers
            Flipper lflipper = new LeftFlipper() { X = 148, Y = Height - 85 };
            _elements.Add(lflipper);

            Flipper rflipper = new RightFlipper() { X = 227, Y = Height - 85 };
            _elements.Add(rflipper);

            //Add Hole
            Hole h = new Hole() { X = 148-40, Y = Height - 25 };
            h.Scale = 4.5f;
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
