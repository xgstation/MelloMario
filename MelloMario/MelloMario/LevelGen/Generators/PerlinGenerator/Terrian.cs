using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.LevelGen.Generators.PerlinGenerator
{
    using MelloMario.LevelGen.NoiseGenerators;
    using MelloMario.Theming;
    using Microsoft.Xna.Framework;

    internal class Terrian : BaseGenerator
    {
        private readonly NoiseConverter noiseConverter;
        private readonly IDictionary<int, int> dict;
        public Terrian(IListener<IGameObject> scoreListener, IListener<ISoundable> soundListener, NoiseConverter noiseConverter) : base(scoreListener, soundListener)
        {
            this.noiseConverter = noiseConverter;
            dict = noiseConverter.GetData();
        }

        protected override void OnRequest(IWorld world, Rectangle range)
        {
            int max = dict.Values.Max();
            int min = dict.Values.Min();
            int mean = (max + min) / 2;
            for (int x = range.Left; x < range.Right; x += Const.GRID)
            {
                for (int y = dict[x % (dict.Count * 32)]+ 80; y < range.Bottom; y+= Const.GRID)
                {
                    if (y < mean)
                    {
                        AddObject("Stair", world, new Point(x, y));
                    }
                    else
                    {
                        AddObject("Floor", world, new Point(x, y));
                    }
                }
            }
        }
    }
}
