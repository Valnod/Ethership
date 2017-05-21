using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class AI
    {
        public List<GridPoint> Pathfind(GridPoint start, GridPoint end)
        {
            //GridPoints that have been checked and put in closed list
            var CheckedGridPoints = new List<GridPoint>();

            //GridPoints next to other points who have been checked
            var UnCheckedGridPoints = new List<GridPoint> { start };  
        }
    }
}
