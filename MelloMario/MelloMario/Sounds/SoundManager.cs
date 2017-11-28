namespace MelloMario.Sounds
{
    #region

    using MelloMario.Sounds.Effects;
    using MelloMario.Sounds.Tracks;

    #endregion

    internal class SoundManager
    {
        public readonly IListener<ISoundable> SoundEffectListener;
        private readonly SoundTrackManager sounds;

        public SoundManager()
        {
            SoundEffectListener = new SoundEffectListener();
            //TODO: Fix it
            //sounds = new SoundTrackManager();
        }

        public void ToggleMute()
        {
            sounds.ToggleMute();
            (SoundEffectListener as SoundEffectListener).ToggleMute();
        }

        public void Update(int time)
        {
        }

        public void Initialize()
        {
        }
    }
}
