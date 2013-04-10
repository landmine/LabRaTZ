using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Capstone_Project
{
    class GameState
    {
        public enum ScreenState
        {
            StartScreen,
            Paused,
            GameScreen,
            GameOverScreen,
            GameWinScreen
        }
        ScreenState screenState;

        public GameState()
        {
            screenState = ScreenState.StartScreen; 
        }

        public ScreenState getScreenState
        {
            get { return screenState; }
            set { screenState = value; }
        }

        

    }
}
