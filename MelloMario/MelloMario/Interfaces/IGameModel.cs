﻿using Microsoft.Xna.Framework;

namespace MelloMario
{
    interface IGameModel
    {
        void ToggleFullScreen();
        void Pause();
        void Reset();
        void Quit();
        void Update(GameTime time);
        void Draw(GameTime time, ZIndex zIndex);
    }
}
