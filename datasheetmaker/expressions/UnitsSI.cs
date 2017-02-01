using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class UnitsSI : IEquatable<UnitsSI>
    {
        readonly KeyValuePair<string, int>[] unitdegrees;

        public KeyValuePair<string, int>[] UnitDegrees {
            get { return unitdegrees; }
        }

        public UnitsSI(params KeyValuePair<string, int>[] unitdegrees) {
            this.unitdegrees = unitdegrees.Where(_ => _.Value != 0).ToArray();
        }

        public UnitsSI Recipricol() =>
            new UnitsSI(
                    unitdegrees
                        .Select(
                                unitdegree =>
                                    new KeyValuePair<string, int>(
                                            unitdegree.Key,
                                            -unitdegree.Value
                                        )
                            )
                        .ToArray()
                );

        public UnitsSI Multiply(UnitsSI factor) =>
            new UnitsSI(
                    unitdegrees
                        .Select(_ => _.Key)
                        .Concat(factor.unitdegrees.Select(_ => _.Key))
                        .Select(
                                unit =>
                                    new KeyValuePair<string, int>(
                                            unit,
                                            factor.unitdegrees.Where(_ => _.Key == unit).Sum(_ => _.Value) +
                                                unitdegrees.Where(_ => _.Key == unit).Sum(_ => _.Value)
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
                                    var unit = factor.Last().ToString();
                                    var degree = 1;

                                    if (factor.Length != 1)
                                        degree = prefix_degrees[factor.First()];

                                    return new KeyValuePair<string, int>(
                                            unit,
                                            degree
                                        );
                                }
                            )
                        .ToArray()
                );

        public override string ToString() =>
            string.Join(
                    " ",
                    unitdegrees
                        .OrderBy(kvp => kvp.Key)
                        .Select(
                                kvp => 
                                    kvp.Value == 1 ?
                                        kvp.Key :
                                        $"{kvp.Key}^{kvp.Value}"
                            )
                );

        public override int GetHashCode() =>
            unitdegrees
                .Aggregate(
                        0,
                        (acc, kvp) =>
                            acc ^
                                kvp.Key.GetHashCode() ^
                                kvp.Value
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
