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
            {'k',3 },
            {'h', 2 },
            {'D',1 },
            {'d', -1 },
            {'c', -2 },
            {'m', -3 },
            {'n', -9 }
        };

        public static UnitsSI Parse(string units) =>
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

        public override string ToString() =>
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
