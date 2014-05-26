using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingLine_pushBack
    {
        [TestMethod]
        public void TestPushBackPushDownFromAbove()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(20, 0);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint= new Vector2(20,50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = new Vector2(0,-1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackPushUpFromBelow()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(20,100);
            Vector2 ballSpeed = new Vector2(0, -5);

            Vector2 hitPoint = new Vector2(20, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = new Vector2(0, 1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackFromLeft()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(-20, 30);
            Vector2 ballSpeed = new Vector2(5, 0);

            Vector2 hitPoint = new Vector2(0, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = new Vector2(-1, 0);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackFromRight()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(70, 30);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint = new Vector2(50, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = new Vector2(1, 0);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestIPushBackTopRightToCenter()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(25, 45);
            Vector2 ballSpeed = new Vector2(-5, 5);

            Vector2 hitPoint = new Vector2(20, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = new Vector2(0,-1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackBotRightToCenter()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(25, 55);
            Vector2 ballSpeed = new Vector2(-5, -5);

            Vector2 hitPoint = new Vector2(20, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = new Vector2(0, 1);
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec, pushBackVec);
        }

        [TestMethod]
        public void TestPushBackFromTopRightToEnd()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(35, 25);
            Vector2 ballSpeed = new Vector2(-5, 5);

            Vector2 hitPoint = new Vector2(50, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X, 0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }

        [TestMethod]
        public void TestPushBackFromBotRightToEnd()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(55, 55);
            Vector2 ballSpeed = new Vector2(-5, -5);

            Vector2 hitPoint = new Vector2(50, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X, 0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }

        [TestMethod]
        public void TestPushBackFromBotLeftToEnd()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(25, 35);
            Vector2 ballSpeed = new Vector2(5, -5);

            Vector2 hitPoint = new Vector2(50, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X, 0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }

        [TestMethod]
        public void TestPushBackFromTopLeftToEnd()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(25, 25);
            Vector2 ballSpeed = new Vector2(5, 5);

            Vector2 hitPoint = new Vector2(50, 50);
            Vector2 pushBackVec;
            Vector2 estimatedPushBackVec = -ballSpeed;
            estimatedPushBackVec.Normalize();

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);

            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            pushBackVec = bL1.getOutOfAreaPush(radius2 * 2, hitPoint, -ballSpeed, center2);
            pushBackVec.Normalize();

            //Assertion
            Assert.AreEqual(estimatedPushBackVec.X, pushBackVec.X,0.01f);
            Assert.AreEqual(estimatedPushBackVec.Y, pushBackVec.Y, 0.01f);
        }
    }
}
