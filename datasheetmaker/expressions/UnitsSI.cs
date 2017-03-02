using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class UnitsSI : IEquatable<UnitsSI>
    {
        readonly KeyValuePair<string, Tuple<int, int>>[] unitdegrees;

        // (unit, [base10exponent, unitexponent])

        public KeyValuePair<string, Tuple<int, int>>[] UnitDegrees {
            get { return unitdegrees; }
        }

        public UnitsSI(params KeyValuePair<string, Tuple<int, int>>[] unitdegrees) {
            //TODO: this doesn't handle when units should be multiplied or divided by exponents of ten
            this.unitdegrees = unitdegrees.Where(_ => _.Value.Item2 != 0).ToArray();
        }

        public UnitsSI Recipricol() =>
            new UnitsSI(
                    unitdegrees
                        .Select(
                                unitdegree =>
                                    new KeyValuePair<string, Tuple<int, int>>(
                                            unitdegree.Key,
                                            new Tuple<int, int>(
                                                    unitdegree.Value.Item1,
                                                    -unitdegree.Value.Item2
                                                )
                                        )
                            )
                        .ToArray()
                );

        public UnitsSI Multiply(UnitsSI factor) =>
            new UnitsSI(
                    unitdegrees
                        .Select(_ => _.Key)
                        .Union(factor.unitdegrees.Select(_ => _.Key))
                        .Select(
                                unit =>
                                    new KeyValuePair<string, Tuple<int, int>>(
                                            unit,
                                            new Tuple<int, int>(
                                                    factor.unitdegrees.Where(_ => _.Key == unit).Sum(_ => _.Value.Item1) +
                                                        unitdegrees.Where(_ => _.Key == unit).Sum(_ => _.Value.Item1),
                                                    factor.unitdegrees.Where(_ => _.Key == unit).Sum(_ => _.Value.Item2) +
                                                        unitdegrees.Where(_ => _.Key == unit).Sum(_ => _.Value.Item2)
                                                )
                                        )
                            )
                        .ToArray()
                );

        static readonly Dictionary<char, int> prefix_degrees = new Dictionary<char, int> {
            {'k', 3 },
            {'h', 2 },
            {'D', 1 },
            {'d', -1 },
            {'c', -2 },
            {'m', -3 },
            {'n', -9 }
        };

        public static UnitsSI ParseOld(string units) =>
            new UnitsSI(
                    units
                        .Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(
                                factor => {
                                    var splt =
                                        factor.Split('^');

                                    var unit = splt[0];
                                    var base10degree = 0;
                                    var unitdegree = 1;

                                    if (unit.Length != 1) {
                                        var p = unit[0];

                                        if (prefix_degrees.TryGetValue(p, out base10degree))
                                            unit = unit.Substring(1);
                                        else base10degree = 1;
                                    }

                                    if (splt.Length > 1)
                                        unitdegree = int.Parse(splt[1]);

                                    return new KeyValuePair<string, Tuple<int, int>>(
                                            unit,
                                            new Tuple<int, int>(
                                                    base10degree,
                                                    unitdegree
                                                )
                                        );
                                }
                            )
                        .ToArray()
                );

        public static readonly string littlenumbers = "⁰¹²³⁴⁵⁶⁷⁸⁹";

        public static string LittleForm(int i) {
            if (i < 0)
                return "‾" + LittleForm(-i);

            if (i == 0)
                return littlenumbers[0].ToString();
            else if (i > 10)
                return LittleForm(i / 10) + littlenumbers[i % 10].ToString();
            else return littlenumbers[i].ToString();
        }

        public static UnitsSI Parse(string units) {
            var running = new UnitsSI();
            var op = "";

            var i_next = 0;
            var i_current = 0;

            while (i_current < units.Length) {
                i_next = units.IndexOfAny("/*".ToCharArray(), i_current);
                if (i_next == -1)
                    i_next = units.Length;

                var subset = units.Substring(i_current, i_next - i_current);
                var operand = ParseOld(subset);
                switch (op) {
                    case "":
                        running = operand;
                        break;

                    case "*":
                        running = running.Multiply(operand);
                        break;

                    case "/":
                        running = running.Multiply(operand.Recipricol());
                        break;

                    default:
                        throw new FormatException();
                }

                if (i_next == units.Length)
                    break;

                op = units[i_next].ToString();
                i_current = i_next + 1;
            }

            return running;
        }

        public string PrettyPrint() {
            var positive =
                unitdegrees
                    .Where(_ => _.Value.Item2 > 0)
                    .Select(
                            _ =>
                                (_.Value.Item1 != 0 ?
                                prefix_degrees
                                    .Keys
                                    .FirstOrDefault(i => prefix_degrees[i] == _.Value.Item1)
                                    .ToString() :
                                    ""
                                    ) + 
                                    _.Key +
                                    (_.Value.Item2 != 1 ?
                                        LittleForm(_.Value.Item2).ToString() :
                                        "")
                        )
                    .ToArray();

            var negative =
                unitdegrees
                    .Where(_ => _.Value.Item2 < 0)
                    .Select(
                            _ =>
                                (_.Value.Item1 != 0 ?
                                prefix_degrees
                                    .Keys
                                    .FirstOrDefault(i => prefix_degrees[i] == _.Value.Item1)
                                    .ToString() :
                                    ""
                                    ) +
                                    _.Key +
                                    (_.Value.Item2 != -1 ?
                                        LittleForm(-_.Value.Item2).ToString() :
                                        "")
                        )
                    .ToArray();

            if (positive.Length == 0 &&
                negative.Length == 0)
                return "";

            if(positive.Length == 0)
                negative =
                    unitdegrees
                        .Where(_ => _.Value.Item2 > 0)
                        .Select(
                                _ =>
                                    prefix_degrees
                                        .Keys
                                        .FirstOrDefault(i => prefix_degrees[i] == _.Value.Item1)
                                        .ToString() +
                                        _.Key +
                                        (_.Value.Item2 != 1 ?
                                            LittleForm(_.Value.Item2).ToString() :
                                            "")
                            )
                        .ToArray();

            var positivestring =
                string.Join(".", positive);

            var negativestring =
                string.Join(".", negative);

            if (positive.Length != 0) {
                if (negativestring.Length != 0)
                    return $"{positivestring}/{negativestring}";
                else return positivestring;
            }
            else return negativestring;
        }

        public override string ToString() =>
            PrettyPrint();

        public string UglyToString() =>
            string.Join(
                    ".",
                    unitdegrees
                        .OrderBy(kvp => kvp.Key)
                        .Select(
                                kvp =>
                                    kvp.Value.Item2 == 1 ?
                                        kvp.Value.Item1 == 0 ?
                                            kvp.Key :
                                            $"{prefix_degrees.FirstOrDefault(_ => _.Value == kvp.Value.Item1).Key}{kvp.Key}" :
                                        kvp.Value.Item1 == 0 ?
                                            $"{kvp.Key}^{kvp.Value.Item2}" :
                                            $"{prefix_degrees.FirstOrDefault(_ => _.Value == kvp.Value.Item1).Key}{kvp.Key}^{kvp.Value.Item2}"
                            )
                );

        public override int GetHashCode() =>
            unitdegrees
                .Aggregate(
                        0,
                        (acc, kvp) =>
                            acc ^
                                kvp.Key.GetHashCode() ^
                                kvp.Value.Item1 ^
                                kvp.Value.Item2
                    );

        public override bool Equals(object obj) =>
            obj is UnitsSI &&
            Equals(obj as UnitsSI);

        public bool Equals(UnitsSI that) =>
            unitdegrees.Length == that.unitdegrees.Length &&
            unitdegrees.All(that.unitdegrees.Contains);

        public static bool operator ==(UnitsSI a, UnitsSI b) =>
            a.Equals(b);

        public static bool operator !=(UnitsSI a, UnitsSI b) =>
            !a.Equals(b);
    }
}
