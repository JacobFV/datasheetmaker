﻿using System;
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

        IExpression expression;
        public IExpression Expression {
            get { return expression; }
            set {
                expression = value;
                Equation = expression.ToString();
            }
        }

        string equation;

        /// <summary>
        /// For dependent values
        /// </summary>
        public string Equation {
            get { return equation; }
            set {
                equation = value;
                expression = ExpressionParser.Parse(equation);
            }
        }

        public override string ToString() => Name;
    }
}
