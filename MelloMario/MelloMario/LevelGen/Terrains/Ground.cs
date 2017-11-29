namespace MelloMario.LevelGen.Terrains
{
    #region

    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Ground : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Ground(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            // note: top / buttom are locked

            new Floor(world, new Point(range.Left, range.Bottom - 2 * Const.GRID), listener);
            new Floor(world, new Point(range.Left, range.Bottom - 1 * Const.GRID), listener);
        }
    }
}
