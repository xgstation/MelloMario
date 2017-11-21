namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using MelloMario.LevelGen.JsonConverters;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Enemies;
    using Microsoft.Xna.Framework;

    #endregion

    // note: this class is created to help the architecture design of level generation
    //       and to verify the correctness of interfaces
    internal class JsonGenerator : StaticGenerator
    {
        private static IEnumerable<IGameObject> GetObjs(string jsonPath, string id, IListener<IGameObject> listener, IListener<ISoundable> soundListener)
        {
            LevelIOJson reader = new LevelIOJson(jsonPath, listener, soundListener);
            IWorld world = reader.Load(id);
            return world.ScanNearby(world.Boundary);
        }

        public JsonGenerator(string jsonPath, string id, IListener<IGameObject> listener, IListener<ISoundable> soundListener) :
            base(GetObjs(jsonPath, id, listener, soundListener))
        {
        }
    }
}
