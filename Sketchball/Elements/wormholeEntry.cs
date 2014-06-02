using Sketchball.Collision;
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
    public class WormholeEntry : PinballElement
    {
        public WormholeExit WormholeExit { get; set; }
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SWomholeEntry);


        private static readonly Size size = new Size(30, 30);
        private static ImageSource imageS = Booster.OptimizeWpfImage("WormholeEntry.png");

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
            this.boundingContainer.AddBoundingBox(bC);
            bC.AssignToContainer(this.boundingContainer);
        }

        public override void notifyIntersection(Ball b)
        {
            b.Location = this.WormholeExit.Location + new Vector(this.WormholeExit.Width / 2, this.WormholeExit.Height / 2);
            player.Play();
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            g.DrawImage(imageS, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
        }
    }
}
