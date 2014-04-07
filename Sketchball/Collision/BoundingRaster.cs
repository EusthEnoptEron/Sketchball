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

        public int width { get; set; }      //TODO has to be updated when window size changes
        public int height { get; set; }

        private LinkedList<IBoundingBox> animatedObjects;
        private BoundingField[,] fields;

        private int fieldWidth;
        private int fieldHeight;

        public Vector2 hitPointDebug = new Vector2(0, 0);


        public BoundingRaster(int cols, int rows, int width, int height)
        {
            this.animatedObjects = new LinkedList<IBoundingBox>();
            this.fields = new BoundingField[cols, rows];

            this.rows = rows;
            this.cols = cols;

            this.width = width;
            this.height = height;

            this.fieldWidth = this.width / this.cols;
            this.fieldHeight = this.height / this.rows;

            for (int x = 0; x < this.cols; x++)
            {
                for (int y = 0; y < this.rows; y++)
                {
                    this.fields[x, y] = new BoundingField(x, y);
                }
            }

        }

        public void takeOverBoundingBoxes(ElementCollection eles)
        {
         
            foreach (PinballElement pE in eles)
            {
                BoundingContainer bC = pE.getBoundingContainer();
                Vector2 worldTrans = bC.parentElement.getLocation();

                int x;
                int y;

                foreach (IBoundingBox b in bC.boundingBoxes)
                {
                  
                    if(b.GetType() == typeof(BoundingCircle))
                    {
                        //Strategy: Make a box around the circle
                        BoundingCircle bCir = (BoundingCircle)b;

                        //position of the circle self (which field)
                        x = ((int)(bCir.position.X + worldTrans.X) / fieldWidth);        //TODO check if this is rounded down
                        y = ((int)(bCir.position.Y+worldTrans.Y) / fieldHeight);

                        
                        //amount of fields x and y (rounded up) 
                        //2*cicFieldsX + (1 where the ceneter is) will make the width of the square which includes the circle)
                        //2*cicFieldsY + (1 where the ceneter is) will make the height of the square which includes the circle)
                        int circFieldsX = (int)Math.Ceiling((double)(bCir.radius*1f / fieldWidth));
                        int circFieldsy = (int)Math.Ceiling((double)(bCir.radius*1f / fieldHeight));

                        //go from the left to the right of the square
                        for (int h = x - circFieldsX; h <= x + circFieldsX; h++)
                        {
                            if (h < 0 || h >= this.cols)
                            {
                                //if h is out of raster skip this
                                continue;
                            }
                            for (int ver = y - circFieldsy; ver <= y + circFieldsy; ver++)
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
                    else        //if(b.GetType() == typeof(BoundingCircle))
                    {
                        //line
                        BoundingLine bL = (BoundingLine)b;

                        float posX = bL.position.X + worldTrans.X;
                        float posY = bL.position.Y + worldTrans.Y;

                        //position of the line base (which field)
                        x = ((int)posX / fieldWidth);
                        y = ((int)posY / fieldHeight);

                        if(x < this.fields.Rank)
                        this.fields[x, y].addReference(bL);     //duplicate entries will be neglected

                        //define unitvector from position to target
                        Vector2 unitV = bL.target - bL.position;
                        unitV.Normalize();


                        if (unitV.X > 0)
                        {
                            takeOverBoundingLineLeftToRight(unitV,  posX,  posY,  x,  y,  bL, worldTrans);
                        }
                        else
                        {
                            takeOverBoundingLineRightToLeft(unitV, posX, posY, x, y, bL, worldTrans);
                        }

                        //at this point all x fields have been added but there might be some y fields who get touched but are not referenced yet.
                        //that are all fields that are above the last x cross (like directly under the target)


                        int endField = ((int)(bL.target.Y+worldTrans.Y) / fieldHeight);

                        if (unitV.Y > 0)        //heading down
                        {
                            for (int i = y + 1; i < endField; i++)
                            {
                                this.fields[x, i].addReference(bL);
                            }
                        }
                        else   //heading up
                        {
                            for (int i = y - 1; i >= endField - 1; i--)
                            {
                                if(i >= 0 && x < cols)
                                this.fields[x, i].addReference(bL);
                            }
                        }
                    }       //if(b.GetType() == typeof(BoundingCircle))
                }       //foreach (IBoundingBox b in bC.boundingBoxes)
            }       //foreach (PinballElement pE in eles)
        }

        private void takeOverBoundingLineLeftToRight(Vector2 unitV, float posX, float posY, int x, int y, BoundingLine bL, Vector2 worldTrans)
        {
            if (unitV.X == 0)
            {
                return;     //just y remains => the whole line is on one column
            }

            float deltaRight = fieldWidth - (posX - fieldWidth * x);     //distance from right end of field to the position
            float factorToNextXCross = deltaRight / unitV.X;

            while (posX < (bL.target.X + worldTrans.X))
            {
                factorToNextXCross = deltaRight / unitV.X;

                //direction left to right
                float nextXCross = posX + deltaRight;
                float nextXCrossY = posY + factorToNextXCross * unitV.Y;      //because factorToNextXCross time unitvector in x direction = new point

                //float newFieldXIdx = ((int)nextXCross / fieldWidth);
                int newFieldYIdx = ((int)(nextXCrossY) / fieldHeight);

                if (unitV.Y > 0)        //heading down
                {
                    if (newFieldYIdx > y)
                    {
                        //we entered new y field(s)
                        for (int i = y; i < newFieldYIdx; i++)
                        {
                            if (x <= fields.Rank)
                            this.fields[x, i].addReference(bL);      //this is a field that the line goes through
                        }
                    }
                }
                else
                {
                    //heading up
                    if (newFieldYIdx < y)
                    {
                        //we entered new y field(s)
                        for (int i = y; i > newFieldYIdx; i--)
                        {
                            if(i >= 0)
                            this.fields[x, i].addReference(bL);      //this is a field that the line goes through
                        }
                    }
                }

                //update field index
                x++;
                y = newFieldYIdx;

                //update position
                posX = nextXCross;
                posY = nextXCrossY;

                //since we are at the border of a field the deltaRight will allways be the full field width
                deltaRight = fieldWidth;

                if (posX <= bL.target.X + worldTrans.X)
                {
                    if(x < fields.Rank)
                    this.fields[x, newFieldYIdx].addReference(bL);       //add the next x field (since the line just gets crossed)
                }
            }

        }

        private void takeOverBoundingLineRightToLeft(Vector2 unitV, float posX, float posY, int x, int y, BoundingLine bL,Vector2 worldTrans)
        {
            if (unitV.X == 0)
            {
                return;     //just y remains => the whole line is on one column
            }

            float deltaLeft =  (posX - fieldWidth * x);     //distance from right end of field to the position
            float factorToNextXCross = deltaLeft / -unitV.X;
          

            while (posX > (bL.target.X+worldTrans.X))
            {
                factorToNextXCross = deltaLeft / -unitV.X;

                //direction left to right
                float nextXCross = posX - deltaLeft;
                float nextXCrossY = posY + factorToNextXCross * unitV.Y;      //because factorToNextXCross time unitvector in x direction = new point

                //float newFieldXIdx = ((int)nextXCross / fieldWidth);
                int newFieldYIdx = ((int)(nextXCrossY) / fieldHeight);

                if (unitV.Y > 0)        //heading down
                {
                    if (newFieldYIdx > y)
                    {
                        //we entered new y field(s)
                        for (int i = y; i < newFieldYIdx; y++)
                        {
                            this.fields[x, i].addReference(bL);      //this is a field that the line goes through
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
                            this.fields[x, i].addReference(bL);      //this is a field that the line goes through
                        }
                    }
                }

                //update field index
                x--;
                y = newFieldYIdx;

                //update position
                posX = nextXCross;
                posY = nextXCrossY;

                //since we are at the border of a field the deltaRight will allways be the full field width
                deltaLeft = fieldWidth;
                if (x > 0)
                {
                    this.fields[x, newFieldYIdx].addReference(bL);       //add the next x field (since the line just gets crossed)
                }
                
            }
        }


        public void handleCollision(Ball ball)
        {
            //Collide first with animated object then with object around ball self
            LinkedList<IBoundingBox> history = new LinkedList<IBoundingBox>();
            //Collision logic
            foreach (IBoundingBox b in this.animatedObjects)
            {
                //does any animated object intersec?
                if (history.Contains(b))
                {
                    continue;
                }

                Vector2 hitPoint = new Vector2(0, 0);
                if (b.intersec(ball.getBoundingContainer().getBoundingBoxes()[0], out hitPoint))       //specify bounding box of ball
                {
                    history.AddFirst(b);
                    //collision
                    Vector2 newDirection = b.reflect(ball.Velocity, hitPoint,ball.getLocation() +ball.getBoundingContainer().getBoundingBoxes()[0].position);
                    Vector2 outOfAreaPush = b.getOutOfAreaPush(ball.Width, hitPoint, newDirection,ball.getLocation());

                    ball.setLocation((hitPoint - new Vector2(ball.Width / 2, ball.Height / 2)) + outOfAreaPush);     // + (ball.Width / 1.5f) * Vector2.Normalize(hitPoint - b.BoundingContainer.parentElement.getLocation()))

                    //ball.boundingContainer.parentElement.World.Gravity = 0;

                    ball.Velocity = b.BoundingContainer.parentElement.reflectManipulation(newDirection);
                    this.hitPointDebug = hitPoint;
                }
            }

            int fieldWidth = this.width / this.cols;
            int fieldHeight = this.height / this.rows;
           
            int x = (int)(ball.X / fieldWidth);
            int y = (int)(ball.Y / fieldHeight);

            history = new LinkedList<IBoundingBox>();

            for (int x1 = x - 1; x1 <= x + 1; x1++)
            {
                if(x1>0&&x1<this.cols)
                {
                    for (int y1 = y - 1; y1 <= y + 1; y1++)
                    {
                        if(y1>0&&y1<this.rows)
                        {
                            foreach (IBoundingBox b in this.fields[x1, y1].getReferences())
                            {
                                if (history.Contains(b))
                                {
                                    continue;
                                }
                               
                                Vector2 hitPoint = new Vector2(0, 0);
                                if (b.intersec(ball.getBoundingContainer().getBoundingBoxes()[0],out hitPoint))       //specify bounding box of ball
                                {
                                    //collision

                                    history.AddFirst(b);

                                    Vector2 newDirection = b.reflect(ball.Velocity, hitPoint, ball.getLocation() + ball.getBoundingContainer().getBoundingBoxes()[0].position);
                                    Vector2 outOfAreaPush = b.getOutOfAreaPush(ball.Width, hitPoint, newDirection, ball.getLocation());

                                    ball.setLocation((hitPoint - new Vector2(ball.Width / 2, ball.Height / 2)) + outOfAreaPush);     // + (ball.Width / 1.5f) * Vector2.Normalize(hitPoint - b.BoundingContainer.parentElement.getLocation()))
 
                                  //  ball.boundingContainer.parentElement.World.Gravity = 0;
                                   
                                    ball.Velocity = b.BoundingContainer.parentElement.reflectManipulation(newDirection);
                                    this.hitPointDebug = hitPoint;
                                }
                            }
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
