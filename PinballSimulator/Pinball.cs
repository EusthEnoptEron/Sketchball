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

namespace PinballSimulator
{
    [Browsable(false)]
    public partial class Pinball : UserControl
    {
        public List<PinballElement> Elements = new List<PinballElement>();
        Bitmap B_BUFFER;
        Graphics G_BUFFER;
        Graphics G_TARGET;
        Point ArrowFrom = Point.Empty;
        Point ArrowTo = Point.Empty;
        Ball ball = null;

        private void LoadBuffers()
        {
            if (B_BUFFER != null)
            {
                B_BUFFER.Dispose();
                G_BUFFER.Dispose();
            }

            B_BUFFER = new Bitmap(Width, Height);
            G_BUFFER = Graphics.FromImage(B_BUFFER);
            G_BUFFER.CompositingQuality = CompositingQuality.HighQuality;
            G_BUFFER.SmoothingMode = SmoothingMode.AntiAlias;

            G_TARGET = CreateGraphics();
        }

        public Pinball() : base()
        {
            InitializeComponent();

            LoadBuffers();

            ball = new Ball() { Location = new Vector2(150, 10) };
            Elements.Add(new Flipper() { Location = new Vector2(50, Height - 10) });
            Elements.Add(ball);
          
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            SizeChanged += Pinball_SizeChanged;
            MouseDown += ArrowStart;
            MouseMove += ArrowMove;
            MouseUp += ArrowEnd;

            Timer t = new Timer();
            t.Interval = 10;
            
            t.Tick += (s, e) =>
            {
                if (IsDisposed || Disposing)
                {
                    t.Stop();
                }
                else
                {
                    Update();
                    Redraw();
                }
            };
            t.Start();
        }

        private void ArrowEnd(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ArrowTo = (e.Location);

                ball.V0 = ball.Velocity + new Vector2(ArrowTo.X - ArrowFrom.X, (ArrowTo.Y - ArrowFrom.Y) * 2);
                
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
        }

        void Pinball_SizeChanged(object sender, EventArgs e)
        {
            LoadBuffers();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
 	       // G_TARGET = e.Graphics;
           // Redraw();
        }

        public void Redraw()
        {
            Draw(G_BUFFER);
            G_TARGET.DrawImageUnscaled(B_BUFFER, 0, 0);
        }



        public void Draw(Graphics g)
        {
            g.Clear(Color.White);
            g.DrawRectangle(Pens.Red, 0, 0, Width - 1, Height - 1);
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

            g.DrawString(ball.Velocity + "", new Font("Arial", 10), Brushes.Red, new Point(0, 0));

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
                    g.TranslateTransform(ball.X + ball.Width / 2, ball.Y + ball.Height / 2);
                    g.RotateTransform(angle);

                    g.DrawLine(pen, 0, 0, len, 0);
                    g.DrawLine(pen, len, 0, len - 10, -10);
                    g.DrawLine(pen, len, 0, len - 10, 10);


                    g.Restore(gstate);
                }
            }
           
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, Width - 1, Height - 1));

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
                if (element.Y + element.Height > Height)
                {
                    element.Y = Height - element.Height;

                    element.V0 = new Vector2(element.Velocity.X * .6f, -element.Velocity.Y * .6f);
                }
                if (element.X < 0 || element.X + element.Width > Width)
                {
                    element.X = Math.Max(0, Math.Min(Width - element.Width, element.X));
                    element.V0 = new Vector2(-element.Velocity.X * .6f, element.Velocity.Y);
                }



            }

        }

    }
}
