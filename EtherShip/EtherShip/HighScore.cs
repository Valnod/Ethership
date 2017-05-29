using System;
using System.Collections.Generic;
using System.IO;
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
        private static HighScore instance;

        public static HighScore Instance
        {
            get
            {
                return instance == null ? instance = new HighScore() : instance;
            }
        }

        //Returns a list of highScoreUnits based on the xml file HighScore
        public List<HighScoreUnit> Scores
        {
            get
            {
                List<HighScoreUnit> units = new List<HighScoreUnit>();

                foreach (XmlNode cNode in xNode.ChildNodes)
                    units.Add(new HighScoreUnit(cNode.SelectSingleNode("Name").InnerText, int.Parse(cNode.SelectSingleNode("Score").InnerText)));

                return units;
            }
        }

        private HighScore()
        {
            if (File.Exists("HighScore.xml"))
            {
                xDoc = new XmlDocument();
                xDoc.Load("HighScore.xml");
            }
            else
            {
                xDoc = new XmlDocument();
                XmlElement head = xDoc.CreateElement("Head");
                xDoc.AppendChild(head);
                xDoc.Save("HighScore.xml");

                XmlNode scoreElement = xDoc.CreateElement("scoreElement");

                XmlNode name = xDoc.CreateElement("Name");
                name.InnerText = "Bob";
                scoreElement.AppendChild(name);

                XmlNode s = xDoc.CreateElement("Score");
                s.InnerText = "1000";
                scoreElement.AppendChild(s);

                xDoc.DocumentElement.AppendChild(scoreElement);
                xDoc.Save("HighScore.xml");
            }
            xNode = xDoc.SelectSingleNode("Head");
        }

        /// <summary>
        /// Adds a score to the local score list.
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(HighScoreUnit score)
        {
            XmlNode scoreElement = xDoc.CreateElement("scoreElement");

            XmlNode name = xDoc.CreateElement("Name");
            name.InnerText = score.Name;
            scoreElement.AppendChild(name);

            XmlNode s = xDoc.CreateElement("Score");
            s.InnerText = score.Score.ToString();
            scoreElement.AppendChild(s);

            xDoc.DocumentElement.AppendChild(scoreElement);
            xDoc.Save("HighScore.xml");
        }
    }
}
