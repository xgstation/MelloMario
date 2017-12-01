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
            sounds = new SoundTrackManager();
        }

        public void ToggleMute()
        {
            SoundTrackManager.ToggleMute();
            (SoundEffectListener as SoundEffectListener).ToggleMute();
        }

        public void Update()
        {
            sounds.Update();
        }

        public void BindPlayer(IPlayer player)
        {
            sounds.BindPlayer(player);
        }

        public void BindModel(IModel model)
        {
            sounds.BindModel(model);
        }
    }
}
