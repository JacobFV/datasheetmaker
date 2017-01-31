using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace datasheetmaker
{
    public partial class MainForm : Form
    {
        BindingList<DataVariable> variables =
            new BindingList<DataVariable>();
        readonly Dictionary<string, BindingList<string>> measurement_values =
            new Dictionary<string, BindingList<string>>();

        public MainForm() {
            InitializeComponent();
            
            variables.Add(new DataVariable { Name = "abc" });
        }
        
        private void mnuDataEditVariables_Click(object sender, EventArgs e) {
            var editorform =
                new VariableEditorForm();

            editorform.Variables = variables;

            editorform.ShowDialog(this);

            measurement_values.Clear();
            lsvData.Clear();

            var dimensions =
                variables.Where(_ => _.Type == VariableType.Dimensional).ToArray();

            var measurements =
                variables.Where(_ => _.Type == VariableType.Independent).ToArray();

            var calculations =
                variables.Where(_ => _.Type == VariableType.Dependent).ToArray();

            lsvData.Columns.Add("Index");

            foreach (var variable in dimensions.Concat(measurements).Concat(calculations)) {
                var column =
                    new ColumnHeader();

                column.Text = variable.Name;
                
                lsvData.Columns.Add(column);
            }

            var i = 0;
            foreach (var ordinates in Combinations(dimensions.Select(_ => _.Values).ToArray())) {
                var row = new ListViewItem();

                row.Text = $"Entry {++i}";
                
                foreach (var ordinate in ordinates)
                    row.SubItems.Add(ordinate);
                    //row.SubItems.Add(ordinate, Color.DarkBlue, Color.LightYellow, lsvData.Font);

                foreach (var measurement in measurements)
                    row.SubItems.Add(measurement.Name);
                    //row.SubItems.Add($"[{measurement.Name}]");

                foreach (var calculation in calculations)
                    row.SubItems.Add(calculation.Name);
                    //row.SubItems.Add($"[{calculation.Name}]", Color.DarkGray, Color.LightGray, lsvData.Font);

                lsvData.Items.Add(row);
            }
        }

        IEnumerable<string[]> Combinations(IEnumerable<string>[] sources, int i = 0) {
            if (i == sources.Length)
                yield return new string[0];
            else {
                foreach (var item in sources[i])
                    foreach (var next_set in Combinations(sources, i + 1))
                        yield return new[] { item }.Concat(next_set).ToArray();
            }
        }

        private void lsvData_SelectedIndexChanged(object sender, EventArgs e) {

        }
    }
}
