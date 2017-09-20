using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario.Commands
{

    public interface ICommand
    {
        void Execute();
    }

    public abstract class ScriptCommand : ICommand
    {
        protected Script receiver;

        public ScriptCommand(Script receiver)
        {
            this.receiver = receiver;
        }
        public abstract void Execute();
    }

}
