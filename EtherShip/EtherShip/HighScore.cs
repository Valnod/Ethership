using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EtherShip
{
    class HighScore
    {
        private XmlDocument xDoc;
        private XmlNode xNode;
        private List<HighScoreUnit> highScoreList;

        internal List<HighScoreUnit> HighScoreList
        {
            get
            {
                return highScoreList;
            }

            set
            {
                highScoreList = value;
            }
        }

        public HighScore()
        {

        }
        
        public void AddHighScore(HighScoreUnit highScoreUnit)
        {

        }
    }
}
