
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MelloMario.Commands;
using System.Diagnostics;

namespace MelloMario.Controllers
{
    public abstract class BaseController : IController
    {
        // TODO: use generic type T instead of int?
        private Dictionary<int, ICommand> commands;

        public BaseController()
        {
            commands = new Dictionary<int, ICommand>();
        }

        public void AddCommand(int key, ICommand value)
        {
            commands.Add(key, value);
        }

        protected void RunCommand(int key)
        {
            if (commands.ContainsKey(key))
            {
                commands[key].Execute();
            }
        }

        public abstract void Update();
    }
}