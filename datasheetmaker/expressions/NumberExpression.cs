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

        static readonly char[] whitespace = " \t\n\r\f\v".ToCharArray();
        static readonly char[] variablenamechars = "^-+1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM._".ToCharArray();

        public static NumberExpression Parse(string src) =>
            Parse(ref src);

        public static NumberExpression Parse(ref string src) {
            var digits_i = 0;
            var sign = 1;

            var copy = src;

            /*if (copy.StartsWith("+"))
                copy = copy.Substring(1);
            else*/
            if (copy.StartsWith("-")) {
                copy = copy.Substring(1);
                sign = -1;
            }

            bool @decimal = false;
            while (digits_i < copy.Length && (char.IsDigit(copy[digits_i]) || copy[digits_i] == '.')) {
                if (copy[digits_i] == '.') {
                    if (@decimal)
                        return null;
                    else @decimal = true;
                }

                digits_i++;
            }

            if (digits_i == 0 || (digits_i == 1 && @decimal))
                return null;

            var digits = sign * double.Parse(copy.Substring(0, digits_i));
            copy = copy.Substring(digits_i).TrimStart(whitespace);

            //TODO: add exponents later

            var units_i = 0;

            while (units_i < copy.Length && variablenamechars.Contains(copy[units_i]))
                units_i++;

            var units = UnitsSI.Parse(copy.Substring(0, units_i));
            copy = copy.Substring(units_i);

            src = copy.TrimStart(whitespace);

            return new NumberExpression {
                Value = digits,
                Units = units
            };
        }

        public override string ToString() =>
            $"{Value} {Units}";
    }
}
