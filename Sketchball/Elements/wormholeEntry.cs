using Sketchball.Collision;
using Sketchball.GameComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.Elements
{
    [DataContract]
    public class WormholeEntry : Wormhole
    {
        [DataMember]
        public WormholeExit WormholeExit { get; set; }
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SWomholeEntry);


        private static readonly Size size = new Size(30, 30);

        protected override Size BaseSize
        {
            get { return size; }
        }


        public WormholeEntry()
        {
            this.pureIntersection = true;
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(15, new Vector(0, 0));
            this.BoundingContainer.AddBoundingBox(bC);
            this.pureIntersection = true;
            bC.AssignToContainer(this.BoundingContainer);
        }

        public override void notifyIntersection(Ball b)
        {
            b.Location = this.WormholeExit.Location + new Vector(this.WormholeExit.Width / 2, this.WormholeExit.Height / 2);
            player.Play();
        }

        protected override void InitResources()
        {
            Image = Booster.OptimizeWpfImage("WormholeEntry.png");
        }
    }
}
