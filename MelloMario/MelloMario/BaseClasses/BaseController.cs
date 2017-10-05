using System.Collections.Generic;

namespace MelloMario.Controllers
{
    abstract class BaseController<T> : IController
    {
        private IDictionary<T, ICommand> commands;

        public BaseController()
        {
            commands = new Dictionary<T, ICommand>();
        }

        public void AddCommand(object key, ICommand value)
        {
            if (key is T)
            {
                commands.Add((T)key, value);
            }
        }

        protected void RunCommand(T key)
        {
            if (commands.ContainsKey(key))
            {
                commands[key].Execute();
            }
        }

        public abstract void Update();
    }
}