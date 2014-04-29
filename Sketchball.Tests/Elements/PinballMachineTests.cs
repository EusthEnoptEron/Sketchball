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
    }
}
