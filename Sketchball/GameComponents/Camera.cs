using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Sketchball.GameComponents
{
    interface Camera
    {
        Size Size { get; set; }
        void Draw(DrawingContext g);
        void zoom(float factor);
        void moveRel(Vector relMove);
        void moveAbs(Vector relAbs);

        Vector Scale { get; }
        Vector Translocation { get; }
        BackgroundManager backgroundManager { get; }
    }
}
