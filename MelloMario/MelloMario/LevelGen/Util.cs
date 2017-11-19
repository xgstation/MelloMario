using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using MelloMario.BlockObjects;
using MelloMario.Factories;
using MelloMario.Theming;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MelloMario.LevelGen
{
    internal class VectorConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //DO NOTHING
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            float X = token["X"].ToObject<float>();
            float Y = token["Y"].ToObject<float>();
            return new Vector2(X, Y);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(Vector2));
        }
    }

    internal class Util
    {
        private static readonly JsonSerializer serializers = new JsonSerializer();

        public static void Initilalize()
        {
            serializers.Converters.Add(new VectorConverter());
        }

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
            obj = tempToken[p[p.Length - 1]].ToObject<T>(serializers);
            return true;
        }

        public static void BatchCreate<T>(Func<Point, T> func, Point startPoint, Point quantity, Point objSize,
            ICollection<Point> ignoredSet, ref Stack<IGameObject> stack)
        {
            for (int x = 0; x < quantity.X; x++)
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

        public static void TriganleCreate<T>(Func<Point, T> createFunc, Point startPoint, Point triangleSize,
            Point objSize, ICollection<Point> ignoredSet, ref Stack<IGameObject> stack)
        {
            int X = triangleSize.X;
            int Y = triangleSize.Y;
            bool directionToRight = X > 0;
            bool directionToDown = Y > 0;
            X = Math.Abs(X);
            Y = Math.Abs(Y);
            for (int y = 0; y < Y; y++)
            for (int x = 0; x <= y; x++)
            {
                Point createLocation = new Point(startPoint.X + (directionToRight ? x : -x) * objSize.X,
                    startPoint.Y + (directionToDown ? y : -y) * objSize.Y);
                Point createIndex = new Point(x + 1, y + 1);
                if (ignoredSet == null || !ignoredSet.Contains(createIndex))
                {
                    if (typeof(T).IsAssignableFrom(typeof(IEnumerable<IGameObject>)))
                    {
                        foreach (IGameObject obj in (IEnumerable<IGameObject>) createFunc(createLocation))
                        {
                            stack.Push(obj);
                        }
                    }
                    else
                    {
                        IGameObject newObject = (IGameObject) createFunc(createLocation);
                        stack.Push(newObject);
                    }
                }
            }
        }

        public static void BatchCreateWithProperties<T1, T2>(Func<Point, T1> createFunc, Point startPoint,
            Point quantity, Point objSize, ICollection<Point> ignoredSet, ref Stack<IGameObject> stack,
            IDictionary<Point, T2> properties, Action<IGameObject, T2> applyProperties)
        {
            Contract.Assume(!((properties == null) ^ (applyProperties == null)));
            for (int x = 0; x < quantity.X; x++)
            for (int y = 0; y < quantity.Y; y++)
            {
                Point createLocation = new Point(startPoint.X + x * objSize.X, startPoint.Y + y * objSize.Y);
                Point createIndex = new Point(x + 1, y + 1);
                if (ignoredSet == null || !ignoredSet.Contains(createIndex))
                {
                    if (typeof(T1).IsAssignableFrom(typeof(IEnumerable<IGameObject>)))
                    {
                        foreach (IGameObject obj in (IEnumerable<IGameObject>) createFunc(createLocation))
                        {
                            Point index = new Point(x, y);
                            if (properties != null && properties.ContainsKey(index))
                            {
                                applyProperties(obj, properties[index]);
                            }
                            stack.Push(obj);
                        }
                    }
                    else
                    {
                        IGameObject newObject = (IGameObject) createFunc(createLocation);
                        Point index = new Point(x, y);
                        if (properties != null && properties.ContainsKey(index))
                        {
                            applyProperties(newObject, properties[index]);
                        }
                        stack.Push(newObject);
                    }
                }
            }
        }

        public static List<IGameObject> CreateSinglePipeline(GameModel model, IGameWorld world, IListener listener,
            string pipelineType, int pipelineLength, Point pipelineLoc)
        {
            int grid = GameConst.GRID;
            List<IGameObject> listOfPipelineComponents = new List<IGameObject>();
            Pipeline in1 = null;
            Pipeline in2 = null;
            switch (pipelineType)
            {
                //TODO: this is sloppy, this should use the game object factory.
                case "V":
                    in1 = new Pipeline(world, pipelineLoc, listener, "LeftIn", model);
                    in2 = new Pipeline(world, new Point(pipelineLoc.X + grid, pipelineLoc.Y), listener, "RightIn",
                        model);
                    pipelineLoc = new Point(pipelineLoc.X, pipelineLoc.Y + grid);
                    goto case "NV";
                case "HL":
                    in1 = new Pipeline(world, pipelineLoc, listener, "TopLeftIn");
                    in2 = new Pipeline(world, new Point(pipelineLoc.X, pipelineLoc.Y + grid), listener, "BottomLeftIn");
                    pipelineLoc = new Point(pipelineLoc.X + GameConst.GRID, pipelineLoc.Y);
                    goto case "NH";
                case "HR":
                    in1 = new Pipeline(world, pipelineLoc, listener, "TopRightIn");
                    in2 = new Pipeline(world, new Point(pipelineLoc.X, pipelineLoc.Y + grid), listener,
                        "BottomRightIn");
                    pipelineLoc = new Point(pipelineLoc.X - pipelineLength * grid, pipelineLoc.Y);
                    goto case "NH";
                case "NV":
                    for (int y = 0; y < pipelineLength; y++)
                    {
                        Point loc1 = new Point(pipelineLoc.X, pipelineLoc.Y + grid * y);
                        Point loc2 = new Point(pipelineLoc.X + grid, pipelineLoc.Y + grid * y);
                        listOfPipelineComponents.Add(new Pipeline(world, loc1, listener, "Left"));
                        listOfPipelineComponents.Add(new Pipeline(world, loc2, listener, "Right"));
                    }
                    break;
                case "NH":
                    for (int x = 0; x < pipelineLength; x++)
                    {
                        Point loc1 = new Point(pipelineLoc.X + grid * x, pipelineLoc.Y);
                        Point loc2 = new Point(pipelineLoc.X + grid * x, pipelineLoc.Y + grid);
                        listOfPipelineComponents.Add(new Pipeline(world, loc1, listener, "Top"));
                        listOfPipelineComponents.Add(new Pipeline(world, loc2, listener, "Bottom"));
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

        public static IList<IGameObject> CreateItemList(IGameWorld world, Point point, IListener listener,
            params string[] s)
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