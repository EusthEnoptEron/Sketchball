﻿using Sketchball.Collision;
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
    public class LeftFlipper : Flipper
    {
        private float factor = 800 / 70;

        public LeftFlipper()
        {
            Trigger = Keys.A;
            DebugTrigger = Keys.Q;
        }

        protected override void InitBounds()
        {
            int r1 = 70;
            int r2 = 16;
            Vector2 mitteKreis = new Vector2(140, 295);
            Vector2 obenKreisLinie = new Vector2(180, 241);
            Vector2 p3 = new Vector2(678, 505);
            Vector2 p4 = new Vector2(700, 534);
            Vector2 circle2 = new Vector2(692,546);
            Vector2 p5 = new Vector2(692, 561);
            Vector2 p6 = new Vector2(645, 566);
            Vector2 p7 = new Vector2(116, 355);
           

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

            BoundingCircle bC1 = new BoundingCircle(r1, mitteKreis- new Vector2(r1,r1));
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

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);
           // g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, Pens.Red);
            });
            //g.TranslateTransform(X, Y);
            
            g.DrawImage(Properties.Resources.FlipperLeft, 0, 0, Width, Height);
        }

        protected override Vector2 Origin
        {
            get
            {
                Vector2 pictureRotPos = new Vector2(157, 304);
                
                return pictureRotPos / factor;
            }
        }

    }
}
