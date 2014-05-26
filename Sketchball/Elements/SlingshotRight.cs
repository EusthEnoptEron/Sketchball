using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Elements
{
    public class SlingshotRight : PinballElement
    {
        private static System.Windows.Media.ImageSource imageS = Booster.OptimizeWpfImage("SlingshotRight.png");

        private static readonly Size size = new Size(110, 110);
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.SSlingshot);

        protected override Size BaseSize
        {
            get { return size; }
        }

        public SlingshotRight()
        {
        }

        protected override void Init()
        {
            //Todo: Correct
            Vector2 p1 = new Vector2(311, 59);
            Vector2 p2 = new Vector2(353, 239);
            Vector2 p3 = new Vector2(123, 367);
            Vector2 p4 = new Vector2(72, 337);

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
            g.DrawImage(imageS, new Rect(0, 0, BaseWidth, BaseHeight));
        }
    }
}
