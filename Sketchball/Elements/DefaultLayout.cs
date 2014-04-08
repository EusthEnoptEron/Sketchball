using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class DefaultLayout : IMachineLayout
    {
        private class Frame : PinballElement
        {
            internal Frame() : base(0,0)
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

        public int Width
        {
            get { return 470; }
        }

        public int Height
        {
            get { return 545; }
        }

        /// <summary>
        /// Initializes the machine with a layout.
        /// !!! Only use once on a machine !!!
        /// </summary>
        /// <param name="machine"></param>
        public void Apply(PinballMachine machine)
        {
            machine.StaticElements.Add(new Frame());

            // Add ramp
            StartingRamp ramp = new StartingRamp();
            machine.StaticElements.Add(ramp);

            ramp.X = Width - ramp.Width - 5;
            ramp.Y = Height - ramp.Height - 5;

            // Add flippers
            Flipper lflipper = new LeftFlipper() { X = 150, Y = Height - 100 };
            machine.StaticElements.Add(lflipper);

            Flipper rflipper = new RightFlipper() { X = 300, Y = Height - 100 };
            machine.StaticElements.Add(rflipper);
        }
        
        private void AddDynamicObjects(PinballMachine machine) 
        {

        }

        private void AddDynamicObjects(PinballGameMachine machine)
        {

        }

  
    }
}
