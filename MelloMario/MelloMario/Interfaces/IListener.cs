namespace MelloMario
{
    internal interface IListener<in T>
    {
        void Subscribe(T objectListened);
    }
}
