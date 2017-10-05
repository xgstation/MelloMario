using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.LevelGen
{

    /*
     documentation/key for level gen

        first line height of level
        second line width of level

        Blocks:
        F:Floor
        B:Brick
        S:Stair
        Q:Question
        P:Pipe(needs 2xY or Yx2 format)
         
        Entities:
        C:Coin
        1:One Up
        R:Fire Flower
        M:Mushroom
        T:Star

        Harm:
        G:Goomba
        K:Green Koopa
        D:Red Kappa

        _:Blank
         
         */

    class LevelReader
    {
        class Pack
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Pack(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private int h;
        private int w;
        private StreamReader input;

        public LevelReader(String path)
        {
            Stream inStream = new FileStream(path,FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            h = Int32.Parse(heightAsString);
            w = Int32.Parse(widthAsString);

        }

        public Pack Initilize()
        {
            return new Pack(h,w);
        }

    }
}
