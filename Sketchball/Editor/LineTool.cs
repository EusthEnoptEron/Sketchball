using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{
    public class LineTool : Tool
    {
        public LineTool(PinballEditControl control) : base(control) {
            Icon  = Properties.Resources.LineTool;
            Label = "Line tool";
            
        }
    }
}
