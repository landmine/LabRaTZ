using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Capstone_Project
{
    class Sprite
    {
        //some aspects of our player or enemy sprites
        protected Texture2D spriteTexture;
        protected Vector2 spritePosition;
        protected Vector2 spriteVelocity;
        protected float spriteTurn;
        Vector2 spriteCenter;
        Vector2 spriteSource;
        SpriteAnimator spriteAnimation;

        //constructor will take in the texture and position of the sprite we want to make
        //and will initialize aspects including the center of the sprite
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.spriteTexture = texture;
            this.spritePosition = position;
            this.spriteSource = new Vector2(spriteTexture.Width / 2, spriteTexture.Height / 2);
            this.spriteCenter = new Vector2((position.X + spriteTexture.Width) / 2,
                                       (position.Y + spriteTexture.Height) / 2);

        }

        //overload constructor for animation
        public Sprite(SpriteAnimator spriteAnimation, Vector2 position)
        {
            this.spriteAnimation = spriteAnimation;
            this.spritePosition = position;
            this.spriteSource = new Vector2(54 / 2, 54 / 2);
            this.spriteCenter = new Vector2((position.X + 54) / 2,
                                       (position.Y + 54) / 2);

        }


        //access the center
        public Vector2 getCenter
        {
            get { return spriteCenter; }
        }
        //access the position
        public Vector2 Position
        {
            get { return spritePosition; }
        }



        public virtual void Update(GameTime gameTime)
        {
            //update the center if a texture is passed in
            if (spriteTexture != null)
            {
                this.spriteCenter = new Vector2(spritePosition.X + spriteTexture.Width / 2,
                                                spritePosition.Y + spriteTexture.Height / 2);
            }
            //else update the center if an animation is passed in
            else
            {
                this.spriteCenter = new Vector2(spritePosition.X + 54 / 2,
                                                spritePosition.Y + 54 / 2);
            }

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            if (spriteTexture != null)
            {
                spriteBatch.Draw(spriteTexture, spriteCenter, null, Color.White,
                                spriteTurn, spriteSource, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteAnimation.Draw(spriteBatch, Color.White);
            }
        }

    }
}
