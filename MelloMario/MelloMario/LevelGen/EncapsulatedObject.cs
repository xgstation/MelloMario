namespace MelloMario.LevelGen
{
    #region

    using System.Collections.Generic;

    #endregion

    internal class EncapsulatedObject<T>
    {
        public EncapsulatedObject(Stack<T> stack)
        {
            RealObj = stack;
        }

        public Stack<T> RealObj { get; }
    }
}
