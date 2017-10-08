using System.Collections.Generic;

namespace MelloMario.Controllers
{
    abstract class BaseController<T> : IController
    {
        private IDictionary<KeyBehavior, IDictionary<T, ICommand>> commands;

        public BaseController()
        {
            commands = new Dictionary<KeyBehavior, IDictionary<T, ICommand>>() {
                { KeyBehavior.press, new Dictionary<T, ICommand>() },
                { KeyBehavior.release, new Dictionary<T, ICommand>() },
                { KeyBehavior.hold, new Dictionary<T, ICommand>() },
            };
        }

        public void AddCommand(object key, ICommand value, KeyBehavior behavior)
        {
            if (key is T)
            {
                commands[behavior].Add((T)key, value);
            }
        }

        protected void RunCommand(T key, KeyBehavior behavior)
        {
            if (commands[behavior].ContainsKey(key))
            {
                commands[behavior][key].Execute();
            }
        }

        public abstract void Update();

    }
}