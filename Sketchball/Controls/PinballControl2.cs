using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using Sketchball.Elements;
using System.Drawing.Text;
using Sketchball.Collision;

namespace Sketchball.Controls
{
    [Browsable(false)]
    public partial class PinballControl2 : UserControl
    {
        private PinballMachine machine = new PinballMachine(new Size(500, 500));
        public ElementCollection Elements;
        Bitmap B_BUFFER;
        Graphics G_BUFFER;
        Graphics G_TARGET;
        Point ArrowFrom = Point.Empty;
        Point ArrowTo = Point.Empty;

        private int fps_debug=0;
       
        int bufferReloads = 0;

        private void LoadBuffers()
        {
            if (B_BUFFER != null)
            {
                B_BUFFER.Dispose();
                G_BUFFER.Dispose();
            }
            bufferReloads++;

            B_BUFFER = new Bitmap(Width, Height);
            G_BUFFER = Graphics.FromImage(B_BUFFER);
            
            G_BUFFER.CompositingQuality = CompositingQuality.HighSpeed;
            G_BUFFER.SmoothingMode = SmoothingMode.AntiAlias;
            G_BUFFER.CompositingMode = CompositingMode.SourceOver;
            G_BUFFER.InterpolationMode = InterpolationMode.Low;
            G_BUFFER.PixelOffsetMode = PixelOffsetMode.Half;


            //G_TARGET = CreateGraphics();
            //G_TARGET.CompositingMode = CompositingMode.SourceCopy;
            //G_TARGET.CompositingQuality = CompositingQuality.AssumeLinear;
            //G_TARGET.SmoothingMode = SmoothingMode.None;
            //G_TARGET.InterpolationMode = InterpolationMode.NearestNeighbor;
            //G_TARGET.TextRenderingHint = TextRenderingHint.SystemDefault;
            //G_TARGET.PixelOffsetMode = PixelOffsetMode.HighSpeed;
        }

        public PinballControl2() : base()
        {
            Elements = machine.Elements;
            InitializeComponent();
   
            LoadBuffers();

            Ball ball = new Ball();
            ball.setLocation(new Vector2(150, 10));
            ball.setParent(machine);

            Flipper f = new Flipper();
            f.setLocation(new Vector2(50, Height - 10));

            Bumper b = new Bumper();
            b.setLocation(new Vector2(300, 300));

            TriangleTMP tr = new TriangleTMP();


            Line l = new Line();
            
            Elements.Add(b);
            Elements.Add(l);
            Elements.Add(tr);
            
            Elements.Add(f);
            machine.addAnimatedObject(tr);
            machine.addBall(ball);  //Changed
          
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            SizeChanged += Pinball_SizeChanged;
            MouseDown += ArrowStart;
            MouseMove += ArrowMove;
            MouseUp += ArrowEnd;

            Timer t = new Timer();
            t.Interval = 1000 / 50;
            
            t.Tick += (s, e) =>
            {
                if (IsDisposed || Disposing)
                {
                    t.Stop();
                }
                else
                {
                    Update();
                    Invalidate();
                }
            };
            this.machine.prepareForLaunch();
            t.Start();
        }

        private void ArrowEnd(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ArrowTo = (e.Location);

                foreach (Ball b in machine.Balls)
                {                   
                    b.Velocity += new Vector2(ArrowTo.X - ArrowFrom.X, (ArrowTo.Y - ArrowFrom.Y) * 2);
                } 
                
                ArrowTo = Point.Empty;

            }
        }

        private void ArrowMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ArrowTo = (e.Location);
            }
        }

        private void ArrowStart(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ArrowFrom = (e.Location);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Ball b = new Ball();
                b.setLocation(new Vector2(e.X, e.Y));
                machine.Balls.Add(b);
                b.setParent(machine);
            }
        }

        void Pinball_SizeChanged(object sender, EventArgs e)
        {
            LoadBuffers();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
 	        G_TARGET = e.Graphics;
            Redraw();
        }

        public void Redraw()
        {
            
            try
            {
                Draw(G_TARGET);
               //G_TARGET.DrawImageUnscaled(B_BUFFER, 0, 0);
            }
            catch (Exception e)
            {

            }
        }

        private void drawBBTMP(Graphics g)
        {
            foreach (PinballElement e in this.Elements)
            {
                foreach (IBoundingBox b in e.boundingContainer.boundingBoxes)
                {
                    b.drawDEBUG(g, Pens.Red);
                }
            }
            

            foreach (Ball ball in this.machine.Balls)
            {
                IBoundingBox b = ball.boundingContainer.boundingBoxes[0];
                b.drawDEBUG(g, Pens.Red);
            }

            for (int y = 0; y < Height; y += 62)
            {
                g.DrawLine(Pens.LightSteelBlue, 0, y, Width, y);
            }

            for (int x = 0; x < Width; x += 62)
            {
                g.DrawLine(Pens.LightSteelBlue, x, 0, x,Height);
            }

            g.DrawString("fps"+this.fps_debug.ToString(), new Font("Arial", 12), Brushes.BlueViolet, new PointF(400, 400));
            this.machine.debugDraw(g);
        }


        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.FillRectangle(Brushes.White, 0, 0, Width, Height);
            //g.DrawRectangle(Pens.Red, 0, 0, Width - 1, Height - 1);
            
            for (int y = 0; y < Height; y += 10)
            {
                g.DrawLine(Pens.LightGray, 0, y, Width, y);
            }

            for (int x = 0; x < Width; x += 10)
            {
                g.DrawLine(Pens.LightGray, x, 0, x, Height);
            }
            
            foreach (PinballElement element in Elements)
            {
                GraphicsState gstate = g.Save();

                g.TranslateTransform(element.X, element.Y);
                element.Draw(g);

                g.Restore(gstate);
            }

            foreach (Ball b in machine.Balls)
            {
                GraphicsState gstate = g.Save();

                g.TranslateTransform(b.X, b.Y);
                b.Draw(g);

                g.Restore(gstate);
            }
            Brush brush = new HatchBrush(HatchStyle.BackwardDiagonal, Color.Gray, Color.Transparent);
            Point[] path = new Point[] { 
                new Point(30, Height),
                new Point(30, 50),
                new Point( (Width - 60) / 2 + 30, 10),
                new Point(Width - 30, 50),
                new Point(Width - 30, Height),

                new Point(Width - 50, Height),
                new Point(Width - 50, 70),
                new Point( (Width - 100) / 2 + 50, 30),
                new Point(50, 70),
                new Point(50, Height)
            };


            g.FillClosedCurve(brush, path);
            g.DrawCurve(Pens.Black, path);

            //g.DrawString(bufferReloads+"", new Font("Arial", 10), Brushes.Red, new Point(0, 0));

            if (ArrowTo != Point.Empty)
            {
                
                Vector2 arrow = new Vector2(ArrowTo.X - ArrowFrom.X, ArrowTo.Y - ArrowFrom.Y);
                if (arrow.Length() > 20)
                {
                    double dangle = (Math.Atan(arrow.Y / arrow.X) * 180 / Math.PI);
                    float angle = (float)dangle;
                    Pen pen = new Pen(Color.Blue, 4);
                    float len = arrow.Length() / 2;

                    if (arrow.X < 0) angle = angle - 180;
                    //if (arrow.Y < 0) angle = -angle;

                 

                    GraphicsState gstate = g.Save();
                    foreach (Ball ball in this.machine.Balls)
                    {
                        g.TranslateTransform(ball.X + ball.Width / 2, ball.Y + ball.Height / 2);
                    }
                    g.RotateTransform(angle);

                    g.DrawLine(pen, 0, 0, len, 0);
                    g.DrawLine(pen, len, 0, len - 10, -10);
                    g.DrawLine(pen, len, 0, len - 10, 10);


                    g.Restore(gstate);
                }
            }
           
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));
            drawBBTMP(g);

        }

        private DateTime prev = DateTime.MinValue;
        
        /// <summary>
        /// Update world and apply physics.
        /// </summary>
        public new void Update()
        {
            DateTime now = DateTime.Now;
            TimeSpan delta = prev == DateTime.MinValue
                ? new TimeSpan(0)
                : now - prev;
            prev = now;

            foreach (PinballElement element in Elements)
            {
                element.Update(delta.Milliseconds);

                if (element is IPhysics)
                {
                    IPhysics el = (IPhysics)element;
                    if (element.Y + element.Height > Height)
                    {
                        element.Y = Height - element.Height;

                        el.Velocity = new Vector2(el.Velocity.X * .6f, -el.Velocity.Y * .6f);
                    }
                    if (element.X < 0 || element.X + element.Width > Width)
                    {
                        element.X = Math.Max(0, Math.Min(Width - element.Width, element.X));
                        el.Velocity = new Vector2(-el.Velocity.X * .6f, el.Velocity.Y);
                    }
                }
            }

            foreach (Ball b in this.machine.Balls)
            {
                b.Update(delta.Milliseconds);

                IPhysics el = (IPhysics)b;
                if (b.Y + b.Height > Height)
                {
                    b.Y = Height - b.Height;

                    el.Velocity = new Vector2(el.Velocity.X * .6f, -el.Velocity.Y * .6f);
                }
                if (b.X < 0 || b.X + b.Width > Width)
                {
                    b.X = Math.Max(0, Math.Min(Width - b.Width, b.X));
                    el.Velocity = new Vector2(-el.Velocity.X * .6f, el.Velocity.Y);
                }
                
            }

            this.machine.handleCollision();

            this.fps_debug = (int)(1 / delta.TotalSeconds);

        }

    }
}
