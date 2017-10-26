using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.LevelGen
{
    class EncapsulatedObject<T>
    {
        private Stack<T> stack;
        public EncapsulatedObject(Stack<T> stack)
        {
            this.stack = stack;
        }
        public Stack<T> RealObj { get { return stack; } }
    }
}
