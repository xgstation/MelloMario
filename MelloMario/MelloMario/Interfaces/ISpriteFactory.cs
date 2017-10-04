using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    interface ISpriteFactory
    {
        ISprite CreateMarioSprite(string status, bool Static);
        ISprite CreateGoombaSprite(string status);

        ISprite CreateStair(string v);
        ISprite CreateQuestion(string v);
        ISprite CreateFloor(string v);
        ISprite CreateBrick(string v);
        ISprite CreatePipeline();

        ISprite CreateRedKoopaSprite(string status);
        ISprite CreateGreenKoopaSprite(string status);
        ISprite CreateStarSprite();
        ISprite CreatCoinSprite();
        ISprite CreatSuperMushroomSprite();
        ISprite CreateFlowerSprite();
        ISprite CreateOneUpMushroomSprite();

        void LoadAllTextures(ContentManager content);
    }
}
