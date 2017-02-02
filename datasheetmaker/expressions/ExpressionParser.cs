using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public static class ExpressionParser
    {
        public static IExpression Parse(string src) {
            var result = Parse_add(ref src);

            if (src.Length > 0)
                return null;

            return result;
        }

        static readonly char[] whitespace = " \t\n\r\f\v".ToCharArray();

        static IExpression Parse_add(ref string src) {
            IExpression term;
            string copy = src;
            
            term = Parse_multiply(ref src);
            if (term == null)
                goto bad;

                while(src.StartsWith("+") || src.StartsWith("-")) {
                    var op = src[0];

                    copy = src;
                    copy = copy.Substring(1).TrimStart(whitespace);

                    var term2 = Parse_multiply(ref copy);
                    if (term2 == null)
                        goto bad;

                    term =
                        op == '+' ?
                            new AddExpression {
                                Left = term,
                                Right = term2
                            } :
                            (IExpression)new SubtractExpression {
                                Left = term,
                                Right = term2
                            };
                    src = copy;
                }

            return term;

        bad:
            src = copy;
            return null;
        }

        static IExpression Parse_multiply(ref string src) {
            IExpression term;
            string copy = src;

            term = Parse_item(ref src);
            if (term == null)
                goto bad;

            while (src.StartsWith("*") || src.StartsWith("/") || src.Length > 0) {
                var op = src[0];

                copy = src;

                if (op == '*' || op == '/')
                    copy = copy.Substring(1).TrimStart(whitespace);

                var term2 = Parse_item(ref copy);
                if (term2 == null)
                    if (op == '*' || op == '/')
                        goto bad;
                    else break;

                term =
                    op == '/' ?
                        new DivisionExpression {
                            Left = term,
                            Right = term2
                        } :
                        (IExpression)new MultiplyExpression {
                            Left = term,
                            Right = term2
                        };
                src = copy;
            }

            return term;

            bad:
            src = copy;
            return null;
        }

        static IExpression Parse_item(ref string src) =>
            Parse_variable(ref src) ??
            Parse_number(ref src) ??
            Parse_subexpression(ref src);
        
        static readonly char[] variablenamechars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM._".ToCharArray();
        static IExpression Parse_variable(ref string src) {
            var i = 0;

            while (i < src.Length && variablenamechars.Contains(src[i]))
                i++;

            if (i == 0)
                return null;

            var name = src.Substring(0, i);

            src = src.Substring(i).TrimStart(whitespace);

            return new VariableExpression { Name = name };
        }

        static IExpression Parse_number(ref string src) =>
            NumberExpression.Parse(ref src);

        static IExpression Parse_subexpression(ref string src) {
            if (src.StartsWith("(")) {
                var copy = src.Substring(1).TrimStart(whitespace);

                var expression = Parse_add(ref copy);
                if (!copy.StartsWith(")") || expression == null)
                    return null;

                src = copy.Substring(1).TrimStart(whitespace);
                return expression;
            }

            return null;
        }
    }
}
