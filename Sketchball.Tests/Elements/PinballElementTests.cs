using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball.Elements;

namespace Sketchball.Tests.Elements
{
    [TestClass]
    public class PinballElementTests
    {
        [TestMethod]
        public void ShouldCloneAllProperties()
        {
            // Arrange
            PinballElement element = new Bumper();
            float bounceFactor = 2;
            float scale        = 2;
            float rotation     = 90;

            // Act
            element.BounceFactor = bounceFactor;
            element.Scale = scale;
            element.BaseRotation = rotation;

            PinballElement clone = (PinballElement)element.Clone();

            // Assert
            Assert.AreEqual(bounceFactor, element.BounceFactor, 0.001);
            Assert.AreEqual(scale, element.Scale, 0.001);
            Assert.AreEqual(rotation, element.BaseRotation, 0.001);
        }
    }
}
