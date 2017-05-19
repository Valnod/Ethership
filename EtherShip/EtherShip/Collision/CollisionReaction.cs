using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    static class CollisionReaction
    {
        static public Vector2 EllipseRectangle(Vector2 pos, Vector2 translation, Vector2 rectPos, int rectWidth)
        {
            Vector2 newTranslation = Vector2.Zero;
            Vector2 perpVec = Vector2.Zero;
            Vector2 direction = Vector2.Zero;
            //Checks what side on the rectangle the collision happens and acts accordingly
            if ((pos.X > (rectPos.X + rectWidth) && pos.Y > (rectPos.Y + rectWidth)) ||
                (pos.X <= rectPos.X && pos.Y > (rectPos.Y + rectWidth)) ||
                (pos.X > (rectPos.X + rectWidth) && pos.Y <= rectPos.Y) || (pos.X <= rectPos.X && pos.Y <= rectPos.Y)) //One of the corners
            {
                //direction from box to the one coolliding
                direction = Vector2.Normalize(rectPos - pos);
                //vector perpendicular to the direction vector
                perpVec = new Vector2(- direction.Y, direction.X);
                //Projection of the velocity on the collision direction
                newTranslation = VectorProjectedOnVector(translation, perpVec);
            }
            else if(pos.X > rectPos.X && pos.X < (rectPos.X + rectWidth) && (pos.Y > (rectPos.Y + rectWidth))) //Bottom of the rectangle
            {
                if (translation.Y < 0)
                    newTranslation = new Vector2(translation.X, 0);
                else if (translation.Y > 0)
                    newTranslation = translation;
            }
            else if (pos.X > rectPos.X && pos.X < (rectPos.X + rectWidth) && (pos.Y <= rectPos.Y)) //Top of the rectangle
            {
                if (translation.Y > 0)
                    newTranslation = new Vector2(translation.X, 0);
                else if (translation.Y < 0)
                    newTranslation = translation;
            }
            else if (pos.Y > rectPos.Y && pos.Y < (rectPos.Y + rectWidth) && (pos.X > (rectPos.X + rectWidth))) //Right side of rectangle
            {
                if (translation.X < 0)
                    newTranslation = new Vector2(0, translation.Y);
                else if (translation.X > 0)
                    newTranslation = translation;
            }
            else if (pos.Y > rectPos.Y && pos.Y < (rectPos.Y + rectWidth) && (pos.X <= rectPos.X)) //Left side of rectangle
            {
                if (translation.X > 0)
                    newTranslation = new Vector2(0, translation.Y);
                else if (translation.X < 0)
                    newTranslation = translation;
            }

            return newTranslation;
        }

        static public Vector2 EllipseCircle(Vector2 pos, Vector2 translation, Vector2 rectPos)
        {
            Vector2 newVelocityVec = Vector2.Zero;
            Vector2 perpVec = Vector2.Zero;
            //direction from circle to the one coolliding
            Vector2 direction = Vector2.Normalize(rectPos - pos);
            //vector perpendicular to the direction vector
            perpVec = new Vector2(-direction.Y, direction.X);

            ////Finds the velocityvector of the colliding object
            //Vector2 velocityVec = Vector2.Normalize(-direction) * speed;

            //Projection of the velocity on the collision direction
            newVelocityVec = VectorProjectedOnVector(translation, perpVec); 

            return newVelocityVec;
        }

        static private Vector2 VectorProjectedOnVector(Vector2 vec1, Vector2 vec2)
        {
            double scalar = ((CollisionCheck.DotProduct(vec1, vec2)) / (vec2.Length() * vec2.Length()));
            return (float)scalar * vec2;
        }
    }
}
