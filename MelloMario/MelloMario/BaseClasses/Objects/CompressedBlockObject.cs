using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MelloMario.MarioObjects;

namespace MelloMario
{
    //NOT FINISHED YET
    //Briefly Introduction:
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
                if (obj is int number)
                {
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
                else if (obj is Interval interval)
                {
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

        private ISet<Tuple<Interval, IGameObject>> objects;

        public CompressedBlockObject(IGameWorld world, Point location, Point size, IGameObject[] gameObjects) : base(world, location, size)
        {
            objects = new HashSet<Tuple<Interval, IGameObject>>();
            foreach (BaseGameObject obj in gameObjects)
            {
                Interval interval = new Interval(obj.Boundary.X, obj.Boundary.Location.X + obj.Boundary.Width);
                objects.Add(new Tuple<Interval, IGameObject>(interval, obj));
            }
            world.AddObject(this);

        }

        protected override void OnSimulation(GameTime time)
        {
            foreach (Tuple<Interval, IGameObject> obj in objects)
            {
                obj.Item2.Update(time);
            }
        }

        protected override void OnCollision(IGameObject target, CollisionMode mode, CornerMode corner, CornerMode cornerPassive)
        {
            if (target is Mario mario)
            {
                Interval marioBoundary = new Interval(mario.Boundary.X, mario.Boundary.X + mario.Boundary.Width);
                // note: cornerPassive == right means that Mario's right half touches bottom left corner of this object
                if (mode == CollisionMode.Bottom && cornerPassive == CornerMode.Right)
                {
                    foreach (Tuple<Interval, IGameObject> obj in objects)
                    {
                        if (obj.Item1.CompareTo(marioBoundary) == 0)
                        {
                            //TODO: Change BaseGameObject in order to Collide conditionally
                            //obj.Item2.Collide();
                        }
                    }
                }
                else if (mode == CollisionMode.Bottom && cornerPassive == CornerMode.Left)
                {
                    foreach (Tuple<Interval, IGameObject> obj in objects)
                    {
                        if (obj.Item1.CompareTo(marioBoundary) == 0)
                        {
                            //TODO: Change BaseGameObject in order to Collide conditionally
                            //obj.Item2.Collide();
                        }
                    }
                }
            }
        }

        protected override void OnOut(CollisionMode mode)
        {

        }

        protected override void OnDraw(GameTime time, Rectangle viewport, ZIndex zIndex)
        {
            foreach (Tuple<Interval, IGameObject> obj in objects)
            {
                obj.Item2.Draw(time, zIndex);
            }
        }

    }
}
