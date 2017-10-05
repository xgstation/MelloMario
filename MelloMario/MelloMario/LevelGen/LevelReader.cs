using MelloMario.BlockObjects;
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
        P:Pipe(down and to the right)
         
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
        public class Pack
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Pack(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        private const int SCALE = 32;
        private int h;
        private int w;
        private StreamReader input;
        private IList<IGameObject> dynamicObjects;

        public LevelReader(String path)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            h = Int32.Parse(heightAsString);
            w = Int32.Parse(widthAsString);

        }

        public Pack Initilize()
        {
            return new Pack(h, w);
        }

        public IGameObject[,] LoadStatic()
        {
            IGameObject[,] staticObjects = new IGameObject[w, h];
            dynamicObjects = new List<IGameObject>();

            for (int i = 0; i < w; ++i)
            {
                string curLine = input.ReadLine();
                for (int j = 0; j < h; ++j)
                {
                    char curChar = curLine.ElementAt(j);
                    switch (curChar)
                    {
                        case 'F':
                            staticObjects[i, j] = new Floor(new Point(i * SCALE, j * SCALE));
                            break;
                        case 'B':
                            staticObjects[i, j] = new Brick(new Point(i * SCALE, j * SCALE));
                            break;
                        case 'S':
                            staticObjects[i, j] = new Stair(new Point(i * SCALE, j * SCALE));
                            break;
                        case 'Q':
                            staticObjects[i, j] = new Question(new Point(i * SCALE, j * SCALE));
                            break;
                        default:
                            //do nothing, blank space dont add anything to blocks
                            break;
                    }
                }
            }

            return staticObjects;
        }

        public IList<IGameObject> LoadDynamic()
        {
            //must be called after load static
            return dynamicObjects;
        }

    }
}
