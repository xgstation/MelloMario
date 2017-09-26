using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MelloMario
{
    public interface IBlockState
    {
        void ChangeToSilent();
        void ChangeToDestroy();
        void ChangeToHidden();
        void ChangeToUsed();
    }
}
