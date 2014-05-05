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
            internal Frame() : base(0,0) {}

            protected override void InitBounds()
            {
                var totalWidth = 235 * 2;
                boundingContainer.AddPolygon(
                   0, 545,
                   75, 145,
                   137, 52,
                   194, 21,
                   235, 7.5f,

                   totalWidth - 194, 21,
                   totalWidth - 137, 52,
                   totalWidth - 75, 145,
                   totalWidth, 545
               );
            }

            public override void Draw(Graphics g)
            {
                boundingContainer.boundingBoxes.ForEach((e) => { e.drawDEBUG(g, Pens.Black); });
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

            _ramp.X = Width - _ramp.Width - 5;
            _ramp.Y = Height - _ramp.Height - 5;



            // Add flippers
            Flipper lflipper = new LeftFlipper() { X = 50, Y = Height - 200 };
            _elements.Add(lflipper);

            Flipper rflipper = new RightFlipper() { X = 250, Y = Height - 200 };
            _elements.Add(rflipper);
        }

        public int Width
        {
            get { return 470; }
        }

        public int Height
        {
            get { return 545; }
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
