using Microsoft.Xna.Framework.Media;

namespace MelloMario.Sounds
{
    internal class SoundTrack : ISoundTrack
    {
        private Song instance;

        public SoundTrack(Song instance)
        {
            this.instance = instance;
        }

        public void Play()
        {
            MediaPlayer.Play(instance);
        }
    }
}
