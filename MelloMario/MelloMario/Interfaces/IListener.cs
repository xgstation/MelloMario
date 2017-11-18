using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    internal interface IListener
    {
        void Subscribe(IGameObject gameObject);
    }
}
