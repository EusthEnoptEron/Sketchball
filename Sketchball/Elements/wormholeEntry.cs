using Sketchball.Collision;
using Sketchball.GameComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{
    /// <summary>
    /// Represents a wormhole entry that sends the ball to the other end.
    /// </summary>
    [DataContract]
    public class WormholeEntry : Wormhole
    {
        /// <summary>
        /// Gets or sets the exit associated with this entry.
        /// </summary>
        [DataMember]
        [Browsable(false)]
        public WormholeExit WormholeExit { get; set; }
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SWomholeEntry);
        private static readonly Size size = new Size(30, 30);

        protected override Size BaseSize
        {
            get { return size; }
        }


        public WormholeEntry()
        {
           
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(10, new Vector(5, 5));
            this.BoundingContainer.AddBoundingBox(bC);
            this.pureIntersection = true;
            bC.AssignToContainer(this.BoundingContainer);
        }

        public override void OnIntersection(Ball b)
        {
            b.Location = this.WormholeExit.Location + new Vector(this.WormholeExit.Width / 2, this.WormholeExit.Height / 2);
            GameWorld.Sfx.Play(player);
        }

        protected override void InitResources()
        {
            Image = Booster.LoadImage("WormholeEntry.png");
        }

        // Try to find an exit that fits our needs.
        protected override void EnterEditor(PinballMachine machine)
        {
            if (machine.DynamicElements == null) return; // may happen if we're deserializing
            if (WormholeExit == null)
            {
                // Let's search for one
                var exits = machine.DynamicElements.OfType<WormholeExit>();
                var occupiedExits = machine.DynamicElements.OfType<WormholeEntry>()
                                                           .Where((el) => { return el.WormholeExit != null; })
                                                           .Select((el) => { return el.WormholeExit; });

                var freeExits = exits.Except(occupiedExits).ToList();

                if (freeExits.Count > 0)
                {
                    WormholeExit = freeExits.Last();
                }
            }
        }

        public override void Update(double delta)
        {
            BaseRotation += 360 * 2 * -delta;
        }

    }
}
