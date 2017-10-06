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
        private List<IGameObject> dynamicObjects;
        private Mario mario;

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
            IGameObject[,] staticObjects = new IGameObject[h, w];
            dynamicObjects = new List<IGameObject>();

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
                            break;

                        //blocks
                        case 'F':
                            staticObjects[i, j] = new Floor(new Point(j * SCALE, i * SCALE));
                            break;
                        case 'B':
                            staticObjects[i, j] = new Brick(new Point(j * SCALE, i * SCALE));
                            break;
                        case 'S':
                            staticObjects[i, j] = new Stair(new Point(j * SCALE, i * SCALE));
                            break;
                        case 'Q':
                            staticObjects[i, j] = new Question(new Point(j * SCALE, i * SCALE));
                            break;

                        //harm
                        case 'G':
                            dynamicObjects.Add(new Goomba(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'K':
                            dynamicObjects.Add(new Koopa(new Point(j * SCALE, i * SCALE), Koopa.ShellColor.green));
                            break;
                        case 'D':
                            dynamicObjects.Add(new Koopa(new Point(j * SCALE, i * SCALE), Koopa.ShellColor.red));
                            break;

                        //entities
                        case 'C':
                            staticObjects[i, j] = new Coin(new Point(j * SCALE, i * SCALE));
                            break;
                        case '1':
                            dynamicObjects.Add(new OneUpMushroom(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'R':
                            dynamicObjects.Add(new FireFlower(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'M':
                            dynamicObjects.Add(new SuperMushroom(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'T':
                            dynamicObjects.Add(new Star(new Point(j * SCALE, i * SCALE)));
                            break;
                        default:
                            //do nothing, blank space dont add anything to blocks
                            break;
                    }
                }
            }

            return staticObjects;
        }

        public List<IGameObject> LoadDynamic()
        {
            //must be called after load static
            return dynamicObjects;
        }

        public Mario LoadMario()
        {
            //must be called after load static
            return mario;
        }

    }
}
