﻿using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    interface ISpriteFactory
    {
        void LoadAllTextures(ContentManager content);

        ISprite CreateMarioSprite(string status, bool Static);

        ISprite CreateGoombaSprite(string status);
        ISprite CreateGreenKoopaSprite(string status);
        ISprite CreateRedKoopaSprite(string status);

        ISprite CreateStarSprite();
        ISprite CreateCoinSprite();
        ISprite CreateSuperMushroomSprite();
        ISprite CreateFireFlowerSprite();
        ISprite CreateOneUpMushroomSprite();

        ISprite CreateQuestionSprite(string v);
        ISprite CreateBrickSprite(string v);
        ISprite CreateFloorSprite();
        ISprite CreateStairSprite();
        ISprite CreatePipelineSprite();
    }
}
