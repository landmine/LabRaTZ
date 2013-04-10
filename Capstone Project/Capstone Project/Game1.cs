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
        EnemyWave enemyWave;
        Player player;
        Texture2D radiusTexture;

        Texture2D sideBar;
        MenuBar menuBar;
        Vector2 menuBarPosition;

        SpriteFont font;

        Texture2D hitpointBar;
        Texture2D hitpointBarOverlay;
        Vector2 hitpointBarPosition;

        Texture2D startButton;
        Vector2 buttonPosition;
        StartWaveButton startWaveButton;

        GameState screenState;
        KeyboardState keyboardState;
        KeyboardState previousKeyboardState;
        Texture2D startScreen;
        Texture2D gameOverScreen;
        Texture2D gameWinScreen;

        //Animation
        SpriteAnimator rabbitAnimation;

        //audio
        SoundEffect laserBlast;
        Sound sound;

        //music
        Song bgMusic;
        bool isMusicPlaying = false;


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

            //audio
            laserBlast = Content.Load<SoundEffect>("Audio/laserBlast");
            sound = new Sound(laserBlast);

            //music
            bgMusic = Content.Load<Song>("Audio/drMario");
            MediaPlayer.IsRepeating = true;


            //tile content
            Texture2D grass = Content.Load<Texture2D>("images/grass");
            Texture2D road = Content.Load<Texture2D>("images/road");

            //sets the tiles in order as numbers in the tiles array
            tileMap.tiles.Add(grass);
            tileMap.tiles.Add(road);

            //Fire Radius
            radiusTexture = Content.Load<Texture2D>("images/radius");

            //tower content
            Texture2D towerTexture = Content.Load<Texture2D>("images/tower");
            Texture2D projectileTexture = Content.Load<Texture2D>("images/laser");

            //Game Menu
            sideBar = Content.Load<Texture2D>("images/GUIstuff/menuBar");
            font = Content.Load<SpriteFont>("SpriteFonts/MoneyFont");
            menuBarPosition = new Vector2((graphics.PreferredBackBufferWidth - sideBar.Width), 0);
            menuBar = new MenuBar(sideBar, font, menuBarPosition);

            //Hitpoint Bar
            hitpointBar = Content.Load<Texture2D>("images/GUIstuff/healthBar");
            hitpointBarOverlay = Content.Load<Texture2D>("images/GUIstuff/healthBarOverlay");
            hitpointBarPosition.X = menuBarPosition.X + 125;
            hitpointBarPosition.Y = menuBarPosition.Y + 17;

            //Start Button
            startButton = Content.Load<Texture2D>("images/GUIstuff/startButton");
            buttonPosition = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            startWaveButton = new StartWaveButton(startButton, buttonPosition, Color.White);

            //player content
            player = new Player(tileMap, towerTexture, projectileTexture, radiusTexture, startWaveButton, hitpointBar, hitpointBarPosition);

            //bunny spriteSheet
            Texture2D bunnySheet = Content.Load<Texture2D>("images/bunnySheet");

            //enemy content
            Texture2D rabbitTexture = Content.Load<Texture2D>("images/rabbitEnemy2");
            rabbitAnimation = new SpriteAnimator(bunnySheet, new Point(5, 2));
            enemyWave = new EnemyWave(0, 10, pathing, rabbitAnimation, player, startWaveButton, hitpointBar, hitpointBarPosition);


            //Game State
            screenState = new GameState();
            startScreen = Content.Load<Texture2D>("images/ScreenStates/StartScreen");
            gameOverScreen = Content.Load<Texture2D>("images/ScreenStates/GameOverScreen");
            gameWinScreen = Content.Load<Texture2D>("images/ScreenStates/GameWinScreen");



        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            //Update the Start Screen
            if (screenState.getScreenState == GameState.ScreenState.StartScreen)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screenState.getScreenState = GameState.ScreenState.GameScreen;
                }
            }
            //Update the Game Screen
            else if (screenState.getScreenState == GameState.ScreenState.GameScreen)
            {
                if (keyboardState.IsKeyUp(Keys.Space) && previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    screenState.getScreenState = GameState.ScreenState.Paused;
                }
                rabbitAnimation.Update(gameTime);
                enemyWave.Update(gameTime);
                player.Update(gameTime, EnemyWave.enemies);

                //play music
                if (!isMusicPlaying)
                {
                    MediaPlayer.Play(bgMusic);
                    isMusicPlaying = true;
                }

                //if hitpoints are 0, go to game over screen
                if (player.getHitpoints == 0)
                {
                    screenState.getScreenState = GameState.ScreenState.GameOverScreen;
                }
                //sets how many enemies to kill to win the game
                else if (EnemyWave.getNumEnemiesDead >= 20)
                {
                    screenState.getScreenState = GameState.ScreenState.GameWinScreen;
                }

                //if wave complete go to game win screen for now
                else if (enemyWave.WaveComplete())
                {                    
                    enemyWave.getFlag = false;
                    enemyWave.getEnemyCount = 0;
                    enemyWave.getEnemyHealth *= 1.2f;
                }

                
            }
            //Update the Pause screen
            if (screenState.getScreenState == GameState.ScreenState.Paused)
            {
                //play music
                if (isMusicPlaying)
                {
                    MediaPlayer.Pause();
                    isMusicPlaying = false;
                }
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screenState.getScreenState = GameState.ScreenState.GameScreen;
                }


            }
            //Update the Game Over Screen
            else if (screenState.getScreenState == GameState.ScreenState.GameOverScreen)
            {
                EnemyWave.getMakeEnemy = false;

            }
            //Update the Game Win Screen
            else if (screenState.getScreenState == GameState.ScreenState.GameWinScreen)
            {
                //put game win screen code here for food
            }

            previousKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            //Draw the Start Screen
            if (screenState.getScreenState == GameState.ScreenState.StartScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(startScreen, new Rectangle(0, 0, startScreen.Width, startScreen.Height), Color.White);
            }
            //Draw the Game Screen
            else if (screenState.getScreenState == GameState.ScreenState.GameScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                tileMap.Draw(spriteBatch);
                menuBar.Draw(spriteBatch, player);
                startWaveButton.Draw(spriteBatch);
                enemyWave.Draw(spriteBatch);
                player.Draw(spriteBatch);
                //spriteBatch.Draw(hitpointBarOverlay, hitpointBarPosition, Color.White);
            }
            //Draw the Pause screen
            if (screenState.getScreenState == GameState.ScreenState.Paused)
            {
                GraphicsDevice.Clear(Color.Black);
                tileMap.Draw(spriteBatch);
                menuBar.Draw(spriteBatch, player);
                startWaveButton.Draw(spriteBatch);
                enemyWave.Draw(spriteBatch);
                player.Draw(spriteBatch);
            }
            //Draw the Game Over Screen
            else if (screenState.getScreenState == GameState.ScreenState.GameOverScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                tileMap.Draw(spriteBatch);
                menuBar.Draw(spriteBatch, player);
                startWaveButton.Draw(spriteBatch);
                enemyWave.Draw(spriteBatch);
                player.Draw(spriteBatch);
                spriteBatch.Draw(gameOverScreen, new Rectangle(0, 0, gameOverScreen.Width, gameOverScreen.Height), Color.White);
            }
            //Draw the Game Win Screen
            else if (screenState.getScreenState == GameState.ScreenState.GameWinScreen)
            {
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Draw(gameWinScreen, new Rectangle(0, 0, gameWinScreen.Width, gameWinScreen.Height), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
