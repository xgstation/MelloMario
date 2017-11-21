using System;

namespace MelloMario
{
    internal delegate void SoundHandler(ISoundable s, EventArgs e);
    internal interface ISoundable
    {
        event SoundHandler SoundEvent;
    }
}
