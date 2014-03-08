using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    class PinballGameMachine : PinballMachine
    {
        public PinballGameMachine(PinballMachine machine) : base(machine.Bounds)
        {
            // Copy constructor
            foreach (PinballElement element in machine.Elements)
            {
                Elements.Add((PinballElement)element.Clone());
            }
        }

        public override void Update(long elapsed)
        {
            foreach (PinballElement element in Elements)
            {
                element.Update(elapsed);
            }

            // Find collisions
            // ...
        }
    }
}
