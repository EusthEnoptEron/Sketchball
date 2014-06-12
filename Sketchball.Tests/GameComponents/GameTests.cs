using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball.GameComponents;
using Sketchball.Elements;
using System.Threading;

namespace Sketchball.Tests.GameComponents
{
    [TestClass]
    public class GameTests
    {
        private PinballMachine pbm;
        private Game game;
        private String user;

        [TestInitialize]
        public void Setup()
        {
            user = "Simon";
            pbm = new PinballMachine();
            game = new Game(pbm, user);
        }

        [TestCleanup]
        public void Teardown()
        {
            // Make sure the game gets disposed
            game.Dispose();
        }


        [TestMethod]
        public void TestStartValues()
        {
            Assert.IsTrue(game.IsRunning);
            Assert.AreEqual(GameStatus.Playing, game.Status);
            Assert.AreEqual(Game.TOTAL_LIVES - 1, game.Lives);
        }

        [TestMethod]
        public void ShouldDecrementLivesOnBall()
        {

            // Act
            game.Machine.KillBall(game.Machine.Balls[0] as Ball);

            // Wait for it to update
            Thread.Sleep(100);

            Assert.AreEqual(Game.TOTAL_LIVES - 2, game.Lives);
            
        }

        [TestMethod]
        public void ShouldPauseCorrectly()
        {
            game.Pause();

            Assert.AreEqual(GameStatus.Paused, game.Status);

            game.Resume();

            Assert.AreEqual(GameStatus.Playing, game.Status);
        }
    }
}
