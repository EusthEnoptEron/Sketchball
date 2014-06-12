using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{
    /// <summary>
    /// Represents a destination for a wormhole entry.
    /// </summary>
    [DataContract]
    public class WormholeExit : Wormhole
    {
        private static readonly Size size = new Size(30, 30);

        /// <summary>
        /// Gets a list of entries associated with this exit.
        /// </summary>
        [Browsable(false)]
        public IEnumerable<WormholeEntry> Entries { 
            get {
                if (World == null) return new WormholeEntry[0];
                else
                {
                    return World.DynamicElements.OfType<WormholeEntry>().Where((el) => { return el.WormholeExit == this; });
                }
            }
        }
        protected override Size BaseSize
        {
            get { return size; }
        }


        public WormholeExit()
        {
        }

        protected override void Init()
        {
            this.pureIntersection = true;

            BoundingCircle bC = new BoundingCircle(15, new Vector(0, 0));
            this.BoundingContainer.AddBoundingBox(bC);
            bC.AssignToContainer(this.BoundingContainer);
            this.pureIntersection = true;
        }

        protected override void InitResources()
        {
            Image = Booster.LoadImage("WormholeExit.png");
        }

        // Tries to find entries that still need a destinatoin.
        protected override void EnterEditor(PinballMachine machine)
        {
            if (machine.DynamicElements == null) return; // if we're deserializing
            foreach (WormholeEntry entry in machine.DynamicElements.OfType<WormholeEntry>())
            {
                if (entry.WormholeExit == null) {
                    entry.WormholeExit = this;
                }
            }
        }

        // Removes itself from the references of entries.
        protected override void LeaveEditor(PinballMachine machine)
        {
            foreach (WormholeEntry entry in Entries)
            {
                entry.WormholeExit = null;    
            }
        }


        public override void Update(double delta)
        {
            BaseRotation += 360 * 2 * delta;
        }
    }
}
