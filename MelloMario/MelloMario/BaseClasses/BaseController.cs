using MelloMario.Commands;
using System.Collections.Generic;

namespace MelloMario.Controllers
{
    abstract class BaseController<T> : IController
    {
        private IDictionary<KeyBehavior, IDictionary<T, ICommand>> commands;
        private GameModel model;

        public BaseController(GameModel model)
        {
            this.model = model;
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
                if (commands[behavior].ContainsKey(castedKey))
                {
                    commands[behavior][castedKey] = value;
                }
                else
                {
                    commands[behavior].Add(castedKey, value);
                }
            }
        }

        protected void RunCommand(T key, KeyBehavior behavior)
        {
            if (commands[behavior].ContainsKey(key))
            {
                if(!model.IsPaused || commands[behavior][key] is Pause)
                    commands[behavior][key].Execute();
            }
        }

        public abstract void Update();

    }
}