using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class AddExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Evaluate(Dictionary<string, double> variables) =>
            Left.Evaluate(variables) + Right.Evaluate(variables);

        public UnitsSI FindUnits(Dictionary<string, UnitsSI> variables) {
            var left = Left.FindUnits(variables);
            var right = Right.FindUnits(variables);

            if (left != right)
                throw new InvalidOperationException();

            return left;
        }

        public void Stringify(
                StringBuilder builder,
                OperatorPrecedence caller,
                Dictionary<string, IExpression> variables
            ) {
            if (caller < OperatorPrecedence.AdditionSubtraction)
                builder.Append("(");

            Left.Stringify(builder, OperatorPrecedence.AdditionSubtraction, variables);
            builder.Append(" + ");
            Right.Stringify(builder, OperatorPrecedence.AdditionSubtraction, variables);

            if (caller < OperatorPrecedence.AdditionSubtraction)
                builder.Append(")");
        }

        public override string ToString() =>
            $"{Left}+{Right}";
    }
}
