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
    [DataContract]
    public class WormholeExit : Wormhole
    {
        private static readonly Size size = new Size(30, 30);
        public WormholeEntry WormholeEntry { get; set; }

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
            Image = Booster.OptimizeWpfImage("WormholeExit.png");
        }
    }
}
