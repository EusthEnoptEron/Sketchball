using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingLine_intersectWithCircle
    {
        [TestMethod]
        public void TestCreation()
        {
            //Preconfig
            Vector position1 = new Vector(0f,50f);
            Vector target1 = new Vector(50f, 50f);

            //Creation
            BoundingLine bL = new BoundingLine(position1, target1);

            //Operation

            //Assertion
            Assert.IsNotNull(bL);
            Assert.AreEqual(bL.Position, position1);
            Assert.AreEqual(bL.target, target1);
        }

        //Below here: Intersection tests with circle
        [TestMethod]
        public void TestIntersectNoIntersect()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(100, 100f);
            Vector ballSpeed = new Vector(-5, 0);
                      

            Vector hitPoint;
            bool isIntersec = false;

            //Creation
            Line parent = new Line();
            Ball ball = new Ball();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingContainer bCont2 = new BoundingContainer(ball);

            BoundingLine bL1 = new BoundingLine(position1,target1);
            BoundingCircle bC2 = new BoundingCircle(radius2, center2);

            bCont.AddBoundingBox(bL1);
            bCont2.AddBoundingBox(bC2);
            ball.Velocity = ballSpeed;
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectTouchTop()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectTouchLeft()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(-40, 30);
            Vector ballSpeed = new Vector(5,0);


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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersectTouchBot()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(20, 50);
            Vector ballSpeed = new Vector(0, -5);


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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

      

        [TestMethod]
        public void TestIntersectTouchRight()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(50, 30);
            Vector ballSpeed = new Vector(-5, 0);


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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsFalse(isIntersec);
        }

        [TestMethod]
        public void TestIntersect1pxOverlapRight()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(49, 30);
            Vector ballSpeed = new Vector(-5, 0);


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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(target1,hitPoint);
        }

        [TestMethod]
        public void TestIntersect1pxOverlapTop()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(20, 11f);
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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40,50), hitPoint);
        }

        [TestMethod]
        public void TestIntersect1pxOverlapBot()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(20, 49);
            Vector ballSpeed = new Vector(0, -5);


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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40,50), hitPoint);
        }

        [TestMethod]
        public void TestIntersect1pxOverlapLeft()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(-39, 30);
            Vector ballSpeed = new Vector(5, 0);


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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(0, 50), hitPoint);
        }

        [TestMethod]
        public void TestIntersectCenterOfCircleOnLine()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);

            int radius2 = 20;
            Vector center2 = new Vector(20, 30);
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
            parent.Location = (new Vector(0, 0));

            //Operation
            isIntersec = bC2.Intersect(bL1, out hitPoint);

            //Assertion
            Assert.IsTrue(isIntersec);
            Assert.AreEqual(new Vector(40, 50), hitPoint);
        }
    }
}
