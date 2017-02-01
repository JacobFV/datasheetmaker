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

            Setup();
        }

        void Setup() {
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

                    foreach (var xvalue in xvariable.Elements("value"))
                        variable.Values.Add(xvalue.Value);

                    variables.Add(variable);
                }

                Setup();
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

                    foreach (var value in variable.Values)
                        file.WriteElementString("value", value);

                    file.WriteEndElement();
                }
                file.WriteEndElement();

                file.WriteStartElement("measurements");

                file.WriteEndElement();

                file.WriteEndElement();
                file.WriteEndDocument();
            }
        }
    }
}
