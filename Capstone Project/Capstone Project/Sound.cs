using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Capstone_Project
{
    class Sound
    {
        static SoundEffect laserBlast;

        public Sound(SoundEffect laserBlast)
        {
            Sound.laserBlast = laserBlast;
        }
        public Sound()
        {
        }

        public static SoundEffect getLaserBlast
        {
            get { return laserBlast; }
        }



    }
}
