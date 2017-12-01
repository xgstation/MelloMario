namespace MelloMario.LevelGen.Generators.Structures
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Coins : BaseGenerator
    {
        public Coins(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                for (int i = range.Top; i < range.Bottom; i += Const.GRID)
                {
                    AddObject("Coin", world, new Point(j, i));
                }
            }
        }
    }
}
