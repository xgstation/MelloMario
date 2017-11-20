namespace MelloMario.Theming
{
    #region

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Characters;
    using MelloMario.Objects.Characters.PowerUpStates;
    using MelloMario.Objects.Items;
    using Microsoft.Xna.Framework;

    #endregion

    internal static class Database
    {
        private static readonly IDictionary<IGameObject, IList<IGameObject>> ItemEnclosedDb =
            new Dictionary<IGameObject, IList<IGameObject>>();

        private static readonly IDictionary<Pipeline, string> PipelineEntranceDb = new Dictionary<Pipeline, string>();
        private static readonly IDictionary<Pipeline, string> PipelinePortalDb = new Dictionary<Pipeline, string>();

        private static readonly IDictionary<string, Tuple<Pipeline, Pipeline>> PipelineIndex =
            new Dictionary<string, Tuple<Pipeline, Pipeline>>();

        private static readonly ConcurrentDictionary<ICharacter, Point> CharacterLocations =
            new ConcurrentDictionary<ICharacter, Point>();

        private static IGameSession Session;

        public static void Initialize(IGameSession newSession)
        {
            Session = newSession;
        }

        public static void AddPipelineIndex(string index, Tuple<Pipeline, Pipeline> pipeline)
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
            return PipelinePortalDb.ContainsKey(pipeline) && PipelineIndex.ContainsKey(PipelinePortalDb[pipeline])
                ? PipelineIndex[PipelinePortalDb[pipeline]].Item1
                : null;
        }

        public static Point GetCharacterLocation()
        {
            // TODO: use viewport collision
            return CharacterLocations.Count != 0 ? CharacterLocations.Values.ToList()[0] : new Point();
        }

        public static bool HasItemEnclosed(IGameObject obj)
        {
            return ItemEnclosedDb.ContainsKey(obj) && ItemEnclosedDb[obj].Count != 0;
        }

        public static IGameObject GetNextItem(IGameObject obj)
        {
            if (!HasItemEnclosed(obj))
            {
                return null;
            }
            if (ItemEnclosedDb[obj][0] is SuperMushroom mushroom)
            {
                if (Session.ScanPlayers()
                    .Any(
                        c => c.Character is MarioCharacter mario
                            && (mario.PowerUpState is Super || mario.PowerUpState is Fire)))
                {
                    ItemEnclosedDb[obj].RemoveAt(0);
                    return mushroom.GetFireFlower();
                }
            }
            IGameObject toBeRemoved = ItemEnclosedDb[obj][0];
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

        public static void Update()
        {
            foreach (IPlayer player in Session.ScanPlayers())
            {
                CharacterLocations.AddOrUpdate(
                    player.Character,
                    ((IGameObject) player.Character).Boundary.Location,
                    (p, loc) => ((IGameObject) p).Boundary.Location);
            }
        }
    }
}
