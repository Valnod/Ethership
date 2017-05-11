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
        public Rectangle rect { get; }
        public Vector2 offset;

        public Frame(Rectangle rect, Vector2 offset)
        {
            this.rect = rect;
            this.offset = offset;
        }

    }
}
