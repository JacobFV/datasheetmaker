using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public enum OperatorPrecedence
    {
        Lower = 0,
        Exponents = 1,
        MultiplicationDivision,
        AdditionSubtraction,
        Higher,
        Top
    }
}
