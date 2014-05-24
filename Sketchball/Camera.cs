using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sketchball
{
    interface Camera
    {
        Size Size { get; set; }
        void Draw(DrawingContext g);
        void zoom(float factor);
        void moveRel(Vector2 relMove);
        void moveAbs(Vector2 relAbs);

        Vector2 Scale { get; }
        Vector2 Translocation { get; }
        BackgroundManager backgroundManager { get; }
    }
}
