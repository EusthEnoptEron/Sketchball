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
        private static Image image = Booster.OptimizeImage(Properties.Resources.FlipperRight, 100);

        public RightFlipper()
        {
            Trigger = Keys.D;
            DebugTrigger = Keys.E;
            RotationRange = -RotationRange;
        }

        protected override void InitBounds()
        {
            //TODO correct
            int y1 = Height / 10 * 8;
            int recHeight = Height / 10 * 2;
            //set up of bounding box
            boundingContainer.AddPolygon(
                0, y1,
                Width, y1,
                Width, y1 + recHeight,
                0, y1 + recHeight,
                0, y1
            );
        }

        protected override Vector2 Origin
        {
            get
            {
                return new Vector2(Width, Height);
            }
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            base.Draw(g);
            g.DrawImage(image, 0, 0, Width, Height);
        }
    }
    
}
