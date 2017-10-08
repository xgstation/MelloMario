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
        private const int SCALE = 32;
        private Point size;
        // Note: Without implementing IDisposable, it may cause resource leak
        //       The code analysis tool generates a warning here
        private StreamReader input;
        private Mario mario;

        public LevelReader(String path)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            size = new Point(Int32.Parse(widthAsString), Int32.Parse(heightAsString));
        }

        public IGameObjectManager LoadObjects()
        {
            IGameObjectManager objectManager = new GameObjectManager(SCALE, size);

            for (int i = 0; i < size.Y; ++i)
            {
                string curLine = input.ReadLine();
                for (int j = 0; j < size.X; ++j)
                {
                    char curChar = curLine.ElementAt(j);
                    switch (curChar)
                    {
                        case '!':
                            mario = new Mario(new Point(j * SCALE, i * SCALE));
                            objectManager.AddObject(mario);
                            break;

                        //blocks
                        case 'F':
                            objectManager.AddObject(new Floor(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'B':
                            objectManager.AddObject(new Brick(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'S':
                            objectManager.AddObject(new Stair(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'Q':
                            objectManager.AddObject(new Question(new Point(j * SCALE, i * SCALE)));
                            break;

                        //harm
                        case 'G':
                            objectManager.AddObject(new Goomba(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'K':
                            objectManager.AddObject(new Koopa(new Point(j * SCALE, i * SCALE), Koopa.ShellColor.green));
                            break;
                        case 'D':
                            objectManager.AddObject(new Koopa(new Point(j * SCALE, i * SCALE), Koopa.ShellColor.red));
                            break;

                        //entities
                        case 'C':
                            objectManager.AddObject(new Coin(new Point(j * SCALE, i * SCALE)));
                            break;
                        case '1':
                            objectManager.AddObject(new OneUpMushroom(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'R':
                            objectManager.AddObject(new FireFlower(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'M':
                            objectManager.AddObject(new SuperMushroom(new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'T':
                            objectManager.AddObject(new Star(new Point(j * SCALE, i * SCALE)));
                            break;
                        default:
                            //do nothing, blank space dont add anything to blocks
                            break;
                    }
                }
            }

            return objectManager;
        }

        public Mario BindMario()
        {
            //must be called after load objects
            return mario;
        }
    }
}
