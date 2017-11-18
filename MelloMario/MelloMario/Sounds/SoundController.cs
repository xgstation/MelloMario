using MelloMario.Factories;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.Sounds
{
    internal static class SoundController
    {
        public enum Songs
        {
            Idle,
            Normal,
            BelowGround,
            Hurry,
            Pause,
            Title,
            GameOver,
            Star
        }

        private static Songs CurrentSong = Songs.Idle;
        private static float SoundEffectVolume = SoundEffect.MasterVolume;

        public static Song Normal = SoundFactory.Instance.CreateSong("01-main-theme-overworld");
        public static Song BelowGround = SoundFactory.Instance.CreateSong("02-underworld");
        public static Song Hurry = SoundFactory.Instance.CreateSong("18-hurry-overworld-");
        public static Song GameOver = SoundFactory.Instance.CreateSong("09-game-over");
        public static Song Star = SoundFactory.Instance.CreateSong("05-starman");

        public static SoundEffectInstance Bounce = SoundFactory.Instance.CreateSoundEffect("smb_jumpsmall");
        public static SoundEffectInstance PowerBounce = SoundFactory.Instance.CreateSoundEffect("smb_jump");
        public static SoundEffectInstance BumpBlock = SoundFactory.Instance.CreateSoundEffect("smb_bump");
        public static SoundEffectInstance BreakBlock = SoundFactory.Instance.CreateSoundEffect("smb_breakblock");
        public static SoundEffectInstance Coin = SoundFactory.Instance.CreateSoundEffect("smb_coin");
        public static SoundEffectInstance Death = SoundFactory.Instance.CreateSoundEffect("smb_mariodie");
        public static SoundEffectInstance SizeUp = SoundFactory.Instance.CreateSoundEffect("smb_powerup");
        public static SoundEffectInstance EnemyKill = SoundFactory.Instance.CreateSoundEffect("smb_stomp");
        public static SoundEffectInstance Pipe = SoundFactory.Instance.CreateSoundEffect("smb_pipe");

        public static SoundEffectInstance SizeUpAppear = SoundFactory.Instance.CreateSoundEffect("smb_powerup_appears");
        public static SoundEffectInstance OneUpCollect = SoundFactory.Instance.CreateSoundEffect("smb_1");

        public static void PlayMusic(Songs song, bool reset = false)
        {
            if (CurrentSong != song || reset)
            {
                MediaPlayer.Stop();
                switch (song)
                {
                    case Songs.Normal:
                        MediaPlayer.Play(Normal);
                        break;
                    case Songs.Hurry:
                        MediaPlayer.Play(Hurry);
                        break;
                    case Songs.BelowGround:
                        MediaPlayer.Play(BelowGround);
                        break;
                    case Songs.Star:
                        MediaPlayer.Play(Star);
                        break;
                    case Songs.GameOver:
                        MediaPlayer.Play(GameOver);
                        break;
                    default:
                        MediaPlayer.Stop();
                        break;
                }
                MediaPlayer.IsRepeating = true;
                CurrentSong = song;
            }
        }

        public static void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            SoundEffect.MasterVolume = MediaPlayer.IsMuted ? 0 : SoundEffectVolume;
            SoundEffectVolume = MediaPlayer.IsMuted ? SoundEffectVolume : SoundEffect.MasterVolume;
        }
    }
}