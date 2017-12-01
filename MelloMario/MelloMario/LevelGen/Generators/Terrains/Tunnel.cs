namespace MelloMario.LevelGen.Generators.Terrains
{
    #region

    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Tunnel : BaseGenerator
    {
        public Tunnel(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            for (int j = range.Left; j < range.Right; j += Const.GRID)
            {
                int mat = PerlinNoiseGenerator.RandomProp(1001, j / Const.GRID, 10) % 5;

                AddObject("Floor", world, new Point(j, range.Top));
                if (j != range.Left && j != range.Right - Const.GRID)
                {
                    AddObject("Floor", world, new Point(j, range.Top + Const.GRID));
                }

                for (int i = 1; i < 3; ++i)
                {
                    AddObject("Floor", world, new Point(j, range.Bottom - i * Const.GRID));
                }
            }
        }
    }
}
