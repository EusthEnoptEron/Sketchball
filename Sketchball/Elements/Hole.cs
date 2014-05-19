using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Hole : PinballElement
    {
        private static readonly Size size = new Size(50, 50);
        private static Image image = Booster.OptimizeImage(Properties.Resources.hole, 50, 50);


        public Hole()
            : base(100, 100)
        {
        }

        protected override void OnDraw(System.Drawing.Graphics g)
        {
            g.DrawImage(image, 0, 0, BaseWidth, BaseHeight);
        }

        protected override void Init()
        {
            BoundingCircle bC = new BoundingCircle(25, new Vector2(0, 0));
            this.boundingContainer.addBoundingBox(bC);
            bC.assigneToContainer(this.boundingContainer);
        }

        protected override Size BaseSize
        {
            get { return size; }
        }
    }
}
