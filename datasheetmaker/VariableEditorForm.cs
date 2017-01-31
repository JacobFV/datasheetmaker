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
    public partial class VariableEditorForm : Form
    {
        public BindingList<DataVariable> Variables { get; set; } =
            new BindingList<DataVariable>();

        DataVariable SelectedVariable {
            get { return lstVariables.SelectedValue as DataVariable; }
        }

        public VariableEditorForm() {
            InitializeComponent();
        }

        private void VariableEditorForm_Load(object sender, EventArgs e) {
            lstVariables.DataSource = Variables;
            lstVariables.DisplayMember = "Name";
        }

        private void lstVariables_SelectedIndexChanged(object sender, EventArgs e) {
            if (SelectedVariable != null) {
                txtName.Text = SelectedVariable.Name;
                txtUnits.Text = SelectedVariable.Units;
                chkIndependent.Checked = SelectedVariable.IsIndependent;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            Variables.Add(new DataVariable { Name = "abc" });
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (lstVariables.SelectedIndex != -1) {
                Variables.RemoveAt(lstVariables.SelectedIndex);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e) {
            SelectedVariable.Name = txtName.Text;
        }

        private void txtUnits_TextChanged(object sender, EventArgs e) {
            SelectedVariable.Units = txtUnits.Text;
        }

        private void chkIndependent_CheckedChanged(object sender, EventArgs e) {
            SelectedVariable.IsIndependent = chkIndependent.Checked;

            if (SelectedVariable.IsIndependent) {
                tabSettings.SelectTab(tabIndependent);

                lstIndependentValues.DataSource = SelectedVariable.Values;
            }
            else {
                tabSettings.SelectTab(tabDependent);

                txtEquation.Text = SelectedVariable.Equation;
            }
        }

        private void btnAddIndependentValue_Click(object sender, EventArgs e) {
            SelectedVariable.Values.Add("1");
        }

        private void btnDeleteIndependentValue_Click(object sender, EventArgs e) {
            if (lstIndependentValues.SelectedIndex != -1) {
                SelectedVariable.Values.RemoveAt(lstIndependentValues.SelectedIndex);
            }
        }

        private void txtIndependentValue_TextChanged(object sender, EventArgs e) {
            if (lstIndependentValues.SelectedIndex != -1) {
                SelectedVariable.Values[lstIndependentValues.SelectedIndex] = txtIndependentValue.Text;
            }
        }

        private void lstIndependentValues_SelectedIndexChanged(object sender, EventArgs e) {
            if (lstIndependentValues.SelectedIndex != -1) {
                txtIndependentValue.Text = SelectedVariable.Values[lstIndependentValues.SelectedIndex];
            }
        }
    }
}
