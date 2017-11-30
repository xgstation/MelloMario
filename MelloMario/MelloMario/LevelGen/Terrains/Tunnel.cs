namespace MelloMario.LevelGen.Terrains
{
    #region

    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Tunnel : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Tunnel(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                world.Add(new Floor(world, new Point(j, 0 * Const.GRID), listener));
                world.Add(new Floor(world, new Point(j, 1 * Const.GRID), listener));

                world.Add(new Floor(world, new Point(j, range.Bottom - 3 * Const.GRID), listener));
                world.Add(new Floor(world, new Point(j, range.Bottom - 2 * Const.GRID), listener));
                world.Add(new Floor(world, new Point(j, range.Bottom - 1 * Const.GRID), listener));
            }
        }
    }
}
