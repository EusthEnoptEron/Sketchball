using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingCircle_intersectWithLine
    {
        [TestMethod]
        public void TestCreation()
        {
            //Preconfig
            int radius = 20;
            Vector center = new Vector(0f, 0f);

            //Creation
            BoundingCircle bC = new BoundingCircle(radius, center);

            //Operation

            //Assertion
            Assert.IsNotNull(bC);
            Assert.AreEqual(bC.Position, center + new Vector(radius, radius));
            Assert.AreEqual(bC.radius, radius);
        }

        //Below here: Test methods for circle-line
        [TestMethod]
        public void TestIntersectIntersectNone()
        {
            //Preconfig
            Vector target1 = new Vector(20, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(10, 0f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = new Vector(0, 0);
            parent.Width = 20;
            parent.Height = 0;

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectNormal()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(0, 30f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = new Vector(0, 0);
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = new Vector(0, 0);
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(20f,hitPoint.X,2);
            Assert.AreEqual(50f, hitPoint.Y, 2);
        }

        [TestMethod]
        public void TestIntersectIntersectNear1pxTop()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(0, 9f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = new Vector(0, 0);
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = new Vector(0, 0);
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectTouchTop()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(0, 10f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = new Vector(0, 0);
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = new Vector(0, 0);
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectOverlap1pxTop()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(0, 11f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(20f, hitPoint.X, 2);
            Assert.AreEqual(50f, hitPoint.Y, 2);
        }

        [TestMethod]
        public void TestIntersectIntersectNormalTop2()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50);

            int radius2 = 20;
            Vector center2 = new Vector(0, 30f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(20f, hitPoint.X, 2);
            Assert.AreEqual(50f, hitPoint.Y, 2);
        }


        [TestMethod]
        public void TestIntersectIntersectOverHalfwayThrough()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50);

            int radius2 = 20;
            Vector center2 = new Vector(0, 40f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(20f, hitPoint.X, 2);
            Assert.AreEqual(50f, hitPoint.Y, 2);
        }

        [TestMethod]
        public void TestIntersectIntersectToFarStillTouchBot()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 0f);

            int radius2 = 20;
            Vector center2 = new Vector(0, 50f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectTooFar1pxAwayBot()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(0, 51f);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectLeft1pxTooFarAway()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(-41, 30);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectLeftTouch()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(-20, 50);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 40;
            ball.Height = 40;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectleft1pxOverlap()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(-39, 30);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(0, hitPoint.X, 2);
            Assert.AreEqual(50f, hitPoint.Y, 2);
        }


        [TestMethod]
        public void TestIntersectIntersectRight1pxTooFarAway()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(41, 30);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectRightTouch()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(40, 30);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectIntersectRight1pxOverlap()
        {
            //Preconfig
            Vector target1 = new Vector(40, 50);
            Vector position1 = new Vector(0f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(39, 30);
            Vector ballSpeed = new Vector(0, 5);

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);
            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            ball.Location = (new Vector(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector(0, 0));
            parent.Width = 40;
            parent.Height = 0;

            //Operation
            isIntersec = bL1.Intersect(bC2, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(40, hitPoint.X, 2);
            Assert.AreEqual(50f, hitPoint.Y, 2);
        }
    }
}
