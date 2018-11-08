using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleSimulator
{
    public enum unitType
    {
        [UnitAttribute(10, 10, 10, 5, 10, 10, 1, 10, 1, 10,0,0)]
        Athen,

        [UnitAttribute(20, 10, 10, 7, 10, 10, 1, 10, 1, 10,0,0)]
        Persian
    }
}
