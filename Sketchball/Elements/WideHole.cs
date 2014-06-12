using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball.Elements
{
    /// <summary>
    /// Represents a wide hole, which, in contrast to a normal hole, is rectangular.
    /// </summary>
    public class WideHole : Hole
    {
        private static readonly Size size = new Size(600, 195);

        protected override void InitResources()
        {
            Image = Booster.LoadImage("wide_hole.png");
        }

        protected override void Init()
        {
            pureIntersection = true;
            BoundingContainer.AddPolyline(new Vector[] {
	            new Vector(25, 25),
	            new Vector(263, 17),
	            new Vector(529, 26),
	            new Vector(565, 35),
	            new Vector(550, 145),
	            new Vector(296, 118),
	            new Vector(84, 114),
	            new Vector(21, 102),
	            new Vector(13, 68),
	            new Vector(26, 27)
            });
        }

        protected override Size BaseSize
        {
            get { return size; }
        }

    }
}
