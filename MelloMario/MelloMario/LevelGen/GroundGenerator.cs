namespace MelloMario.LevelGen
{
    #region

    using MelloMario.Objects.Blocks;
    using Microsoft.Xna.Framework;

    #endregion

    // note: this class is created to help the architecture design of level generation
    //       and to verify the correctness of interfaces
    //       DO NOT implement actual functionality in it until the design is clear
    internal class GroundGenerator : ILevelGenerator
    {
        private readonly IListener<IGameObject> listener;

        public GroundGenerator(IListener<IGameObject> listener)
        {
            this.listener = listener;
        }

        public void Request(IWorld world, Rectangle range)
        {
            // note: top / buttom are locked

            while (world.Boundary.Right < range.Right)
            {
                new Floor(world, new Point(world.Boundary.Right, 13 * 32), listener);
                new Floor(world, new Point(world.Boundary.Right, 14 * 32), listener);
                world.Extend(0, 32, 0, 0);
            }
            while (world.Boundary.Left > range.Left)
            {
                new Floor(world, new Point(world.Boundary.Left - 32, 13 * 32), listener);
                new Floor(world, new Point(world.Boundary.Left - 32, 14 * 32), listener);
                world.Extend(32, 0, 0, 0);
            }
        }
    }
}
