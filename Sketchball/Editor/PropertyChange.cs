using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Editor
{
    public class PropertyChange<T> : IChange
    {
        private T element;
        private object oldValue;
        private object newValue;
        private PropertyInfo property;
 
        public PropertyChange(T element, string propertyName, object newValue, object oldValue)
        {
            property = typeof(T).GetProperty(propertyName);
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
