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
        private Game game;
        private GameView gameView;
        private GameHUD HUD;

        public PlayForm(PinballMachine world)
        {
            InitializeComponent();

            // Initialize game
            game = new Game(world);
            gameView = new GameView(game);

            gameView.Dock = DockStyle.Fill;

            Controls.Add(gameView);
        }
    }
}
