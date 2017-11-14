using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MelloMario.Factories;

namespace MelloMario.Sounds
{
    static class SoundController
    {
        public static Song Normal = SoundFactory.Instance.CreateSong("01-main-theme-overworld");
        public static Song BelowGround = SoundFactory.Instance.CreateSong("02-underworld");
        public static Song Hurry = SoundFactory.Instance.CreateSong("18-hurry-overworld-");
        public static Song GameOver = SoundFactory.Instance.CreateSong("09-game-over");

        public static SoundEffect bounce = SoundFactory.Instance.CreateSoundEffect("smb_jumpsmall");
        public static SoundEffect powerBounce = SoundFactory.Instance.CreateSoundEffect("smb_jump-super");
        public static SoundEffect bumpBlock = SoundFactory.Instance.CreateSoundEffect("smb_bump");
        public static SoundEffect breakBlock = SoundFactory.Instance.CreateSoundEffect("smb_breakblock");
        public static SoundEffect coin = SoundFactory.Instance.CreateSoundEffect("smb_coin");
        public static SoundEffect death = SoundFactory.Instance.CreateSoundEffect("smb_mariodie");
        public static SoundEffect sizeUp = SoundFactory.Instance.CreateSoundEffect("smb_powerup");
        public static SoundEffect enemyKill = SoundFactory.Instance.CreateSoundEffect("smb_stomp");
        public static SoundEffect pipe = SoundFactory.Instance.CreateSoundEffect("smb_pipe");

        public static SoundEffect sizeUpAppear = SoundFactory.Instance.CreateSoundEffect("smb_powerup_appears");
        public static SoundEffect oneUpCollect = SoundFactory.Instance.CreateSoundEffect("smb_1-up");

        public enum Songs { normal, belowGround, hurry, pause, title, gameOver }
        public static Songs CurrentSong = Songs.normal;

        public static void PlayMusic(Songs song)
        {
            switch (song)
            {
                case Songs.normal:
                    MediaPlayer.Play(Normal);
                    //MediaPlayer.IsRepeating = true;
                    break;
                case Songs.hurry:
                    MediaPlayer.Play(Hurry);
                    //MediaPlayer.IsRepeating = true;
                    break;
                case Songs.belowGround:
                    MediaPlayer.Play(BelowGround);
                    break;
                default:
                    break;
            }
            MediaPlayer.IsRepeating = true;

            CurrentSong = song;
        }

        public static void Mute()
        {
            MediaPlayer.Stop();
        }
    }
}
