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
            BlockObjects.QuestionBlock question = null;

            foreach (IGameObject obj in Receiver)
            {
                if (obj is MarioObjects.Mario)
                {
                    mario = (MarioObjects.Mario)obj;
                }
                if (obj is BlockObjects.QuestionBlock)
                {
                    question = (BlockObjects.QuestionBlock)obj;
                }
            }
            question.State.Bump();
        }
    }
}