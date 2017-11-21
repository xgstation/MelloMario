namespace MelloMario.LevelGen
{
    #region

    using System.Collections.Generic;
    using MelloMario.LevelGen.JsonConverters;

    #endregion

    // note: this class is created to help the architecture design of level generation
    //       and to verify the correctness of interfaces
    internal class JsonGenerator : StaticGenerator
    {
        private static IEnumerable<IGameObject> GetObjs(
            string jsonPath,
            string id,
            IListener<IGameObject> listener,
            IListener<ISoundable> soundListener)
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
