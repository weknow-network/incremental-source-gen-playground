using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceGenTester
{
    [Flags]
    public enum Color
    {
        NONE = 0,
        RED = 1,
        GREEN = 2, 
        BLUR = 4
    }
}
