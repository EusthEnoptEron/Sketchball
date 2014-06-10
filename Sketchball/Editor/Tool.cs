using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Sketchball.Editor
{
    /// <summary>
    /// Represents a tool that can be used to perform an action on a PinballEditControl.
    /// </summary>
    public abstract class Tool
    {   
        /// <summary>
        /// The control we work on.
        /// </summary>
        protected PinballEditControl Editor;

        /// <summary>
        /// Gets the label of this tool.
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// Gets an icon that belongs to this tool.
        /// </summary>
        public Image Icon { get; protected set; }


	    protected Tool(PinballEditControl control) {
		    Editor = control;

            Label = "Unnamed Tool";
            Icon  = new Bitmap(1, 1);
	    }

        /// <summary>
        /// Activates this tool. (-> Setup)
        /// </summary>
        public void Enter()
        {
            // Bind handlers
            Editor.MouseDown += OnMouseDown;
            Editor.MouseUp += OnMouseUp;
            Editor.MouseMove += OnMouseMove;
            Editor.Paint += Draw;

            OnSelect();
        }


        /// <summary>
        /// Deactivates this tool. (-> Teardown)
        /// </summary>
        public void Leave()
        {
            // Unbind handlers
            Editor.MouseDown -= OnMouseDown;
            Editor.MouseUp -= OnMouseUp;
            Editor.MouseMove -= OnMouseMove;
            Editor.Paint -= Draw;

            OnUnselect();
        }

	    protected virtual void OnMouseDown(object sender, MouseEventArgs e) {}
	    protected virtual void OnMouseUp(object sender, MouseEventArgs e) {}
	    protected virtual void OnMouseMove(object sender, MouseEventArgs e) {}
        protected virtual void Draw(object sender, DrawingContext e) {}

        protected virtual void OnSelect() {}
        protected virtual void OnUnselect() {}
    }
}
