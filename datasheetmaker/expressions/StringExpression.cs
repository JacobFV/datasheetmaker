using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class StringExpression : IExpression, ILiteral<string>
    {
        public string Value { get; set; }
        
        public double Evaluate(Dictionary<string, double> variables) {
            throw new InvalidOperationException();
        }

        public void Stringify(
                StringBuilder builder, 
                OperatorPrecedence caller, 
                Dictionary<string, IExpression> variables
            ) {
            builder.Append("\"" + Value + "\"");
        }

        public UnitsSI FindUnits(Dictionary<string, UnitsSI> variables) =>
            null;
    }
}
