using System.Collections.Generic;

namespace MelloMario.Controllers
{
    abstract class BaseController<T> : IController
    {
        private IDictionary<KeyBehavior, IDictionary<T, ICommand>> commands;

        protected abstract void OnUpdate();

        protected void RunCommand(T key, KeyBehavior behavior)
        {
            if (commands[behavior].ContainsKey(key))
            {
                commands[behavior][key].Execute();
            }
        }

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
            if (key is T castedKey)
            {
                commands[behavior].Add(castedKey, value);
            }
        }

        public void Update()
        {
            OnUpdate();
        }

        public void Reset()
        {
            foreach (Dictionary<T, ICommand> dict in commands.Values)
            {
                dict.Clear();
            }
        }
    }
}
