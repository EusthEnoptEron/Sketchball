﻿using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball
{
    /// <summary>
    /// Phases the control can assume
    /// </summary>
    public enum GameStatus
    {
        Setup,
        Playing,
        GameOver,
        Pause
    }

    /// <summary>
    /// Control that houses a game of pinball.
    /// </summary>
    public class Game
    {
       


        /// <summary>
        /// Total number of lives (<=> balls)
        /// </summary>
        public const int TOTAL_LIVES = 3;


        /// <summary>
        /// Gets the current game status.
        /// </summary>
        public GameStatus Status { get; private set; }


        /// <summary>
        /// Gets or sets the machine currently displayed.
        /// </summary>
        public PinballGameMachine Machine { get; private set;}

        
        /// <summary>
        /// Gets the score of the current game.
        /// </summary>
        public int Score { get; private set; }

        
        /// <summary>
        /// Gets the number of remaining lives of the current game.
        /// </summary>
        public int Lives { get; private set; }

        
        /// <summary>
        /// Original machine from which the game machines are made.
        /// </summary>
        private PinballMachine OriginalMachine;


        public Game(PinballMachine machine) {
            Status = GameStatus.Setup;

            OriginalMachine = machine;

            Start();
        }

         
        /// <summary>
        /// Starts the game.
        /// </summary>
        public void Start()
        {
            Status = GameStatus.Playing;

            Machine = new PinballGameMachine(OriginalMachine);
            Machine.prepareForLaunch();

            Score = 0;
            Lives = TOTAL_LIVES;

            // Wire up event handlers
            Machine.Collision += OnScore;
            Machine.GameOver += OnGameOver;

            Machine.IntroduceBall();
            Lives--;
        }

        
        /// <summary>
        /// Add a new ball if needed when ball gets lost.
        /// </summary>
        private void OnGameOver()
        {
            if (Lives == 0)
            {
                // GameOver... :(
                Status = GameStatus.GameOver;
            }
            else
            {
                Lives--;
                Machine.IntroduceBall();
            }
        }

        /// <summary>
        /// Increment score when a collision happened.
        /// </summary>
        /// <param name="sender"></param>
        private void OnScore(PinballElement sender)
        {
            Score += sender.Value;
        }

        

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void Pause()
        {
            Status = GameStatus.Pause;
        }


        
        /// <summary>
        /// Resumes the game if paused.
        /// </summary>
        public void Resume()
        {
        }

        
        /// <summary>
        /// Updates positions and checks for collisions, etc.
        /// </summary>
        public void Update(long elapsed)
        {            
            // Update elements
            Machine.Update(elapsed);
        }


        public bool IsRunning {
            get {
                return Machine.HasBall();
            }
        }

    }
}
