namespace MelloMario.LevelGen.Terrains
{
    #region

    using System;
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
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                world.Add(
                    new Floor(
                        world,
                        new Point(j, range.Bottom - (3 + Math.Abs(j / 96 * 123456789 / 65535) % 5) * Const.GRID),
                        listener)); // TODO: random
            }
        }
    }
}
