using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Elements
{
    public class SlingshotLeft : PinballElement
    {
        private static System.Windows.Media.ImageSource imageS = Booster.OptimizeWpfImage("SlingshotLeft.png");

        private static readonly Size size = new Size(110, 110);
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.SSlingshot);


        protected override Size BaseSize
        {
            get { return size; }
        }

        public SlingshotLeft()
        {
        }

        protected override void Init()
        {
            Vector2 p1 = new Vector2(127, 60);
            Vector2 p2 = new Vector2(364, 335);
            Vector2 p3 = new Vector2(316, 366);
            Vector2 p4 = new Vector2(87, 239);

            p1 /= 4;
            p2 /= 4;
            p3 /= 4;
            p4 /= 4;

            BoundingLine bL1 = new BoundingLine(p1, p2);
            BoundingLine bL2 = new BoundingLine(p2, p3);
            BoundingLine bL3 = new BoundingLine(p3, p4);
            BoundingLine bL4 = new BoundingLine(p4, p1);

            bL1.BounceFactor = 2;
            this.boundingContainer.AddBoundingBox(bL1);
            this.boundingContainer.AddBoundingBox(bL2);
            this.boundingContainer.AddBoundingBox(bL3);
            this.boundingContainer.AddBoundingBox(bL4);

        }

        public override void notifyIntersection(Ball b)
        {
            player.Play();
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {

            g.DrawImage(imageS, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
        }
    }
}
