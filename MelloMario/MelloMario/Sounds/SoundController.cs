using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MelloMario.Factories;

namespace MelloMario.Sounds
{
    class SoundController
    {
        // TODO: use a factory
        public static Song Normal, BelowGround, Hurry;
        public static SoundEffect bounce, bumpBlock, breakBlock, coin, fireFlower, sizeUp, sizeDown, enemyKill, GameOver;

        public enum Songs { normal, belowGround, hurry, pause, title }
        public static Songs currentSong;

        public SoundController(Game1 game)
        {
            bounce = SoundFactory.Instance.CreateSoundEffect("smb_jumpsmall");
            bumpBlock = SoundFactory.Instance.CreateSoundEffect("smb_bump");
            Normal = SoundFactory.Instance.CreateSong("01-main-theme-overworld");
            BelowGround = SoundFactory.Instance.CreateSong("02-underworld");
            Hurry = SoundFactory.Instance.CreateSong("18-hurry-overworld-");
            currentSong = Songs.normal;
        }

        public static void PlayMusic(Songs song)
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
