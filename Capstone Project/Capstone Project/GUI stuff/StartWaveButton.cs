using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Capstone_Project
{
    class StartWaveButton
    {
        Texture2D startWaveButton;
        Vector2 position;
        Vector2 buttonPosition;
        Color color = new Color();
        
        public StartWaveButton(Texture2D startWaveButton, Vector2 position, Color color)
        {
            this.startWaveButton = startWaveButton;
            this.position = position;
            this.color = color;
            buttonPosition = new Vector2(position.X - 165, position.Y -75);
        }

        public Vector2 getButtonPosition
        {
            get { return buttonPosition; }
        }

        public Color getColor
        {
            get { return color; }
            set { color = value; }
        }

        public Texture2D getStartWaveButton
        {
            get { return startWaveButton; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(startWaveButton, buttonPosition, color);
        }
    }
}
