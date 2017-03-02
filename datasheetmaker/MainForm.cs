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
        string filename = "";
        static readonly string[] collectors = new string[] {
            "Avg",
            "Mean",
            "Median",
            "Mode",
            "Range",
            "Min",
            "Max",
            "Mid",
            "StdDev",
        };

        public string Filename {
            get { return filename; }
            set {
                filename = value;

                Open();
            }
        }

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

            var trackedcells =
                Enumerable
                    .Range(0, dtaGrid.Rows.Count)
                    .Select(
                            row_i =>
                            Enumerable
                                .Range(0, dtaGrid.Columns.Count)
                                .Select(
                                        column_i =>
                                            dtaGrid.Rows[row_i].Cells[column_i].Value
                                    )
                                .ToArray()
                        )
                    .ToArray();

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
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
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
                dimensions.Select(_ => _.Values.Select(__ => new KeyValuePair<DataVariable, string>(_, __.Key))).ToArray();
            
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
            }

            updating = true;
            for (int row_i = 0; row_i < trackedcells.Length; row_i++)
                for (int column_i = dimensions.Length; column_i < dimensions.Length + measurements.Length; column_i++)
                    dtaGrid.Rows[row_i].Cells[column_i].Value = trackedcells[row_i][column_i];
            updating = false;
        }

        bool updating = false;
        bool updating_averages = false;
        void UpdateAverages() {
            if (updating)
                return;

            if (updating_averages)
                return;

            updating = true;
            updating_averages = true;

            var dimensions =
                variables.Where(_ => _.Type == VariableType.Dimensional).ToArray();

            var measurements =
                variables.Where(_ => _.Type == VariableType.Independent).ToArray();

            var calculations =
                variables.Where(_ => _.Type == VariableType.Dependent).ToArray();

            var units = measurements.Concat(calculations).Select(_ => _.Units).ToArray();

            var kvps =
                dimensions.Select(_ => _.Values.Select(__ => new KeyValuePair<DataVariable, string>(_, __.Key))).ToArray();
            
            var i = 0;
            foreach (var ordinates in Combinations(kvps)) {
                for (var j = ordinates.Length - 1; j >= 0 ; j--) {
                    var ordinate = ordinates[j];

                    if (ordinate.Key.BehavesLikeTrials) {
                        bool shouldcollectvalues =
                            collectors.Contains(ordinate.Value);
                        
                        if (shouldcollectvalues) {
                            var k_skip =
                                j + 1 != ordinates.Length ?
                                    Combinations(kvps.Skip(j + 1).ToArray()).Count() :
                                    1;
                            var k_times = ordinate.Key.Values.Count(_ => _.Key.Any(char.IsDigit));
                            var k_start = i - k_skip * k_times;

                            var collection =
                                FlipDimensions(
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
                                                                        cell.Value != null ?
                                                                        NumberExpression.Parse(cell.Value.ToString())?.Value.ToString() ??
                                                                            cell.Value.ToString() :
                                                                            null
                                                                )
                                                            //.Where(_ => _ != null)
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
                                                    select
                                                        k != null ?
                                                            k.Value :
                                                            0
                                                ).ToArray()
                                        )
                                    .ToArray();

                            string[] answers = new string[measurements.Length + calculations.Length];

                            switch (ordinate.Value) {
                                case "Avg":
                                case "Mean":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            var val = collection_nums[m].Average();
                                            //if (Math.Abs(val - 1.5) < 0.01) {
                                            //    System.Diagnostics.Debugger.Break();
                                            //}

                                            answers[m] = val.ToString("0.#####");
                                            if (units[m] != "")
                                                answers[m] += " " + units[m];
                                        }
                                        catch (IndexOutOfRangeException) {
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

                                            answers[m] = list[list.Count / 2].ToString("0.#####");
                                            if (units[m] != "")
                                                answers[m] += " " + units[m];
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
                                        if (units[m] != "")
                                            answers[m] += " " + units[m];
                                    }

                                    break;

                                case "Range":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = (collection_nums[m].Max() - collection_nums[m].Min()).ToString("0.#####");
                                            if (units[m] != "")
                                                answers[m] += " " + units[m];
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Min":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = collection_nums[m].Min().ToString("0.#####");
                                            if (units[m] != "")
                                                answers[m] += " " + units[m];
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Max":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = collection_nums[m].Max().ToString("0.#####");
                                            if (units[m] != "")
                                                answers[m] += " " + units[m];
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "Mid":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = ((collection_nums[m].Max() + collection_nums[m].Min()) / 2f).ToString("0.#####");
                                            if (units[m] != "")
                                                answers[m] += " " + units[m];
                                        }
                                        catch (InvalidOperationException) {
                                        }
                                    }

                                    break;

                                case "StdDev":
                                    for (int m = 0; m < answers.Length; m++) {
                                        try {
                                            answers[m] = collection_nums[m].StandardDeviation().ToString("0.#####");
                                            if (units[m] != "")
                                                answers[m] += " " + units[m];
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

            updating = false;
            updating_averages = false;
        }

        void UpdateVariables(int rowindex) {
            if (updating)
                return;

            updating = true;

            var row = dtaGrid.Rows[rowindex];

            var boundvariables_values = new Dictionary<string, double>();
            var boundvariables_units = new Dictionary<string, UnitsSI>();

            for (var i = 0; i < dtaGrid.ColumnCount; i++) {
                var variable =
                    (DataVariable)dtaGrid.Columns[i].Tag;

                var cell =
                    row.Cells[i].FormattedValue?.ToString();

                if (cell != null) {
                    if (variable.Type == VariableType.Dependent) {
                        try {
                            var value = variable.Expression.Evaluate(boundvariables_values);
                            var units = variable.Expression.FindUnits(boundvariables_units);

                            row.Cells[i].Value = value.ToString("0.#####") + " " + units.ToString();
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

            updating = false;
        }

        IEnumerable<int> RangeInts(int start, int skip, int times) {
            while (times-- > 0) {
                yield return start;

                start += skip;
            }
        }

        IEnumerable<T[]> FlipDimensions<T>(T[][] sources, int i = 0, int j = -1) {
            if (j == -1) {
                for (j = 0; j < sources[0].Length; j++) {
                    foreach (var flip in FlipDimensions(sources, i + 1, j))
                        yield return new[] { sources[0][j] }.Concat(flip).ToArray();
                }
            }
            else if (i == sources.Length)
                yield return new T[0];
            else {
                foreach (var flip in FlipDimensions(sources, i + 1, j))
                    yield return new[] { sources[i][j] }.Concat(flip).ToArray();
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
            if (string.IsNullOrEmpty(filename))
                diagSave.ShowDialog(this);
            else Save();
        }

        private void diagOpen_FileOk(object sender, CancelEventArgs e) {
            filename = diagOpen.FileName;
            Open();
        }

        void Open() {
            using (var stream = File.OpenRead(filename)) {
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
                    variable.ShowWork =
                        xvariable.Attribute("show-work") != null ?
                            bool.Parse(xvariable.Attribute("show-work").Value) :
                            false;
                    variable.ShowComments =
                        xvariable.Attribute("show-comments") != null ?
                            bool.Parse(xvariable.Attribute("show-comments").Value) :
                            false;

                    if (xvariable.Attribute("equation") != null)
                        variable.Equation = xvariable.Attribute("equation").Value;

                    foreach (var xvalue in xvariable.Elements("value")) {
                        var val =
                            xvalue.Value;

                        var comments =
                            xvalue
                                .Attribute("comments")
                                ?.Value ??
                                "";

                        variable.Values.Add(new KeyValuePair<string, string>(val, comments));
                    }

                    variables.Add(variable);
                }

                updating = true;
                
                SetupNewVariables();

                updating = true;
                updating_averages = true;

                foreach (var xmeasurement in xdatasheet.Element("measurements").Elements("variable")) {
                    var name = xmeasurement.Attribute("name").Value;
                    var i = variables.Select((dt, j) => new { dt, j }).Where(k => k.dt.Name == name).First().j;

                    foreach (var xvalue in xmeasurement.Elements("value")) {
                        var j = int.Parse(xvalue.Attribute("i").Value);
                        var row = dtaGrid.Rows[j];

                        row.Cells[i].Value = xvalue.Value;
                    }
                }

                updating = !mnuDataAutomaticallyUpdate.Checked;
                updating_averages = !mnuDataAutomaticallyAverage.Checked;
            }
        }

        void Save() {
            using (var file = XmlWriter.Create(filename)) {
                file.WriteStartDocument();
                file.WriteStartElement("datasheet");

                file.WriteStartElement("variables");
                foreach (var variable in variables) {
                    file.WriteStartElement("variable");
                    file.WriteAttributeString("name", variable.Name);
                    file.WriteAttributeString("units", variable.Units);
                    file.WriteAttributeString("type", variable.Type.ToString());
                    file.WriteAttributeString("behaves-like-trial", variable.BehavesLikeTrials.ToString());
                    file.WriteAttributeString("show-comments", variable.ShowComments.ToString());
                    file.WriteAttributeString("show-work", variable.ShowWork.ToString());

                    if (variable.Equation != "")
                        file.WriteAttributeString("equation", variable.Equation);

                    foreach (var value in variable.Values) {
                        file.WriteStartElement("value");

                        if (!string.IsNullOrWhiteSpace(value.Value))
                            file.WriteAttributeString("comments", value.Value);

                        file.WriteValue(value.Key);

                        file.WriteEndElement();
                    }

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

                            var cellvalue = row.Cells[i].FormattedValue?.ToString();
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

        private void diagSave_FileOk(object sender, CancelEventArgs e) {
            filename = diagSave.FileName;

            Save();
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

            //var val = NumberExpression.Parse(dtaGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "");
            //if (val != null &&
            //    Math.Abs(val.Value - 3.293333f) < 0.00001f)
            //    System.Diagnostics.Debugger.Break();

            changingcellargs.Add(tuple);

            UpdateVariables(e.RowIndex);

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
                                                                            ?.Value
                                                                            .ToString()
                                                                            ?? cell.Value.ToString()
                                                                )
                                                    )
                                        )
                                )
                        );

                    break;

                case 2:
                case 3:
                    var dimensions =
                        variables.Where(_ => _.Type == VariableType.Dimensional).ToArray();

                    var measurements =
                        variables.Where(_ => _.Type == VariableType.Independent).ToArray();

                    var calculations =
                        variables.Where(_ => _.Type == VariableType.Dependent).ToArray();

                    var units = measurements.Concat(calculations).Select(_ => _.Units).ToArray();

                    var kvps =
                        dimensions.Select(_ => _.Values.Select(__ => new KeyValuePair<DataVariable, string>(_, __.Key + "(" + __.Value + ")"))).ToArray();

                    var collectedtrials =
                        Combinations(kvps)
                            .Select(
                                    combination =>
                                        combination
                                            .FirstOrDefault(
                                                    kvp => 
                                                        collectors.Contains(kvp.Value)
                                                )
                                )
                            .ToArray();

                    // Formatted Data
                    File.WriteAllLines(
                            diagExport.FileName,
                            new[] {
                                string.Join(
                                        diagExport.FilterIndex == 2 ? "," : "\t",
                                        variables
                                            .Select(
                                                    variable => {
                                                        if(string.IsNullOrWhiteSpace(variable.Equation) ||
                                                            !variable.ShowWork ||
                                                            variable.Expression == null)
                                                            return variable.Name;

                                                        var equationbuilder  =
                                                            new StringBuilder();

                                                        variable.Expression.Stringify(equationbuilder, OperatorPrecedence.Top, new Dictionary<string, IExpression>());

                                                        return
                                                            variable.Name +
                                                            " = " +
                                                            equationbuilder.ToString();
                                                    }
                                                )
                                    )
                            }.Concat(
                                Enumerable
                                    .Range(0, dtaGrid.Rows.Count)
                                    .Select(i => new { row = dtaGrid.Rows[i], i })
                                    .Select(
                                            row => {
                                                var variablevalues =
                                                    row
                                                        .row
                                                        .Cells
                                                        .Cast<DataGridViewCell>()
                                                        .Select(
                                                                (cell, j) => {
                                                                var variable =
                                                                    variables[cell.ColumnIndex];

                                                                var val =
                                                                    cell.FormattedValue.ToString();

                                                                var numexp =
                                                                    NumberExpression.Parse(val);

                                                                var comments =
                                                                    (variable.Type == VariableType.Dimensional ? variable.Values.FirstOrDefault(_ => _.Key == val).Value : null);
                                                                    
                                                                    if (numexp == null)
                                                                        return new KeyValuePair<string, KeyValuePair<string, IExpression>>(
                                                                                variable.Name,
                                                                                new KeyValuePair<string, IExpression>(
                                                                                        comments,
                                                                                        new StringExpression {
                                                                                            Value = cell.FormattedValue.ToString()
                                                                                        }
                                                                                    )
                                                                            );

                                                                    return
                                                                        new KeyValuePair<string, KeyValuePair<string, IExpression>>(
                                                                                variable.Name,
                                                                                new KeyValuePair<string, IExpression>(
                                                                                        comments,
                                                                                        numexp
                                                                                    )
                                                                            );
                                                                }
                                                            )
                                                        .Where(_ => _.Key != null)
                                                        .ToArray();

                                                var variables_map =
                                                    new Dictionary<string, IExpression>();

                                                var variable_map_value =
                                                    new Dictionary<string, double>();

                                                var variable_map_units =
                                                    new Dictionary<string, UnitsSI>();

                                                foreach (var variablevalue in variablevalues) {
                                                    var numexp =
                                                        variablevalue.Value.Value as NumberExpression;

                                                    variables_map.Add(variablevalue.Key, variablevalue.Value.Value);

                                                    if (numexp != null) {
                                                        variable_map_value.Add(variablevalue.Key, numexp.Value);
                                                        variable_map_units.Add(variablevalue.Key, numexp.Units);
                                                    }
                                                }

                                                return
                                                    string.Join(
                                                            ",",
                                                            row
                                                                .row
                                                                .Cells
                                                                .Cast<DataGridViewCell>()
                                                                .Select(
                                                                        (cell, j) => {
                                                                            var variable =
                                                                                variables[cell.ColumnIndex];

                                                                            var ordinate =
                                                                                collectedtrials[row.i];

                                                                            string equ = null;
                                                                            
                                                                            switch (ordinate.Value) {
                                                                                case null:
                                                                                    if (variable.Type == VariableType.Dependent) {
                                                                                        var expressionbuilder =
                                                                                            new StringBuilder();

                                                                                        variable.Expression.Stringify(
                                                                                                expressionbuilder,
                                                                                                OperatorPrecedence.Top,
                                                                                                variables_map
                                                                                            );

                                                                                        equ = expressionbuilder.ToString();
                                                                                    }
                                                                                    break;

                                                                                default:
                                                                                    if (j >= dimensions.Length) {
                                                                                        var k_times = ordinate.Key.Values.Count(_ => _.Key.Any(char.IsDigit));
                                                                                        equ = collectedtrials[row.i].Value + "{above " + ordinate.Key.Values.Count(val => val.Key.Any(char.IsDigit)).ToString() + "}";
                                                                                    }
                                                                                    break;
                                                                            }

                                                                            if (equ != null)
                                                                                return cell.FormattedValue + " = " + equ;
                                                                            else if (
                                                                                variable.Type == VariableType.Dimensional &&
                                                                                variable.ShowComments)
                                                                                return $"{cell.FormattedValue} ({variable.Values.FirstOrDefault(_ => _.Key == cell.FormattedValue.ToString()).Value})";

                                                                            return cell.FormattedValue;
                                                                        }
                                                                    )
                                                        );
                                            }
                                        )
                                )
                        );

                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private void mnuDataAutomaticallyUpdate_Click(object sender, EventArgs e) {
            updating = !mnuDataAutomaticallyUpdate.Checked;

            mnuDataAutomaticallyAverage.Checked &= !updating;
            mnuDataAutomaticallyAverage.Enabled = !updating;
        }

        private void mnuDataAutomaticallyAverage_Click(object sender, EventArgs e) {
            updating_averages = !mnuDataAutomaticallyAverage.Checked;
        }

        private void mnuDataUpdateNow_Click(object sender, EventArgs e) {
            var tmp = updating;

            updating = false;

            for (int i = 0; i < dtaGrid.RowCount; i++)
                UpdateVariables(i);

            UpdateAverages();
            updating = tmp;
        }

        private void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                foreach (DataGridViewCell cell in dtaGrid.SelectedCells) {
                    cell.Value = "";
                }
            }
        }
    }
}
