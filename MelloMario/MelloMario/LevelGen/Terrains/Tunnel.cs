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
            // note: top / buttom are locked

            world.Add(new Floor(world, new Point(range.Left, 0 * Const.GRID), listener));
            world.Add(new Floor(world, new Point(range.Left, 1 * Const.GRID), listener));

            world.Add(new Floor(world, new Point(range.Left, range.Bottom - 3 * Const.GRID), listener));
            world.Add(new Floor(world, new Point(range.Left, range.Bottom - 2 * Const.GRID), listener));
            world.Add(new Floor(world, new Point(range.Left, range.Bottom - 1 * Const.GRID), listener));
        }
    }
}
