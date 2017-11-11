using System.Collections.Generic;
using MelloMario.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MelloMario.Theming;
using MelloMario.Sprites.BlockSprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MelloMario.Factories
{
    class SoundFactory : ISoundFactory
    {
        private static ISoundFactory instance = new SoundFactory();

        private ContentManager content;

        private IDictionary<string, Song> songs;
        private IDictionary<string, SoundEffect> soundEffects;

        private SoundFactory()
        {
            songs = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
        }

        public static ISoundFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public void BindContentManager(ContentManager newContentManager)
        {
            content = newContentManager;
        }

        public Song CreateSong(string name)
        {
            if (!songs.ContainsKey(name))
            {
                songs.Add(name, content.Load<Song>("Music/" + name));
            }

            return songs[name];
        }

        public SoundEffect CreateSoundEffect(string name)
        {
            if (!soundEffects.ContainsKey(name))
            {
                soundEffects.Add(name, content.Load<SoundEffect>("SFX/" + name));
            }

            return soundEffects[name];
        }
    }
}
     