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

        int money = 50;

        //variables for tile positions
        int tileX;
        int tileY;
        int x;
        int y;

        public Player(TileMap tileMap, Texture2D towerTexture, Texture2D projectileTexture)
        {
            this.tileMap = tileMap;
            this.towerTexture = towerTexture;
            this.projectileTexture = projectileTexture;
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

            //lays a tower if single clicked but not held down
            if (mouseState.LeftButton == ButtonState.Released
                && previousState.LeftButton == ButtonState.Pressed)
            {
                //check for clicking within bounds
                if (!(x < 0 || x > tileMap.getArrayWidth - 1 || y < 0 || y > tileMap.getArrayHeight - 1))
                {
                    //makes sure the tile is not a path and that there isnt a tower already
                    //before laying a new tower down
                    if (tileMap.getTileMapArray[y, x] == 0 && isTileEmpty())
                    {
                        LaserTower tower = new LaserTower(towerTexture, projectileTexture, new Vector2(tileX, tileY));
                        towerList.Add(tower);
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

            //used for single clicking
            previousState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tower tower in towerList)
            {
                tower.Draw(spriteBatch);
            }
        }



    }
}
