using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class Projection
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public Projection(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }

        /// <summary>
        /// Checks if the two projections overlap.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Overlapping(Projection other)
        {
            //Checks if they overlap
            if (other.Min > this.Max || this.Min > other.Max)
                return false;
            //If they doesn't overlap
            return true;
        }

        /// <summary>
        /// Checks if the two projections overlap and if so returns a push size greater than 0.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public double OverlappingV2(Projection other)
        {
            return this.Max - other.Min;
        }
    }
}
