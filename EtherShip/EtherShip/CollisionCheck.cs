﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EtherShip
{
    class CollisionCheck
    {

        public bool CircleBox(CollisionCircle circle, CollisionRectangle rect)
        {
            return false;
        }

        public bool CircleCircle(CollisionCircle circle1, CollisionCircle circle2)
        {
            return false;
        }
        public bool ElipseCircle(CollisionCircle Circle, CollisionElipse elipse)
        {
            return false;
        }
        public bool ElipseBox(CollisionElipse elipse, CollisionRectangle rect)
        {
            return false;
        }





    }
}
