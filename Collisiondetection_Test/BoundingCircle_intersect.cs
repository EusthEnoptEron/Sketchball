using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingCircleIntersectTest1
    {
        [TestMethod]
        public void TestCreation()
        {
            //Preconfig
            int radius = 20;
            Vector2 center = new Vector2(0f,0f);

            //Creation
            BoundingCircle bC = new BoundingCircle(radius, center);

            //Operation

            //Assertion
            Assert.IsNotNull(bC);
            Assert.AreEqual(bC.position, center + new Vector2(radius, radius));
            Assert.AreEqual(bC.radius, radius);
        }

        [TestMethod]
        public void TestIntersectNoIntersect()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(42f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);
                      

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectNoIntersectHitPointNull()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(42f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.AreEqual(hitPoint, new Vector2(0,0));
        }

        [TestMethod]
        public void TestIntersectNoIntersectNarrow()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(41f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectNoIntersectTouch()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(40f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

           
            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }


        [TestMethod]
        public void TestIntersectIntersectOverlap1px()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(39f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.setLocation(new Vector2(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector2(40, 20), hitPoint);
        }


        [TestMethod]
        public void TestIntersectIntersectCongruent()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(0f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.setLocation(new Vector2(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector2(40, 20), hitPoint);
        }


        [TestMethod]
        public void TestIntersectIntersectNormal()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(30f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.setLocation(new Vector2(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector2(40, 20), hitPoint);
        }

        [TestMethod]
        public void TestIntersectIntersectTooFar()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(-10f, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.setLocation(new Vector2(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector2(40, 20), hitPoint);
        }

        [TestMethod]
        public void TestIntersectIntersect1pxLeftTooFar()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(-19, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.setLocation(new Vector2(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector2(40, 20), hitPoint);
        }

        [TestMethod]
        public void TestIntersectIntersectBallSpeedZero()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(-19, 0f);
            Vector2 ballSpeed = new Vector2(0, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.setLocation(new Vector2(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.Fail();
        }

        [TestMethod]
        public void TestIntersectIntersectTouchLeft()
        {
            //Preconfig
            int radius1 = 20;
            Vector2 center1 = new Vector2(0f, 0f);
            int radius2 = 20;
            Vector2 center2 = new Vector2(-20, 0f);
            Vector2 ballSpeed = new Vector2(-5, 0);

            Vector2 hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.addBoundingBox(bC1);
            bCont.addBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.setLocation(new Vector2(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.intersec(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

    }
}
