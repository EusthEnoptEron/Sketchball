using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;
using System.Windows;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingCircle_reflect
    {
        [TestMethod]
        public void TestReflection315DirectionCenter()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(0f, 0f);
            int radius = 20;

            Vector ballSpeed = new Vector(100, 100);

            Vector hitPoint = new Vector(120f - 14.142f, 120f - 14.142f);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection0Direction()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(120f, -100f);
            int radius = 20;

            Vector ballSpeed = new Vector(0, 100);

            Vector hitPoint = new Vector(120,100);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection45Direction()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(140f, 100f);
            int radius = 20;

            Vector ballSpeed = new Vector(-100, 100);

            Vector hitPoint = new Vector(120f+14.1421f, 120-14.1421f);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection90Direction()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(150f, 120f);
            int radius = 20;

            Vector ballSpeed = new Vector(-100, 0);

            Vector hitPoint = new Vector(140, 120);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X,0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection135Direction()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(140f, 120f);
            int radius = 20;

            Vector ballSpeed = new Vector(-100, -100);

            Vector hitPoint = new Vector(120 + 14.1421f, 120 + 14.1421f);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection180Direction()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(120f, 150f);
            int radius = 20;

            Vector ballSpeed = new Vector(0, -100);

            Vector hitPoint = new Vector(120, 140);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection225Direction()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(100f, 140f);
            int radius = 20;

            Vector ballSpeed = new Vector(100, -100);

            Vector hitPoint = new Vector(120 - 14.1421f, 120 + 14.1421f);
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection270Direction()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(90f, 120f);
            int radius = 20;

            Vector ballSpeed = new Vector(100, 0);

            Vector hitPoint = new Vector(100, 120 );
            Vector expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflectionHorizontalTopTouchDirection()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(0, 100);
            int radius = 20;

            Vector ballSpeed = new Vector(100, 0);

            Vector hitPoint = new Vector(120, 100);
            Vector expectedReflection = ballSpeed;     //since entryangle =0
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflectionVerticalLeftTouchDirection()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(100, 50);
            int radius = 20;

            Vector ballSpeed = new Vector(0, 100);

            Vector hitPoint = new Vector(100, 120);
            Vector expectedReflection = ballSpeed;     //since entryangle =0
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }


        [TestMethod]
        public void TestReflectionHorizontalTopRandomSimulatedDirection()
        {
            //Preconfig
            Vector position = new Vector(100f, 100f);
            Vector ballPos = new Vector(0, 50);
            int radius = 20;

            Vector ballSpeed = new Vector(120,100)-ballPos;

            Vector hitPoint = new Vector(120, 100);
            Vector expectedReflection = ballSpeed;
            expectedReflection.Y = -expectedReflection.Y;
            expectedReflection.Normalize();
            Vector reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.AddBoundingBox(bC2);

            //Operation
            parent.Location = (new Vector(0, 0));

            reflection = bC2.Reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }
      
    }
}
