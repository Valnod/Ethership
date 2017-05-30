using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class HighScoreUnit
    {
        public string Name { get; private set; }
        public int Score { get; private set; }

        public HighScoreUnit(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }
    }
    
}
