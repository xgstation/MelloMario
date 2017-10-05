using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario
{
    interface IMarioState
    {
        void Down();
        void Up();
        void Right();
        void Left();
        void Die();
        void ChangeToFireState();
        void ChangeToSuperState();
        void ChangeToStandardState();
        void Update(GameTime game);
        void Draw(SpriteBatch spriteBatch, Vector2 location);
    }
}
