using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{
    public class CircleTool : Tool
    {
        public CircleTool(PinballEditControl control) : base(control)
        {
            Label = "Circle";
            Icon = Properties.Resources.circle_outline_512;
        }
    }
}
