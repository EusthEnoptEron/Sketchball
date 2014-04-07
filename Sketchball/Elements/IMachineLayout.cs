﻿using Sketchball.Collision;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public interface IMachineLayout
    {
        int Width { get; }
        int Height { get; }
        BoundingContainer Bounds { get; }
        
        void DrawBackground(Graphics g);
    }
}