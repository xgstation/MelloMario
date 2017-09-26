using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    interface ISpriteFactory
    {
        ISprite CreateSprite(string textureName, bool Static);
        ISprite CreateGoombaSprite();
        ISprite CreateDefeatedGreenKoopaSprite();
        ISprite CreateJumpOnGreenKoopaSprite();
        ISprite CreateStarSprite();
        ISprite CreatCoinSprite();
        ISprite CreatSuperMushroomSprite();
        ISprite CreateFlowerSprite();
        ISprite CreatoneUpMushroomSprite();
        ISprite CreateRedKoopaSprite();
        ISprite CreateJumpOnRedKoopaSprite();
        ISprite CreateDefeatedRedKoopaSprite();
        ISprite CreateGreenKoopaSprite();
    }
}
