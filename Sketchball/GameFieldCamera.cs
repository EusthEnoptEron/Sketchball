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

        public BackgroundManager backgroundManager { get; private set; }

        public Vector2 Translocation { get; set; }
        public Vector2 Scale { get; set; }

        public Size Size
        {
            get
            {
                return this.backgroundManager.Size;
            }
            set
            {
                if (this.backgroundManager.orginalSize.Width == 0 && this.backgroundManager.orginalSize.Height == 0 && this.Size.Width != 0 && this.Size.Height != 0)
                {
                    this.backgroundManager.orginalSize = new Size(this.Size.Width, this.Size.Height);
                }
                if (this.backgroundManager.orginalSize.Width != 0 && this.backgroundManager.orginalSize.Height != 0)
                {
                    float ratio = (this.Size.Width * 1f) / (this.backgroundManager.orginalSize.Width);
                    float ratio2 = this.Size.Height * 1.02f / this.backgroundManager.orginalSize.Height;
                    ratio = Math.Min(ratio, ratio2);
                    if (Scale == new Vector2(0, 0))
                    {
                        this.Scale = new Vector2(ratio, ratio);
                    }
                }
                this.backgroundManager.Size = value;
                UpdateBackground();

            }
        }

        public GameFieldCamera(Game game)
        {
            Game = game;
            this.Translocation = new Vector2(0, 0);
            this.Scale = new Vector2(0, 0);
            //this.maximumSize = new Size(Screen.PrimaryScreen.Bounds.Width - this.offsetRight, (Screen.PrimaryScreen.Bounds.Height - offsetTopYAbs) / 100 * (100 - this.offsetY));
            this.backgroundManager = new BackgroundManager(game.Machine.Width, game.Machine.Height);
            UpdateBackground();
        }

        private void UpdateBackground()
        {
            this.backgroundManager.UpdateBackground(this.Game.Machine.Width, this.Game.Machine.Height);
        }



        public void Draw(Graphics g)
        {

            int offsetYAbs = this.backgroundManager.Size.Height / 100 * backgroundManager.offsetY;        

            GraphicsState state = g.Save();
            try
            {
                int startPosXAbs;
                float ratio = CalculateRatio(this.backgroundManager.Background_Body.Width, this.backgroundManager.Background_Body.Height);

                if (this.Scale.X > 0)
                {
                    startPosXAbs = (int)(((this.backgroundManager.Size.Width - backgroundManager.offsetRight * this.Scale.X) / 2 - (this.backgroundManager.Background_Bot.Width) / 2) / this.Scale.X);
                }
                else
                {
                    startPosXAbs = (int)(((this.backgroundManager.Size.Width - backgroundManager.offsetRight) / 2 - (this.backgroundManager.Background_Bot.Width) / 2));
                }

                if (Scale.X > 0 && Scale.Y > 0)
                {
                    g.ScaleTransform(this.Scale.X, this.Scale.Y);
                }
                if (this.Translocation.X > 0 && this.Translocation.Y > 0)
                {
                    g.TranslateTransform(this.Translocation.X, this.Translocation.Y);
                }

                g.DrawImageUnscaled(backgroundManager.Background_Rings, startPosXAbs, offsetYAbs + this.backgroundManager.offsetTopYAbs);
                g.DrawImageUnscaled(backgroundManager.Background_Body, backgroundManager.Background_Rings.Width + startPosXAbs - 1, offsetYAbs + this.backgroundManager.offsetTopYAbs);
                g.DrawImageUnscaled(backgroundManager.Background_Bot, backgroundManager.Background_Rings.Width + startPosXAbs - 1, backgroundManager.Background_Body.Height + offsetYAbs + this.backgroundManager.offsetTopYAbs - 1);
                g.DrawImageUnscaled(backgroundManager.Background_End, startPosXAbs + backgroundManager.Background_Rings.Width + backgroundManager.Background_Body.Width - 2, offsetYAbs + this.backgroundManager.offsetTopYAbs);

                g.DrawImageUnscaled(backgroundManager.Background_LogoTop, startPosXAbs + backgroundManager.Background_Rings.Width + backgroundManager.Background_Body.Width - this.backgroundManager.Background_LogoTop.Width, offsetYAbs + this.backgroundManager.offsetTopYAbs + 10);

                g.TranslateTransform(startPosXAbs + backgroundManager.Background_Rings.Width, (offsetYAbs + this.backgroundManager.offsetTopYAbs + this.backgroundManager.Background_LogoTop.Height + 15));

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
            return this.backgroundManager.minimumSize;
        }

        public void zoom(float factor)
        {
            if (factor > 0)
            {
                this.Scale *= factor;
                if (Scale.X < 1||Scale.Y < 1)
                {
                    Scale = new Vector2(1, 1);
                }
                this.backgroundManager.scaleMainBackground(this.Scale);
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
