using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Wall : Component
    {
        public bool visible = true;
        public Wall(GameObject obj) : base(obj)
        {

        }
    }
}
