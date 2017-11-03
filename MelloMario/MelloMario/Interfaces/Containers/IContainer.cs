using System.Collections.Generic;

namespace MelloMario
{
    interface IContainer<T>
    {
        IEnumerable<T> ScanAll();
        void Add(T value);
        void Move(T value);
        void Remove(T value);
        void Update();
    }
}
