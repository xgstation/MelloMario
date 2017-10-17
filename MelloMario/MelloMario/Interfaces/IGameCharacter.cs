﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IGameCharacter : IGameObject
    {
        void Left();
        void LeftRelease();
        void Right();
        void RightRelease();
        void Jump();
        void Crouch();
        void Action();
    }
}
