﻿namespace MelloMario.Commands
{
    class Pause : BaseCommand<GameModel>
    {
        public Pause(GameModel model) : base(model)
        {
        }
        public override void Execute()
        {
            Receiver.Pause();
        }
    }
}