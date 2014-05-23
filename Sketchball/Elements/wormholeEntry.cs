using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class WormholeEntry : PinballElement
    {
        public WormholeExit WormholeExit { get; set; }
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.SWomholeEntry);

        private static readonly Size size = new Size(30, 30);
        private static Image image = Booster.OptimizeImage(Properties.Resources.WormholeEntry, size.Width);

        protected override Size BaseSize
        {
            get { return size; }
        }


        public WormholeEntry()
        {
            this.pureIntersection = true;
        }

        protected override void OnDraw(Graphics g)
        {
            /* g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, Pens.Red);
            });
            g.TranslateTransform(X, Y);*/
            g.DrawImage(image, 0, 0, Width, Height);
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(15, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
        }

        public override void notifyIntersection(Ball b)
        {
            b.Location = this.WormholeExit.Location + new Vector2(this.WormholeExit.Width / 2, this.WormholeExit.Height / 2);
            player.Play();
        }
    }
}
