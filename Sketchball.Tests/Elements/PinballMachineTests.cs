using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball.Elements;
using System.IO;
using System.Linq;

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
            Flipper flipper = new Flipper() { X = 5, Y = 5 };
            Stream stream = new MemoryStream();

            pbm.Add(flipper);
            pbm.Save(stream);
            stream.Position = 0;
            pbm2 = PinballMachine.FromStream(stream);

            Assert.AreEqual(1, pbm2.DynamicElements.Count);
            Assert.AreEqual(flipper.X, pbm2.DynamicElements.Last().X, 0.1);
            Assert.AreEqual(flipper.Y, pbm2.DynamicElements.Last().Y, 0.1);
        }



        [TestMethod]
        public void CanAddElement()
        {
            PinballMachine pbm = new PinballMachine();
            Flipper flipper = new Flipper() { X = 5, Y = 5 };

            pbm.Add(flipper);

            Assert.IsTrue(pbm.Elements.Contains(flipper));
        }


    }
}
