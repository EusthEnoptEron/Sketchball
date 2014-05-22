﻿using Sketchball.Collision;
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

        public WormholeEntry()
            : base()
        {
            Width = 30;
            Height = 30;
            this.pureIntersection = true;

            this.setLocation(new Vector2(0, 100));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            /* g.TranslateTransform(-X, -Y);
            boundingContainer.boundingBoxes.ForEach((b) =>
            {
                b.drawDEBUG(g, Pens.Red);
            });
            g.TranslateTransform(X, Y);*/
            g.DrawImage(Booster.OptimizeImage(Properties.Resources.WormholeEntry,Width,Height), 0, 0, Width, Height);
        }

        protected override void InitBounds()
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
