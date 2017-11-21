namespace MelloMario.LevelGen
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using MelloMario.Objects.Blocks;
    using MelloMario.Objects.Enemies;
    using Microsoft.Xna.Framework;

    #endregion

    internal class JsonGenerator : StaticGenerator
    {
        public JsonGenerator(IEnumerable<IGameObject> objects)
        {
            this.objects = objects;
        }
    }
}
