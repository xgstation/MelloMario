using System;
using System.Collections.Generic;
using MelloMario.BlockObjects;

namespace MelloMario.Theming
{
    internal class GameDataBase
    {
        private static readonly IDictionary<IGameObject, bool> HiddenDb = new Dictionary<IGameObject, bool>();
        private static readonly IDictionary<IGameObject, IList<IGameObject>> ItemEnclosedDb = new Dictionary<IGameObject, IList<IGameObject>>();
        private static readonly IDictionary<Pipeline, string> PipelineEntranceDb = new Dictionary<Pipeline, string>();
        private GameDataBase() { }
        public static GameDataBase GetInstance { get; } = new GameDataBase();

        public bool IsHidden(IGameObject obj)
        {
            return HiddenDb.ContainsKey(obj) && HiddenDb[obj];
        }

        public void SetHidden(IGameObject obj, bool isHidden)
        {
            if (HiddenDb.ContainsKey(obj))
            {
                HiddenDb[obj] = isHidden;
            }
            else
            {
                HiddenDb.Add(obj, isHidden);
            }
        }
        public bool HasItemEnclosed(IGameObject obj)
        {
            return ItemEnclosedDb.ContainsKey(obj) && ItemEnclosedDb[obj].Count != 0;
        }

        public IGameObject GetEnclosedItem(IGameObject obj)
        {
            try
            {
                return HasItemEnclosed(obj) ? ItemEnclosedDb[obj][0] : null;
            }
            finally
            {
                ItemEnclosedDb[obj].RemoveAt(0);
            }
        }

        public IList<IGameObject> GetEnclosedItems(IGameObject obj)
        {
            return HasItemEnclosed(obj) ? ItemEnclosedDb[obj] : null;
        }
        public void SetEnclosedItem(IGameObject obj, IList<IGameObject> objs)
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
        public bool IsEntrance(Pipeline pipeline)
        {
            return PipelineEntranceDb.ContainsKey(pipeline);
        }

        public string GetEntranceIndex(Pipeline pipeline)
        {
            return IsEntrance(pipeline) ? PipelineEntranceDb[pipeline] : null;
        }

        public void SetEntranceIndex(Pipeline pipeline, string index)
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
