using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    public class FontManager
    {
        private static FontManager _instance = null;

        private FontFamily courgette;
        private System.Windows.Media.FontFamily courgetteWpf;

        private FontManager()
        {
            PrivateFontCollection fontCollection = new PrivateFontCollection();

            // Add fonts
            String path = (Path.Combine(Application.ExecutablePath, "..", "Resources", "Courgette-Regular.ttf"));

            fontCollection.AddFontFile(path);
            courgette = fontCollection.Families[0];

            courgetteWpf = new System.Windows.Media.FontFamily(new Uri("pack://application:,,,/"), "./Resources/#Courgette");
        }

        private static FontManager Instance()
        {
            if (_instance == null) _instance = new FontManager();
            return _instance;
        }

        public static FontFamily Courgette
        {
            get
            {
                return Instance().courgette;
            }
        }

        public static System.Windows.Media.FontFamily CourgetteWpf
        {
            get {
                return Instance().courgetteWpf;
            }
        }
    }
}
