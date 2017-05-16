using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace EtherShip
{
    class GameObjectPool
    {  
        // Enemy
        public List<Enemy> ActiveEnemyList { get; }
        public List<Enemy> InactiveEnemyList { get; }
        //Tower
        public List<Tower> ActiveTowerList { get; }
        public List<Tower> InactiveTwoerList { get; }
        //Wall 
        public List<Wall> ActiveWallList { get; }
        public List<Wall> InactiveWallList { get; }
        //Clutter
        public List<Clutter> ActiveClutterList { get; }
        public List<Clutter> InactiveClutterList { get; }
        //Whale
        public List<Whale> ActiveWhaleList { get; }
        public List<Whale> InactiveWhaleList { get; }

        public Player player;

        public void CreateEnemy()
        {

        }

        public void DeleteEnemy(Enemy enemy)
        {

        }

        public void CreateWall(Vector2 position)
        {

        }

        public void DeleteWall(Wall wall)
        {

        }

        public void CreateClutter(Vector2 clutter)
        {

        }

        public void DeleteClutter(Clutter clutter)
        {

        }

        public void CreateTower(Vector2 tower)
        {

        }

        public void DeleteTower(Tower tower)
        {

        }

        public void CreateWhale()
        {

        }

        public void DeleteWhale(Whale whale)
        {

        }

        public void CreatePlayer()
        {

        }

        public static TrackEnemy()
        {
            ActiveEnemyList = new list<>
        }

       



    }
}
