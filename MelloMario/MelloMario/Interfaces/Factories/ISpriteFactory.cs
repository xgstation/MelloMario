using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface ISpriteFactory
    {
        void BindContentManager(ContentManager newContentManager);
        void BindSpriteBatch(SpriteBatch newSpriteBatch);

        ISprite CreateMarioSprite(string powerUpStatus, string movementStatus, string protectionStatus, string facing);

        ISprite CreateGoombaSprite(string status);
        ISprite CreateFlagSprite(bool status);
        ISprite CreateKoopaSprite(string color, string status);
        ISprite CreatePiranhaSprite(string color);
        ISprite CreateFireSprite();
        ISprite CreateStarSprite();
        ISprite CreateCoinSprite(bool isHud = false);
        ISprite CreateSuperMushroomSprite();
        ISprite CreateFireFlowerSprite();
        ISprite CreateOneUpMushroomSprite();
        ISprite CreateTextSprite(string text);
        ISprite CreateQuestionSprite(string status);
        ISprite CreateBrickSprite(string status);
        ISprite CreateFloorSprite();
        ISprite CreateStairSprite();
        ISprite CreatePipelineSprite(string type);
        ISprite CreateSceneSprite(string type, ZIndex zIndex);
    }
}
