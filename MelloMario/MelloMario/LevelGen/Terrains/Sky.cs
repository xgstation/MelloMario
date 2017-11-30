namespace MelloMario.LevelGen.Terrains
{
    #region

    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Sky : IGenerator
    {
        private readonly IListener<IGameObject> listener;

        public Sky(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            world.Add(new Floor(world, new Point(range.Left, range.Bottom - (3 + range.Left / 96 * 123456789 / 65535 % 4) * Const.GRID), listener)); // TODO: random
            //world.Add(new Floor(world, new Point(range.Left, range.Bottom - 2 * Const.GRID), listener));
            //world.Add(new Floor(world, new Point(range.Left, range.Bottom - 1 * Const.GRID), listener));
        }
    }
}
