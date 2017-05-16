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
        public List<GameObject> AddActive { get; set; }
        public List<GameObject> RemoveActive { get; set; }

        // Enemy
        private List<GameObject> ActiveEnemyList;
        private List<GameObject> InactiveEnemyList;
        //Tower
        private List<GameObject> ActiveTowerList;
        private List<GameObject> InactiveTwoerList;
        //Wall 
        private List<GameObject> ActiveWallList;
        private List<GameObject> InactiveWallList;
        //Clutter
        private List<GameObject> ActiveClutterList;
        private List<GameObject> InactiveClutterList;
        //Whale
        private List<GameObject> ActiveWhaleList;
        private List<GameObject> InactiveWhaleList;

        public GameObject player;

        public GameObjectPool()
        {
            ActiveTowerList = new List<GameObject>();
            ActiveEnemyList = new List<GameObject>();
            ActiveClutterList = new List<GameObject>();
            ActiveWallList = new List<GameObject>();
            ActiveWhaleList = new List<GameObject>();

            InactiveClutterList = new List<GameObject>();
            InactiveEnemyList = new List<GameObject>();
            InactiveTwoerList = new List<GameObject>();
            InactiveWallList = new List<GameObject>();
            InactiveWhaleList = new List<GameObject>();

            AddActive = new List<GameObject>();
            RemoveActive = new List<GameObject>();
        }

        public void CreateEnemy()
        {
            /*gameObject = new GameObject(new Vector2());
            Enemy enemy = new Enemy(gameObject, 3, 80, 1, new Vector2(100, 100));
            enemy.obj.AddComponnent(new SpriteRenderer(gameObject, "enemyBlack1", 1, 1));
            ActiveEnemyList.Add(enemy);
            InactiveEnemyList.Remove(enemy);*/
        }

        public void DeleteEnemy(Enemy enemy)
        {
            /*ActiveEnemyList.Remove(enemy);
            InactiveEnemyList.Add(enemy);*/
        }

        public void CreateWall(Vector2 position)
        {

        }

        public void DeleteWall(GameObject wall)
        {

        }

        public void CreateClutter(Vector2 clutter)
        {

        }

        public void DeleteClutter(GameObject clutter)
        {

        }

        /// <summary>
        /// Creates a tower at the given location, towerPos.
        /// </summary>
        /// <param name="towerPos"></param>
        public void CreateTower(Vector2 towerPos)
        {
            if (InactiveTwoerList.Count > 0)
            {
                AddActive.Add(InactiveTwoerList[1]);
                InactiveTwoerList.RemoveAt(1);
            }
            else
            {
                GameObject obj = new GameObject(towerPos);
                obj.AddComponnent(new Tower(obj, 10, 100));
                obj.AddComponnent(new SpriteRenderer(obj, "rectangle", 1f, 0.5f));
                obj.LoadContent(GameWorld.Instance.Content);
                AddActive.Add(obj);
            }
        }

        public void DeleteTower(GameObject tower)
        {

        }

        public void CreateWhale()
        {

        }

        public void DeleteWhale(GameObject whale)
        {

        }

        /// <summary>
        /// Creates a player.
        /// </summary>
        public void CreatePlayer()
        {
            GameObject obj = new GameObject(new Vector2(100, 100));
            obj.AddComponnent(new Player(obj, new Vector2(1, 0), 3, false));
            obj.AddComponnent(new SpriteRenderer(obj, "circle", 1f, 0.5f));
            obj.LoadContent(GameWorld.Instance.Content);
            player = obj;
        }

        /// <summary>
        /// Updates all gameObjects in all the lists.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            foreach (GameObject go in ActiveTowerList)
                go.Update(gameTime);
            foreach (GameObject go in ActiveEnemyList)
                go.Update(gameTime);
            foreach (GameObject go in ActiveWallList)
                go.Update(gameTime);
            foreach (GameObject go in ActiveWhaleList)
                go.Update(gameTime);
            foreach (GameObject go in ActiveClutterList)
                go.Update(gameTime);
        }

        /// <summary>
        /// Draws all gameObjects in all the lists.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            foreach (GameObject go in ActiveTowerList)
                go.Draw(spriteBatch);
            foreach (GameObject go in ActiveEnemyList)
                go.Draw(spriteBatch);
            foreach (GameObject go in ActiveWallList)
                go.Draw(spriteBatch);
            foreach (GameObject go in ActiveWhaleList)
                go.Draw(spriteBatch);
            foreach (GameObject go in ActiveClutterList)
                go.Draw(spriteBatch);
        }
      

       

        /// <summary>
        /// Adds gameObjects from AddActive list to their correct active lists.
        /// </summary>
        public void AddToActive()
        {
            foreach(GameObject go in AddActive)
            {
                if (go.GetComponent<Tower>() != null)
                    ActiveTowerList.Add(go);
                if (go.GetComponent<Enemy>() != null)
                    ActiveEnemyList.Add(go);
                if (go.GetComponent<Wall>() != null)
                    ActiveWallList.Add(go);
                if (go.GetComponent<Whale>() != null)
                    ActiveWhaleList.Add(go);
                if (go.GetComponent<Clutter>() != null)
                    ActiveClutterList.Add(go);
            }
            AddActive.Clear();
        }

        /// <summary>
        /// Moves gameObjects from active lists to inactive lists  
        /// </summary>
        public void RemoveFromActive()
        {
            foreach (GameObject go in AddActive)
            {
                if (go.GetComponent<Tower>() != null)
                    InactiveTwoerList.Add(go);
                if (go.GetComponent<Enemy>() != null)
                    InactiveEnemyList.Add(go);
                if (go.GetComponent<Wall>() != null)
                    InactiveWallList.Add(go);
                if (go.GetComponent<Whale>() != null)
                    InactiveWhaleList.Add(go);
                if (go.GetComponent<Clutter>() != null)
                    InactiveClutterList.Add(go);
            }
            RemoveActive.Clear();
        }
    }
}
