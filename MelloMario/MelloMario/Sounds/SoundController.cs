using MelloMario.Factories;
using MelloMario.MarioObjects;
using MelloMario.MarioObjects.PowerUpStates;
using MelloMario.MarioObjects.ProtectionStates;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.Sounds
{
    internal static class SoundController
    {
        private static IGameModel Model;

        public static void Initialize(IGameModel model)
        {
            Model = model;
        }
        private enum Songs
        {
            Idle,
            Normal,
            BelowGround,
            Hurry,
            Title,
            GameOver,
            Star
        }

        private static Songs CurrentBGM = Songs.Idle;
        private static float SoundEffectVolume = SoundEffect.MasterVolume;

        private static bool PlayFinished= false;

        private static readonly Song Normal = SoundFactory.Instance.CreateSong("01-main-theme-overworld");
        private static readonly Song BelowGround = SoundFactory.Instance.CreateSong("02-underworld");
        private static readonly Song Star = SoundFactory.Instance.CreateSong("05-starman");
        private static readonly Song Hurry = SoundFactory.Instance.CreateSong("18-hurry-overworld-");

        //public static Song GameOver = SoundFactory.Instance.CreateSong("09-game-over");

        private static readonly SoundEffectInstance Bounce = SoundFactory.Instance.CreateSoundEffect("smb_jumpsmall");
        private static readonly SoundEffectInstance PowerBounce = SoundFactory.Instance.CreateSoundEffect("smb_jump");
        private static readonly SoundEffectInstance BumpBlock = SoundFactory.Instance.CreateSoundEffect("smb_bump");
        private static readonly SoundEffectInstance BreakBlock = SoundFactory.Instance.CreateSoundEffect("smb_breakblock");
        private static readonly SoundEffectInstance Coin = SoundFactory.Instance.CreateSoundEffect("smb_coin");
        private static readonly SoundEffectInstance Death = SoundFactory.Instance.CreateSoundEffect("smb_mariodie");
        private static readonly SoundEffectInstance SizeUp = SoundFactory.Instance.CreateSoundEffect("smb_powerup");
        private static readonly SoundEffectInstance EnemyKill = SoundFactory.Instance.CreateSoundEffect("smb_stomp");
        private static readonly SoundEffectInstance Pipe = SoundFactory.Instance.CreateSoundEffect("smb_pipe");

        private static readonly SoundEffectInstance GameOver = SoundFactory.Instance.CreateSoundEffect("smb_gameover");
        private static readonly SoundEffectInstance GameWon = SoundFactory.Instance.CreateSoundEffect("smb_stage_clear");

        private static readonly SoundEffectInstance SizeUpAppear = SoundFactory.Instance.CreateSoundEffect("smb_powerup_appears");
        private static readonly SoundEffectInstance OneUpCollect = SoundFactory.Instance.CreateSoundEffect("smb_1");

        private static void PlayMusic(Songs song, bool reset = false)
        {
            if (CurrentBGM != song || reset)
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
                    default:
                        MediaPlayer.Stop();
                        break;
                }
                MediaPlayer.IsRepeating = true;
                CurrentBGM = song;
            }
        }

        public static void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
            SoundEffect.MasterVolume = MediaPlayer.IsMuted ? 0 : SoundEffectVolume;
            SoundEffectVolume = MediaPlayer.IsMuted ? SoundEffectVolume : SoundEffect.MasterVolume;
        }

        private static void Pause()
        {
            MediaPlayer.Pause();
        }

        private static void Resume()
        {
            MediaPlayer.Resume();
        }

        private static void UpdatePausing()
        {
            //Pausing state detector
            if (Model.IsPaused)
            {
                //Avoid repeat pausing
                if (MediaPlayer.State != MediaState.Paused)
                {
                    Pause();
                }
            }
            else if (MediaPlayer.State == MediaState.Paused)
            {
                Resume();
            }
        }

        //TODO: Finish Other Sound Effeect
        private static void UpdateMarioSoundEffect()
        {
            //TODO: Finish Mario Sound Effeect
            MarioCharacter mario = Model.ActivePlayer.Character as MarioCharacter;
            if (mario?.MovementState is MarioObjects.MovementStates.Jumping jumping && !jumping.Finished)
            {
                if (mario.PowerUpState is Super || mario.PowerUpState is Fire)
                {
                    PowerBounce.Play();
                }
                else
                {
                    Bounce.Play();
                }
            }
            if (mario?.ProtectionState is Dead)
            {
                Death.Play();
            }
        }

        private static void UpdateBGM()
        {
            //BGM Updater
            switch (Model.ActivePlayer.Character)
            {
                case MarioCharacter mario when mario.ProtectionState is Dead:
                    PlayMusic(Songs.Normal);
                    break;
                case MarioCharacter mario when mario.ProtectionState is Starred:
                    PlayMusic(Songs.Star);
                    break;
                case MarioCharacter mario when mario.Player.TimeRemain <= 90000:
                    PlayMusic(Songs.Hurry);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == "Normal":
                    PlayMusic(Songs.Normal);
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == "Sub":
                    PlayMusic(Songs.BelowGround);
                    break;
                default:
                    PlayMusic(Songs.Normal);
                    break;
            }
        }
        public static void Update()
        {
            UpdatePausing();
            UpdateMarioSoundEffect();
            UpdateBGM();
        }
    }
}
