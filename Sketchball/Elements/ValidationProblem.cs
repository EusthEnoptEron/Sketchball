using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    public class ValidationProblem
    {
        public string Message { get; private set; }
        public List<PinballElement> ElementsInvolved { get; private set; }

        public ValidationProblem(string msg, IEnumerable<PinballElement> elements)
        {
            Message = msg;
            ElementsInvolved = elements.ToList();
        }

        public ValidationProblem(string msg, PinballElement element) : this(msg, new PinballElement[] { element })
        {
        }
    }
}
