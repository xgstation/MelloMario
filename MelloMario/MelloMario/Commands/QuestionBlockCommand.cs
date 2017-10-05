using System.Collections.Generic;

namespace MelloMario.Commands
{
    class QuestionBlockCommand : BaseCommand<IGameObject[,]>
    {
        public QuestionBlockCommand(IGameObject[,] obj) : base(obj)
        {
        }

        public override void Execute()
        {
            // temporary code for demo in sprint 1
            MarioObjects.Mario mario = null;
            BlockObjects.Question question = null;

            foreach (IGameObject obj in Receiver)
            {
                if (obj is MarioObjects.Mario)
                {
                    mario = (MarioObjects.Mario)obj;
                }
                if (obj is BlockObjects.Question)
                {
                    question = (BlockObjects.Question)obj;
                }
            }
            question.State.Bump();
        }
    }
}