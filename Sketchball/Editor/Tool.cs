using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball.Editor
{

    public abstract class Tool
    {
        protected readonly int curserCorrection = -15;
        protected PinballEditControl Control;
        public string Label { get; protected set; }
        public Image Icon { get; protected set; }

	    protected Tool(PinballEditControl control) {
		    Control = control;

            Label = "Unnamed Tool";
            Icon  = new Bitmap(1, 1);
	    }

        public virtual void Enter()
        {
            // Bind handlers
            Control.MouseDown += OnMouseDown;
            Control.MouseUp += OnMouseUp;
            Control.MouseMove += OnMouseMove;
            Control.Paint += Draw;

            OnSelect();
        }


        public virtual void Leave()
        {
            // Unbind handlers
            Control.MouseDown -= OnMouseDown;
            Control.MouseUp -= OnMouseUp;
            Control.MouseMove -= OnMouseMove;
            Control.Paint -= Draw;

            OnUnselect();
        }

	    protected virtual void OnMouseDown(object sender, MouseEventArgs e) {}
	    protected virtual void OnMouseUp(object sender, MouseEventArgs e) {}
	    protected virtual void OnMouseMove(object sender, MouseEventArgs e) {}
        protected virtual void Draw(object sender, PaintEventArgs e) {}

        protected virtual void OnSelect() {}
        protected virtual void OnUnselect() {}
    }
}
