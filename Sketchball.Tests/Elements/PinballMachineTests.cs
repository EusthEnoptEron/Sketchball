using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball.Elements;
using System.IO;
using System.Linq;
using Sketchball.GameComponents;

namespace Sketchball.Tests.Elements
{
    [TestClass]
    public class PinballMachineTests
    {
        /// <summary>
        /// Tests basic serialization.
        /// </summary>
        [TestMethod]
        public void CanSerialize()
        {
            // Arrange
            PinballMachine pbm = new PinballMachine();
            string fileName = Path.GetTempFileName();
            FileInfo info;

            // Act
            pbm.Save(fileName);
            info = new FileInfo(fileName);

            // Assert
            Assert.IsTrue( info.Length > 0 );

            // Cleanup
            File.Delete(fileName);
        }

        /// <summary>
        /// Tests for basic deserialization capabilities.
        /// </summary>
        [TestMethod]
        public void CanDeserialize()
        {
            PinballMachine pbm = new PinballMachine();
            PinballMachine pbm2;
            string fileName = Path.GetTempFileName();

            pbm.Save(fileName);
            pbm2 = PinballMachine.FromFile(fileName);

            Assert.IsNotNull(pbm2);
            Assert.AreEqual(pbm.Elements.Count(), pbm2.Elements.Count());

            // Cleanup
            File.Delete(fileName);
        }

        [TestMethod]
        public void HasBidirectionalConsistencyAfterDeserialization()
        {
            PinballMachine pbm = new PinballMachine();
            PinballMachine pbm2;
            Stream stream = new MemoryStream();

            pbm.Save(stream);
            stream.Seek(0, SeekOrigin.Begin);
            pbm2 = PinballMachine.FromStream(stream);

            Assert.AreSame(pbm2, pbm2.DynamicElements.Owner);
        }

        [TestMethod]
        public void CanSerializeWithUserObject()
        {
            PinballMachine pbm = new PinballMachine();
            PinballMachine pbm2;
            Flipper flipper = new LeftFlipper() { X = 5, Y = 5 };
            Stream stream = new MemoryStream();

            pbm.Add(flipper);
            pbm.Save(stream);
            stream.Position = 0;
            pbm2 = PinballMachine.FromStream(stream);

            Assert.AreEqual(1, pbm2.DynamicElements.Count);
            Assert.AreEqual(flipper.X, pbm2.DynamicElements.Last().X, 0.1);
            Assert.AreEqual(flipper.Y, pbm2.DynamicElements.Last().Y, 0.1);
        }

        /// <summary>
        /// Tests if the file is truncated on save. We encountered this error because 
        /// we originally used FileMode.Create which only overrode the old file instead of truncating it.
        /// e.g. the file would representatively look like this (1 = data from the first machine, 2 = data from the 2nd machine):
        /// 22222222222222222222222222
        /// 22222222222222222222222222
        /// 22222222222222222222222222
        /// 22222222222111111111111111 <-----
        /// </summary>
        [TestMethod]
        public void CanSerializeTwiceIntoSameFile()
        {
            // Arrange
            var mShorter = new PinballMachine();
            var mLonger = new PinballMachine();
            string file = Path.GetTempFileName();
            mLonger.Add(new Bumper());

            // Act
            mLonger.Save(file);
            mShorter.Save(file);

            var newMachine = PinballMachine.FromFile(file);
            Assert.IsTrue(newMachine.IsValid());
        }


        [TestMethod]
        public void CanAddElement()
        {
            PinballMachine pbm = new PinballMachine();
            Flipper flipper = new LeftFlipper() { X = 5, Y = 5 };

            pbm.Add(flipper);

            Assert.IsTrue(pbm.Elements.Contains(flipper));
        }

        [TestMethod]
        public void ShouldMaintainCorrectRelationship()
        {
            var machine = new PinballMachine();
            var flipper = new LeftFlipper();


            Assert.IsNull(flipper.World);

            machine.Add(flipper);

            Assert.AreEqual(machine, flipper.World);

            machine.Remove(flipper);

            Assert.IsNull(flipper.World);
        }

        [TestMethod]
        public void ShouldBeInvalidForCrossingLines()
        {
            var machine = new PinballMachine();
            var line1 = new Line(100, 100, 100, 200);
            var line2 = new Line(50, 150, 150, 150);

            machine.Add(line1);
            machine.Add(line2);

            Assert.IsFalse(machine.IsValid());
        }

        [TestMethod]
        public void ShouldBeValidForParallelLines()
        {
            var machine = new PinballMachine();
            var line1 = new Line(100, 100, 100, 200);
            var line2 = new Line(105, 100, 105, 200);

            machine.Add(line1);
            machine.Add(line2);

            Assert.IsTrue(machine.IsValid());
        }

        [TestMethod]
        public void ShouldDeepCloneReferences()
        {
            var machine = new PinballMachine();
            var element = new Bumper();
            var entry = new HighscoreEntry("Simon", 1, DateTime.Now);

            machine.Add(element);
            machine.Highscores.Add(entry);
            var copy = machine.Clone() as PinballMachine;

            Assert.AreNotSame(copy, machine);
            Assert.AreNotSame(copy.Balls, machine.Balls);
            Assert.AreNotSame(copy.Highscores, machine.Highscores);
            Assert.AreNotSame(copy.Highscores.First(), machine.Highscores.First());
            Assert.AreNotSame(copy.Layout, machine.Layout);
        }
    }
}
