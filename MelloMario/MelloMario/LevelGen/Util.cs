using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MelloMario.LevelGen
{
    class Util
    {
        public static bool TryGet<T>(out T obj, JToken token, params string[] p)
        {
            obj = default(T);
            if (token.Type is JTokenType.Array) return false;
            var tempToken = token;
            for (var i = 0; i < p.Length - 1; i++)
            {
                if (tempToken[p[i]] != null)
                {
                    if (tempToken.Type is JTokenType.Array) return false;
                    tempToken = tempToken[p[i]];
                }
                else
                {
                    return false;
                }
            }
            var str = p[p.Length - 1];
            if (tempToken.Type is JTokenType.Array) return false;
            if (tempToken[str] == null) return false;
            obj = tempToken[p[p.Length - 1]].ToObject<T>();
            return true;
        }

    }
}
