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
    /// <summary>
    /// Represents a hole into which a ball can fall.
    /// </summary>
    [DataContract]
    public class Hole : PinballElement
    {
        private static readonly Size size = new Size(50, 50);
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SHole);

        public Hole()  : base(0, 0)
        {
        }

        protected override void Init()
        {
            int r = 16;
            BoundingCircle bC = new BoundingCircle(r, new Vector( (BaseWidth / 2) - r, (BaseHeight / 2) - r ));
            this.BoundingContainer.AddBoundingBox(bC);
            bC.AssignToContainer(this.BoundingContainer);
            this.pureIntersection = true;
        }

        protected override void InitResources()
        {
            Image = Booster.LoadImage("hole.png");
        }

        protected override Size BaseSize
        {
            get { return size; }
        }

        public override void OnIntersection(Ball b)
        {
            GameWorld.KillBall(b);
            GameWorld.Sfx.Play(player);
        }
    }
}
