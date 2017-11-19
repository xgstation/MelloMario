using MelloMario.BlockObjects;
using Microsoft.Xna.Framework;

namespace MelloMario.LevelGen
{
    internal class InfiniteGenerator
    {
        private readonly ICamera camera;
        private readonly IListener listener;
        private float rightMostReachedX;
        private readonly IGameWorld world;

        internal InfiniteGenerator(IGameWorld world, IListener listener, ICamera camera)
        {
            this.world = world;
            this.camera = camera;
            this.listener = listener;
            rightMostReachedX = world.Boundary.Width;
        }

        internal void Update()
        {
            float temp = camera.Location.X + 800 + 32;
            if ((int) temp / 32 >= (int) rightMostReachedX / 32)
            {
                new Floor(world, new Point(world.Boundary.Right, 13 * 32), listener);
                new Floor(world, new Point(world.Boundary.Right, 14 * 32), listener);
                world.Extend(1, 0);
                rightMostReachedX += 32;
            }
        }
    }
}