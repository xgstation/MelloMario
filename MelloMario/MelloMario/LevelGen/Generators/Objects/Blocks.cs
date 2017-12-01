namespace MelloMario.LevelGen.Generators.Objects
{
    #region

    using System;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Blocks : BaseGenerator
    {
        public Blocks(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            int val = PerlinNoiseGenerator.Random(3902, (range.Left * 1100 + range.Top * 2367) / Const.GRID) % 40;

            if (val < 1)
            {
                AddObject("Floor", world, range.Location);
            }
            else if (val < 2)
            {
                AddObject("Brick", world, range.Location);
            }
            else if (val < 3)
            {
                AddObject("Stair", world, range.Location);
            }
            else if (val < 4)
            {
                AddObject("Question", world, range.Location);
            }
        }
    }
}
