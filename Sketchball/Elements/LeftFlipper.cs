using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Sketchball.Elements
{
    [DataContract]
    public class LeftFlipper : Flipper
    {
        private float factor = 800 / 70;
        private static Image image = Booster.OptimizeImage(Properties.Resources.FlipperLeft, 100);

        private static System.Windows.Media.ImageSource imageS = Booster.OptimizeWpfImage("FlipperLeft.png");

        public LeftFlipper()
        {
            Trigger = Keys.A;
            DebugTrigger = Keys.Q;
        }

        protected override void Init()
        {
            int r1 = 70;
            int r2 = 16;
            Vector mitteKreis = new Vector(140, 295);
            Vector obenKreisLinie = new Vector(180, 241);
            Vector p3 = new Vector(678, 505);
            Vector p4 = new Vector(700, 534);
            Vector circle2 = new Vector(692,546);
            Vector p5 = new Vector(692, 561);
            Vector p6 = new Vector(645, 566);
            Vector p7 = new Vector(116, 355);
           

            r1 = (int)(r1/factor);
            r2 = (int)(r2 / factor);
            mitteKreis /= factor;
            obenKreisLinie /= factor;
            p3 /= factor;
            p4 /= factor;
            circle2 /= factor;
            p5 /= factor;
            p6 /= factor;
            p7 /= factor;

            BoundingCircle bC1 = new BoundingCircle(r1, mitteKreis- new Vector(r1,r1));
            BoundingLine bL1 = new BoundingLine(obenKreisLinie, p3);
            BoundingLine bL2 = new BoundingLine(p3, p4);
            BoundingCircle bC2 = new BoundingCircle(r2, circle2 - new Vector(r2, r2));
            BoundingLine bL3 = new BoundingLine(p5, p6);
            BoundingLine bL4 = new BoundingLine(p6, p7);

            //bL1.bounceFactor = 2;
            this.BoundingContainer.AddBoundingBox(bC1);
            this.BoundingContainer.AddBoundingBox(bL1);
            this.BoundingContainer.AddBoundingBox(bL2);
            this.BoundingContainer.AddBoundingBox(bC2);
            this.BoundingContainer.AddBoundingBox(bL3);
            this.BoundingContainer.AddBoundingBox(bL4);

        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            base.OnDraw(g);
            g.DrawImage(imageS, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
        }

        protected override Vector Origin
        {
            get
            {
                Vector pictureRotPos = new Vector(157, 304);
                return pictureRotPos / factor;
            }
        }

    }
}
