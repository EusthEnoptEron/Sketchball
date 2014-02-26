using Sketchball.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketchball.Collision
{
    public class BoundingRaster
    {
        public int rows { get; private set; }
        public int cols { get; private set; }

        public int width { get; set; }
        public int height { get; set; }

        private LinkedList<IBoundingBox> animatedObjects;
        private BoundingField[,] fields;

        public BoundingRaster(int rows, int cols, int width, int height)
        {
            this.animatedObjects = new LinkedList<IBoundingBox>();
            this.fields = new BoundingField[rows, cols];

            this.rows = rows;
            this.cols = cols;

            this.width = width;
            this.height = height;
        }

        public void takeOverBoundingBoxes(LinkedList<PinballElement> eles)
        {
            int fieldWidth = this.width / this.cols;
            int fieldHeight = this.height / this.rows;

            foreach (PinballElement pE in eles)
            {
                BoundingContainer bC = pE.getBoundingContainer();

                int x;
                int y;

                foreach (IBoundingBox b in bC.boundingBoxes)
                {
                    if(b.GetType() == typeof(BoundingCircle))
                    {
                        //Strategy: Make a box around the circle
                        BoundingCircle bCir = (BoundingCircle)b;

                        //position of the circle self (which field)
                        x = ((int)bCir.position.X / fieldWidth);
                        y = ((int)bCir.position.Y / fieldHeight);

                        
                        //amount of fields x and y (rounded up) 
                        //2*cicFieldsX + (1 where the ceneter is) will make the width of the square which includes the circle)
                        //2*cicFieldsY + (1 where the ceneter is) will make the height of the square which includes the circle)
                        int circFieldsX = (int)Math.Ceiling((double)(bCir.radius / fieldWidth));
                        int circFieldsy = (int)Math.Ceiling((double)(bCir.radius / fieldHeight));

                        //go from the left to the right of the square
                        for (int h = x - circFieldsX; h >= x + circFieldsX; h++)
                        {
                            if (h < 0 || h >= this.cols)
                            {
                                //if h is out of raster skip this
                                continue;
                            }
                            for (int ver = y - circFieldsy; ver == y + circFieldsy; ver++)
                            {
                                if(ver < 0||ver>=this.rows)
                                {
                                     //if ver is out of raster skip this
                                    continue;
                                }
                                //this is a field which should hold a reference to the boundingCircle
                                this.fields[h, ver].addReference(bCir);     //duplicate entries will be neglected
                            }
                        }
                    }
                    else
                    {
                        //line
                        BoundingLine bL = (BoundingLine)b;

                        float posX = bL.position.X;
                        float posY = bL.position.Y;
                        //position of the line base (which field)
                        x = ((int)posX / fieldWidth);
                        y = ((int)posY / fieldHeight);

                        //add the start
                        this.fields[x, y].addReference(bL);     //duplicate entries will be neglected

                        Vector2 unitV = bL.target - bL.position;
                        unitV.Normalize();


                        if (unitV.X > 0)
                        {
                            float deltaRight = fieldWidth - (posX - fieldWidth * x);     //distance from right end of field to the position
                            float factorToNextXCross = deltaRight / unitV.X;

                            while (posX < bL.target.X)
                            {
                                //direction left to right
                                float nextXCross = posX + deltaRight;
                                float nextXCrossY = posY + factorToNextXCross * unitV.Y;      //because factorToNextXCross time unitvector in x direction = new point

                                //float newFieldXIdx = ((int)nextXCross / fieldWidth);
                                int newFieldYIdx = ((int)nextXCrossY / fieldHeight);

                                if (unitV.Y > 0)        //heading down
                                {
                                    if (newFieldYIdx > y)
                                    {
                                        //we entered new y field(s)
                                        for (int i = y; i < newFieldYIdx; y++)
                                        {
                                            this.fields[x, i].addReference(b);      //this is a field that the line goes through
                                        }
                                    }
                                }
                                else
                                {
                                    //heading up
                                    if (newFieldYIdx < y)
                                    {
                                        //we entered new y field(s)
                                        for (int i = y; i > newFieldYIdx; y--)
                                        {
                                            this.fields[x, i].addReference(b);      //this is a field that the line goes through
                                        }
                                    }
                                }

                                //update field index
                                x++;
                                y = newFieldYIdx;

                                //update position
                                posX += nextXCross;
                                posY += nextXCrossY;

                                //since we are at the border of a field the deltaRight will allways be the full field width
                                deltaRight = fieldWidth;

                                this.fields[x, newFieldYIdx].addReference(b);       //add the next x field (since the line just gets crossed)
                            }

                            //at this point all x fields have been added but there might be some y fields who get touched but are not referenced yet.
                            //that are all fields that are above the last x cross (like directly under the target)


                            int endField = ((int)bL.target.Y / fieldHeight);
                            if (unitV.Y > 0)        //heading down
                            {
                                for (int i = y + 1; i >= endField; i++)
                                {
                                    this.fields[x, i].addReference(bL);
                                }
                            }
                            else   //heading up
                            {
                                for (int i = endField; i <= y - 1; i++)
                                {
                                    this.fields[x, i].addReference(bL);
                                }
                            }

                        }
                        else
                        {
                            //direction right to left
                            float deltaLeft = (bL.position.X - fieldWidth * x);     //distance from left begin of field to the position
                        }

                    }
                  
                }
            }
        }


        public void handleCollision(Ball ball)
        {
            //Collide first with animated object then with object around ball self

            //Collision logic
            foreach (IBoundingBox b in this.animatedObjects)
            {
                //does any animated object intersec?
                if (b.intersec(ball.getBoundingContainer().))       //specify bounding box of ball
                {
                    //collision
                    ball.Acceleration = b.reflect(ball.Acceleration);       //TODO: is acceleration the velocity vector of the ball?
                }
            }

            int fieldWidth = this.width / this.cols;
            int fieldHeight = this.height / this.rows;

            int x = (int)(pos.X / fieldWidth);
            int y = (int)(pos.Y / fieldHeight);

            HashSet<IBoundingBox> surrounding = new HashSet<IBoundingBox>();
            for (int x1 = x - 1; x1 < x + 1; x1++)
            {
                for (int y1 = y - 1; y1 < y + 1; y1++)
                {
                    foreach (IBoundingBox b in this.fields[x1, y1].getReferences())
                    {
                        if (b.intersec(ball.getBoundingContainer().))       //specify bounding box of ball
                        {
                            //collision
                            ball.Acceleration = b.reflect(ball.Acceleration);       //TODO: is acceleration the velocity vector of the ball?
                        }
                    }
                }
            }
        }

        


        public void addAnimatedObject(IBoundingBox aO)
        {
            this.animatedObjects.AddLast(aO);
        }

        public void removeAnimatedObject(IBoundingBox aO)
        {
            this.animatedObjects.Remove(aO);
        }

    }
}
