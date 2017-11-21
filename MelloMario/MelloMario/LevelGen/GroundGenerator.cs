namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Security.Cryptography;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Enemies;
    using Microsoft.Xna.Framework;

    #endregion

    // note: this class is created to help the architecture design of level generation
    //       and to verify the correctness of interfaces
    //       DO NOT implement actual functionality in it until the design is clear
    internal class GroundGenerator : ILevelGenerator
    {
        private readonly ISession session;
        private readonly IListener<IGameObject> listener;

        private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();

        internal GroundGenerator(ISession session, IListener<IGameObject> listener)
        {
            this.session = session;
            this.listener = listener;
        }

        public void Request(Rectangle range, IWorld world)
        {
            //float temp = camera.Viewport.Location.X + 800 + 32;
            //if ((int) temp / 32 >= (int) rightMostReachedX / 32)
            //{
            //    difficultyIndex += double.Epsilon;
            //    new Floor(world, new Point(world.Boundary.Right, 13 * 32), listener);
            //    new Floor(world, new Point(world.Boundary.Right, 14 * 32), listener);
            //    world.Extend(0, 32, 0, 0);
            //    rightMostReachedX += 32;
            //}
        }
    }
}
