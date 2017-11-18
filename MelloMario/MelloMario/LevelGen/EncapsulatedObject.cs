using System.Collections.Generic;

namespace MelloMario.LevelGen
{
    internal class EncapsulatedObject<T>
    {
        public EncapsulatedObject(Stack<T> stack)
        {
            RealObj = stack;
        }

        public Stack<T> RealObj { get; }
    }
}