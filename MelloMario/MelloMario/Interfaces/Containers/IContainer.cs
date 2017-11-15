namespace MelloMario
{
    interface IContainer<T>
    {
        void Add(T value);
        void Move(T value);
        void Remove(T value);
        void Update();
    }
}
