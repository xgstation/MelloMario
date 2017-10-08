using System.Collections.Generic;

namespace MelloMario.Controllers
{
    abstract class BaseController<T> : IController
    {
        private IDictionary<T, ICommand> commands;
        protected IDictionary<T, ICommand> holdCommands;

        public BaseController()
        {
            commands = new Dictionary<T, ICommand>();
            holdCommands = new Dictionary<T, ICommand>();
        }

        public void AddCommand(object key, ICommand value)
        {
            if (key is T)
            {
                commands.Add((T)key, value);
            }
        }

        public void AddHoldCommand(object key, ICommand value)
        {
            if (key is T)
            {
                holdCommands.Add((T)key, value);
            }
        }

        protected void RunCommand(T key)
        {
            if (commands.ContainsKey(key))
            {
                commands[key].Execute();
            } else if(holdCommands.ContainsKey(key))
            {
                holdCommands[key].Execute();
            }
        }

        public abstract void Update();
        
    }
}