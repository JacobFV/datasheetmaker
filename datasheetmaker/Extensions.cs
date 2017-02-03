using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    //http://stackoverflow.com/questions/3141692/standard-deviation-of-generic-list/6252351#6252351
    public static class Extend
    {
        public static double StandardDeviation(this IEnumerable<double> values) {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }
    }
}
