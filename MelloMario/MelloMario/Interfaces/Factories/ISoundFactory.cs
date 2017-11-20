using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    internal interface ISoundFactory
    {
        void BindContentManager(ContentManager content);

        ISoundTrack CreateSoundTrack(string name);

        ISoundEffect CreateSoundEffect(string name);
    }
}
