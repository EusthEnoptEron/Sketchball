using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball.Elements;

namespace Sketchball.Tests.Editor
{
    /// <summary>
    /// Test fixture for History class.
    /// </summary>
    [TestClass]
    public class HistoryTests
    {

        /// <summary>
        /// Makes sure that CanUndp() is false at the start (there are no changes!)
        /// </summary>
        [TestMethod]
        public void CanUndoIsCorrectAtStart()
        {
            var history = new History();

            Assert.IsFalse(history.CanUndo());
        }


        /// <summary>
        /// Makes sure that CanRedo() is false at the start (there are no changes!)
        /// </summary>
        [TestMethod]
        public void CanRedoIsCorrectAtStart()
        {
            var history = new History();

            Assert.IsFalse(history.CanRedo());
        }


        /// <summary>
        /// Makes sure that a change is correctly undone.
        /// </summary>
        [TestMethod]
        public void ShouldCorrectlyUndo()
        {
            var history = new History();
            var element = new Ball() { X = 0, Y = 0 };
            var change = new TranslationChange(element, 5, 5);

            history.AddAndDo(change);
            history.Undo();

            Assert.IsFalse(history.CanUndo());
            Assert.IsTrue(history.CanRedo());

            Assert.AreEqual(0, element.X, 0.1);
            Assert.AreEqual(0, element.Y, 0.1);
        }


        /// <summary>
        /// Makes sure that a change is correctly redone.
        /// </summary>
        [TestMethod]
        public void ShouldCorrectlyRedo()
        {
            var history = new History();
            var element = new Ball() { X = 0, Y = 0 };
            var change = new TranslationChange(element, 5, 5);

            history.AddAndDo(change);
            history.Undo();
            history.Redo();

            Assert.IsTrue(history.CanUndo());
            Assert.IsFalse(history.CanRedo());

            Assert.AreEqual(5, element.X, 0.1);
            Assert.AreEqual(5, element.Y, 0.1);
        }

        /// <summary>
        /// Makes sure that change events are raised correctly on:
        ///     1. add
        ///     2. undo
        ///     3. redo
        /// </summary>
        [TestMethod]
        public void ShouldRaiseEvents()
        {
            var eventsRaised = 0;
            var history = new History();
            var element = new Ball() { X = 0, Y = 0 };
            var change = new TranslationChange(element, 5, 5);

            // Bind closure expression
            history.Change += () =>
            {
                eventsRaised++;
            };

            // Act
            history.AddAndDo(change);
            history.Undo();
            history.Redo();

            Assert.AreEqual(3, eventsRaised);
        }
    }
}
