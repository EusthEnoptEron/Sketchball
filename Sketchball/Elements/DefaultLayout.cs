using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class DefaultLayout : IMachineLayout
    {
        private class LayoutElement : PinballElement
        {
            internal LayoutElement()
            {
                X = Y = 0;
            }

            public override void Draw(Graphics g)
            {
            }
        }

        public int Width
        {
            get { return 470; }
        }

        public int Height
        {
            get { return 545; }
        }

        public BoundingContainer Bounds
        {
            get;
            private set;
        }

        public void DrawBackground(Graphics g)
        {
            Bounds.boundingBoxes.ForEach((e) => { e.drawDEBUG(g, Pens.Black); });
        }

        public DefaultLayout()
        {
            Bounds = new BoundingContainer(new LayoutElement());

            var totalWidth = 235 * 2;

            Bounds.AddPolygon(
                0, 545,
                75, 145,
                137, 52,
                194, 21,
                235, 7.5f,

                totalWidth - 194, 21,
                totalWidth - 137, 52,
                totalWidth - 75, 145,
                totalWidth, 545
            );

        }

    }
}
