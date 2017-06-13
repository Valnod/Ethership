using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Frame
    {
        public Rectangle Rect { get; }
        public Vector2 offset;

        public Frame(Rectangle rect, Vector2 offset)
        {
            this.Rect = rect;
            this.offset = offset;
        }

    }
}
