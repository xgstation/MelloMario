namespace MelloMario.LevelGen.Generators.Objects
{
    #region

    using System;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class Enemies : BaseGenerator
    {
        public Enemies(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            int val = PerlinNoiseGenerator.Random(3902, (range.Left * 1100 + range.Top * 2367) / Const.GRID) % 100;

            if (val < 5)
            {
                AddObject("Goomba", world, range.Location);
            }
            else if (val < 7)
            {
                AddObject("GreenKoopa", world, range.Location);
            }
            else if (val < 9)
            {
                AddObject("RedKoopa", world, range.Location);
            }
            else if (val < 11)
            {
                AddObject("Beetle", world, range.Location);
            }
            else if (val < 12)
            {
                AddObject("Thwomp", world, range.Location);
            }
        }
    }
}
