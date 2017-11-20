namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Security.Cryptography;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Enemies;
    using Microsoft.Xna.Framework;

    #endregion

    internal class InfiniteGenerator
    {
        private readonly ICamera camera;
        private readonly IListener listener;
        private readonly IGameWorld world;
        private float rightMostReachedX;
        private int elapsed;
        private double difficultyIndex;

        private static readonly RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();

        internal InfiniteGenerator(IGameWorld world, IListener listener, ICamera camera)
        {
            this.world = world;
            this.camera = camera;
            this.listener = listener;
            rightMostReachedX = world.Boundary.Width;
        }

        internal void Update(int time)
        {
            float temp = camera.Viewport.Location.X + 800 + 32;
            if ((int) temp / 32 >= (int) rightMostReachedX / 32)
            {
                difficultyIndex += double.Epsilon;
                new Floor(world, new Point(world.Boundary.Right, 13 * 32), listener);
                new Floor(world, new Point(world.Boundary.Right, 14 * 32), listener);
                world.Extend(1, 0);
                rightMostReachedX += 32;

                elapsed += time;
                int eoffset = (int) (difficultyIndex / double.Epsilon) / 5;
                if (elapsed > 400 - eoffset)
                {
                    byte[] a = new byte[4];
                    RngCrypto.GetBytes(a);
                    int p = BitConverter.ToInt32(a, 0);

                    RngCrypto.GetBytes(a);
                    int q = BitConverter.ToInt32(a, 0);
                    elapsed = 0;
                    new Goomba(
                        world,
                        new Point(world.Boundary.Right - (40 + q % 4) * (q % 3) - 300 + q % 256, 0 * 32 + p % 128),
                        listener);
                    if (difficultyIndex / double.Epsilon > 20)
                    {
                        new Koopa(
                            world,
                            new Point(world.Boundary.Right + (40 + q % 4) * (q % 5), 32 + p % 324),
                            listener,
                            p % 2 == 0 ? "Green" : "Red");
                    }
                    if (difficultyIndex / double.Epsilon > 200)
                    {
                        new Piranha(
                            world,
                            new Point(world.Boundary.Right + p % 3 * 32, 32 * 13),
                            listener,
                            new Point(32, 48),
                            250 - p % 249,
                            1250 - q % 1249,
                            1f,
                            p % 4 == 0 ? "Green" : (p % 3 == 0 ? "Red" : (p % 2 == 0 ? "Cyan" : "Gray")));
                    }
                }
            }
        }
    }
}
