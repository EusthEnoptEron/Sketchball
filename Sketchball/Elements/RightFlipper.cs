using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Elements
{
    [DataContract]
    public class RightFlipper : Flipper
    {
        private float factor = 800 / 70;
        private static Image image = Booster.OptimizeImage(Properties.Resources.FlipperRight, 100);

        public RightFlipper()
        {
        }

        protected override void Init()
        {
            Trigger = Keys.D;
            DebugTrigger = Keys.E;
            RotationRange = -RotationRange;


            Vector2 offset = new Vector2(-10, -10);
            int r1 = 70;
            int r2 = 16;
            Vector2 mitteKreis = new Vector2(-(140 - 400) + 400, 295) + offset;
            Vector2 obenKreisLinie = new Vector2(-(180 - 400) + 400, 241) + offset;
            Vector2 p3 = new Vector2(-(678 - 400) + 400, 505) + offset;
            Vector2 p4 = new Vector2(-(700 - 400) + 400, 534) + offset;
            Vector2 circle2 = new Vector2(-(692 - 400) + 400, 546) + offset;
            Vector2 p5 = new Vector2(-(692 - 400) + 400, 561) + offset;
            Vector2 p6 = new Vector2(-(645 - 400) + 400, 566) + offset;
            Vector2 p7 = new Vector2(-(116 - 400) + 400, 355) + offset;

          
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

            BoundingCircle bC1 = new BoundingCircle(r1, mitteKreis - new Vector2(r1, r1));
            BoundingLine bL1 = new BoundingLine(obenKreisLinie, p3);
            BoundingLine bL2 = new BoundingLine(p3, p4);
            BoundingCircle bC2 = new BoundingCircle(r2, circle2 - new Vector2(r2, r2));
            BoundingLine bL3 = new BoundingLine(p5, p6);
            BoundingLine bL4 = new BoundingLine(p6, p7);

            //bL1.bounceFactor = 2;
            this.boundingContainer.addBoundingBox(bC1);
            this.boundingContainer.addBoundingBox(bL1);
            this.boundingContainer.addBoundingBox(bL2);
            this.boundingContainer.addBoundingBox(bC2);
            this.boundingContainer.addBoundingBox(bL3);
            this.boundingContainer.addBoundingBox(bL4);
        }

        protected override Vector2 Origin
        {
            get
            {
                Vector2 pictureRotPos = new Vector2(-(157 - 400) + 400, 304);

                return pictureRotPos / factor;
            }
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            base.OnDraw(g);
            g.DrawImage(image, 0, 0, BaseWidth, BaseHeight);
        }
    }
    
}
