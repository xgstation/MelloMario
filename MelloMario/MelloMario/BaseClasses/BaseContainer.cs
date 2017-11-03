using System.Collections.Generic;

namespace MelloMario.Containers
{
    abstract class BaseContainer<Key, Value>
    {
        private IDictionary<Value, Key> keys;
        private IDictionary<Key, ISet<Value>> values;

        private ISet<Value> toAdd;
        private ISet<Value> toMove;
        private ISet<Value> toRemove;

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
                foreach (Value value in values[key])
                {
                    yield return value;
                }
            }
        }

        public BaseContainer()
        {
            keys = new Dictionary<Value, Key>();
            values = new Dictionary<Key, ISet<Value>>();
            toAdd = new HashSet<Value>();
            toMove = new HashSet<Value>();
            toRemove = new HashSet<Value>();
        }

        public IEnumerable<Value> ScanAll()
        {
            foreach (KeyValuePair<Key, ISet<Value>> pair in values)
            {
                foreach (Value value in pair.Value)
                {
                    yield return value;
                }
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
