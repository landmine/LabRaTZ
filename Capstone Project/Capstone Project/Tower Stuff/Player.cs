using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Capstone_Project
{
    class Player
    {
        Texture2D towerTexture;
        Texture2D projectileTexture;

        public List<Tower> towerList = new List<Tower>();
        private TileMap tileMap;

        MouseState mouseState;
        MouseState previousState;

        int money = 30;
        protected LaserTower laserTower;
        Texture2D radiusTexture;
        Texture2D hoverTexture;
        Rectangle radiusRect;
        Rectangle hoverRect;
        bool isRadiusOn = false;
        int maxHitPoints;
        float currentHitPoints;
        StartWaveButton startWaveButton;
        Texture2D hitpointBar;
        Rectangle hitpointRect;
        float hitpointBarWidth;
        Vector2 hitpointBarPosition;
        Color color;
        int spentMoney;
        

        //variables for tile positions
        int tileX;
        int tileY;
        int x;
        int y;

        public Player(TileMap tileMap, 
                      Texture2D towerTexture, 
                      Texture2D projectileTexture, 
                      Texture2D radiusTexture,
                      Texture2D hoverTexture,
                      StartWaveButton startWaveButton,
                      Texture2D hitpointBar,
                      Vector2 hitpointBarPosition)
        {
            this.tileMap = tileMap;
            this.towerTexture = towerTexture;
            this.projectileTexture = projectileTexture;
            this.radiusTexture = radiusTexture;
            this.hoverTexture = hoverTexture;
            this.startWaveButton = startWaveButton;
            this.maxHitPoints = 100;
            this.currentHitPoints = maxHitPoints;
            this.hitpointBar = hitpointBar;
            this.hitpointBarPosition = hitpointBarPosition;
        }

        public int getMoney
        {
            get { return money; }
            set { money = value; }
        }

        public float getHitpoints
        {
            get { return currentHitPoints; }
            set { currentHitPoints = value; }
        }

        public float getHitpointPercent
        {
            get { return currentHitPoints / maxHitPoints; }
        }

        public int getSpentMoney
        {
            get { return spentMoney; }
        }

        public Color getColor
        {
            get { return color; }
        }

        //check if the tile is empty so we can place a tower or not
        public bool isTileEmpty()
        {
            bool clearTile = true;
            foreach (Tower towers in towerList)
            {
                if (towers.Position == new Vector2(tileX, tileY))
                {
                    clearTile = false;
                    break;
                }

            }
            return clearTile;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            mouseState = Mouse.GetState();

            //variables to figure out what cell the mouse is clicking in
            x = (int)(mouseState.X / tileMap.getTileSize);
            y = (int)(mouseState.Y / tileMap.getTileSize);
            tileX = (x * tileMap.getTileSize);
            tileY = (y * tileMap.getTileSize);

            hoverRect = new Rectangle(tileX, tileY, tileMap.getTileSize, tileMap.getTileSize);

            //lays a tower if single clicked but not held down
            if (mouseState.LeftButton == ButtonState.Released
                && previousState.LeftButton == ButtonState.Pressed)
            {
                //check for start button bounds and begin wave if clicked
                if( mouseState.X >= startWaveButton.getButtonPosition.X && 
                    mouseState.X <= startWaveButton.getButtonPosition.X + startWaveButton.getStartWaveButton.Width &&
                    mouseState.Y >= startWaveButton.getButtonPosition.Y && 
                    mouseState.Y <= startWaveButton.getButtonPosition.Y + startWaveButton.getStartWaveButton.Height&&
                    EnemyWave.getMakeEnemy == false)
                {
                    startWaveButton.getColor = Color.Green;
                    EnemyWave.BeginWave();
                    EnemyWave.getWaveCount++;
                }
                

                //check for clicking within bounds
                if (!(x < 0 || x > tileMap.getArrayWidth - 1 || y < 0 || y > tileMap.getArrayHeight - 1))
                {
                    //makes sure the tile is not a path and that there isnt a tower already
                    //before laying a new tower down
                    if (tileMap.getTileMapArray[y, x] == 0 && isTileEmpty())
                    {
                        if (money >= 10)
                        {
                            laserTower = new LaserTower(towerTexture, projectileTexture, new Vector2(tileX, tileY), isRadiusOn);
                            towerList.Add(laserTower);
                            money -= 10;
                            spentMoney += 10;
                        }
                    }

                    //if there is a tower with a radius, this click turns it off and sets isRadiusOn to false
                    if (tileMap.getTileMapArray[y, x] == 0 && !isTileEmpty() && laserTower.getIsRadiusOn)
                    {
                        radiusRect = new Rectangle(0, 0, 0, 0);
                        laserTower.getIsRadiusOn = false;
                    }
                    
                    //else if there is a tower, show it's radius and set isRadiusOn to true
                    else if (tileMap.getTileMapArray[y, x] == 0 && !isTileEmpty())
                    {
                        int centerX = tileX - (int)laserTower.getFireRadius + towerTexture.Width/2;
                        int centerY = tileY - (int)laserTower.getFireRadius + towerTexture.Height/2;
                        radiusRect = new Rectangle(centerX, centerY, (int)laserTower.getFireRadius * 2, (int)laserTower.getFireRadius * 2);
                        laserTower.getIsRadiusOn = true;
                    }
                
                }
            }


            //go through all the towers and have them look for enemies within their shoot radius
            foreach (Tower tower in towerList)
            {
                if (tower.TargetEnemy == null)
                {
                    tower.findEnemy(enemies);
                }

                tower.Update(gameTime);
            }

            //tint the health bar based on where it's at
            if (getHitpointPercent < .6 &&
                getHitpointPercent > .3)
            {
                color = Color.Gold;
            }
            else if (getHitpointPercent <= .3)
            {
                color = Color.DarkRed;
            }
            else
            {
                color = Color.DarkGreen;
            }

            //used for single clicking
            previousState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towerList)
            {
                tower.Draw(spriteBatch);
            }

            if (!(x < 0 || x > tileMap.getArrayWidth - 1 || y < 0 || y > tileMap.getArrayHeight - 1))
            {
                spriteBatch.Draw(hoverTexture, hoverRect, Color.White);
            }
 
            spriteBatch.Draw(radiusTexture, radiusRect, Color.White);

            //draw the hp bar underlay
            hitpointRect = new Rectangle((int)hitpointBarPosition.X,
                                       (int)hitpointBarPosition.Y,
                                       hitpointBar.Width,
                                       hitpointBar.Height);
            spriteBatch.Draw(hitpointBar, hitpointRect, Color.Gray);

            //draw the hitpoint bar based on health
            hitpointBarWidth = getHitpointPercent * hitpointBar.Width;
            hitpointRect = new Rectangle((int)hitpointBarPosition.X,
                                        (int)hitpointBarPosition.Y,
                                        (int)hitpointBarWidth,
                                        hitpointBar.Height);
            spriteBatch.Draw(hitpointBar, hitpointRect, color);
        }



    }
}
