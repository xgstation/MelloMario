using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    internal interface IScreenState : IState
    {
        void OnGameStart();
        void OnGameProgress();
        void OnGamePause();
        void OnGameOver();
        void OnGameWon();
    }
}
