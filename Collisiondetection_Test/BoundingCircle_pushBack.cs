using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingCircle_pushBack
    {
        [TestMethod]
        public void TestBoundingCirclePushBackTop()
        {
            //Preconfig
            int radius = 20;
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(120, 80);

            Vector2 ballSpeed = new Vector2(120, 100) - ballPos;

            Vector2 hitPoint = new Vector2(120, 100);
            Vector2 expectedPushBack = (radius * 2 / 1.9f) * Vector2.Normalize((hitPoint - (position + new Vector2(radius, radius))));

            Vector2 pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));
            pushBackVec = bC2.getOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackRight()
        {
            //Preconfig
            int radius = 20;
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(150, 120);
            Vector2 hitPoint = new Vector2(140, 120);

            Vector2 ballSpeed = hitPoint - ballPos;

            Vector2 expectedPushBack = (radius * 2 / 1.9f) * Vector2.Normalize((hitPoint - (position + new Vector2(radius, radius))));

            Vector2 pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));
            pushBackVec = bC2.getOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackBot()
        {
            //Preconfig
            int radius = 20;
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(120, 150);
            Vector2 hitPoint = new Vector2(120, 140);

            Vector2 ballSpeed = hitPoint - ballPos;

            Vector2 expectedPushBack = (radius * 2 / 1.9f) * Vector2.Normalize((hitPoint - (position + new Vector2(radius, radius))));

            Vector2 pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));
            pushBackVec = bC2.getOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackLeft()
        {
            //Preconfig
            int radius = 20;
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(90, 120);
            Vector2 hitPoint = new Vector2(100, 120);

            Vector2 ballSpeed = hitPoint - ballPos;

            Vector2 expectedPushBack = (radius * 2 / 1.9f) * Vector2.Normalize((hitPoint - (position + new Vector2(radius, radius))));

            Vector2 pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));
            pushBackVec = bC2.getOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackLeftTop()
        {
            //Preconfig
            int radius = 20;
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(100, 100);
            Vector2 hitPoint = new Vector2(120 - 14.1421f, 120 - 14.1421f);

            Vector2 ballSpeed = hitPoint - ballPos;

            Vector2 expectedPushBack = (radius * 2 / 1.9f) * Vector2.Normalize((hitPoint - (position + new Vector2(radius, radius))));

            Vector2 pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));
            pushBackVec = bC2.getOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }
    }
}
