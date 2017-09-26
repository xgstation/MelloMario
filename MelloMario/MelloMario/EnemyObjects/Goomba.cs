using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MelloMario.EnemyObjects.GoombaStates;

namespace MelloMario.EnemyObjects
{
    public class Goomba: BaseEnemy
    {
        public IGoombaState State;

        public Goomba(Vector2 initLocation) : base(initLocation)
        {

        }

        public void transNormal()
        {
            State.transNormal();
        }
        public void transDefeated()
        {
            State.transDefeated();
        }

        public override void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, Location);
        }
    }
}
