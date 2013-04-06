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
        
        

        //constructor will take in the texture and position of the sprite we want to make
        //and will initialize aspects including the center of the sprite
        public Sprite(Texture2D texture, Vector2 position)
        {
            spriteTexture = texture;
            spritePosition = position;
            spriteSource = new Vector2(spriteTexture.Width/2, spriteTexture.Height/2);
            spriteCenter = new Vector2((spritePosition.X + texture.Width)/2,
                                       (spritePosition.Y + texture.Height)/2);

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
            //update the center
            this.spriteCenter = new Vector2(spritePosition.X + spriteTexture.Width / 2,
                                            spritePosition.Y + spriteTexture.Height / 2);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteTexture, spriteCenter, null, Color.White,
                            spriteTurn, spriteSource, 1.0f, SpriteEffects.None, 0);
        }

    }
}
