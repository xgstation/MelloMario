namespace MelloMario.LevelGen.Generators.Objects
{
    #region

    using System;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Backgrounds : BaseGenerator
    {
        public Backgrounds(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            int val = PerlinNoiseGenerator.Random(3902, (range.Left * 1100 + range.Top * 2367) / Const.GRID) % 20;

            if (val < 1)
            {
                AddObject("Bush", world, range.Location);
            }
            else if (val < 2)
            {
                AddObject("LeftTriangle", world, range.Location);
            }
            else if (val < 3)
            {
                AddObject("RightTriangle", world, range.Location);
            }
            else if (val < 4)
            {
                AddObject("Arc", world, range.Location);
            }
            else if (val < 5)
            {
                AddObject("Rectangle", world, range.Location);
            }
        }
    }
}
