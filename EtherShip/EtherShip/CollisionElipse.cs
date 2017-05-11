using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class CollisionElipse
    {
        public float LRadius { get; }
        public float SRadius { get; }
        
        public CollisionElipse(float lRadius, float sRadius)
        {
            this.LRadius = lRadius;
            this.SRadius = sRadius;
        }
    }
}
