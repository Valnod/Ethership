using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class CollisionRectangle : Component, IDrawable, Iloadable
    {
        //One coloum is the edges ([i,0]), where the second ([i,1]) is a translocation used to indicated the vectors true placement around the position.
        public Vector2[,] edges;

        //Used to draw the points
        private Texture2D pointSprite;
        private Rectangle sourceRectPoint;

        public CollisionRectangle(GameObject obj) : base(obj)
        {
            edges = new Vector2[4, 4];
            GenerateSides();
        }

        private void GenerateSides()
        {
            //The vertixes of the box
            edges[0, 1] = new Vector2(0, 0);
            edges[1, 1] = new Vector2(0, obj.GetComponent<SpriteRenderer>().spriteRectangleForCollision.Height);
            edges[2, 1] = new Vector2(obj.GetComponent<SpriteRenderer>().spriteRectangleForCollision.Width, obj.GetComponent<SpriteRenderer>().spriteRectangleForCollision.Height);
            edges[3, 1] = new Vector2(obj.GetComponent<SpriteRenderer>().spriteRectangleForCollision.Width, 0);

            //The edges of the box
            edges[0, 0] = edges[1, 1] - edges[0, 1];
            edges[1, 0] = edges[2, 1] - edges[1, 1];
            edges[2, 0] = edges[3, 1] - edges[2, 1];
            edges[3, 0] = edges[0, 1] - edges[3, 1];
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
