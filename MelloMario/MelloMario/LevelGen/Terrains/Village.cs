namespace MelloMario.LevelGen.Terrains
{
    #region

    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Village : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Village(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            world.Add(new Floor(world, new Point(range.Left, range.Bottom - 3 * Const.GRID), listener));
            world.Add(new Floor(world, new Point(range.Left, range.Bottom - 2 * Const.GRID), listener));
            world.Add(new Floor(world, new Point(range.Left, range.Bottom - 1 * Const.GRID), listener));
        }
    }
}
