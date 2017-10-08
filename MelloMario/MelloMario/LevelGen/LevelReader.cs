using MelloMario.BlockObjects;
using MelloMario.EnemyObjects;
using MelloMario.ItemObjects;
using MelloMario.MarioObjects;
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

        !:Mario

        Blocks:
        F:Floor
        B:Brick
        S:Stair
        Q:Question
        P:Pipe(down and to the right)
         
        Entities:
        C:Coin
        1:One Up Mushroom
        R:Fire Flower
        M:Super Mushroom
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
        private List<IGameObject>[,] allObjects;
        private Mario mario;

        public LevelReader(String path)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            h = Int32.Parse(heightAsString);
            w = Int32.Parse(widthAsString);

            allObjects = new List<IGameObject>[h,w];

        }

        public Pack Initilize()
        {
            return new Pack(h, w);
        }

        public List<IGameObject>[,] LoadObjects()
        {
            for (int i = 0; i < h; ++i)
            {
                for (int j = 0; j < w; ++j)
                {
                    allObjects[i,j] = new List<IGameObject>();
                }
            }

            for (int i = 0; i < h; ++i)
            {
                string curLine = input.ReadLine();
                for (int j = 0; j < w; ++j)
                {
                    char curChar = curLine.ElementAt(j);
                    switch (curChar)
                    {
                        case '!':
                            mario = new Mario(new Point(j * SCALE, i * SCALE));
                            allObjects[i, j].Add(mario);
                            break;

                        //blocks
                        case 'F':
                            allObjects[i, j].Add(new Floor(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'B':
                            allObjects[i, j].Add(new Brick(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'S':
                            allObjects[i, j].Add(new Stair(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'Q':
                            allObjects[i, j].Add(new Question(new Point(j * SCALE, i * SCALE)));
                            break;

                        //harm
                        case 'G':
                            allObjects[i, j].Add(new Goomba(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'K':
                            allObjects[i, j].Add(new Koopa(new Point(j * SCALE, i * SCALE), Koopa.ShellColor.green));
                            break;
                        case 'D':
                            allObjects[i, j].Add(new Koopa(new Point(j * SCALE, i * SCALE), Koopa.ShellColor.red));
                            break;

                        //entities
                        case 'C':
                            allObjects[i, j].Add(new Coin(new Point(j * SCALE, i * SCALE)));
                            break;
                        case '1':
                            allObjects[i, j].Add(new OneUpMushroom(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'R':
                            allObjects[i, j].Add(new FireFlower(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'M':
                            allObjects[i, j].Add(new SuperMushroom(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'T':
                            allObjects[i, j].Add(new Star(new Point(j * SCALE, i * SCALE)));
                            break;
                        default:
                            //do nothing, blank space dont add anything to blocks
                            break;
                    }
                }
            }

            return allObjects;
        }

        public Mario BindMario()
        {
            //must be called after load objects
            return mario;
        }

    }
}
