using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    interface Camera
    {
        Size Size { get; set; }
        void Draw(Graphics g);
        void zoom(float factor);
        void moveRel(Vector2 relMove);
        void moveAbs(Vector2 relAbs);

        Vector2 Scale { get; }
        BackgroundManager backgroundManager { get; }
    }
}
