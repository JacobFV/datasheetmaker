using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class VariableExpression : IExpression
    {
        public string Name { get; set; }

        public double Evaluate(Dictionary<string, double> variables) =>
            variables[Name];

        public UnitsSI FindUnits(Dictionary<string, UnitsSI> variables) =>
            variables[Name];
    }
}
