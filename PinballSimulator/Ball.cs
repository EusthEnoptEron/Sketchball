using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballSimulator
{
    public class Ball : PinballElement
    {
        public Ball()
        {
            Width = 30;
            Height = 30;
            AffectedByGravity = true;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.FillEllipse(Brushes.Peru, 0, 0, Width, Height);
        }
    }
}
