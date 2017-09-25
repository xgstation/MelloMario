using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.EnemyObjects.GoombaStates;

namespace MelloMario.EnemyObjects
{
    public class Goomba: BaseEnemy
    {
        public IGoombaState GoombaState;

        public Goomba(Vector2 initLocation) : base(initLocation)
        {

        }

        public void transNormal()
        {
            GoombaState.transNormal();
        }
        public void transDefeated()
        {
            GoombaState.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            GoombaState.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GoombaState.Draw(spriteBatch, Location);
        }
    }
}
