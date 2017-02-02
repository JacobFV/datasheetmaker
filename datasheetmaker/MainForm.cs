using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace datasheetmaker
{
    public partial class MainForm : Form
    {
        BindingList<DataVariable> variables =
            new BindingList<DataVariable>();

        public MainForm() {
            InitializeComponent();

            //ExpressionParser.Parse("1");
            //ExpressionParser.Parse("4cm*2km");
            //ExpressionParser.Parse("1+3");

            variables.Add(new DataVariable { Name = "abc" });
        }

        private void mnuDataEditVariables_Click(object sender, EventArgs e) {
            var editorform =
                new VariableEditorForm();

            editorform.Variables = variables;

            editorform.ShowDialog(this);

            SetupNewVariables();
        }

        void SetupNewVariables() {
            variables = new BindingList<DataVariable>(variables.OrderBy(x => x.Type).ToList());

            dtaGrid.Rows.Clear();
            dtaGrid.Columns.Clear();

            var dimensions =
                variables.Where(_ => _.Type == VariableType.Dimensional).ToArray();

            var measurements =
                variables.Where(_ => _.Type == VariableType.Independent).ToArray();

            var calculations =
                variables.Where(_ => _.Type == VariableType.Dependent).ToArray();

            dtaGrid.ColumnCount = variables.Count;
            var k = 0;
            foreach (var variable in dimensions.Concat(measurements).Concat(calculations)) {
                var column =
                    dtaGrid.Columns[k++];

                //switch (variable.Type) {
                //    case VariableType.Dependent:
                //        column.DefaultCellStyle.BackColor = Color.AliceBlue;
                //        break;

                //    default:
                //        break;
                //}

                column.Name = $"clm{variable.Name}";
                column.HeaderText = $"{variable.Name} ({variable.Units})";
                column.ReadOnly = variable.Type != VariableType.Independent;
                column.Tag = variable;
            }

            var kvps =
                dimensions.Select(_ => _.Values.Select(__ => new KeyValuePair<DataVariable, string>(_, __))).ToArray();

            var i = 0;
            foreach (var ordinates in Combinations(kvps)) {
                var row = new DataGridViewRow();
                dtaGrid
                    .Rows
                    .Add(
                            ordinates.Select(_ => _.Value)
                                .Concat(measurements.Select(_ => ""))
                                .Concat(calculations.Select(_ => ""))
                                .ToArray()
                        );

                continue;
                var j = 0;

                foreach (var ordinate in ordinates) {
                    var cell = row.Cells[j++];

                    //cell.ReadOnly = true;
                    cell.Value = ordinate.Key.Name;
                }

                foreach (var measurement in measurements) {
                    var cell = row.Cells[j++];

                    cell.ToolTipText = measurement.Name;
                }

                foreach (var calculation in calculations) {
                    var cell = row.Cells[j++];

                    cell.ReadOnly = true;
                    cell.ToolTipText = calculation.Name;
                }
            }
        }

        IEnumerable<T[]> Combinations<T>(IEnumerable<T>[] sources, int i = 0) {
            if (i == sources.Length)
                yield return new T[0];
            else {
                foreach (var item in sources[i])
                    foreach (var next_set in Combinations(sources, i + 1))
                        yield return new[] { item }.Concat(next_set).ToArray();
            }
        }

        private void mnuFileOpen_Click(object sender, EventArgs e) {
            diagOpen.ShowDialog(this);
        }

        private void mnuFileSave_Click(object sender, EventArgs e) {
            diagSave.ShowDialog(this);
        }

        private void diagOpen_FileOk(object sender, CancelEventArgs e) {
            using (var stream = diagOpen.OpenFile()) {
                var file =
                    XDocument.Load(stream);

                var xdatasheet =
                    file.Element("datasheet");

                variables.Clear();

                foreach (var xvariable in xdatasheet.Element("variables").Elements("variable")) {
                    var variable = new DataVariable();
                    variable.Name = xvariable.Attribute("name").Value;
                    variable.Type = (VariableType)Enum.Parse(typeof(VariableType), xvariable.Attribute("type").Value);
                    variable.Units = xvariable.Attribute("units").Value;

                    if (xvariable.Attribute("equation") != null)
                        variable.Equation = xvariable.Attribute("equation").Value;

                    foreach (var xvalue in xvariable.Elements("value"))
                        variable.Values.Add(xvalue.Value);

                    variables.Add(variable);
                }

                SetupNewVariables();

                foreach (var xmeasurement in xdatasheet.Element("measurements").Elements("variable")) {
                    var name = xmeasurement.Attribute("name").Value;
                    var i = variables.Select((dt, j) => new { dt, j }).Where(k => k.dt.Name == name).First().j;

                    foreach(var xvalue in xmeasurement.Elements("value")) {
                        var j = int.Parse(xvalue.Attribute("i").Value);
                        var row = dtaGrid.Rows[j];

                        row.Cells[i].Value = xvalue.Value;
                    }
                }
            }
        }

        private void diagSave_FileOk(object sender, CancelEventArgs e) {
            using (var file = XmlWriter.Create(diagSave.FileName)) {
                file.WriteStartDocument();
                file.WriteStartElement("datasheet");

                file.WriteStartElement("variables");
                foreach (var variable in variables) {
                    file.WriteStartElement("variable");
                    file.WriteAttributeString("name", variable.Name);
                    file.WriteAttributeString("units", variable.Units);
                    file.WriteAttributeString("type", variable.Type.ToString());

                    if (variable.Equation != "")
                        file.WriteAttributeString("equation", variable.Equation);

                    foreach (var value in variable.Values)
                        file.WriteElementString("value", value);

                    file.WriteEndElement();
                }
                file.WriteEndElement();

                file.WriteStartElement("measurements");
                for (int i = 0; i < variables.Count; i++) {
                    var variable =
                        variables[i];

                    if (variable.Type == VariableType.Independent) {
                        file.WriteStartElement("variable");
                        file.WriteAttributeString("name", variable.Name);

                        for (int j = 0; j < dtaGrid.Rows.Count; j++) {
                            var row = dtaGrid.Rows[j];

                            var cellvalue = row.Cells[i].Value.ToString();

                            file.WriteStartElement("value");
                            file.WriteAttributeString("i", j.ToString());
                            file.WriteValue(cellvalue);
                            file.WriteEndElement();
                        }

                        file.WriteEndElement();
                    }
                }
                file.WriteEndElement();

                file.WriteEndElement();
                file.WriteEndDocument();
            }
        }

        private void dtaGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
        }

        private void dtaGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            var variable =
                (DataVariable)dtaGrid.Columns[e.ColumnIndex].Tag;

            if (variables[e.ColumnIndex].Units == "")
                return;

            var src = e.Value.ToString();

            var number =
                NumberExpression.Parse(ref src);

            if (number?.Units.UnitDegrees.Length == 0) {
                e.CellStyle.BackColor = Color.LightGray;

                e.Value = e.Value.ToString() + " " + variable.Units;
            }
            else if (number == null || number.Units != UnitsSI.Parse(variable.Units)) {
                e.CellStyle.BackColor = Color.Peru;
            }
            else {
                e.CellStyle.BackColor = Color.White;
            }

            e.FormattingApplied = true;
        }

        private void mnuHelpFormattingUnits_Click(object sender, EventArgs e) {
            MessageBox.Show(@"Use the period (.) to separate units like kg.m.s^-2\n(that's it)");
        }

        private void dtaGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1)
                return;

            var row = dtaGrid.Rows[e.RowIndex];

            var boundvariables_values = new Dictionary<string, double>();
            var boundvariables_units = new Dictionary<string, UnitsSI>();

            for (var i = 0; i < dtaGrid.ColumnCount; i++) {
                var variable =
                    (DataVariable)dtaGrid.Columns[i].Tag;

                var cell =
                    row.Cells[i].Value.ToString();

                if (variable.Type == VariableType.Dependent) {
                    try {
                        var value = variable.Expression.Evaluate(boundvariables_values);
                        var units = variable.Expression.FindUnits(boundvariables_units);

                        row.Cells[i].Value = value + " " + units.ToString();
                    }
                    catch (KeyNotFoundException) {
                    }
                }

                var numexp = NumberExpression.Parse(ref cell);
                if (numexp != null) {
                    boundvariables_values.Add(variable.Name, numexp.Value);
                    boundvariables_units.Add(variable.Name, numexp.Units);
                }
            }
        }

        private void mnuFileExport_Click(object sender, EventArgs e) {
            diagExport.ShowDialog(this);
        }

        private void mnuFileExportRawData_Click(object sender, EventArgs e) {
            diagExport.FilterIndex = 1;
            diagExport.ShowDialog(this);
        }

        private void mnuFileExportFormattedDatasheet_Click(object sender, EventArgs e) {
            diagExport.FilterIndex = 2;
            diagExport.ShowDialog(this);
        }

        private void diagExport_FileOk(object sender, CancelEventArgs e) {
            switch (diagExport.FilterIndex) {
                case 1:
                    // Raw Data
                    File.WriteAllLines(
                            diagExport.FileName,
                            new[] {
                                string.Join(
                                        ",",
                                        variables
                                            .Select(
                                                    variable =>
                                                        variable.Units != "" ?
                                                            $"{variable.Name} ({variable.Units})" :
                                                            variable.Name
                                                )
                                    )
                            }.Concat(
                                Enumerable
                                    .Range(0, dtaGrid.Rows.Count)
                                    .Select(i => dtaGrid.Rows[i])
                                    .Select(
                                            row =>
                                                string.Join(
                                                        ",",
                                                        row
                                                            .Cells
                                                            .Cast<DataGridViewCell>()
                                                            .Select(
                                                                    cell =>
                                                                        NumberExpression.Parse(cell.Value.ToString())
                                                                            .Value
                                                                            .ToString()
                                                                            ?? cell.Value.ToString()
                                                                )
                                                    )
                                        )
                                )
                        );

                    break;

                case 2:
                    // Formatted Data
                    File.WriteAllLines(
                            diagExport.FileName,
                            new[] {
                                string.Join(
                                        ",",
                                        variables.Select(variable =>variable.Name)
                                    )
                            }.Concat(
                                Enumerable
                                    .Range(0, dtaGrid.Rows.Count)
                                    .Select(i => dtaGrid.Rows[i])
                                    .Select(
                                            row =>
                                                string.Join(
                                                        ",",
                                                        row
                                                            .Cells
                                                            .Cast<DataGridViewCell>()
                                                            .Select(
                                                                    cell => {
                                                                        var variable =
                                                                            variables[cell.ColumnIndex];

                                                                        if (variable.Type == VariableType.Dependent)
                                                                            return cell.Value + " = " + variable.Expression.ToString();

                                                                        return cell.Value;
                                                                    }
                                                                )
                                                    )
                                        )
                                )
                        );

                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
