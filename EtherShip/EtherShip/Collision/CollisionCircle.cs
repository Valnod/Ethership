﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class CollisionCircle : Component, Iloadable, IDrawable
    {
        public float Radius { get; set; }
        //One coloum is the edges ([i,0]), where the second ([i,1]) is a translocation used to indicated the vectors true placement around the position.
        public Vector2[,] edges;
        public List<float> lengthOfEdges;

        //Indicates the number of edges that make up the polygon
        private int numberOfEdges = 36;

        //Used to draw the points
        private Texture2D pointSprite;
        private Rectangle sourceRectPoint;

        public CollisionCircle(GameObject obj) : base(obj)
        {
            this.Radius = (obj.GetComponent<SpriteRenderer>().spriteRectangleForCollision.Width)/2;
            edges = new Vector2[numberOfEdges, 2];
            lengthOfEdges = new List<float>();
            GenerateSides();
        }

        private void GenerateSides()
        {
            //The first point on the edge of the circle
            Vector2 vecStart = new Vector2(Radius, 0);
            //The end point of the edge of the circle
            Vector2 vecEnd = new Vector2(0, 0);
            //The vector indicating the edge
            Vector2 edgeVec = new Vector2(0, 0);
            //Generates the rest of the points on the circles edge, and thereby makes the edges by combining two points.
            for (int i = 0; i < numberOfEdges; i++)
            {
                vecEnd = RotPointsAroundPointMath.RotatePoint(vecStart, Vector2.Zero, (360 / numberOfEdges));
                edgeVec = vecEnd - vecStart;
                edges[i, 0] = edgeVec;
                edges[i, 1] = vecStart;

                float length = edgeVec.Length();
                lengthOfEdges.Add(length);

                //Changes the start point for the next edge
                vecStart = vecEnd;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG //draws the points which makes up the collision box
            for (int i = 0; i < edges.GetLength(0) - 1; i++)
            {
                spriteBatch.Draw(pointSprite, edges[i, 1] + obj.position, sourceRectPoint, Color.Black, 1f, Vector2.Zero, 1f, SpriteEffects.None, 1);
            }
#endif
        }

        public void LoadContent(ContentManager content)
        {
            pointSprite = content.Load<Texture2D>("rectangle");
            sourceRectPoint = new Rectangle(0, 0, 2, 2);
        }
    }
}
