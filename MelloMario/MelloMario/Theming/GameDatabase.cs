using System.Collections.Generic;
using System.Linq;
using MelloMario.BlockObjects;
using MelloMario.MarioObjects;
using Microsoft.Xna.Framework;

namespace MelloMario.Theming
{
    static class GameDatabase
    {
        private static IDictionary<IGameObject, IList<IGameObject>> ItemEnclosedDb = new Dictionary<IGameObject, IList<IGameObject>>();
        private static IDictionary<Pipeline, string> PipelineEntranceDb = new Dictionary<Pipeline, string>();
        private static IDictionary<ICharacter, Point> CharacterLocations = new Dictionary<ICharacter, Point>();

        public static bool HasCharacters()
        {
            return CharacterLocations.Count != 0;
        }
        public static Point GetCharacterLocation()
        {
            return HasCharacters() ? CharacterLocations.Values.ToList()[0] : new Point();
        }
        public static Point GetCharacterLocation(ICharacter character)
        {
            return HasCharacters() ? CharacterLocations[character] : new Point();
        }
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

        public static void Clear()
        {
            ItemEnclosedDb.Clear();
            PipelineEntranceDb.Clear();
            CharacterLocations.Clear();
        }
    }
}
