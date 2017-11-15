using System.Collections.Generic;

namespace MelloMario.LevelGen
{
    class EncapsulatedObject<T>
    {
        private Stack<T> stack;

        public EncapsulatedObject(Stack<T> stack)
        {
            this.stack = stack;
        }

        public Stack<T> RealObj
        {
            get { return stack; }
        }
    }
}
