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
                cboType.SelectedIndex = (int)SelectedVariable.Type;
                lstIndependentValues.DataSource = SelectedVariable.Values;
                chkBehavesLikeTrials.Checked = SelectedVariable.BehavesLikeTrials;
                txtEquation.Text = SelectedVariable.Equation;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e) {
            Variables.Add(new DataVariable { Name = "abc" });
            lstVariables.SelectedIndex = Variables.Count - 1;
            lstVariables_SelectedIndexChanged(null, null);
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (lstVariables.SelectedIndex != -1) {
                Variables.RemoveAt(lstVariables.SelectedIndex);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e) {
            SelectedVariable.Name = txtName.Text;
            Variables.ResetItem(lstVariables.SelectedIndex);
        }

        private void txtUnits_TextChanged(object sender, EventArgs e) {
            SelectedVariable.Units = txtUnits.Text;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e) {
            SelectedVariable.Type = (VariableType)cboType.SelectedIndex;

            switch (SelectedVariable.Type) {
                case VariableType.Dimensional:
                    tabSettings.SelectTab(tabDimensional);

                    lstIndependentValues.DataSource = SelectedVariable.Values;

                    chkBehavesLikeTrials.Visible = true;

                    break;

                case VariableType.Independent:
                    tabSettings.SelectTab(tabIndependent);

                    chkBehavesLikeTrials.Visible = false;

                    break;

                case VariableType.Dependent:
                    tabSettings.SelectTab(tabDependent);

                    txtEquation.Text = SelectedVariable.Equation;

                    chkBehavesLikeTrials.Visible = false;

                    break;
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

        private void txtEquation_TextChanged(object sender, EventArgs e) {
            SelectedVariable.Equation = txtEquation.Text;
        }

        private void mnuVariablesMoveUp_Click(object sender, EventArgs e) {
            if (lstVariables.SelectedIndex <= 0)
                return;

            var tmp = Variables[lstVariables.SelectedIndex - 1];
            Variables[lstVariables.SelectedIndex - 1] = Variables[lstVariables.SelectedIndex];
            Variables[lstVariables.SelectedIndex] = tmp;

            lstVariables.SelectedIndex--;
        }

        private void mnuVariablesMoveDown_Click(object sender, EventArgs e) {
            if (lstVariables.SelectedIndex == -1 ||
                lstVariables.SelectedIndex + 1 >= Variables.Count)
                return;

            var tmp = Variables[lstVariables.SelectedIndex + 1];
            Variables[lstVariables.SelectedIndex + 1] = Variables[lstVariables.SelectedIndex];
            Variables[lstVariables.SelectedIndex] = tmp;

            lstVariables.SelectedIndex++;
        }

        private void chkBehavesLikeTrials_VisibleChanged(object sender, EventArgs e) {
            if (chkBehavesLikeTrials.Visible)
                tabSettings.Top += chkBehavesLikeTrials.Height;
            else {
                tabSettings.Top -= chkBehavesLikeTrials.Height;
            }
        }

        private void chkBehavesLikeTrials_CheckedChanged(object sender, EventArgs e) {
            SelectedVariable.BehavesLikeTrials = chkBehavesLikeTrials.Checked;
        }
    }
}
