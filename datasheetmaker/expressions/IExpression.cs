using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public interface IExpression
    {
        UnitsSI FindUnits(Dictionary<string, UnitsSI> variables);

        double Evaluate(Dictionary<string, double> variables);
    }
}
