using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MelloMario.Factories;
using MelloMario.Theming;

namespace MelloMario.Sounds
{
    static class SoundController
    {
        private static IGameModel Model;
        private static float SoundEffectVolume = SoundEffect.MasterVolume;
        public static Song Normal = SoundFactory.Instance.CreateSong("01-main-theme-overworld");
        public static Song BelowGround = SoundFactory.Instance.CreateSong("02-underworld");
        public static Song Hurry = SoundFactory.Instance.CreateSong("18-hurry-overworld-");
        public static Song GameOver = SoundFactory.Instance.CreateSong("09-game-over");
        public static Song Star = SoundFactory.Instance.CreateSong("05-starman");


        public static SoundEffect Bounce = SoundFactory.Instance.CreateSoundEffect("smb_jumpsmall");
        public static SoundEffect PowerBounce = SoundFactory.Instance.CreateSoundEffect("smb_jump");
        public static SoundEffect BumpBlock = SoundFactory.Instance.CreateSoundEffect("smb_bump");
        public static SoundEffect BreakBlock = SoundFactory.Instance.CreateSoundEffect("smb_breakblock");
        public static SoundEffect Coin = SoundFactory.Instance.CreateSoundEffect("smb_coin");
        public static SoundEffect Death = SoundFactory.Instance.CreateSoundEffect("smb_mariodie");
        public static SoundEffect SizeUp = SoundFactory.Instance.CreateSoundEffect("smb_powerup");
        public static SoundEffect EnemyKill = SoundFactory.Instance.CreateSoundEffect("smb_stomp");
        public static SoundEffect Pipe = SoundFactory.Instance.CreateSoundEffect("smb_pipe");

        public static SoundEffect SizeUpAppear = SoundFactory.Instance.CreateSoundEffect("smb_powerup_appears");
        public static SoundEffect OneUpCollect = SoundFactory.Instance.CreateSoundEffect("smb_1");

        public enum Songs { Idle, Normal, BelowGround, Hurry, Pause, Title, GameOver, Star }

        public static Songs CurrentSong = Songs.Idle;

        public static void Initialize(IGameModel newModel)
        {
            Model = newModel;
        }

        private static void PlayMusic(Songs song)
        {
            if (CurrentSong == song) return;
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

        public static void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            SoundEffect.MasterVolume = MediaPlayer.IsMuted ? 0 : SoundEffectVolume;
            SoundEffectVolume = MediaPlayer.IsMuted ? SoundEffectVolume : SoundEffect.MasterVolume;
        }

        public static void Update()
        {
            PlayMusic(Model.ThemeMusic);
        }
        
    }
}
