using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingCircleIntersectWithCircle
    {
        [TestMethod]
        public void TestCreation()
        {
            //Preconfig
            int radius = 20;
            Vector center = new Vector(0f,0f);

            //Creation
            BoundingCircle bC = new BoundingCircle(radius, center);

            //Operation

            //Assertion
            Assert.IsNotNull(bC);
            Assert.AreEqual(bC.Position, center + new Vector(radius, radius));
            Assert.AreEqual(bC.radius, radius);
        }

        //Below here: Intersection tests with circle
        [TestMethod]
        public void TestIntersectNoIntersect()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(42f, 0f);
            Vector ballSpeed = new Vector(-5, 0);
                      

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);

            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectNoIntersectHitPointNull()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(42f, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);

            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.AreEqual(hitPoint, new Vector(0,0));
        }

        [TestMethod]
        public void TestIntersectNoIntersectNarrow()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(41f, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectNoIntersectTouch()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(40f, 0f);
            Vector ballSpeed = new Vector(-5, 0);

           
            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }


        [TestMethod]
        public void TestIntersectIntersectOverlap1px()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(39f, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40, 20), hitPoint);
        }


        [TestMethod]
        public void TestIntersectIntersectCongruent()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(0f, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint, ballSpeed);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40, 20), hitPoint);
        }


        [TestMethod]
        public void TestIntersectIntersectNormal()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(30f, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40, 20), hitPoint);
        }

        [TestMethod]
        public void TestIntersectIntersectTooFar()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(-10f, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint, ballSpeed);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40, 20), hitPoint);
        }

        [TestMethod]
        public void TestIntersectIntersect1pxLeftTooFar()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(-19, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint, ballSpeed);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40, 20), hitPoint);
        }

        [TestMethod]
        public void TestIntersectIntersectBallSpeedZero()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(-19, 0f);
            Vector ballSpeed = new Vector(0, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint, ballSpeed);

            //Assertion
            Assert.IsTrue(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectTouchLeft()
        {
            //Preconfig
            int radius1 = 20;
            Vector center1 = new Vector(0f, 0f);
            int radius2 = 20;
            Vector center2 = new Vector(-40, 0f);
            Vector ballSpeed = new Vector(-5, 0);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Bumper parent = new Bumper();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingCircle bC1 = new BoundingCircle(radius1, center1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bC1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));
            parent.Width = 2 * radius1;
            parent.Height = 2 * radius1;

            //Operation
            isIntersec = bC1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

    }
}
