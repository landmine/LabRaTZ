using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Capstone_Project
{
    class MenuBar
    {
        Texture2D menuBar;
        SpriteFont font;
        Vector2 position;
        Vector2 moneyTextPosition;
        Vector2 hpsTextPosition;
        Vector2 waveTextPosition;
        Vector2 scoreTextPosition;

        public MenuBar(Texture2D menuBar, SpriteFont font, Vector2 position)
        {
            this.menuBar = menuBar;
            this.font = font;
            this.position = position;
            moneyTextPosition = new Vector2(position.X + 60, position.Y + 15);
            hpsTextPosition = new Vector2(position.X + 100, position.Y + 15);
            waveTextPosition = new Vector2(position.X + 138, position.Y + 48);
            scoreTextPosition = new Vector2(position.X + 23, position.Y + 48);
        }

        public void Draw(SpriteBatch spriteBatch, Player player)
        {
            spriteBatch.Draw(menuBar, position, Color.White);

            string moneyText = string.Format("{0}", player.getMoney);
            string hpsText = string.Format("Hp", player.getHitpoints);
            string waveText = string.Format("Wave {0}", EnemyWave.getWaveCount);
            string scoreText = string.Format("Score {0}", EnemyWave.getNumEnemiesDead * player.getSpentMoney);
            spriteBatch.DrawString(font, moneyText, moneyTextPosition, Color.Gold);
            spriteBatch.DrawString(font, hpsText, hpsTextPosition, player.getColor);
            spriteBatch.DrawString(font, waveText, waveTextPosition, Color.CadetBlue);
            spriteBatch.DrawString(font, scoreText, scoreTextPosition, Color.DarkOrange);
        }



    }
}
