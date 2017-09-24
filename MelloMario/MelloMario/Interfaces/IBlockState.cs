using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    interface IBlockState
    {
        void Silent();
        void Destroy();
        void Hidden();
        void Used();
    }
}
