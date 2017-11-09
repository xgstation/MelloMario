using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.Sounds
{
    class SoundController
    {
        public static Song Normal, BelowGround, Hurry, Pause, TitleScreen;
        public static SoundEffect bounce, bumpBlock, breakBlock, coin, fireFlower, sizeUp, sizeDown, enemyKill, GameOver;

        public enum songs { normal, belowGround, hurry, pause, title }
        public static songs currentSong;

        public SoundController(Game1 game)
        {
            bounce = game.Content.Load<SoundEffect>("SFX/smb_jumpsmall");
            Normal = game.Content.Load<Song>("Music/01-main-theme-overworld");
            currentSong = songs.normal;
        }

        public static void PlayMusic(songs song)
        {
            MediaPlayer.Play(Normal);
            MediaPlayer.IsRepeating = true;
        }

        public void Mute()
        {
            MediaPlayer.Stop();
        }
    }
}
