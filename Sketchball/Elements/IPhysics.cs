using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    interface IPhysics
    {
        /// <summary>
        /// Current velocity of the object on the projected coordinate system.
        /// </summary>
        Vector2 Velocity { get; set; }

        /// <summary>
        /// Mass of the object.
        /// </summary>
        float Mass { get; set; }
    }
}
