using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public abstract class Wormhole : PinballElement
    {
        public static WormholeEntry WormholeEntryPending { get; set; }
        public static WormholeExit WormholeExitPending { get; set; }

    }
}
