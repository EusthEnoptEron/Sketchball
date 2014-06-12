using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Sketchball.Elements
{
    /// <summary>
    /// An element drawn by the user.
    /// </summary>
    [DataContract]
    public abstract class CustomElement : PinballElement
    {
        /// <summary>
        /// Gets the geometry that describes the shape of the element.
        /// </summary>
        protected abstract Geometry Geometry { get; }

        [Browsable(true), Category("Appearance"), Description("Sets the color of the line")]
        public System.Drawing.Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                createPen();
            }
        }

        [DataMember]
        private System.Drawing.Color _color = System.Drawing.Color.Black;


        private Pen myPen;

        [Browsable(true), Category("Appearance"), Description("Sets the width of the line")]
        [DisplayName("Line width")]
        [DataMember]
        public int LineWidth
        {
            get
            {
                return _lineWidth;
            }
            set
            {
                _lineWidth = Math.Max(1, Math.Min(5, value));
                createPen();
            }
        }

        private int _lineWidth = 1;

        public CustomElement() : base()
        {
        }

        protected override void Init()
        {
            createPen();
        }

        private void createPen()
        {
            myPen = new Pen(new SolidColorBrush(System.Windows.Media.Color.FromRgb(Color.R, Color.G, Color.B)), LineWidth);
        }

        protected override void OnDraw(DrawingContext g)
        {
            g.DrawGeometry(null, myPen, Geometry);
        }

        protected override void OnClone(PinballElement element)
        {
            var el = element as CustomElement;
            el.Color = System.Drawing.Color.FromArgb(255, Color.R, Color.G, Color.B);
        }
    }
}
