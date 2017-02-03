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
                column.HeaderText =
                    variable.Units != "" ?
                        $"{variable.Name} ({variable.Units})" :
                        variable.Name;
                column.ReadOnly = variable.Type != VariableType.Independent;
                column.Tag = variable;
            }

            if (variables.Count == 0)
                return;

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

        bool updatingaverages = false;
        void UpdateAverages() {
            if (updatingaverages)
                return;

            updatingaverages = true;

            var dimensions =
                variables.Where(_ => _.Type == VariableType.Dimensional).ToArray();

            var measurements =
                variables.Where(_ => _.Type == VariableType.Independent).ToArray();

            var calculations =
                variables.Where(_ => _.Type == VariableType.Dependent).ToArray();

            var kvps =
                dimensions.Select(_ => _.Values.Select(__ => new KeyValuePair<DataVariable, string>(_, __))).ToArray();
            
            var i = 0;
            foreach (var ordinates in Combinations(kvps)) {
                for (var j = ordinates.Length - 1; j >= 0 ; j--) {
                    var ordinate = ordinates[j];

                    if (ordinate.Key.BehavesLikeTrials) {
                        bool shouldcollectvalues = false;

                        switch (ordinate.Value) {
                            case "Ave":
                            case "Mean":
                            case "Median":
                            case "Mode":
                            case "Range":
                            case "Max":
                            case "Min":
                            case "Mid":
                            case "StdDev":
                                shouldcollectvalues = true;
                                break;

                            default:
                                shouldcollectvalues = false;
                                break;
                        }

                        if (shouldcollectvalues) {
                            var k_skip =
                                j + 1 != ordinates.Length ?
                                    Combinations(kvps.Skip(j + 1).ToArray()).Count() :
                                    1;
                            var k_times = ordinate.Key.Values.Count(_ => _.Any(char.IsDigit));
                            var k_start = i - k_skip * k_times;

                            var collection =
                                Combinations(
                                        RangeInts(k_start, k_skip, k_times)
                                            .Select(
                                                    k =>
                                                        dtaGrid
                                                            .Rows[k]
                                                            .Cells
                                                            .Cast<DataGridViewCell>()
                                                            .Skip(dimensions.Length)
                                                            .Select(
                                                                    cell =>
                                                                        NumberExpression.Parse(cell.Value.ToString())?.Value.ToString() ??
                                                                            cell.Value.ToString()
                                                                )
                                                            .ToArray()
                                                )
                                            .ToArray()
                                    )
                                .ToArray();

                            var collection_nums =
                                collection
                                    .Select(
                                            column => (
                                                from x in column
                                                let k = NumberExpression.Parse(x)
                                                where k != null
                                                select k.Value
                                                ).ToArray()
                                        )
                                    .ToArray();

                            string[] answers = new string[measurements.Length + calculations.Length];

                            switch (ordinate.Value) {
                                case "Ave":
                                case "Mean":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = collection_nums[m].Average().ToString();
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Median":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            var list = collection_nums[m].ToList();
                                            list.Sort();

                                            answers[m] = list[list.Count / 2].ToString();
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Mode":
                                    for(int m = 0; m < answers.Length; m++) {
                                        var freq = new Dictionary<string, int>();

                                        foreach (var item in collection[m]) {
                                            if (freq.ContainsKey(item))
                                                freq[item]++;
                                            else freq.Add(item, 1);
                                        }

                                        var highestmode =
                                            freq.Values.Max();

                                        answers[m] = string.Join(";", freq.Keys.Where(key => freq[key] == highestmode));
                                    }

                                    break;

                                case "Range":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = (collection_nums[m].Max() - collection_nums[m].Min()).ToString();
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Min":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = collection_nums[m].Min().ToString();
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Max":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = collection_nums[m].Max().ToString();
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Mid":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = ((collection_nums[m].Max() + collection_nums[m].Min()) / 2f).ToString();
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "StdDev":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = collection_nums[m].StandardDeviation().ToString();
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;
                            }

                            var row = dtaGrid.Rows[i];
                            for (int m = dimensions.Length; m < row.Cells.Count; m++) {
                                row.Cells[m].Value = answers[m - dimensions.Length];
                            }
                        }
                    }
                }

                i++;
            }

            updatingaverages = false;
        }

        IEnumerable<int> RangeInts(int start, int skip, int times) {
            while (times-- > 0) {
                yield return start;

                start += skip;
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
                    variable.BehavesLikeTrials =
                        xvariable.Attribute("behaves-like-trial") != null ?
                            bool.Parse(xvariable.Attribute("behaves-like-trial").Value) :
                            false;

                    if (xvariable.Attribute("equation") != null)
                        variable.Equation = xvariable.Attribute("equation").Value;

                    foreach (var xvalue in xvariable.Elements("value"))
                        variable.Values.Add(xvalue.Value);

                    variables.Add(variable);
                }

                SetupNewVariables();

                updatingaverages = true;

                foreach (var xmeasurement in xdatasheet.Element("measurements").Elements("variable")) {
                    var name = xmeasurement.Attribute("name").Value;
                    var i = variables.Select((dt, j) => new { dt, j }).Where(k => k.dt.Name == name).First().j;

                    foreach (var xvalue in xmeasurement.Elements("value")) {
                        var j = int.Parse(xvalue.Attribute("i").Value);
                        var row = dtaGrid.Rows[j];

                        row.Cells[i].Value = xvalue.Value;
                    }
                }

                updatingaverages = false;
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
                    file.WriteAttributeString("behaves-like-trial", variable.BehavesLikeTrials.ToString());

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

                            var cellvalue = row.Cells[i].Value?.ToString();
                            if (cellvalue != null) {
                                file.WriteStartElement("value");
                                file.WriteAttributeString("i", j.ToString());
                                file.WriteValue(cellvalue);
                                file.WriteEndElement();
                            }
                        }

                        file.WriteEndElement();
                    }
                }
                file.WriteEndElement();

                file.WriteEndElement();
                file.WriteEndDocument();
            }
        }

        private void dtaGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            var variable =
                (DataVariable)dtaGrid.Columns[e.ColumnIndex].Tag;

            if (variables[e.ColumnIndex].Units == "")
                return;

            if (e.Value == null)
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

        List<Tuple<int, int>> changingcellargs =
            new List<Tuple<int, int>>();
        private void dtaGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex == -1)
                return;

            var tuple =
                new Tuple<int, int>(e.ColumnIndex, e.RowIndex);

            if (changingcellargs.Contains(tuple))
                return;

            changingcellargs.Add(tuple);

            var row = dtaGrid.Rows[e.RowIndex];

            var boundvariables_values = new Dictionary<string, double>();
            var boundvariables_units = new Dictionary<string, UnitsSI>();

            for (var i = 0; i < dtaGrid.ColumnCount; i++) {
                var variable =
                    (DataVariable)dtaGrid.Columns[i].Tag;

                var cell =
                    row.Cells[i].Value?.ToString();

                if (cell != null) {
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

            UpdateAverages();

            changingcellargs.Remove(tuple);
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
