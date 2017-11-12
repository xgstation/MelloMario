using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MelloMario.Factories;

namespace MelloMario.Sounds
{
    class SoundController
    {
        // TODO: use a factory
        public static Song Normal, BelowGround, Hurry;
        public static SoundEffect bounce, powerBounce, bumpBlock, breakBlock, coin, death, sizeUp, sizeUpAppear, sizeDown, enemyKill, GameOver;

        public enum Songs { normal, belowGround, hurry, pause, title }
        public static Songs currentSong;

        public SoundController(Game1 game)
        {
            bounce = SoundFactory.Instance.CreateSoundEffect("smb_jumpsmall");
            powerBounce = SoundFactory.Instance.CreateSoundEffect("smb_jump-super");
            bumpBlock = SoundFactory.Instance.CreateSoundEffect("smb_bump");
            breakBlock = SoundFactory.Instance.CreateSoundEffect("smb_breakblock");
            coin = SoundFactory.Instance.CreateSoundEffect("smb_coin");
            death = SoundFactory.Instance.CreateSoundEffect("smb_mariodie");
            sizeUp = SoundFactory.Instance.CreateSoundEffect("smb_powerup");

            sizeUpAppear = SoundFactory.Instance.CreateSoundEffect("smb_powerup_appears");

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
