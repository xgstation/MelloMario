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
        b:Hidden Brick
        S:Stair
        Q:Question
        q:Hidden Question
        P:Pipe(to the right one block)
        V:Vertical Pipe(to the right one block)
         
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

        public LevelReader(String path)
        {
            Stream inStream = new FileStream(path, FileMode.Open);
            input = new StreamReader(inStream);

            String heightAsString = input.ReadLine();
            String widthAsString = input.ReadLine();

            size = new Point(Int32.Parse(widthAsString), Int32.Parse(heightAsString));
        }

        public Tuple<IGameWorld, IGameCharacter> LoadObjects()
        {
            IGameWorld world = new GameWorld(SCALE, size);
            IGameCharacter character = null;

            for (int i = 0; i < size.Y; ++i)
            {
                string curLine = input.ReadLine();
                for (int j = 0; j < size.X; ++j)
                {
                    char curChar = curLine.ElementAt(j);
                    switch (curChar)
                    {
                        case '!':
                            character = new Mario(world, new Point(j * SCALE, i * SCALE));
                            world.AddObject(character);
                            break;

                        //blocks
                        case 'F':
                            world.AddObject(new Floor(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'B':
                            world.AddObject(new Brick(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'b':
                            world.AddObject(new Brick(world, new Point(j * SCALE, i * SCALE), true));
                            break;
                        case 'S':
                            world.AddObject(new Stair(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'Q':
                            world.AddObject(new Question(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'q':
                            world.AddObject(new Question(world, new Point(j * SCALE, i * SCALE), true));
                            break;
                        case 'P':
                            world.AddObject(new Pipeline(world, new Point(j * SCALE, i * SCALE), "LeftIn"));
                            world.AddObject(new Pipeline(world, new Point((j+1) * SCALE, i * SCALE), "RightIn"));
                            break;
                        case 'V':
                            world.AddObject(new Pipeline(world, new Point(j * SCALE, i * SCALE), "Left"));
                            world.AddObject(new Pipeline(world, new Point((j + 1) * SCALE, i * SCALE), "Right"));
                            break;

                        //harm
                        case 'G':
                            world.AddObject(new Goomba(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'K':
                            world.AddObject(new Koopa(world, new Point(j * SCALE, i * SCALE), Koopa.ShellColor.green));
                            break;
                        case 'D':
                            world.AddObject(new Koopa(world, new Point(j * SCALE, i * SCALE), Koopa.ShellColor.red));
                            break;

                        //entities
                        case 'C':
                            world.AddObject(new Coin(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case '1':
                            world.AddObject(new OneUpMushroom(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'R':
                            world.AddObject(new FireFlower(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'M':
                            world.AddObject(new SuperMushroom(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        case 'T':
                            world.AddObject(new Star(world, new Point(j * SCALE, i * SCALE)));
                            break;
                        default:
                            //do nothing, blank space dont add anything to blocks
                            break;
                    }
                }
            }

            return new Tuple<IGameWorld, IGameCharacter>(world, character);
        }
    }
}
