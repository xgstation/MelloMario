using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MelloMario.BlockObjects;
using MelloMario.Factories;
using MelloMario.ItemObjects;
using MelloMario.MarioObjects;
using MelloMario.MarioObjects.PowerUpStates;
using Microsoft.Xna.Framework;

namespace MelloMario.Theming
{
    static class GameDatabase
    {
        private static IDictionary<IGameObject, IList<IGameObject>> ItemEnclosedDb = new Dictionary<IGameObject, IList<IGameObject>>();
        private static IDictionary<Pipeline, string> PipelineEntranceDb = new Dictionary<Pipeline, string>();
        private static IDictionary<Pipeline, string> PipelinePortalDb = new Dictionary<Pipeline, string>();
        private static IDictionary<string, Tuple<Pipeline, Pipeline>> PipelineIndex = new Dictionary<string, Tuple<Pipeline, Pipeline>>();
        private static IDictionary<ICharacter, Point> CharacterLocations = new Dictionary<ICharacter, Point>();
        private static IDictionary<ICharacter, int> CharacterLifes = new Dictionary<ICharacter, int>();
        private static IGameSession session;

        public static void Initialize(IGameSession session)
        {
            GameDatabase.session = session;
        }

        public static void AddPipelineIndex(string index, Tuple<Pipeline,Pipeline> pipeline)
        {
            if (!PipelineIndex.ContainsKey(index))
            {
                PipelineIndex.Add(index, pipeline);
            }
        }
        public static void AddPortal(Pipeline pipeline, string portalTo)
        {
            if (!PipelinePortalDb.ContainsKey(pipeline))
            {
                PipelinePortalDb.Add(pipeline, portalTo);
            }
        }
        public static bool IsPortal(Pipeline pipeline)
        {
            return PipelinePortalDb.ContainsKey(pipeline);
        }
        public static Pipeline GetPortal(Pipeline pipeline)
        {
            return PipelinePortalDb.ContainsKey(pipeline) && 
                PipelineIndex.ContainsKey(PipelinePortalDb[pipeline])
                ? PipelineIndex[PipelinePortalDb[pipeline]].Item1
                : null;
        }
        public static void AddOneLife(ICharacter character)
        {
            if (CharacterLifes.ContainsKey(character))
            {
                ++CharacterLifes[character];
            }
        }

        public static void SubtractOneLife(ICharacter character)
        {
            if (CharacterLifes.ContainsKey(character))
            {
                if (CharacterLifes[character] <= 0)
                {
                    Debug.WriteLine("If you insist having negative lifes.");
                }
                --CharacterLifes[character];

            }
        }
        public static void SetCharacterLifes(ICharacter character, int lifes)
        {
            if (CharacterLifes.ContainsKey(character))
            {
                CharacterLifes[character] = lifes;
            }
            else
            {
                CharacterLifes.Add(character, lifes);
            }
        }
        public static int GetCharacterLifes(ICharacter character)
        {
            return CharacterLifes.ContainsKey(character) ? CharacterLifes[character] : 0;
        }
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

        public static IGameObject GetNextItem(IGameObject obj)
        {
            if (!HasItemEnclosed(obj)) return null;
            if (ItemEnclosedDb[obj][0] is SuperMushroom mushroom)
            {
                if (session.ScanPlayers().Any(c => (c as PlayerMario)?.PowerUpState is Super))
                {
                    ItemEnclosedDb[obj].RemoveAt(0);
                    return mushroom.GetFireFlower();
                }
            }
            var toBeRemoved = ItemEnclosedDb[obj][0];
            ItemEnclosedDb[obj].RemoveAt(0);
            return toBeRemoved;
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
