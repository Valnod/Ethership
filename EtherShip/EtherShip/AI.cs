using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Threading;

namespace EtherShip
{
    static class AI
    {
        static public List<GridPoint> Pathfind(GridPoint start, GridPoint end, int windowWidth, int windowHeight)
        {
            //end position ind map coordinate (x,y)
            int endX = (int)(end.Pos.X / GameWorld.Instance.Map.GridPointSize);
            int endY = (int)(end.Pos.Y / GameWorld.Instance.Map.GridPointSize);
            //finds the amount of gridpoint on the x and y axis
            int xGridAmount = windowWidth / GameWorld.Instance.Map.GridPointSize;
            int yGridAmount = windowHeight / GameWorld.Instance.Map.GridPointSize;

            //GridPoints that have been checked and put in closed list
            var CheckedGridPoints = new List<AStarNode>();

            //GridPoints next to other points who have been checked
            var UnCheckedGridPoints = new List<AStarNode>();

            //initiates a new AstarNode
            AStarNode startASN = new AStarNode();

            AStarNode currentASN = null;

            startASN.parent = null;

            //set final value & pathValue = 0 
            startASN.finalValue = startASN.pathValue = 0;

            startASN.currentGP = start;

            UnCheckedGridPoints.Add(startASN);

            //if the list still contains unchecked GripdPoints or if we found the "end" of the route, it continues to run   
            while (UnCheckedGridPoints.Count != 0 && (currentASN = UnCheckedGridPoints[0]).currentGP != end)
            {
                //Convert from GameCoordinate (window x,y) to MapCoordinate(Grid x,y)
                int x = (int)(currentASN.currentGP.Pos.X / GameWorld.Instance.Map.GridPointSize);
                int y = (int)(currentASN.currentGP.Pos.Y / GameWorld.Instance.Map.GridPointSize);


                //Checks the gridPoints around the currentGP for any "candidates" 
                for (int xOffset = -1; xOffset <= 1; xOffset++ )
                {
                    for (int yOffset = -1; yOffset <= 1; yOffset++ )
                    {
                        //checks of the gridpoint is inside the array (gameWindow)
                        if (x + xOffset >= 0 && y + yOffset >= 0 && x + xOffset < xGridAmount && y + yOffset < yGridAmount && (xOffset != 0 || yOffset != 0))
                        {
                            GridPoint gp = GameWorld.Instance.Map.MapGrid[x+xOffset,y+yOffset];
                            if (gp.Occupant == null && CheckedGridPoints.Find(asn => asn.currentGP == gp) == null  && UnCheckedGridPoints.Find(asn => asn.currentGP == gp) == null)
                            {
                                if (xOffset != 0 && yOffset != 0)
                                {
                                    if (GameWorld.Instance.Map.MapGrid[x + xOffset ,y].Occupant != null || GameWorld.Instance.Map.MapGrid[x, y + yOffset].Occupant != null)
                                    {
                                        continue;
                                    }
                                }
                            
                                AStarNode asn = new AStarNode();
                                asn.currentGP = gp;
                                asn.parent = currentASN;
                                //brackets is an if condition with the "condition" of 14 and "else" of 10
                                asn.pathValue = currentASN.pathValue + (xOffset != 0 && yOffset != 0 ? 14 : 10);
                                //finding the nearest gridpoint to a possible "end" way using 90 degree angles only, using the heuristic values
                                asn.finalValue = asn.pathValue + ((Math.Abs(endX - (x + xOffset) + Math.Abs(endY - (y + yOffset)))) * 10);

                                //FARVE GRØN!!!!!
                                asn.currentGP.Color = Color.Green;


                                //makes sure that the UncheckGridPoints list is sorted with the lowest number first ([0])
                                for (int i = 0; i<= UnCheckedGridPoints.Count; i++ )
                                {
                                    if(i == UnCheckedGridPoints.Count)
                                    {
                                        UnCheckedGridPoints.Add(asn);
                                    }
                                    if (asn.finalValue <= UnCheckedGridPoints[i].finalValue)
                                    {
                                        UnCheckedGridPoints.Insert(i, asn);
                                        break;
                                    }
                                }   
                            }
                            else  
                            {
                               AStarNode asnTemp = UnCheckedGridPoints.Find(asn => asn.currentGP == gp);

                                if (asnTemp != null && asnTemp.pathValue > currentASN.pathValue + (xOffset != 0 && yOffset != 0 ? 14 : 10))
                                {
                                    asnTemp.parent = currentASN;
                                    asnTemp.pathValue = currentASN.pathValue + (xOffset != 0 && yOffset != 0 ? 14 : 10);
                                    asnTemp.finalValue = currentASN.pathValue + ((Math.Abs(endX - (x + xOffset)) + Math.Abs(endY - (y + yOffset))) * 10);
                                    UnCheckedGridPoints.Remove(asnTemp);

                                    for (int i = 0; i <= UnCheckedGridPoints.Count; i++)
                                    {
                                        if (i == UnCheckedGridPoints.Count)
                                        {
                                            UnCheckedGridPoints.Add(asnTemp);
                                        }
                                        if (asnTemp.finalValue <= UnCheckedGridPoints[i].finalValue)
                                        {
                                            UnCheckedGridPoints.Insert(i, asnTemp);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                UnCheckedGridPoints.Remove(currentASN);
                CheckedGridPoints.Add(currentASN);
                //FARVE BLÅ!!!!!!!!!!
                //Thread.Sleep(1000);
                currentASN.currentGP.Color = Color.Blue;
            }
            foreach (AStarNode asn in UnCheckedGridPoints)
            {
                asn.currentGP.Color = Color.Black;
            }
            foreach (AStarNode asn in CheckedGridPoints)
            {
                asn.currentGP.Color = Color.Black;
            }
            if (CheckedGridPoints.Count == 0)
            {
                return null;
            }
            else
            {
                List<GridPoint> path = new List<GridPoint>();
                for (AStarNode asn = currentASN; asn != null; asn = asn.parent)
                {
                    //FARVE RØD!!!!!!!!!!
                    asn.currentGP.Color = Color.Red;
                    path.Add(asn.currentGP);
                }
                path.Reverse();
                return path;
            }
        }
 
        private class AStarNode
        {
            //Candidate for shortest known path
            public  AStarNode parent;
            //current GridPoint
            public GridPoint currentGP;
            // Heuristic plus g(movement)
            public int finalValue;
            //total price for movement (g)
            public int pathValue;
        }
    }
}
