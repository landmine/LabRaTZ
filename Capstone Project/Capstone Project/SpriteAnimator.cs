using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Capstone_Project
{
    class SpriteAnimator
    {
        public Point frameSize = new Point(54, 54);
        public Point currentFrame;
        Point sheetSize = new Point(0, 0);
        Texture2D sprite;
        int timeSinceLastFrame = 0;

        //lower the number faster the animation
        int movementPerFrame = 150;

        public SpriteAnimator(Texture2D newSprite, Point newSheetSize)
        {
            sprite = newSprite;
            sheetSize = newSheetSize;
        }



        public void Update(GameTime gameTime)
        {
            currentFrame.Y = 0;
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > movementPerFrame)
            {
                timeSinceLastFrame -= movementPerFrame;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {

            foreach (Enemy enemy in EnemyWave.getEnemies)
            {


                spriteBatch.Draw(sprite, 
                                new Vector2 (enemy.Position.X + 54/2, enemy.Position.Y + 54/2),
                                new Rectangle(currentFrame.X * frameSize.X, currentFrame.Y * frameSize.Y, frameSize.X, frameSize.Y),
                                Color.White, 
                                enemy.getSpriteTurn, 
                                new Vector2((54/2), (54/2)), 
                                .7f, 
                                SpriteEffects.None, 
                                0);

            }
        }


    }
}

