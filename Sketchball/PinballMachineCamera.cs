using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    public class PinballMachineCamera : Camera
    {
        internal PinballMachine Machine { get; set; }

        public PinballMachineCamera(PinballMachine machine)
        {
            Machine = machine;
        }

        public void Draw(Graphics g)
        {
            RectangleF bounds = g.VisibleClipBounds;

            float widthRatio = bounds.Width / Machine.Width;
            float heightRatio = bounds.Height / Machine.Height;

            float ratio = Math.Min(widthRatio, heightRatio);

            GraphicsState state = g.Save();
            try
            {
                g.ScaleTransform(ratio, ratio);
                Machine.Draw(g);
            }
            finally
            {
                g.Restore(state);
            }
        }
    }
}
