using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datasheetmaker
{
    public sealed class DataVariable
    {
        public string Name { get; set; }
        public string Units { get; set; }

        public VariableType Type { get; set; }

        /// <summary>
        /// For independent variables
        /// </summary>
        public BindingList<string> Values { get; } =
            new BindingList<string>();

        /// <summary>
        /// For dependent values
        /// </summary>
        public string Equation { get; set; }

        public override string ToString() => Name;
    }
}
