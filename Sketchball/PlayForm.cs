using Sketchball.Controls;
using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sketchball
{
    public partial class PlayForm : Form
    {
        private PinballGameControl Game;
        public PlayForm(PinballMachine world)
        {
            InitializeComponent();

            // Initialize game
            Game = new PinballGameControl(world);
            Game.Dock = DockStyle.Fill;

            Controls.Add(Game);
        }
    }
}
