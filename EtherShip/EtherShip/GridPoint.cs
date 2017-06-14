using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class GridPoint
    {
        public Vector2 Pos { get; set; }
        public GameObject Occupant { get; set; }
        public Color Color { get; set; }
        public int Heat { get; set; }

        public GridPoint(Vector2 pos, GameObject occupant)
        {
            this.Pos = pos;
            this.Occupant = occupant;
            this.Color = Color.Black;
            this.Heat = 0;
        }
    }
}
