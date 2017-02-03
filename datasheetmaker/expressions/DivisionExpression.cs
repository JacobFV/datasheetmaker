﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class DivisionExpression : IExpression
    {
        public IExpression Left { get; set; }
        public IExpression Right { get; set; }

        public double Evaluate(Dictionary<string, double> variables) =>
            Left.Evaluate(variables) / Right.Evaluate(variables);

        public UnitsSI FindUnits(Dictionary<string, UnitsSI> variables) {
            var left = Left.FindUnits(variables);
            var right = Right.FindUnits(variables);

            return left.Multiply(right);
        }

        public override string ToString() =>
            $"{Left}÷{Right}";
    }
}
