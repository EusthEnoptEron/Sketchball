using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Elements
{
    public class Hole : PinballElement
    {
        private static readonly Size size = new Size(50, 50);
        private static System.Windows.Media.ImageSource imageS = Booster.OptimizeWpfImage("hole.png");
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.SHole);

        public Hole()  : base(100, 100)
        {
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(25, new Vector(0, 0));
            this.boundingContainer.AddBoundingBox(bC);
            bC.AssignToContainer(this.boundingContainer);
            this.pureIntersection = true;
        }

        protected override Size BaseSize
        {
            get { return size; }
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            g.DrawImage(imageS, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
        }

        public override void notifyIntersection(Ball b)
        {
            b.Location = new Vector(0, 2000);
            player.Play();
        }
    }
}
