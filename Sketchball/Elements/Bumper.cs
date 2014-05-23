using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Bumper : PinballElement
    {
        private static Image image = Booster.OptimizeImage(Properties.Resources.Bumper, 50, 50);
        private static readonly Size size = new Size(30, 30);
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.SBumper);

        public Bumper()
        {
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            /* g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, Pens.Red);
            });
            g.TranslateTransform(X, Y);*/
            g.DrawImage(image, 0, 0, BaseWidth, BaseHeight);
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(15, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
        }

        protected override Size BaseSize
        {
            get { return size; }
        }

        public override void notifyIntersection(Ball b)
        {
            player.Play();
        }
    }
}
