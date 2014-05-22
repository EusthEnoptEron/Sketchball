using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class SlingshotRight : PinballElement
    {

        private static Image image = Booster.OptimizeImage(Properties.Resources.SlingshotRight, 150);
        private System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.SSlingshot);

        public SlingshotRight()
            : base()
        {
            Width = 110;
            Height = 110;

            this.setLocation(new Vector2(100, 100));
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawImage(image, 0, 0, Width, Height);
        }

        protected override void InitBounds()
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

            bL1.bounceFactor = 2;
            this.boundingContainer.addBoundingBox(bL1);
            this.boundingContainer.addBoundingBox(bL2);
            this.boundingContainer.addBoundingBox(bL3);
            this.boundingContainer.addBoundingBox(bL4);

        }

        public override void notifyIntersection(Ball b)
        {
            player.Play();
        }
    }
}
