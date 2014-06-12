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
    /// Represents a bumper that bounces the ball away.
    /// </summary>
    [DataContract]
    public class Bumper : PinballElement
    {
        private static readonly Size size = new Size(30, 30);
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SBumper);

        public Bumper()
        {
            Value = 10;
        }

        protected override void Init()
        {
            BoundingCircleZentripush bC = new BoundingCircleZentripush(15, new Vector(0, 0));
            this.BoundingContainer.AddBoundingBox(bC);
            bC.AssignToContainer(this.BoundingContainer);
        }

        protected override void InitResources()
        {
            Image = Booster.LoadImage("BumperSpiral.png");
        }

        protected override Size BaseSize
        {
            get { return size; }
        }

        public override void OnIntersection(Ball b)
        {
            GameWorld.Sfx.Play(player);
        }
    }
}
