using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Capstone_Project
{
    class LaserTower : Tower
    {
        bool isRadiusOn = false;

        public LaserTower(Texture2D texture, Texture2D projectileTexture, Vector2 position, bool isRadiusOn)
            : base(texture, projectileTexture, position)
        {
            //initialize damage and fire radius
            this.damage = 8;
            this.fireRadius = 100;
            this.isRadiusOn = isRadiusOn;

        }

        public float getFireRadius
        {
            get { return fireRadius; }
        }

        public bool getIsRadiusOn
        {
            get { return isRadiusOn; }
            set { isRadiusOn = value; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //compares the speed of the projectile and makes sure the enemy isn't null
            if (projectileTimer >= .35f && targetEnemy != null)
            {
                //creates a projectile object
                Projectile projectile = new Projectile(projectileTexture, Vector2.Subtract(getCenter,
                                        new Vector2(projectileTexture.Width / 2)), spriteTurn, 16, damage);

                //adds the projectile to our list and resets the timer
                projectileList.Add(projectile);
                projectileTimer = 0;

                //laser sound
                SoundEffectInstance laserBlast = Sound.getLaserBlast.CreateInstance();
                laserBlast.Play();
            }

            for (int i = 0; i < projectileList.Count; i++)
            {
                Projectile projectile = projectileList[i];

                //have the projectiles turn with the tower shooting them
                projectile.SetRotation(spriteTurn);
                projectile.Update(gameTime);

                if (!withinRadius(projectile.getCenter))
                    projectile.RemoveProjectile();

                //take enemy damage
                if (targetEnemy != null && Vector2.Distance(projectile.getCenter, targetEnemy.getCenter) < 30)
                {
                    targetEnemy.getPresentHealth -= projectile.Damage;
                    projectile.RemoveProjectile();
                }

                //remove projectile
                if (projectile.projectileGone())
                {
                    projectileList.Remove(projectile);
                    i--;
                }

                
            }
        }




    }
}
