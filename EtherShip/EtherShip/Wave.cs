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
        public int WaveNumber { get; set; } // wich wave it is        
        private float spawnTimer = 0;       //when should we spawn an enemy
        private int enemiesSpawned = 0;     //how many enemies have spawned

        private bool enemyAtEnd;            //has an enemy reached the player?
        private bool spawningEnemies;       // are we still spawning enemies?
        private Map map;                    // a reference to the map


        

        //public bool RoundOver
        //{
        //    get
        //    {
        //        return GameWorld.Instance.gameObjectPool.ActiveEnemyList.Count == 0 && enemiesSpawned == numberOfEnemies;
        //    }
        //    set { RoundOver = value; }
        //}
        public bool EnemyAtEnd
        {
            get
            {
                return EnemyAtEnd;
            }
            set { EnemyAtEnd = value; }
        }
       
        public Wave(int waveNumber, int numberOfEnemies, Map map)
        {
            this.WaveNumber = waveNumber;
            this.map = map;
            this.numberOfEnemies = numberOfEnemies;

        }
        private void AddEnemy()
        {
            GameWorld.Instance.gameObjectPool.CreateEnemy();
            
            enemiesSpawned++;
            //defines the changes from wave to wave, should one round be stronger etc etc.
            if(WaveNumber == 5)
            {
                GameWorld.Instance.gameObjectPool.CreateWhale();
                spawningEnemies = false;
           }
        }
        public void Start()
        {
            //if (WaveNumber == 5)
            //{
            //    spawningEnemies = false;
            //}
            //spawningEnemies = true;
        }
        public void WaveOver()
        {
            //if (GameWorld.Instance.gameObjectPool.ActiveWhaleList.Count > 0)
            //{
            //    spawningEnemies = false;

            //}

            //else
            //{

            enemiesSpawned = 0;
                GameWorld.Instance.BetweenRounds = true;
                WaveNumber++;
                spawningEnemies = true;
            //}
            
        }
       
       public void Update(GameTime gameTime)
        {
            //first we check if we have the desired number of enemies otherwise we spawn our enemiesf
            if (enemiesSpawned >= (numberOfEnemies + WaveNumber))
            {
                spawningEnemies = false; 
            }
            else if (spawningEnemies)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(spawnTimer > 1)
                {
                    AddEnemy();
                    spawnTimer = 0;
                }
            }
            
            if(GameWorld.Instance.gameObjectPool.ActiveEnemyList.Count <= 0 && spawningEnemies == false)
            {
                WaveOver();
            }

        }
    }
}
