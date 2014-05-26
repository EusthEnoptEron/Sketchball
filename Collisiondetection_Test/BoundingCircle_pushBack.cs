using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;

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
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(120, 80);

            Vector ballSpeed = new Vector(120, 100) - ballPos;

            Vector hitPoint = new Vector(120, 100);
            Vector expectedPushBack = (radius * 2 / 1.9f) * ((hitPoint - (position + new Vector(radius, radius)))).AsNormalized();

            Vector pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));
            pushBackVec = bC2.GetOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackRight()
        {
            //Preconfig
            int radius = 20;
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(150, 120);
            Vector hitPoint = new Vector(140, 120);

            Vector ballSpeed = hitPoint - ballPos;

            Vector expectedPushBack = (radius * 2 / 1.9f) * ((hitPoint - (position + new Vector(radius, radius))).AsNormalized());

            Vector pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));
            pushBackVec = bC2.GetOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackBot()
        {
            //Preconfig
            int radius = 20;
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(120, 150);
            Vector hitPoint = new Vector(120, 140);

            Vector ballSpeed = hitPoint - ballPos;

            Vector expectedPushBack = (radius * 2 / 1.9f) * (hitPoint - (position + new Vector(radius, radius))).AsNormalized();

            Vector pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));
            pushBackVec = bC2.GetOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackLeft()
        {
            //Preconfig
            int radius = 20;
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(90, 120);
            Vector hitPoint = new Vector(100, 120);

            Vector ballSpeed = hitPoint - ballPos;

            Vector expectedPushBack = (radius * 2 / 1.9f) * ((hitPoint - (position + new Vector(radius, radius)))).AsNormalized();

            Vector pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));
            pushBackVec = bC2.GetOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }

        [TestMethod]
        public void TestBoundingCirclePushBackLeftTop()
        {
            //Preconfig
            int radius = 20;
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(100, 100);
            Vector hitPoint = new Vector(120 - 14.1421f, 120 - 14.1421f);

            Vector ballSpeed = hitPoint - ballPos;

            Vector expectedPushBack = (radius * 2 / 1.9f) * ((hitPoint - (position + new Vector(radius, radius)))).AsNormalized();

            Vector pushBackVec;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));
            pushBackVec = bC2.GetOutOfAreaPush(radius * 2, hitPoint, ballSpeed, ballPos);

            //Assertion
            Assert.AreEqual(expectedPushBack, pushBackVec);
        }
    }
}
