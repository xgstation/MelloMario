using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario.BaseClasses
{
    //NOT FINISHED YET
    class CompressedBlockObject : BaseGameObject
    {
        private class Interval : IComparable
        {
            public int min;
            public int max;
            public Interval(int min, int max)
            {
                this.min = min;
                this.max = max;
            }
            public Interval() : this(0, 0) { }
            public int CompareTo(object obj)
            {
                if (obj is int)
                {
                    int number = (int)obj;
                    if (number < min)
                    {
                        return 1;
                    }
                    else if (number > max)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else if (obj is Interval)
                {
                    var interval = obj as Interval;
                    if (interval.max < min)
                    {
                        return 1;
                    }
                    else if (interval.min > max)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        private SortedSet<Tuple<Interval, BaseGameObject>> objects;

        public CompressedBlockObject(IGameWorld world, Point location, Point size, BaseGameObject[] gameObjects) : base(world, location, size)
        {
            var objects = new SortedSet<Tuple<Interval, BaseGameObject>>();
            foreach (BaseGameObject obj in gameObjects)
            {
                var interval = new Interval(obj.Boundary.X, obj.Boundary.Location.X + obj.Boundary.Width);
                objects.Add(new Tuple<Interval, BaseGameObject>(interval, obj));
            }
            world.AddObject(this);

        }

        protected override void OnCollision(IGameObject target, CollisionMode mode)
        {
            if (target is Mario)
            {
                var mario = target as Mario;
                var marioBoundary = new Interval(mario.Boundary.X, mario.Boundary.X + mario.Boundary.Width);
                switch (mode)
                {
                    case CollisionMode.LeftBottom:
                        if (target.Boundary.Center.X > Boundary.Location.X)
                        {
                            foreach (var obj in objects)
                            {
                                if (obj.Item1.CompareTo(marioBoundary) == 0)
                                {
                                    //TODO: Change BaseGameObject in order to Collide conditionally
                                    //obj.Item2.Collide();
                                }
                            }
                        }
                        break;
                    case CollisionMode.RightBottom:
                        if (target.Boundary.Center.X < Boundary.Location.X + Boundary.Width)
                        {
                            foreach (var obj in objects)
                            {
                                if (obj.Item1.CompareTo(marioBoundary) == 0)
                                {
                                    //TODO: Change BaseGameObject in order to Collide conditionally
                                    //obj.Item2.Collide();
                                }
                            }
                        }
                        break;
                    default:
                        //DO NOTHING
                        break;
                }
            }
        }
        protected override void OnOut(CollisionMode mode)
        {

        }

        protected override void OnSimulation(GameTime time)
        {

        }
        public override void Update(GameTime time)
        {
            foreach (var obj in objects)
            {
                obj.Item2.Update(time);
            }
        }
        public override void Draw(GameTime time)
        {
            foreach (var obj in objects)
            {
                obj.Item2.Draw(time);
            }
        }

    }
}
