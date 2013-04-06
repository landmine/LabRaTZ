using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Capstone_Project
{
    class EnemyWave
    {

        int enemyNumber;
        int enemyCount = 0;
        int waveCount;
        float spawnTimer = 0;  
        bool makeEnemy;
        bool enemyPassed;
        public List<Enemy> enemies = new List<Enemy>();
        EnemyPathing enemyPathing; 
        Texture2D enemyTexture;

        public EnemyWave(int waveCount, int enemyNumber,
                        EnemyPathing enemyPathing, 
                        Texture2D enemyTexture)
        {
            this.waveCount = waveCount;
            this.enemyNumber = enemyNumber;

            this.enemyPathing = enemyPathing;
            this.enemyTexture = enemyTexture;
        }

        public bool WaveComplete()
        {
            if(enemies.Count == 0 && enemyCount == enemyNumber)
            return true;
            else
            return false;

        }

        public bool EnemyAtEnd
        {
            get { return enemyPassed; }
            set { enemyPassed = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        private void AddEnemy()
        {
            Enemy enemy = new Enemy(enemyTexture,
            enemyPathing.Pathing.Peek(), 400, 10, .8f);
            enemy.CopyPathing(enemyPathing.Pathing);
            enemies.Add(enemy);
            spawnTimer = 0;

            enemyCount++;
        }

        public void BeginWave()
        {
            makeEnemy = true;
        }

        public void Update(GameTime gameTime)
        {
            if (enemyCount == enemyNumber)
                makeEnemy = false;
            if (makeEnemy)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (spawnTimer > 2)
                    AddEnemy();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);
                if (enemy.enemyDead)
                {
                    if (enemy.PresentHealth > 0)
                    {
                        enemyPassed = true;
                    }

                    enemies.Remove(enemy);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
                enemy.Draw(spriteBatch);
        }
        



    }
}
