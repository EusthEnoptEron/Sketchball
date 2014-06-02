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
    public class SlingshotLeft : PinballElement
    {
        private static ImageSource imageS = Booster.OptimizeWpfImage("SlingshotLeft.png");

        private static readonly Size size = new Size(110, 110);
        private static readonly SoundPlayer player = new SoundPlayer(Properties.Resources.SSlingshot);


        protected override Size BaseSize
        {
            get { return size; }
        }

        public SlingshotLeft()
        {
        }

        protected override void Init()
        {
            Vector p1 = new Vector(127, 60);
            Vector p2 = new Vector(364, 335);
            Vector p3 = new Vector(316, 366);
            Vector p4 = new Vector(87, 239);

            p1 /= 4;
            p2 /= 4;
            p3 /= 4;
            p4 /= 4;

            BoundingLine bL1 = new BoundingLine(p1, p2);
            BoundingLine bL2 = new BoundingLine(p2, p3);
            BoundingLine bL3 = new BoundingLine(p3, p4);
            BoundingLine bL4 = new BoundingLine(p4, p1);

            bL1.BounceFactor = 2;
            this.BoundingContainer.AddBoundingBox(bL1);
            this.BoundingContainer.AddBoundingBox(bL2);
            this.BoundingContainer.AddBoundingBox(bL3);
            this.BoundingContainer.AddBoundingBox(bL4);

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
