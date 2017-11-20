using Microsoft.Xna.Framework.Audio;

namespace MelloMario.Sounds
{
    internal class SoundEffect : ISoundEffect
    {
        public SoundEffect(SoundEffectInstance instance)
        {
            instance.Play();
        }
    }
}
