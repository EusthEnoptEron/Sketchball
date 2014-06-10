using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{
    /// <summary>
    /// Represents a change to a property.
    /// </summary>
    public class PropertyChange : IChange
    {
        private object element;
        private object oldValue;
        private object newValue;
        private PropertyInfo property;
 

        public PropertyChange(object element, string propertyName, object newValue, object oldValue)
        {
            property = element.GetType().GetProperty(propertyName);
            this.element = element;
            this.newValue = newValue;
            this.oldValue = oldValue;
        }

        public void Do()
        {
            property.SetValue(element, newValue);
        }

        public void Undo()
        {
            property.SetValue(element, oldValue);
        }
    }
}
