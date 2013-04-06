using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Capstone_Project
{
    class Tower : Sprite
    {   
        TileMap tileMap = new TileMap();
        
        protected int damage;
        protected float projectileTimer;
        protected List<Projectile> projectileList = new List<Projectile>();
        protected float fireRadius;
        protected Enemy targetEnemy;
        protected Texture2D projectileTexture;

        

        public Tower(Texture2D texture, Texture2D projectileTexture, Vector2 position)
            : base(texture, position)
        {
            this.projectileTexture = projectileTexture;
        }

        public Enemy TargetEnemy
        {
            get { return targetEnemy; }
        }       

        public void findEnemy(List<Enemy> enemies)
        {
            targetEnemy = null;

            foreach (Enemy enemy in enemies)
            {
                //if the tower and enemy are within fire radius, then set enemy as the target
                if (Vector2.Distance(getCenter, enemy.getCenter) <= fireRadius)
                {
                    targetEnemy = enemy;
                }
            }
        }

        public bool withinRadius(Vector2 position)
        {
            if (fireRadius >= Vector2.Distance(getCenter, position))
                return true;
            else
                return false;
        }

        protected void lookAtEnemy()
        {
            //finds the direction to face
            Vector2 direction = getCenter - targetEnemy.getCenter;
            direction.Normalize();

            //turns the tower
            spriteTurn = (float)Math.Atan2(-direction.X, direction.Y);
        }

        


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            projectileTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (targetEnemy != null)
            {
                //turn towards enemy if within distance
                lookAtEnemy();

                //if not within distance, target is null, stop turning
                if (!withinRadius(targetEnemy.getCenter) || targetEnemy.enemyDead)
                {
                    targetEnemy = null;
                    projectileTimer = 0;
                }
            }     

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            //draw projectiles
            foreach (Projectile projectile in projectileList)
                projectile.Draw(spriteBatch);

            
        }




    }
}
