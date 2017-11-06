using System;
using System.Collections.Generic;

namespace MelloMario.LevelGen
{
    class EncapsulatedObject<T> : IDisposable
    {
        private Stack<T> stack;

        public EncapsulatedObject(Stack<T> stack)
        {
            this.stack = stack;
        }

        public Stack<T> RealObj
        {
            get
            {
                return stack;
            }
        }

        public void Dispose()
        {
            if (stack != null)
            {
                stack.Clear();
                stack = null;
            }
        }
    }
}
