using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingLine_reflect
    {
        [TestMethod]
        public void TestBoundingLineReflect0Top()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);


            Vector2 ballSpeed = new Vector2(0, 10);
            Vector2 ballPos = new Vector2(20f, 0f);

            Vector2 hitPoint = new Vector2(20, 50);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            reflection = bL1.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect180Bot()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);


            Vector2 ballSpeed = new Vector2(0, -10);
            Vector2 ballPos = new Vector2(20f, 100f);

            Vector2 hitPoint = new Vector2(20, 50);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            reflection = bL1.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect45Right()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);


            Vector2 ballSpeed = new Vector2(-5, 5);
            Vector2 ballPos = new Vector2(20f, 45);

            Vector2 hitPoint = new Vector2(15, 50);
            Vector2 expectedReflection = ballSpeed;
            expectedReflection.Y = -expectedReflection.Y;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            reflection = bL1.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect90Right()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);


            Vector2 ballSpeed = new Vector2(-5, 0);
            Vector2 ballPos = new Vector2(60, 50);

            Vector2 hitPoint = new Vector2(50, 50);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            reflection = bL1.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect270Left()
        {
            //Preconfig
            Vector2 position1 = new Vector2(0f, 50f);
            Vector2 target1 = new Vector2(50f, 50f);


            Vector2 ballSpeed = new Vector2(5, 0);
            Vector2 ballPos = new Vector2(-20, 50);

            Vector2 hitPoint = new Vector2(0, 50);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.addBoundingBox(bL1);
            parent.Location = (new Vector2(0, 0));

            //Operation
            reflection = bL1.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }
    }
}
