using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sketchball.Elements
{
    [DataContract]
    public abstract class CustomElement : PinballElement
    {
        protected abstract Geometry Geometry { get; }

        [DataMember]
        public System.Drawing.Color Color { get; set; }

        private Pen myPen;

        [DataMember]
        public int LineWidth
        {
            get
            {
                return _lineWidth;
            }
            set
            {
                if(value > 0 && value < 5)
                    _lineWidth = value;
            }
        }
        
        private int _lineWidth = 1;

        public CustomElement() : base()
        {
            Color = System.Drawing.Color.Black;
        }

        protected override void OnDraw(DrawingContext g)
        {
            myPen = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromRgb(Color.R, Color.G, Color.B)), LineWidth);
            g.DrawGeometry(null, myPen, Geometry);
        }
    }
}
