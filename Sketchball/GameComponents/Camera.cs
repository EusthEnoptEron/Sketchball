using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.GameComponents
{
    /// <summary>
    /// Represents a camera that cna draw part of a GameWorld.+
    /// </summary>
    interface Camera
    {
        Size Size { get; set; }
        void Draw(DrawingContext g);
        Vector Scale { get; }
        Vector Translocation { get; }
    }
}
