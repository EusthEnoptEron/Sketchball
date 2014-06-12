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
    /// <summary>
    /// Layout for a pinball machine. Every instance can be used on only one machine.
    /// </summary>
    public interface IMachineLayout : ICloneable
    {
        /// <summary>
        /// Gets the width of the layout design.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the layout design.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets a reference to the starting ramp associated with this layout.
        /// </summary>
        StartingRamp Ramp { get; }
        

        /// <summary>
        /// Applies the layout by filling the machine's static elements list.
        /// </summary>
        /// <param name="machine"></param>
        void Apply(PinballMachine machine);
    }
}
