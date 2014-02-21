using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinballSimulator
{
    public abstract class PinballControl : UserControl
    {
        public PinballControl()
        {
            BackColor = Color.Transparent;

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        void PinballControl_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        public abstract void Draw(Graphics g);
        public abstract void Update(long delta);

    }
}
