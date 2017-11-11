using System;
using System.Collections.Generic;
using System.Linq;
using MelloMario.BlockObjects;
using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;

namespace MelloMario.LevelGen
{
    class Util
    {
        public static bool TryGet<T>(out T obj, JToken token, params string[] p)
        {
            obj = default(T);
            if (token.Type is JTokenType.Array)
            {
                return false;
            }
            JToken tempToken = token;
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (tempToken[p[i]] != null)
                {
                    if (tempToken.Type is JTokenType.Array)
                    {
                        return false;
                    }
                    tempToken = tempToken[p[i]];
                }
                else
                {
                    return false;
                }
            }
            string str = p[p.Length - 1];
            if (tempToken.Type is JTokenType.Array)
            {
                return false;
            }
            if (tempToken[str] == null)
            {
                return false;
            }
            obj = tempToken[p[p.Length - 1]].ToObject<T>();
            return true;
        }
        public static void BatchCreate<T>(Func<Point, T> func, Point startPoint, Point quantity, Point objSize,
            ICollection<Point> ignoredSet, int grid, ref Stack<IGameObject> stack)
        {
            for (int x = 0; x < quantity.X; x++)
            {
                for (int y = 0; y < quantity.Y; y++)
                {
                    Point createLocation = new Point(startPoint.X + x * objSize.X, startPoint.Y + y * objSize.Y);
                    Point createIndex = new Point(x + 1, y + 1);
                    if (ignoredSet == null || !ignoredSet.Contains(createIndex))
                    {
                        if (typeof(T).IsAssignableFrom(typeof(IEnumerable<IGameObject>)))
                        {
                            foreach (IGameObject obj in (IEnumerable<IGameObject>) func(createLocation))
                            {
                                stack.Push(obj);
                            }
                        }
                        else
                        {
                            stack.Push((IGameObject) func(createLocation));
                        }
                    }
                }
            }
        }

        public static List<IGameObject> CreateSinglePipeline(GameModel model, IGameWorld world, int grid, string pipelineType, int pipelineLength, Point pipelineLoc)
        {
            List<IGameObject> listOfPipelineComponents = new List<IGameObject>();
            Pipeline in1 = null;
            Pipeline in2 = null;
            switch (pipelineType)
            {
                case "V":
                    in1 = new Pipeline(world, pipelineLoc, "LeftIn", model);
                    in2 = new Pipeline(world, new Point(pipelineLoc.X + grid, pipelineLoc.Y), "RightIn", model);
                    pipelineLoc = new Point(pipelineLoc.X, pipelineLoc.Y + grid);
                    goto case "NV";
                case "HL":
                    in1 = new Pipeline(world, pipelineLoc, "TopLeftIn");
                    in2 = new Pipeline(world, new Point(pipelineLoc.X, pipelineLoc.Y + grid), "BottomLeftIn");
                    pipelineLoc = new Point(pipelineLoc.X + GameConst.GRID, pipelineLoc.Y);
                    goto case "NH";
                case "HR":
                    in1 = new Pipeline(world, pipelineLoc, "TopRightIn");
                    in2 = new Pipeline(world, new Point(pipelineLoc.X, pipelineLoc.Y + grid), "BottomRightIn");
                    pipelineLoc = new Point(pipelineLoc.X - pipelineLength * grid, pipelineLoc.Y);
                    goto case "NH";
                case "NV":
                    for (int y = 0; y < pipelineLength; y++)
                    {
                        Point loc1 = new Point(pipelineLoc.X, pipelineLoc.Y + grid * y);
                        Point loc2 = new Point(pipelineLoc.X + grid, pipelineLoc.Y + grid * y);
                        listOfPipelineComponents.Add(new Pipeline(world, loc1, "Left"));
                        listOfPipelineComponents.Add(new Pipeline(world, loc2, "Right"));
                    }
                    break;
                case "NH":
                    for (int x = 0; x < pipelineLength; x++)
                    {
                        Point loc1 = new Point(pipelineLoc.X + grid * x, pipelineLoc.Y);
                        Point loc2 = new Point(pipelineLoc.X + grid * x, pipelineLoc.Y + grid);
                        listOfPipelineComponents.Add(new Pipeline(world, loc1, "Top"));
                        listOfPipelineComponents.Add(new Pipeline(world, loc2, "Bottom"));
                    }
                    break;
                default:
                    //DO NOTHING
                    break;
                    ;
            }
            listOfPipelineComponents.Insert(0, in1);
            listOfPipelineComponents.Insert(1, in2);
            return listOfPipelineComponents;
        }
        public static IList<IGameObject> CreateItemList(IGameWorld world, Point point, Listener listener, params string[] s)
        {
            return s?.Select(t => GameObjectFactory.Instance.CreateGameObject(t, world, point, listener)).ToList();
        }
        public static bool TryReadIgnoreSet(JToken token, out ISet<Point> toBeIgnored)
        {
            toBeIgnored = new HashSet<Point>();
            if (token["Ignored"] == null)
            {
                return false;
            }
            List<JToken> ignoredToken = token["Ignored"].ToList();
            foreach (JToken t in ignoredToken)
            {
                toBeIgnored.Add(t.ToObject<Point>());
            }
            return true;
        }
    }
}
