using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MelloMario.BlockObjects.QuestionStates
{
    internal class Castle : BaseGameObject
    {
        public Castle(IGameWorld world, Point location, Point size) : base(world, location, size) { }

        protected override void OnUpdate(int time) { }

        protected override void OnSimulation(int time) { }

        protected override void OnDraw(int time, SpriteBatch spriteBatch) { }
    }
}
