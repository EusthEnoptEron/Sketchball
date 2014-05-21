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
    public class BackgroundManager
    {

        public Size orginalSize { get; set; }

        private float scaleModBG = 1.01f;

        public readonly int offsetY = 2;           //%
        public readonly int offsetTopYAbs = 25;    //abs
        public readonly int offsetRight = 450;     //offset Right due to score etc
        public readonly Size minimumSize;

        private Image Notebook_Rings = Properties.Resources.Notebook_Ringe;
        private Image Notebook_Bot = Properties.Resources.Notebook_bot;
        private Image Notebook_Body = Properties.Resources.Notebook_body;
        private Image Notebook_End = Properties.Resources.Notebook_Ende;

        private Image Notebook_bfhLogoTop = Properties.Resources.Logo_BFH;

        private Image mainBg = Properties.Resources.TableBackground;

        public Bitmap Background_Body { get; private set; }
        public Bitmap Background_Bot { get; private set; }
        public Bitmap Background_Rings { get; private set; }
        public Bitmap Background_End { get; private set; }

        public Bitmap Background_LogoTop { get; private set; }

        public Bitmap Background_TableBackground { get; private set; }

        public event EventHandler backgroundChanged;
       
        private Size _size;
        public Size Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public BackgroundManager(int width, int height)
        {
            this.minimumSize = new Size((int)(((width + this.offsetRight))), (int)((height + this.offsetTopYAbs) * 1.3f));
            this.Background_TableBackground = Booster.OptimizeImage(mainBg, (int)(this.mainBg.Width * scaleModBG), (int)(this.mainBg.Height * scaleModBG));
        }

        public void UpdateBackground(int gameWidth, int gameHeight)
        {

            if (!Size.IsEmpty)
            {
                //Background = Booster.OptimizeImage(Notebook, (int)(ratio * Game.Machine.Width), (int)(ratio * Game.Machine.Height));
                int offsetYAbs = this.Size.Height / 100 * offsetY;
                int calcH = Math.Min(this.Size.Height, (int)(gameHeight * 1.2f));
                float scaleY = calcH * 1f / this.Notebook_Rings.Height * 0.98f;

                Background_End = Booster.OptimizeImage(Notebook_End, this.Notebook_End.Width, (int)(this.Notebook_End.Height * scaleY));
                Background_Rings = Booster.OptimizeImage(Notebook_Rings, this.Notebook_Rings.Width, (int)(this.Notebook_Rings.Height * scaleY));
                Background_Bot = Booster.OptimizeImage(Notebook_Bot, gameWidth, (int)(this.Notebook_Bot.Height * scaleY));
                Background_Body = Booster.OptimizeImage(Notebook_Body, gameWidth, (int)(this.Notebook_Body.Height * scaleY + 1));

                Background_LogoTop = Booster.OptimizeImage(Notebook_bfhLogoTop, (int)(this.Notebook_bfhLogoTop.Width / 2), (int)(this.Notebook_bfhLogoTop.Height / 2));
            }
        }

        public void scaleMainBackground(Vector2 factor)
        {
         
            if (factor.X < 1 && this.Background_TableBackground.Width * factor.X < this.mainBg.Width)
            {
                return;//ignore
            }

            if (factor.Y < 1 && this.Background_TableBackground.Height * factor.Y < this.mainBg.Height)
            {
                return;//ignore
            }

            if (factor.X > 2.7f)
            {
                factor = new Vector2(2.7f, 2.7f);
            }

           
            this.Background_TableBackground = Booster.OptimizeImage(mainBg, (int)(this.mainBg.Width * scaleModBG * factor.X), (int)(this.mainBg.Height * scaleModBG * factor.Y));
            this.backgroundChanged.Invoke(this, null);
        }
    }
}
