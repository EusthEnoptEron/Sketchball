using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;

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
            Vector2 center = new Vector2(0f, 0f);

            //Creation
            BoundingCircle bC = new BoundingCircle(radius, center);

            //Operation

            //Assertion
            Assert.IsNotNull(bC);
            Assert.AreEqual(bC.Position, center + new Vector2(radius, radius));
            Assert.AreEqual(bC.radius, radius);
        }

        //Below here: Test methods for circle-line
        [TestMethod]
        public void TestIntersectIntersectNone()
        {
            //Preconfig
            Vector2 target1 = new Vector2(20, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(10, 0f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            parent.Location = new Vector2(0, 0);
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 30f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = new Vector2(0, 0);
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = new Vector2(0, 0);
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 9f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = new Vector2(0, 0);
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = new Vector2(0, 0);
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 10f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = new Vector2(0, 0);
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = new Vector2(0, 0);
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 11f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 30f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 40f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 0f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 50f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(0, 51f);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(-41, 30);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(-20, 50);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 40;
            ball.Height = 40;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(-39, 30);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(41, 30);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(40, 30);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
            Vector2 target1 = new Vector2(40, 50);
            Vector2 position1 = new Vector2(0f, 50f);

            int radius2 = 20;
            Vector2 center2 = new Vector2(39, 30);
            Vector2 ballSpeed = new Vector2(0, 5);

            Vector2 hitPoint;
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
            ball.Location = (new Vector2(0, 0));
            ball.Width = 20;
            ball.Height = 20;
            parent.Location = (new Vector2(0, 0));
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
