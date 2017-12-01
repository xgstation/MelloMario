using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelloMario.LevelGen.Generators.PerlinGenerator;
namespace MelloMario.LevelGen.Generators
{
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    internal class Perlin : BaseGenerator
    {
        private PerlinNoiseGenerator perlinNoiseGenerator;
        private readonly NoiseConverter noiseConverter;
        private int newSeedCounter;
        private readonly Terrian terrian;

        public Perlin(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener) : base(scoreListener, soundListener)
        {
            perlinNoiseGenerator = new PerlinNoiseGenerator(5120);
            noiseConverter = new NoiseConverter();
            noiseConverter.SetSize(40, 16);
            terrian = new Terrian(scoreListener, soundListener, noiseConverter);
            noiseConverter.NewData();
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            //if (world.Boundary.Width % 2560 > newSeedCounter)
            //{
            //    newSeedCounter++;
            //    noiseConverter.NewSeed();
            //}

            while (world.Boundary.Left > range.Left - Const.GRID)
            {
                Rectangle subRange = new Rectangle(
                    32 * Const.GRID,
                    world.Boundary.Top,
                    world.Boundary.Left - 32 * Const.GRID,
                    world.Boundary.Height);
                terrian.Request(world, subRange);
                world.Extend(subRange.Width, 0, 0, 0);
            }
            while (world.Boundary.Right < range.Right + Const.GRID)
            {
                Rectangle subRange = new Rectangle(
                    world.Boundary.Right,
                    world.Boundary.Top,
                    32 * Const.GRID - world.Boundary.Right,
                    world.Boundary.Height);
                terrian.Request(world, subRange);
                world.Extend(0, subRange.Width, 0, 0);
            }
        }
    }
}
