using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class NumberExpression : IExpression
    {
        public double Value { get; set; }
        public UnitsSI Units { get; set; }

        public double Evaluate(Dictionary<string, double> variables) =>
            Value;

        public UnitsSI FindUnits(Dictionary<string, UnitsSI> variables) =>
            Units;
    }
}
