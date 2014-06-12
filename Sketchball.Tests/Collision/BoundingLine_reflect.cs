using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingLine_reflect
    {
        [TestMethod]
        public void TestBoundingLineReflect0Top()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);


            Vector ballSpeed = new Vector(0, 10);
            Vector ballPos = new Vector(20f, 0f);

            Vector hitPoint = new Vector(20, 50);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            reflection = bL1.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect180Bot()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);


            Vector ballSpeed = new Vector(0, -10);
            Vector ballPos = new Vector(20f, 100f);

            Vector hitPoint = new Vector(20, 50);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            reflection = bL1.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect45Right()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);


            Vector ballSpeed = new Vector(-5, 5);
            Vector ballPos = new Vector(20f, 45);

            Vector hitPoint = new Vector(15, 50);
            Vector expectedReflection = ballSpeed;
            expectedReflection.Y = -expectedReflection.Y;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            reflection = bL1.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect90Right()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);


            Vector ballSpeed = new Vector(-5, 0);
            Vector ballPos = new Vector(60, 50);

            Vector hitPoint = new Vector(50, 50);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            reflection = bL1.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }

        [TestMethod]
        public void TestBoundingLineReflect270Left()
        {
            //Preconfig
            Vector position1 = new Vector(0f, 50f);
            Vector target1 = new Vector(50f, 50f);


            Vector ballSpeed = new Vector(5, 0);
            Vector ballPos = new Vector(-20, 50);

            Vector hitPoint = new Vector(0, 50);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Line parent = new Line();
            BoundingContainer bCont = new BoundingContainer(parent);
            BoundingLine bL1 = new BoundingLine(position1, target1);
            bCont.AddBoundingBox(bL1);
            parent.Location = (new Vector(0, 0));

            //Operation
            reflection = bL1.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection, reflection);
        }
    }
}
