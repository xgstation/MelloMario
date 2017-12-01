namespace MelloMario.LevelGen.Generators.Objects
{
    #region

    using System;
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Objects.Blocks;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    #endregion

    internal class None : IGenerator
    {
        public void Request(IWorld world, Rectangle range)
        {
        }
    }
}
