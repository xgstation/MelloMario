namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Island : Plain
    {
        public Island(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            int m1 = PerlinNoiseGenerator.Random(3333, range.Left / Const.GRID) % 4 + 2;
            int m2 = PerlinNoiseGenerator.Random(3333, range.Left / Const.GRID) % 4 + 2;

            base.OnRequest(world, new Rectangle(range.X + m1 * Const.GRID, range.Y, range.Width - (m1 + m2) * Const.GRID, range.Height));
        }
    }
}
