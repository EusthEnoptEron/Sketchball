using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Elements
{
    /// <summary>
    /// Represents a problem with the validation.
    /// </summary>
    public class ValidationProblem
    {
        /// <summary>
        /// Gets the message that describes the problem.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets a list of elements involved in the problem.
        /// </summary>
        public List<PinballElement> ElementsInvolved { get; private set; }


        /// <summary>
        /// Iniializes a new problem with the given properties.
        /// </summary>
        /// <param name="msg">Describes the problem.</param>
        /// <param name="elements">List of elements involved.</param>
        public ValidationProblem(string msg, IEnumerable<PinballElement> elements)
        {
            Message = msg;
            ElementsInvolved = elements.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg">Describes the problem.</param>
        /// <param name="element">Element involved.</param>
        public ValidationProblem(string msg, PinballElement element) : this(msg, new PinballElement[] { element })
        {
        }
    }
}
