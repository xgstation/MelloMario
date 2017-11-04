using System.Collections.Generic;
using MelloMario.BlockObjects;

namespace MelloMario.Theming
{
    static class GameDataBase
    {
        private static IDictionary<IGameObject, IList<IGameObject>> ItemEnclosedDb = new Dictionary<IGameObject, IList<IGameObject>>();
        private static IDictionary<Pipeline, string> PipelineEntranceDb = new Dictionary<Pipeline, string>();
        

        public static bool HasItemEnclosed(IGameObject obj)
        {
            return ItemEnclosedDb.ContainsKey(obj) && ItemEnclosedDb[obj].Count != 0;
        }

        public static IGameObject GetEnclosedItem(IGameObject obj)
        {
            if (HasItemEnclosed(obj))
            {
                IGameObject item = ItemEnclosedDb[obj][0];
                ItemEnclosedDb[obj].RemoveAt(0);
                return item;
            }
            else
            {
                return null;
            }
        }

        public static IList<IGameObject> GetEnclosedItems(IGameObject obj)
        {
            return HasItemEnclosed(obj) ? ItemEnclosedDb[obj] : null;
        }

        public static void SetEnclosedItem(IGameObject obj, IList<IGameObject> objs)
        {
            if (ItemEnclosedDb.ContainsKey(obj))
            {
                ItemEnclosedDb[obj] = objs;
            }
            else
            {
                ItemEnclosedDb.Add(obj, objs);
            }
        }

        public static bool IsEntrance(Pipeline pipeline)
        {
            return PipelineEntranceDb.ContainsKey(pipeline);
        }

        public static string GetEntranceIndex(Pipeline pipeline)
        {
            return IsEntrance(pipeline) ? PipelineEntranceDb[pipeline] : null;
        }

        public static void SetEntranceIndex(Pipeline pipeline, string index)
        {
            if (IsEntrance(pipeline))
            {
                PipelineEntranceDb[pipeline] = index;
            }
            else
            {
                PipelineEntranceDb.Add(pipeline, index);
            }
        }
    }
}
