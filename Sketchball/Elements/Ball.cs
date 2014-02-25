﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class Ball : PinballElement
    {
        public Ball()
        {
            Width = 60;
            Height = 60;
            AffectedByGravity = true;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            //g.FillEllipse(Brushes.Peru, 0, 0, Width, Height);
            g.DrawImage(Properties.Resources.BallWithAlpha, 0, 0, Width, Height);
        }
    }
}