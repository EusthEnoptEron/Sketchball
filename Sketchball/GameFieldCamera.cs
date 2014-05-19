using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    public class GameFieldCamera : Camera
    {
        internal Game Game { get; set; }

       // public readonly Size maximumSize;          //Max size that the book may have
        private readonly Size minimumSize;
        private Size orginalSize;

        private readonly int offsetY = 2;           //%
        private readonly int offsetTopYAbs = 25;    //abs
        private readonly int offsetRight = 320;     //offset Right due to score etc
  
        private Image Notebook_Rings = Properties.Resources.Notebook_Ringe;
        private Image Notebook_Bot = Properties.Resources.Notebook_bot;
        private Image Notebook_Body = Properties.Resources.Notebook_body;
        private Image Notebook_End = Properties.Resources.Notebook_Ende;

        private Image Notebook_bfhLogoTop = Properties.Resources.Logo_BFH;

        private Bitmap Background_Body;
        private Bitmap Background_Bot;
        private Bitmap Background_Rings;
        private Bitmap Background_End;

        private Bitmap Background_LogoTop;

        public Vector2 Translocation { get; set; }
        public Vector2 Scale { get; set; }


        private Size _size;
        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                if (this.orginalSize.Width == 0 && this.orginalSize.Height == 0 && this.Size.Width != 0 && this.Size.Height!=0)
                {
                    this.orginalSize = new Size(this.Size.Width, this.Size.Height);                 
                }
                if (this.orginalSize.Width != 0 && this.orginalSize.Height != 0)
                {
                    float ratio = (this.Size.Width * 1f) / (this.orginalSize.Width );
                    float ratio2 = this.Size.Height * 1.02f / this.orginalSize.Height;
                    ratio = Math.Min(ratio,ratio2);
                    if(Scale == new Vector2(0,0))
                    {
                        this.Scale = new Vector2(ratio, ratio);
                    }
                }
                _size = value;
                UpdateBackground();

            }
        }

        public GameFieldCamera(Game game)
        {
            Game = game;
            this.Translocation = new Vector2(0, 0);
            this.Scale = new Vector2(0, 0);
            //this.maximumSize = new Size(Screen.PrimaryScreen.Bounds.Width - this.offsetRight, (Screen.PrimaryScreen.Bounds.Height - offsetTopYAbs) / 100 * (100 - this.offsetY));
            this.minimumSize = new Size((int)(((this.Game.Machine.Width + this.offsetRight) )), (int)((this.Game.Machine.Height + this.offsetTopYAbs) * 1.3f));
            
            UpdateBackground();
        }

        private void UpdateBackground()
        {

            if (Size != Size.Empty)
            {
                //Background = Booster.OptimizeImage(Notebook, (int)(ratio * Game.Machine.Width), (int)(ratio * Game.Machine.Height));
                int offsetYAbs = this.Size.Height / 100 * offsetY;
                int calcH = Math.Min(this.Size.Height, (int)(this.Game.Machine.Height*1.2f));
                float scaleY = calcH * 1f / this.Notebook_Rings.Height*0.98f;

                Background_End = Booster.OptimizeImage(Notebook_End, this.Notebook_End.Width, (int)(this.Notebook_End.Height * scaleY));
                Background_Rings = Booster.OptimizeImage(Notebook_Rings, this.Notebook_Rings.Width, (int)(this.Notebook_Rings.Height * scaleY));
                Background_Bot = Booster.OptimizeImage(Notebook_Bot, this.Game.Machine.Width, (int)(this.Notebook_Bot.Height * scaleY));

                Background_Body = Booster.OptimizeImage(Notebook_Body, this.Game.Machine.Width, (int)(this.Notebook_Body.Height * scaleY+1));

                Background_LogoTop = Booster.OptimizeImage(Notebook_bfhLogoTop, (int)(this.Notebook_bfhLogoTop.Width/2 ), (int)(this.Notebook_bfhLogoTop.Height/2 ));
             }
        }



        public void Draw(Graphics g)
        {
            
            int offsetYAbs = this.Size.Height / 100 * offsetY;        

            GraphicsState state = g.Save();
            try
            {
                int startPosXAbs;
                float ratio = CalculateRatio(this.Background_Body.Width, this.Background_Body.Height);

                if (this.Scale.X > 0)
                {
                    startPosXAbs = (int)(((this.Size.Width - offsetRight * this.Scale.X) / 2 - (this.Background_Bot.Width) / 2) / this.Scale.X);
                }
                else
                {
                    startPosXAbs = (int)(((this.Size.Width - offsetRight) / 2 - (this.Background_Bot.Width) / 2));
                }

                if (Scale.X > 0 && Scale.Y > 0)
                {
                    g.ScaleTransform(this.Scale.X, this.Scale.Y);
                }
                if (this.Translocation.X > 0 && this.Translocation.Y > 0)
                {
                    g.TranslateTransform(this.Translocation.X, this.Translocation.Y);
                }

                g.DrawImageUnscaled(Background_Rings, startPosXAbs, offsetYAbs + this.offsetTopYAbs);
                g.DrawImageUnscaled(Background_Body, Background_Rings.Width + startPosXAbs-1, offsetYAbs + this.offsetTopYAbs);
                g.DrawImageUnscaled(Background_Bot, Background_Rings.Width + startPosXAbs-1, Background_Body.Height + offsetYAbs + this.offsetTopYAbs-1);
                g.DrawImageUnscaled(Background_End, startPosXAbs+Background_Rings.Width + Background_Body.Width - 2, offsetYAbs + this.offsetTopYAbs);

                g.DrawImageUnscaled(Background_LogoTop, startPosXAbs+Background_Rings.Width + Background_Body.Width - this.Background_LogoTop.Width, offsetYAbs + this.offsetTopYAbs + 10);

                g.TranslateTransform(startPosXAbs + Background_Rings.Width, (offsetYAbs + this.offsetTopYAbs + this.Background_LogoTop.Height + 15) );

                g.ScaleTransform(ratio, ratio);
                Game.Machine.Draw(g);
            }
            finally
            {
                g.Restore(state);
            }
        }

        private float CalculateRatio(float width, float height) 
        {
            float widthRatio = (float)(width) / Game.Machine.Width;
            float heightRatio = (float)(height) / Game.Machine.Height;

            return Math.Min(widthRatio, heightRatio);
        }

        public Size getMinSize()
        {
            return this.minimumSize;
        }

        public void zoom(float factor)
        {
            if (factor > 0)
            {
                this.Scale *= factor;
            }
        }

        public void moveRel(Vector2 relativ)
        {
            this.Translocation += relativ;
        }

        public void moveAbs(Vector2 absPos)
        {
            this.Translocation = absPos;
        }

    }
}
