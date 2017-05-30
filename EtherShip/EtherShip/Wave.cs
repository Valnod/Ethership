﻿using System;
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


        

        //public bool RoundOver
        //{
        //    get
        //    {
        //        return GameWorld.Instance.gameObjectPool.ActiveEnemyList.Count == 0 && enemiesSpawned == numberOfEnemies;
        //    }
        //    set { RoundOver = value; }
        //}
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
       
        public Wave(int waveNumber, int numberOfEnemies, Map map)
        {
            this.waveNumber = waveNumber;
            this.map = map;
            this.numberOfEnemies = numberOfEnemies;

        }
        private void AddEnemy()
        {
            GameWorld.Instance.gameObjectPool.CreateEnemy();
            
            enemiesSpawned++;
            //defines the changes from wave to wave, should one round be stronger etc etc.
            if(waveNumber == 3)
            {
                GameWorld.Instance.gameObjectPool.CreateWhale();
                spawningEnemies = false;
           }
        }
        public void Start()
        {
            spawningEnemies = true;
        }
        public void WaveOver()
        {
            if (GameWorld.Instance.gameObjectPool.ActiveWhaleList.Count > 0)
            {
                spawningEnemies = false;
            }
            else
            {
                GameWorld.Instance.BetweenRounds = true;
                waveNumber++;
                spawningEnemies = true;
            }
        }
       
       public void Update(GameTime gameTime)
        {
            //first we check if we have the desired number of enemies otherwise we spawn our enemies 
            if(enemiesSpawned >= (numberOfEnemies + waveNumber))
            {
                spawningEnemies = false; 
            }
            if (spawningEnemies )
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(spawnTimer > 2)
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
