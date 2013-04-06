using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Capstone_Project
{
    class Enemy : Sprite
    {
        float enemySpeed = .8f;
        float enemyHealth;
        float presentHealth;
        bool living = true;
        int money;
        Queue<Vector2> pathing = new Queue<Vector2>();

        public Enemy(Texture2D texture, Vector2 position, float health, int money, float speed)
            : base(texture, position)
        {
            this.enemySpeed = speed;
            this.enemyHealth = health;
            this.presentHealth = enemyHealth;
            this.money = money;
        }

        public bool enemyDead
        {
            get { return !living; }
        }

        public float PresentHealth
        {
            get { return presentHealth; }
            set { presentHealth = value; }
        }

        public int Money
        {
            get { return money; }
        }


        //copy pathing queue over
        public void CopyPathing(Queue<Vector2> pathing)
        {
            foreach (Vector2 path in pathing)
                this.pathing.Enqueue(path);

            this.spritePosition = this.pathing.Dequeue();
        }

        //return the distance from path
        public float AreWeThereYet
        {
            get { return Vector2.Distance(spritePosition, pathing.Peek()); }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //moves the enemy along the path
            if (pathing.Count != 0)
            {
                if (AreWeThereYet < enemySpeed)
                {
                    spritePosition = pathing.Peek();
                    pathing.Dequeue();
                }

                else
                {
                    Vector2 direction = pathing.Peek() - spritePosition;
                    direction.Normalize();

                    spriteVelocity = direction * enemySpeed;

                    spritePosition += spriteVelocity;
                }
            }
            else
                living = false;

            if (presentHealth <= 0)
                living = false;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (living)
            {
                base.Draw(spriteBatch);
            }
        }


    }
}
