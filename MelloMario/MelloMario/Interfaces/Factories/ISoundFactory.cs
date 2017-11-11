using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MelloMario
{
    interface ISoundFactory
    {
        // note: probably need another abstraction layer?
        //       like Texture2D -> ISprite, it is good to do Song/SoundEffect -> ISound
        //       but do not add it until we know the use of sound in the whole game very well
        Song CreateSong(string name);
        SoundEffect CreateSoundEffect(string name);
        void BindContentManager(ContentManager content);
    }
}
