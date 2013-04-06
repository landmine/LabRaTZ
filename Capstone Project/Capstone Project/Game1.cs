using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Capstone_Project
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        TileMap tileMap = new TileMap();
        EnemyPathing pathing = new EnemyPathing();
        //Enemy labEnemy;
        EnemyWave enemyWave;
        Player player;
        Texture2D menuBar;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = tileMap.getArrayWidth * tileMap.getTileSize + 216;
            graphics.PreferredBackBufferHeight = tileMap.getArrayHeight * tileMap.getTileSize;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //tile content
            Texture2D grass = Content.Load<Texture2D>("images/grass");
            Texture2D road = Content.Load<Texture2D>("images/road");

            //sets the tiles in order as numbers in the tiles array
            tileMap.tiles.Add(grass);
            tileMap.tiles.Add(road);

            //enemy content
            Texture2D enemyTexture = Content.Load<Texture2D>("images/rabbitEnemy");
            //labEnemy = new Enemy(enemyTexture, Vector2.Zero, 80, 10, .8f);
            //labEnemy.CopyPathing(pathing.Pathing);
            enemyWave = new EnemyWave(0, 10, pathing, enemyTexture);
            enemyWave.BeginWave();

            //tower content
            Texture2D towerTexture = Content.Load<Texture2D>("images/tower");
            Texture2D projectileTexture = Content.Load<Texture2D>("images/laser");
            player = new Player(tileMap, towerTexture, projectileTexture);

            //Game Menu
            menuBar = Content.Load<Texture2D>("images/menuBar");
        }

        protected override void Update(GameTime gameTime)
        {
            //labEnemy.Update(gameTime);

            //List<Enemy> enemies = new List<Enemy>();
            //enemies.Add(labEnemy);

            enemyWave.Update(gameTime);
            player.Update(gameTime, enemyWave.enemies);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            tileMap.Draw(spriteBatch);
            //labEnemy.Draw(spriteBatch);
            enemyWave.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.Draw(menuBar,new Rectangle((graphics.PreferredBackBufferWidth - menuBar.Width),
                                                    0, 
                                                    menuBar.Width, 
                                                    menuBar.Height),
                                                    Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
