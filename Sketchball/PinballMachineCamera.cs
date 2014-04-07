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

        public void Draw(Graphics g, Rectangle bounds)
        {
            float widthRatio = (float)bounds.Width / Machine.Width;
            float heightRatio = (float)bounds.Height / Machine.Height;

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
