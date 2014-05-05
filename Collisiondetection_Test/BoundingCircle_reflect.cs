using System;
using Sketchball.Collision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketchball;
using Sketchball.Elements;

namespace Collisiondetection_Test
{
    [TestClass]
    public class BoundingCircle_reflect
    {
        [TestMethod]
        public void TestReflection315DirectionCenter()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(0f, 0f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(100, 100);

            Vector2 hitPoint = new Vector2(120f - 14.142f, 120f - 14.142f);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection0Direction()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(120f, -100f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(0, 100);

            Vector2 hitPoint = new Vector2(120,100);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection45Direction()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(140f, 100f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(-100, 100);

            Vector2 hitPoint = new Vector2(120f+14.1421f, 120-14.1421f);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection90Direction()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(150f, 120f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(-100, 0);

            Vector2 hitPoint = new Vector2(140, 120);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X,0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection135Direction()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(140f, 120f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(-100, -100);

            Vector2 hitPoint = new Vector2(120 + 14.1421f, 120 + 14.1421f);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection180Direction()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(120f, 150f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(0, -100);

            Vector2 hitPoint = new Vector2(120, 140);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection225Direction()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(100f, 140f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(100, -100);

            Vector2 hitPoint = new Vector2(120 - 14.1421f, 120 + 14.1421f);
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflection270Direction()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(90f, 120f);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(100, 0);

            Vector2 hitPoint = new Vector2(100, 120 );
            Vector2 expectedReflection = -ballSpeed;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflectionHorizontalTopTouchDirection()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(0, 100);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(100, 0);

            Vector2 hitPoint = new Vector2(120, 100);
            Vector2 expectedReflection = ballSpeed;     //since entryangle =0
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }

        [TestMethod]
        public void TestReflectionVerticalLeftTouchDirection()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(100, 50);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(0, 100);

            Vector2 hitPoint = new Vector2(100, 120);
            Vector2 expectedReflection = ballSpeed;     //since entryangle =0
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }


        [TestMethod]
        public void TestReflectionHorizontalTopRandomSimulatedDirection()
        {
            //Preconfig
            Vector2 position = new Vector2(100f, 100f);
            Vector2 ballPos = new Vector2(0, 50);
            int radius = 20;

            Vector2 ballSpeed = new Vector2(120,100)-ballPos;

            Vector2 hitPoint = new Vector2(120, 100);
            Vector2 expectedReflection = ballSpeed;
            expectedReflection.Y = -expectedReflection.Y;
            expectedReflection.Normalize();
            Vector2 reflection;

            //Creation
            Bumper parent = new Bumper();
            BoundingCircle bC2 = new BoundingCircle(radius, position);
            BoundingContainer bCont = new BoundingContainer(parent);
            bCont.addBoundingBox(bC2);

            //Operation
            parent.setLocation(new Vector2(0, 0));

            reflection = bC2.reflect(ballSpeed, hitPoint, ballPos);
            reflection.Normalize();

            //Assertion
            Assert.AreEqual(expectedReflection.X, reflection.X, 0.001f);
            Assert.AreEqual(expectedReflection.Y, reflection.Y, 0.001f);
        }
      
    }
}
