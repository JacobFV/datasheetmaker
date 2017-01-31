using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public enum VariableType
    {
        /// <summary>
        /// Something that was decided before the experiment was started
        /// </summary>
        Dimensional = 0,

        /// <summary>
        /// A measured value
        /// </summary>
        Independent,

        /// <summary>
        /// A calculation
        /// </summary>
        Dependent
    }
}
