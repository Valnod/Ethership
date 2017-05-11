using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class CollisionRectangle
    {
        public Rectangle rect;

        public CollisionRectangle(Rectangle rect)
        {
            this.rect = rect;
        }

        public Rectangle GetRect()
        {
            return rect;
        }
    }
}
