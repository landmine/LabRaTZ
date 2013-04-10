using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Capstone_Project
{
    class EnemyWave
    {

        int enemyNumber;
        int enemyCount = 0;
        static int waveCount;
        float spawnTimer = 0;
        static bool makeEnemy;
        bool flag = false;
        bool enemyPassed;
        public static List<Enemy> enemies = new List<Enemy>();
        EnemyPathing enemyPathing;
        //Texture2D enemyTexture;
        SpriteAnimator spriteAnimation;
        Player player;
        StartWaveButton startWaveButton;
        float enemyHealth = 100;
        int enemyMoney = 1;
        static Enemy enemy;
        static float spawnSpacer = 2;
        Texture2D hitpointBar;
        Rectangle hitpointRect;
        float hitpointBarWidth;
        Vector2 hitpointBarPosition;
        Color color;
        static int numEnemiesDead;

        public EnemyWave(int waveCount, 
                         int enemyNumber,
                         EnemyPathing enemyPathing,
                         SpriteAnimator spriteAnimation, 
                         Player player,
                         StartWaveButton startWaveButton,
                         Texture2D hitpointBar, 
                         Vector2 hitpointBarPosition)
        {
            EnemyWave.waveCount = waveCount;
            this.enemyNumber = enemyNumber;
            this.enemyPathing = enemyPathing;
            this.spriteAnimation = spriteAnimation;
            this.player = player;
            this.startWaveButton = startWaveButton;
            this.hitpointBar = hitpointBar;
            this.hitpointBarPosition = hitpointBarPosition;
        }

        //test if all enemies in a wave are gone
        public bool WaveComplete()
        {
            if (enemies.Count == 0 && enemyCount == enemyNumber)
                return true;
            else
                return false;
        }
        public static float getSpawnSpacer
        {
            get { return spawnSpacer; }
            set { spawnSpacer = value; }
        }

        public Enemy getEnemy
        {
            get { return enemy; }
            set { enemy = value; }
        }

        public static int getNumEnemiesDead
        {
            get { return numEnemiesDead; }
        }

        public bool getFlag
        {
            get { return flag; }
            set { flag = value; }
        }

        public float getEnemyHealth
        {
            get { return enemyHealth; }
            set { enemyHealth = value; }
        }

        public int getEnemyNumber
        {
            get { return enemyNumber; }
            set { enemyNumber = value; }
        }

        public int getEnemyCount
        {
            get { return enemyCount; }
            set { enemyCount = value; }
        }

        //test if an enemy made it to the end
        public bool EnemyAtEnd
        {
            get { return enemyPassed; }
            set { enemyPassed = value; }
        }

        public static int getWaveCount
        {
            get { return waveCount; }
            set { waveCount = value; }
        }

        public static List<Enemy> getEnemies
        {
            get { return enemies; }
        }

        //add an enemy to the list
        private void AddEnemy()
        {
            enemy = new Enemy(spriteAnimation, enemyPathing.Pathing.Peek(), enemyHealth, enemyMoney, Enemy.getEnemySpeed);            
            enemy.CopyPathing(enemyPathing.Pathing);
            enemies.Add(enemy);
            spawnTimer = 0;

            enemyCount++;
        }

        //start the wave
        public static void BeginWave()
        {
            makeEnemy = true;
        }

        public static bool getMakeEnemy
        {
            get { return makeEnemy; }
            set { makeEnemy = value; }
        }


        public void Update(GameTime gameTime)
        {

            if (enemyCount == enemyNumber)
                makeEnemy = false;
            if (makeEnemy)
            {
                spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                //immediately spawns the first enemy when AddEnemy is called
                if (flag == false)
                {
                    AddEnemy();
                    flag = true;
                }
                else if (spawnTimer > spawnSpacer && flag == true)
                    AddEnemy();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);

                if (enemy.enemyDead)
                {
                    //if enemies slip by, take hitpoints away from player
                    if (enemy.getPresentHealth > 0)
                    {
                        enemyPassed = true;
                        player.getHitpoints -= 10;

                    }
                    //else get money for killing the enemy
                    else
                    {
                        player.getMoney += enemy.getMoney;
                        numEnemiesDead++;
                    }

                    enemies.Remove(enemy);

                    //changes button back after wave
                    if (WaveComplete())
                    {
                        enemyCount = 10;
                        makeEnemy = false;
                        startWaveButton.getColor = Color.White;
                    }
                }
            }

            


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);

                //tint the health bar based on where it's at
                if ((enemy.getPresentHealth / enemy.getEnemyHealth) < .6 &&
                    (enemy.getPresentHealth / enemy.getEnemyHealth) > .3)
                {
                    color = Color.Gold;
                }
                else if ((enemy.getPresentHealth / enemy.getEnemyHealth) <= .3)
                {
                    color = Color.DarkRed;
                }
                else
                {
                    color = Color.DarkGreen;
                }


                //draw the hp bar underlay
                hitpointRect = new Rectangle((int)enemy.Position.X + 15,
                                           (int)enemy.Position.Y + 10,
                                           hitpointBar.Width/3,
                                           hitpointBar.Height/3);
                spriteBatch.Draw(hitpointBar, hitpointRect, Color.Gray);

                //draw the hitpoint bar based on health
                hitpointBarWidth = (enemy.getPresentHealth/enemy.getEnemyHealth) * hitpointBar.Width;
                hitpointRect = new Rectangle((int)enemy.Position.X + 15,
                                            (int)enemy.Position.Y + 10,
                                            (int)hitpointBarWidth/3,
                                            hitpointBar.Height/3);
                spriteBatch.Draw(hitpointBar, hitpointRect, color);
            }

           
        }


    }
}
