using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Capstone_Project
{
    class Projectile:Sprite
    {
        private int damage;
        public static float speed;
        private int projectileRemove;

        public Projectile(Texture2D texture, Vector2 position, float rotation, 
        int speed, int damage) : base(texture, position)
        {
            this.damage = damage;
            this.spriteTurn = rotation;
            Projectile.speed = speed;
        }

        public void SetRotation(float value)
        {
            spriteTurn = value;

            //rotates projectile around the z axis
            spriteVelocity = Vector2.Transform(new Vector2(0, -speed),
                Matrix.CreateRotationZ(spriteTurn));
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public static float getSpeed
        {
            get { return speed; }
            set { speed = value; }
        }

        //see if the projectile has expired
        public bool projectileGone()
        {
            return projectileRemove > 100;
        }

        //set the projectile to expire
        public void RemoveProjectile()
        {
            this.projectileRemove = 101;
        }

        public override void Update(GameTime gameTime)
        {
            projectileRemove++;
            spritePosition += spriteVelocity;

            base.Update(gameTime);
        }


    }
}
