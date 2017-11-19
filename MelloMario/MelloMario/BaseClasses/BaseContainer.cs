using System.Collections.Generic;
using System.Linq;

namespace MelloMario.Containers
{
    internal abstract class BaseContainer<Key, Value>
    {
        private readonly IDictionary<Value, Key> keys;

        private readonly ISet<Value> toAdd;
        private readonly ISet<Value> toMove;
        private readonly ISet<Value> toRemove;
        private readonly IDictionary<Key, ISet<Value>> values;

        public BaseContainer()
        {
            keys = new Dictionary<Value, Key>();
            values = new Dictionary<Key, ISet<Value>>();
            toAdd = new HashSet<Value>();
            toMove = new HashSet<Value>();
            toRemove = new HashSet<Value>();
        }

        private void DoAdd(Value value)
        {
            Key key = GetKey(value);

            if (!values.ContainsKey(key))
            {
                values.Add(key, new HashSet<Value>());
            }

            keys.Add(value, key);
            values[key].Add(value);
        }

        private void DoRemove(Value value)
        {
            Key key = keys[value];

            keys.Remove(value);
            values[key].Remove(value);
        }

        protected abstract Key GetKey(Value value);

        protected IEnumerable<Value> Scan(Key key)
        {
            if (values.ContainsKey(key))
            {
                return values[key];
            }
            return Enumerable.Empty<Value>();
        }

        protected IEnumerable<Key> ScanKeys()
        {
            return values.Keys;
        }

        protected IEnumerable<Value> ScanValues()
        {
            foreach (ISet<Value> value in values.Values)
            foreach (Value item in value)
            {
                yield return item;
            }
        }

        public void Add(Value value)
        {
            toAdd.Add(value);
        }

        public void Move(Value value)
        {
            toMove.Add(value);
        }

        public void Remove(Value value)
        {
            toRemove.Add(value);
        }

        public void Update()
        {
            foreach (Value value in toAdd)
            {
                if (!keys.ContainsKey(value))
                {
                    DoAdd(value);
                }
            }
            toAdd.Clear();

            foreach (Value value in toMove)
            {
                if (keys.ContainsKey(value))
                {
                    DoRemove(value);
                    DoAdd(value);
                }
            }
            toMove.Clear();

            foreach (Value value in toRemove)
            {
                if (keys.ContainsKey(value))
                {
                    DoRemove(value);
                }
            }
            toRemove.Clear();
        }
    }
}