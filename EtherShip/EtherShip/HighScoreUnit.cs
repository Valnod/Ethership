using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherShip
{
    class HighScoreUnit
    {
        private string name;
        private int score;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public HighScoreUnit(string name, int score)
        {
            name = this.Name;
            score = this.Score;
        }
    }
    
}
