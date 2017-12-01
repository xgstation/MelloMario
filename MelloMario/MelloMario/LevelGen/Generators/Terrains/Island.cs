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
            base.OnRequest(world, new Rectangle(range.X + 2 * Const.GRID, range.Y, range.Width - 4 * Const.GRID, range.Height));
        }
    }
}
