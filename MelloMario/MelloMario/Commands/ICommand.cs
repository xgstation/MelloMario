using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//mostly a copy of examples but I guess there is no reason to reinvent the wheel
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
