using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Capstone_Project
{
    class TileMap
    {

        public List<Texture2D> tiles = new List<Texture2D>();
        
        //controls the tile size
        int tileSize = 54;
        
        //this is what our level looks like
        int[,] tileMapArray = new int[,] 
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,0,1,0},
            {0,1,0,0,0,0,0,0,0,0,0,1,0,1,0},
            {0,1,0,1,1,1,1,1,1,1,0,1,0,1,0},
            {0,1,0,1,0,0,0,0,0,1,0,1,0,1,0},
            {0,1,0,1,0,1,1,1,1,1,0,1,0,1,0},
            {0,1,0,1,0,0,0,0,0,0,0,1,0,1,0},
            {0,1,0,1,1,1,1,1,1,1,1,1,0,1,0},
            {0,1,0,0,0,0,0,0,0,0,0,0,0,1,0},
            {0,1,1,1,1,1,1,1,1,1,1,1,1,1,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

        //get methods
        public int getArrayHeight
        {
            get { return tileMapArray.GetLength(0); }
        }
        public int getArrayWidth
        {
            get { return tileMapArray.GetLength(1); }
        }
        public int getTileSize
        {
            get { return tileSize; }
        }
        public int[,] getTileMapArray
        {
            get { return tileMapArray; }
        }


        public void Draw(SpriteBatch batch)
        {
            for (int x = 0; x < tileMapArray.GetLength(1); ++x)
            {
                for (int y = 0; y < tileMapArray.GetLength(0); ++y)
                {
                    //stores info of what kind of tile it is into the tile texture list
                    //then uses the rectangle to space it out and draw it on the screen
                    batch.Draw(tiles[tileMapArray[y,x]], 
                                new Rectangle(
                                x * tileSize, 
                                y * tileSize, 
                                tileSize, 
                                tileSize), 
                                Color.White);
                }
            }
        }

    }
}
