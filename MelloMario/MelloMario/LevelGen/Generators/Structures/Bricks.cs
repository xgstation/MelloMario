namespace MelloMario.LevelGen.Generators.Structures
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Bricks : BaseGenerator
    {
        public Bricks(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int i = range.Bottom - 3 * Const.GRID;
                AddObject("Brick", world, new Point(j, i));
            }
        }
    }
}
