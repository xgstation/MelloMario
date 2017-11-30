namespace MelloMario
{
    internal interface ISpriteFactory<T>
    {
        void BindLoader(T loader);

        ISprite CreateTextSprite(string text, float fontSize = 18f);
        ISprite CreateSplashSprite();
        ISprite CreateMarioSprite(string powerUpStatus, string movementStatus, string protectionStatus, string facing);
        ISprite CreateGoombaSprite(string status);
        ISprite CreateKoopaSprite(string color, string status);
        ISprite CreateBeetleSprite(string status);
        ISprite CreatePiranhaSprite(string color);
        ISprite CreateFireSprite();
        ISprite CreateStarSprite();
        ISprite CreateCoinSprite(bool isHud = false);
        ISprite CreateSuperMushroomSprite();
        ISprite CreateFireFlowerSprite();
        ISprite CreateOneUpMushroomSprite();
        ISprite CreateQuestionSprite(string status);
        ISprite CreateBrickSprite(string status);
        ISprite CreateFloorSprite();
        ISprite CreateStairSprite();
        ISprite CreatePipelineSprite(string type);
        ISprite CreateFlagSprite(bool isTop);
        ISprite CreateSceneSprite(string type, ZIndex zIndex);
        ISprite CreateTitle(ZIndex zIndex);
        ISprite CreateThwompSprite(string name);
    }
}
