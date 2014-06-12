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
    /// <summary>
    /// Represents a right flipper.
    /// </summary>
    [DataContract]
    public class RightFlipper : Flipper
    {
        private const float factor = 800 / 70f;

        public RightFlipper()
        {
            Trigger = Keys.D;
        }

        protected override void Init()
        {
            base.Init();
            RotationRange = -RotationRange;

            Vector offset = new Vector(-10, -10);
            int r1 = 70;
            int r2 = 16;
            Vector mitteKreis = new Vector(-(140 - 400) + 400, 295) + offset;
            Vector obenKreisLinie = new Vector(-(180 - 400) + 400, 241) + offset;
            Vector p3 = new Vector(-(678 - 400) + 400, 505) + offset;
            Vector p4 = new Vector(-(700 - 400) + 400, 534) + offset;
            Vector circle2 = new Vector(-(692 - 400) + 400, 546) + offset;
            Vector p5 = new Vector(-(692 - 400) + 400, 561) + offset;
            Vector p6 = new Vector(-(645 - 400) + 400, 566) + offset;
            Vector p7 = new Vector(-(116 - 400) + 400, 355) + offset;
            Vector peak = new Vector(-(707 - 400) + 400, 553) + offset;

          
            r1 = (int)(r1 / factor);
            r2 = (int)(r2 / factor);
            mitteKreis /= factor;
            obenKreisLinie /= factor;
            p3 /= factor;
            p4 /= factor;
            circle2 /= factor;
            p5 /= factor;
            p6 /= factor;
            p7 /= factor;
            peak /= factor;

            BoundingCircle bC1 = new BoundingCircle(r1, mitteKreis - new Vector(r1, r1));
            BoundingLine bL1 = new BoundingLine(obenKreisLinie, peak);
            BoundingLine bL2 = new BoundingLine(p3, p4);
            BoundingCircle bC2 = new BoundingCircle(r2, circle2 - new Vector(r2, r2));
            BoundingLine bL3 = new BoundingLine(p5, p6);
            BoundingLine bL4 = new BoundingLine(peak, p7);

            //bL1.bounceFactor = 2;
            this.BoundingContainer.AddBoundingBox(bC1);
            this.BoundingContainer.AddBoundingBox(bL1);
            //this.BoundingContainer.AddBoundingBox(bL2);
           // this.BoundingContainer.AddBoundingBox(bC2);
            //this.BoundingContainer.AddBoundingBox(bL3);
            this.BoundingContainer.AddBoundingBox(bL4);
        }

        protected override Vector origin
        {
            get
            {
                Vector pictureRotPos = new Vector(-(157 - 400) + 400, 304);
                return pictureRotPos / factor;
            }
        }

        protected override void InitResources()
        {
            Image = Booster.LoadImage("FlipperRight.png");
        }

        protected override void OnDraw(System.Windows.Media.DrawingContext g)
        {
            base.OnDraw(g);

            g.DrawImage(Image, new System.Windows.Rect(0, 0, BaseWidth, BaseHeight));
        }
    }
    
}
