using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball.GameComponents;

namespace Sketchball.Tests.GameComponents
{
    [TestClass]
    public class HighscoreListTests
    {
        [TestMethod]
        public void ShouldIgnoreZeroEntries()
        {
            var list = new HighscoreList();

            list.Add(new HighscoreEntry("Simon", 0, DateTime.Now));

            Assert.AreEqual(0, list.Count);
        }


        [TestMethod]
        public void ShouldUseCorrectOrder()
        {
            // Arrange
            var list = new HighscoreList();
            var entry1 =  new HighscoreEntry("Simon", 1, DateTime.Now);
            var entry2 =  new HighscoreEntry("Simon", 2, DateTime.Now);


            // Act
            list.Add(entry1);
            list.Add(entry2);

            // Assert
            var enumerator = list.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreSame(entry2, enumerator.Current);
        }
    }
}
