using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.Factories
{
    internal class SoundFactory : ISoundFactory
    {
        private ContentManager content;

        private readonly IDictionary<string, Song> songs;
        private readonly IDictionary<string, SoundEffectInstance> soundEffects;

        private SoundFactory()
        {
            songs = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffectInstance>();
        }

        public static ISoundFactory Instance { get; } = new SoundFactory();

        public void BindContentManager(ContentManager newContentManager)
        {
            content = newContentManager;
        }

        public Song CreateSong(string name)
        {
            if (!songs.ContainsKey(name))
                songs.Add(name, content.Load<Song>("Music/" + name));

            return songs[name];
        }

        public SoundEffectInstance CreateSoundEffect(string name)
        {
            if (!soundEffects.ContainsKey(name))
                soundEffects.Add(name, content.Load<SoundEffect>("SFX/" + name).CreateInstance());

            return soundEffects[name];
        }
    }
}