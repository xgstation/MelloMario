namespace MelloMario.Sounds.Tracks
{
    #region

    using MelloMario.Factories;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.ProtectionStates;
    using Microsoft.Xna.Framework.Media;

    #endregion

    internal class SoundTrackManager
    {
        // note: please do not cache soundtracks since the factory have already done that

        private ISoundTrack currentTrack;
        private IPlayer player;

        public void BindPlayer(IPlayer newPlayer)
        {
            player = newPlayer;
        }

        public static void ToggleMute()
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }

        public void Update()
        {
            UpdateBGM();
        }

        private void PlayMusic(ISoundTrack track)
        {
            if (track.Equals(currentTrack) && MediaPlayer.State != MediaState.Stopped)
            {
                return;
            }
            MediaPlayer.Stop();
            track?.Play();
            MediaPlayer.IsRepeating = true;
            currentTrack = track;
        }

        private void UpdateBGM()
        {
            //BGM Updater
            switch (player?.Character)
            {
                case MarioCharacter mario when mario.ProtectionState is Dead:
                    MediaPlayer.Stop();
                    break;
                case MarioCharacter mario when mario.ProtectionState is Starred:
                    PlayMusic(SoundFactory.Instance.CreateSoundTrack("Star"));
                    break;
                case MarioCharacter mario when mario.Player.TimeRemain <= 90000:
                    PlayMusic(SoundFactory.Instance.CreateSoundTrack("Hurry"));
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == WorldType.normal:
                    PlayMusic(SoundFactory.Instance.CreateSoundTrack("Normal"));
                    break;
                case MarioCharacter mario when mario.CurrentWorld.Type == WorldType.underground:
                    PlayMusic(SoundFactory.Instance.CreateSoundTrack("BelowGround"));
                    break;
                default:
                    PlayMusic(SoundFactory.Instance.CreateSoundTrack("Normal"));
                    break;
            }
        }
    }
}
