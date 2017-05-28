using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EtherShip
{
    class Wave
    {
        public int wave;


        public GameObject obj;
        private int numberOfEnemies;        // number of enemies to spawn
        private int waveNumber;             // wich wave it is
        private float spawnTimer = 0;       //when should we spawn an enemy
        private int enemiesSpawned = 0;     //how many enemies have spawned

        private bool enemyAtEnd;            //has an enemy reached the player?
        private bool spawningEnemies;       // are we still spawning enemies?
        private Map map;                    // a reference to the map


        public List<Enemy> enemies = new List<Enemy>(); // a list of enemies

        public bool RoundOver
        {
            get
            {
                return enemies.Count == 0 && enemiesSpawned == numberOfEnemies;
            }
        }
        public int RoundNumber
        {
            get
            {
                return waveNumber;
            }
        }
        public bool EnemyAtEnd
        {
            get
            {
                return EnemyAtEnd;
            }
            set { EnemyAtEnd = value; }
        }
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }
        public Wave(int waveNumber, int numberOfEnemies, Map map)
        {
            this.waveNumber = waveNumber;
            this.map = map;
            this.numberOfEnemies = numberOfEnemies;

        }
        private void AddEnemy()
        {
            Enemy enemy = new Enemy(obj, 100, 10f, 10, new Vector2(10, 10));
            enemies.Add(enemy);
            spawnTimer = 0;

            enemiesSpawned++;
            //defines the changes from wave to wave, should one round be stronger etc etc.
            if(waveNumber == 5)
            {
                int health = 200;

                enemy = new Enemy(obj, health, 5, 20, new Vector2(10, 10));
            }
        }
        public void Start()
        {
            spawningEnemies = true;
        }
       public void Update(GameTime gameTime)
        {
            //first we check if we have the desired number of enemies otherwise we spawn our enemies 
            if(enemiesSpawned == numberOfEnemies)
            {
                spawningEnemies = false; 
            }
            if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(spawnTimer > 2)
                {
                    AddEnemy();
                }
            }
            
        }
    }
}
