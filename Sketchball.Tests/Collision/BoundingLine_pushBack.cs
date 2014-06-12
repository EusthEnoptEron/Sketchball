using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingLine_pushBack
    {
        [TestMethod]
        public void TestPushBackPushDownFromAbove()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(20, 0);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint= new Vector(20,50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = new Vector(0,-1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackPushUpFromBelow()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(20,100);
            Vector ballSpeed = new Vector(0, -5);

            Vector hitPoint = new Vector(20, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = new Vector(0, 1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackFromLeft()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(-20, 30);
            Vector ballSpeed = new Vector(5, 0);

            Vector hitPoint = new Vector(0, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = new Vector(-1, 0);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackFromRight()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(70, 30);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint = new Vector(50, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = new Vector(1, 0);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestIPushBackTopRightToCenter()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(25, 45);
            Vector ballSpeed = new Vector(-5, 5);

            Vector hitPoint = new Vector(20, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = new Vector(0,-1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackBotRightToCenter()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(25, 55);
            Vector ballSpeed = new Vector(-5, -5);

            Vector hitPoint = new Vector(20, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = new Vector(0, 1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackFromTopRightToEnd()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(35, 25);
            Vector ballSpeed = new Vector(-5, 5);

            Vector hitPoint = new Vector(50, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X, 0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }

        [TestMethod]
        public void TestPushBackFromBotRightToEnd()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(55, 55);
            Vector ballSpeed = new Vector(-5, -5);

            Vector hitPoint = new Vector(50, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X, 0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }

        [TestMethod]
        public void TestPushBackFromBotLeftToEnd()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(25, 35);
            Vector ballSpeed = new Vector(5, -5);

            Vector hitPoint = new Vector(50, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X, 0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }

        [TestMethod]
        public void TestPushBackFromTopLeftToEnd()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(25, 25);
            Vector ballSpeed = new Vector(5, 5);

            Vector hitPoint = new Vector(50, 50);
            Vector pushBackVec;
            Vector estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            pushBackVec = bL1.GetOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X,0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }
    }
}
