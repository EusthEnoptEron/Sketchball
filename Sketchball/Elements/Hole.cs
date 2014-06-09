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
    public class Hole : PinballElement
    {
        private static readonly Size size = new Size(50, 50);
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SHole);

        public Hole()  : base(100, 100)
        {
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(20, new Vector(0, 0));
            this.BoundingContainer.AddBoundingBox(bC);
            bC.AssignToContainer(this.BoundingContainer);
            this.pureIntersection = true;
        }

        protected override void InitResources()
        {
            Image = Booster.OptimizeWpfImage("hole.png");
        }

        protected override Size BaseSize
        {
            get { return size; }
        }

        public override void notifyIntersection(Ball b)
        {
            b.Location = new Vector(0, 2000);
            GameWorld.Sfx.Play(player);
        }
    }
}
