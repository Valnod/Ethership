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
    static class GameObjectPool
    {
        private static GameObject gameObject;
        //Enemy
        public static List<Enemy> ActiveEnemyList = new List<Enemy>();
        public static List<Enemy> InactiveEnemyList = new List<Enemy>();
        //Tower
        public static List<GameObject> ActiveTowerList { get; }
        public static List<GameObject> InactiveTowerList { get; }
        //Wall 
        public static List<GameObject> ActiveWallList { get; }
        public static List<GameObject> InactiveWallList { get; }
        //Clutter
        public static List<GameObject> ActiveClutterList { get; }
        public static List<GameObject> InactiveClutterList { get; }
        //Whale
        public static List<GameObject> ActiveWhaleList { get; }
        public static List<GameObject> InactiveWhaleList { get; }

        public static GameObject player;

        public static void CreateEnemy()
        {
            gameObject = new GameObject(new Vector2());
            Enemy enemy = new Enemy(gameObject, 3, 80, 1, new Vector2(100, 100));
            enemy.gameObject.AddComponnent(new SpriteRendere(gameObject, "enemyBlack1", 1));
            ActiveEnemyList.Add(enemy);
            InactiveEnemyList.Remove(enemy);
        }

        public static void DeleteEnemy(Enemy enemy)
        {
            ActiveEnemyList.Remove(enemy);
            InactiveEnemyList.Add(enemy);
        }

        public static void CreateWall(Vector2 position)
        {

        }

        public static void DeleteWall(GameObject wall)
        {

        }

        public static void CreateClutter(Vector2 clutter)
        {

        }

        public static void DeleteClutter(GameObject clutter)
        {

        }

        public static void CreateTower(Vector2 tower)
        {

        }

        public static void DeleteTower(GameObject tower)
        {

        }

        public static void CreateWhale()
        {

        }

        public static void DeleteWhale(GameObject whale)
        {

        }

        public static void CreatePlayer()
        {

        }
    }
}
