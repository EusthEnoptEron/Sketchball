using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sketchball
{
    /// <summary>
    /// Represents a change to the position of an element.
    /// </summary>
    public class TranslationChange : IChange
    {
        private int dx;
        private int dy;
        private PinballElement element;

        public TranslationChange(PinballElement element, Vector vector) : this(element, (int)vector.X, (int)vector.Y) {}

        public TranslationChange(PinballElement element, int dx, int dy)
        {
            this.element = element;
            this.dx = dx;
            this.dy = dy;
        }

        public void Do()
        {
            element.X += dx;
            element.Y += dy;
        }

        public void Undo()
        {
            element.X -= dx;
            element.Y -= dy;
        }
    }
}
