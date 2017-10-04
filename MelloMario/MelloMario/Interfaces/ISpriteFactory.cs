using Microsoft.Xna.Framework.Content;

namespace MelloMario
{
    interface ISpriteFactory
    {
        ISprite CreateMarioSprite(string status, bool Static);
        ISprite CreateGoombaSprite(string status);

        ISprite CreateStairSprite(string v);
        ISprite CreateQuestionSprite(string v);
        ISprite CreateFloorSprite(string v);
        ISprite CreateBrickSprite(string v);
        ISprite CreatePipelineSprite();

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
