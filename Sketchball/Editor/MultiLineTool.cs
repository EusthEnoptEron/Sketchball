using Sketchball.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using Sketchball.Collision;
using Sketchball.Elements;

namespace Sketchball.Editor
{
    public class MultiLineTool : Tool
    {
        private Vector2 startPos;
        private Vector2 actualPos;
        private bool drawing = false;


        public MultiLineTool(PinballEditControl control)
            : base(control)
        {
            Icon = Properties.Resources.MultiLineTool;
            Label = "Multi Line tool";
        }

        protected override void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (drawing == false)
            {
                this.startPos = new Vector2(e.X + curserCorrection, e.Y + curserCorrection);
                this.actualPos = new Vector2(e.X + curserCorrection, e.Y + curserCorrection);
                this.drawing = true;
                this.Control.Refresh();
            }
        }

        public override void Enter()
        {
            Control.MouseDoubleClick += OnMouseDoubleClick;
            base.Enter();
        }

       

        public override void Leave()
        {
            Control.MouseDoubleClick -= OnMouseDoubleClick;
            base.Leave();
        }

        protected override void OnMouseUp(object sender, MouseEventArgs e)
        {
            this.actualPos = new Vector2(e.X + curserCorrection, e.Y + curserCorrection);

            //Create Line
            Line l = new Line(this.startPos.X, this.startPos.Y, this.actualPos.X, this.actualPos.Y);
            this.Control.PinballMachine.DynamicElements.Add(l);

            this.startPos = actualPos;
            this.Control.Refresh();
        }

        protected void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMouseUp(sender, e);
            this.drawing = false;
        }

        protected override void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (this.drawing)
            {
                this.actualPos.X = e.X + curserCorrection;
                this.actualPos.Y = e.Y + curserCorrection;

                this.Control.Refresh();
            }
        }

        protected override void Draw(object sender, PaintEventArgs e)
        {
            if (this.drawing)
            {
                e.Graphics.DrawLine(System.Drawing.Pens.Black, startPos.X, startPos.Y, actualPos.X, actualPos.Y);
            }
        }
    }
}
