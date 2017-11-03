using System.Collections.Generic;

namespace MelloMario
{
    interface IContainer<Key, Value>
    {
        IEnumerable<Value> ScanAll();
        IEnumerable<Value> Scan(Key key);
        void Add(Value value);
        void Move(Value value);
        void Remove(Value value);
        void Update();
    }
}
