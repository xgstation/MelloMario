using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface ISpriteFactory
    {
        void LoadAllTextures(ContentManager content);
        void BindSpriteBatch(SpriteBatch spriteBatch);

        ISprite CreateMarioSprite(string status, bool isStatic, bool isStar);
        ISprite CreateMarioSprite(string status, bool isStatic);

        ISprite CreateGoombaSprite(string status);
        ISprite CreateGreenKoopaSprite(string status);
        ISprite CreateRedKoopaSprite(string status);

        ISprite CreateStarSprite();
        ISprite CreateCoinSprite();
        ISprite CreateSuperMushroomSprite();
        ISprite CreateFireFlowerSprite();
        ISprite CreateOneUpMushroomSprite();

        ISprite CreateQuestionSprite(string status);
        ISprite CreateBrickSprite(string status);
        ISprite CreateFloorSprite();
        ISprite CreateStairSprite();
        ISprite CreatePipelineSprite(string type);
    }
}
