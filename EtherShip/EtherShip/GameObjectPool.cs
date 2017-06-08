using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace EtherShip
{
    class GameObjectPool
    {
        public GameObject gameObject;

        private bool generateThread;
        private bool runThread;
        private bool[] done;
       
        public List<GameObject> AddActive { get; set; }
        public List<GameObject> RemoveActive { get; set; }

        // Enemy
        public List<GameObject> ActiveEnemyList { get; set; }
        private List<GameObject> InactiveEnemyList;
        //Tower
        public List<GameObject> ActiveTowerList { get; set; }
        private List<GameObject> InactiveTowerList;
        //Projectile
        public List<GameObject> ActiveProjectileList { get; set; }
        private List<GameObject> InactiveProjectileList;
        //Wall 
        private List<GameObject> ActiveWallList;
        private List<GameObject> InactiveWallList;
        //Clutter
        private List<GameObject> ActiveClutterList;
        private List<GameObject> InactiveClutterList;
        //Whale
        public List<GameObject> ActiveWhaleList;
        private List<GameObject> InactiveWhaleList;

        public GameObject player;

        public GameObjectPool()
        {
            ActiveTowerList = new List<GameObject>();
            ActiveEnemyList = new List<GameObject>();
            ActiveClutterList = new List<GameObject>();
            ActiveWallList = new List<GameObject>();
            ActiveWhaleList = new List<GameObject>();
            ActiveProjectileList = new List<GameObject>();

            InactiveClutterList = new List<GameObject>();
            InactiveEnemyList = new List<GameObject>();
            InactiveTowerList = new List<GameObject>();
            InactiveWallList = new List<GameObject>();
            InactiveWhaleList = new List<GameObject>();
            InactiveProjectileList = new List<GameObject>();

            AddActive = new List<GameObject>();
            RemoveActive = new List<GameObject>();

            generateThread = true;
            runThread = true;
            done = new bool[3];
            for (int i = 0; i < done.Count(); i++)
                done[i] = true;
        }

        public void CreateEnemy()
        {
            if (InactiveEnemyList.Count > 0)
            {
                InactiveEnemyList[0].position = new Vector2(GameWorld.Instance.Window.ClientBounds.Width / 2, 40);
                InactiveEnemyList[0].GetComponent<Enemy>().ResetHealth();
                AddActive.Add(InactiveEnemyList[0]);
            }
            else
            {
                GameObject obj = new GameObject(new Vector2(GameWorld.Instance.Window.ClientBounds.Width / 2, 40));
                obj.AddComponnent(new Enemy(obj, 10, 3f, 1, new Vector2(), 10, 35));
                obj.AddComponnent(new SpriteRenderer(obj, "EnemyShip_00000", 0.2f, 0f, 0.5f));
                obj.LoadContent(GameWorld.Instance.Content);
                obj.AddComponnent(new CollisionCircle(obj));
                obj.GetComponent<CollisionCircle>().LoadContent(GameWorld.Instance.Content);
                AddActive.Add(obj);
            }
        }

        public void DeleteEnemy(Enemy enemy)
        {
           
        }

        /// <summary>
        /// Creates a wall at the given location, position.
        /// </summary>
        /// <param name="position"></param>
        public void CreateWall(Vector2 position)
        {
            if (InactiveWallList.Count > 0)
            {
                InactiveWallList[0].position = position;
                AddActive.Add(InactiveWallList[0]);

                GameWorld.Instance.Map[position].Occupant = InactiveTowerList[0];
            }
            else
            {
                GameObject obj = new GameObject(position);
                obj.AddComponnent(new Wall(obj));
                obj.AddComponnent(new SpriteRenderer(obj, "Wall_00030", 0.065f, 0f, 0.5f));
                obj.LoadContent(GameWorld.Instance.Content);
                obj.AddComponnent(new CollisionRectangle(obj));
                obj.GetComponent<CollisionRectangle>().LoadContent(GameWorld.Instance.Content);
                AddActive.Add(obj);

                GameWorld.Instance.Map[position].Occupant = obj;
            }
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
            if (InactiveTowerList.Count > 0)
            {
                InactiveTowerList[0].position = towerPos;
                AddActive.Add(InactiveTowerList[0]);

                GameWorld.Instance.Map[towerPos].Occupant = InactiveTowerList[0];
            }
            else
            {
                GameObject obj = new GameObject(towerPos);
                obj.AddComponnent(new Tower(obj, 500000, 300));
                obj.AddComponnent(new SpriteRenderer(obj, "TowerRemove_00009", 0.065f, 0f, 1f));
                obj.AddComponnent(new SpriteRenderer(obj, "turret with harpoon", 0.065f, 0f, 1f));
                obj.LoadContent(GameWorld.Instance.Content);
                obj.AddComponnent(new CollisionCircle(obj));
                obj.GetComponent<CollisionCircle>().LoadContent(GameWorld.Instance.Content);
                AddActive.Add(obj);

                GameWorld.Instance.Map[towerPos].Occupant = obj;
            }
        }

        public void DeleteTower(GameObject tower)
        {

        }

        /// <summary>
        /// Creates a projectile at the given location, towerPos.
        /// </summary>
        /// <param name="towerPos"></param>
        public void CreateProjectile(Vector2 projectileStartPos, GameObject target)
        {
            if (InactiveProjectileList.Count > 0)
            {
                InactiveProjectileList[0].GetComponent<Projectile>().target = target;
                InactiveProjectileList[0].position = projectileStartPos;
                AddActive.Add(InactiveProjectileList[0]);
            }
            else
            {
                GameObject obj = new GameObject(projectileStartPos);
                obj.AddComponnent(new Projectile(obj, 70, 1, target));
                obj.AddComponnent(new SpriteRenderer(obj, "harpoon", 0.2f, 0f, 1f));
                obj.LoadContent(GameWorld.Instance.Content);
                obj.AddComponnent(new CollisionCircle(obj));
                obj.GetComponent<CollisionCircle>().LoadContent(GameWorld.Instance.Content);
                AddActive.Add(obj);
            }
        }

        public void DeleteProjectile(GameObject projectile)
        {

        }

        public void CreateWhale()
        {

            if (InactiveWhaleList.Count > 0)
            {
                AddActive.Add(InactiveWhaleList[0]);
            }
            else
            {
                GameObject obj = new GameObject(new Vector2(400, 400));
                obj.AddComponnent(new Whale(obj, new Vector2(1, 0), new Vector2(1, 0), 10, 5, 1f));
                obj.AddComponnent(new SpriteRenderer(obj, "Whale", 0.2f, 0f, 0.5f));
                obj.LoadContent(GameWorld.Instance.Content);
                obj.AddComponnent(new CollisionCircle(obj));
                obj.GetComponent<CollisionCircle>().LoadContent(GameWorld.Instance.Content);
                AddActive.Add(obj);
            }

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
            obj.AddComponnent(new SpriteRenderer(obj, "space whaler ship", 0.2f, 0f, 1f));
            obj.LoadContent(GameWorld.Instance.Content);
            obj.AddComponnent(new CollisionCircle(obj));
            obj.GetComponent<CollisionCircle>().LoadContent(GameWorld.Instance.Content);
            player = obj;
        }

        /// <summary>
        /// Updates all gameObjects in all the lists.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //player.Update(gameTime);

            //for (int i = 0; i < ActiveTowerList.Count(); i++)
            //    ActiveTowerList[i].Update(gameTime);
            //for (int i = 0; i < ActiveEnemyList.Count(); i++)
            //    ActiveEnemyList[i].Update(gameTime);
            //for (int i = 0; i < ActiveWallList.Count(); i++)
            //    ActiveWallList[i].Update(gameTime);
            //for (int i = 0; i < ActiveWhaleList.Count(); i++)
            //    ActiveWhaleList[i].Update(gameTime);
            //for (int i = 0; i < ActiveClutterList.Count(); i++)
            //    ActiveClutterList[i].Update(gameTime);
            //for (int i = 0; i < ActiveProjectileList.Count(); i++)
            //    ActiveProjectileList[i].Update(gameTime);

            if (generateThread)
            {
                StartThreads(gameTime);
                generateThread = false;
            }

            if (!done[2])
            {
                player.Update(gameTime);
                done[2] = true;
            }

            if(done.All(x => x))
            {
                for (int i = 0; i < done.Count(); i++)
                    done[i] = false;
            }
        }

        private void StartThreads(GameTime gameTime)
        {
            //done 0
            //Towers, walls and clutter
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (runThread)
                {
                    if (!done[0])
                    {
                        for (int i = 0; i < ActiveTowerList.Count(); i++)
                            ActiveTowerList[i].Update(gameTime);
                        for (int i = 0; i < ActiveWallList.Count(); i++)
                            ActiveWallList[i].Update(gameTime);
                        for (int i = 0; i < ActiveClutterList.Count(); i++)
                            ActiveClutterList[i].Update(gameTime);
                        done[0] = true;
                    }
                }
            }).Start();

            //done 1
            //Enemy, whale and projectile
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (runThread)
                {
                    if (!done[1])
                    {
                        for (int i = 0; i < ActiveEnemyList.Count(); i++)
                            ActiveEnemyList[i].Update(gameTime);
                        for (int i = 0; i < ActiveWhaleList.Count(); i++)
                            ActiveWhaleList[i].Update(gameTime);
                        for (int i = 0; i < ActiveProjectileList.Count(); i++)
                            ActiveProjectileList[i].Update(gameTime);
                        done[1] = true;
                    }
                }
            }).Start();


        }

        /// <summary>
        /// Draws all gameObjects in all the lists.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            for (int i = 0; i < ActiveTowerList.Count(); i++)
                ActiveTowerList[i].Draw(spriteBatch);
            for (int i = 0; i < ActiveEnemyList.Count(); i++)
                ActiveEnemyList[i].Draw(spriteBatch);
            for (int i = 0; i < ActiveWallList.Count(); i++)
                ActiveWallList[i].Draw(spriteBatch);
            for (int i = 0; i < ActiveWhaleList.Count(); i++)
                ActiveWhaleList[i].Draw(spriteBatch);
            for (int i = 0; i < ActiveClutterList.Count(); i++)
                ActiveClutterList[i].Draw(spriteBatch);
            for (int i = 0; i < ActiveProjectileList.Count(); i++)
                ActiveProjectileList[i].Draw(spriteBatch);
        }

        /// <summary>
        /// Adds gameObjects from AddActive list to their correct active lists.
        /// </summary>
        public void AddToActive()
        {
            for(int i = 0; i < AddActive.Count(); i++)
            {
                GameObject go = AddActive[i];

                if (go.GetComponent<Tower>() != null)
                {
                    ActiveTowerList.Add(go);
                    if (InactiveTowerList.Contains(go))
                        InactiveTowerList.Remove(go);
                }
                if (go.GetComponent<Enemy>() != null)
                {
                    ActiveEnemyList.Add(go);
                    if (InactiveEnemyList.Contains(go))
                        InactiveEnemyList.Remove(go);
                }
                if (go.GetComponent<Wall>() != null)
                {
                    ActiveWallList.Add(go);
                    if (InactiveWallList.Contains(go))
                        InactiveWallList.Remove(go);
                }
                if (go.GetComponent<Whale>() != null)
                {
                    ActiveWhaleList.Add(go);
                    if (InactiveWhaleList.Contains(go))
                        InactiveWhaleList.Remove(go);
                }
                if (go.GetComponent<Clutter>() != null)
                {
                    ActiveClutterList.Add(go);
                    if (InactiveClutterList.Contains(go))
                        InactiveClutterList.Remove(go);
                }
                if (go.GetComponent<Projectile>() != null)
                {
                    ActiveProjectileList.Add(go);
                    if (InactiveProjectileList.Contains(go))
                        InactiveProjectileList.Remove(go);
                }
            }
            AddActive.Clear();
        }

        /// <summary>
        /// Moves gameObjects from RemoveActive lists to inactive lists  
        /// </summary>
        public void RemoveFromActive()
        {
            for(int i = 0; i < RemoveActive.Count(); i++)
            {
                GameObject go = RemoveActive[i]; 

                if (go.GetComponent<Tower>() != null)
                {
                    InactiveTowerList.Add(go);
                    ActiveTowerList.Remove(go);
                }
                if (go.GetComponent<Enemy>() != null)
                {
                    InactiveEnemyList.Add(go);
                    ActiveEnemyList.Remove(go);
                }
                if (go.GetComponent<Wall>() != null)
                {
                    InactiveWallList.Add(go);
                    ActiveWallList.Remove(go);
                }
                if (go.GetComponent<Whale>() != null)
                {
                    InactiveWhaleList.Add(go);
                    ActiveWhaleList.Remove(go);
                }
                if (go.GetComponent<Clutter>() != null)
                {
                    InactiveClutterList.Add(go);
                    ActiveClutterList.Remove(go);
                }
                if (go.GetComponent<Projectile>() != null)
                {
                    InactiveProjectileList.Add(go);
                    ActiveProjectileList.Remove(go);
                }
            }
            RemoveActive.Clear();
        }

        /// <summary>
        /// Returns a list of all GameObjects the player shall check collision with.
        /// </summary>
        /// <returns></returns>
        public List<GameObject> CollisionListForPlayer()
        {
            List<GameObject> list = new List<GameObject>();

            var allObjects = ActiveClutterList.Concat(ActiveEnemyList)
                                    .Concat(ActiveWallList)
                                    .Concat(ActiveTowerList)
                                    .Concat(ActiveWhaleList)
                                    .ToList();
            return allObjects;
        }

        /// <summary>
        /// Returns a list of all GameObjects the enemy shall check collision with.
        /// </summary>
        /// <returns></returns>
        public List<GameObject> CollisionListForEnemy()
        {
            List<GameObject> list = new List<GameObject>();

            var allObjects = ActiveClutterList.Concat(ActiveEnemyList)
                                    .Concat(ActiveWallList)
                                    .Concat(ActiveTowerList)
                                    .ToList();
            return allObjects;
        }

        /// <summary>
        /// Makes all active gameobjects inactive.
        /// </summary>
        public void ClearLists()
        {
            for(int i = 0; i < ActiveEnemyList.Count(); i++)
                RemoveActive.Add(ActiveEnemyList[i]);
            for (int i = 0; i < ActiveProjectileList.Count(); i++)
                RemoveActive.Add(ActiveProjectileList[i]);
            for (int i = 0; i < ActiveTowerList.Count(); i++)
                RemoveActive.Add(ActiveTowerList[i]);
            for (int i = 0; i < ActiveWallList.Count(); i++)
                RemoveActive.Add(ActiveWallList[i]);
            for (int i = 0; i < ActiveWhaleList.Count(); i++)
                RemoveActive.Add(ActiveWhaleList[i]);
            for (int i = 0; i < ActiveClutterList.Count(); i++)
                RemoveActive.Add(ActiveClutterList[i]);
        }
    }
}
