namespace MelloMario
{
    internal interface ISoundFactory<T>
    {
        void BindLoader(T loader);

        ISoundTrack CreateSoundTrack(string name);

        ISoundEffect CreateSoundEffect(string name);
    }
}
