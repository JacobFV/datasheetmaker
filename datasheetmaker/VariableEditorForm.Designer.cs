namespace datasheetmaker
{
    partial class VariableEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblName;
            System.Windows.Forms.Label lblEquation;
            System.Windows.Forms.Label lblIndependentValues;
            System.Windows.Forms.Label lblUnits;
            System.Windows.Forms.Label lblType;
            this.lstVariables = new System.Windows.Forms.ListBox();
            this.mnuVariables = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuVariablesMoveUp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVariablesMoveDown = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabDependent = new System.Windows.Forms.TabPage();
            this.txtEquation = new System.Windows.Forms.TextBox();
            this.tabDimensional = new System.Windows.Forms.TabPage();
            this.txtIndependentValue = new System.Windows.Forms.TextBox();
            this.btnDeleteIndependentValue = new System.Windows.Forms.Button();
            this.btnAddIndependentValue = new System.Windows.Forms.Button();
            this.lstIndependentValues = new System.Windows.Forms.ListBox();
            this.tabIndependent = new System.Windows.Forms.TabPage();
            this.txtUnits = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkBehavesLikeTrials = new System.Windows.Forms.CheckBox();
            lblName = new System.Windows.Forms.Label();
            lblEquation = new System.Windows.Forms.Label();
            lblIndependentValues = new System.Windows.Forms.Label();
            lblUnits = new System.Windows.Forms.Label();
            lblType = new System.Windows.Forms.Label();
            this.mnuVariables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabDependent.SuspendLayout();
            this.tabDimensional.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            lblName.Location = new System.Drawing.Point(14, 12);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(57, 20);
            lblName.TabIndex = 9;
            lblName.Text = "&Name:";
            lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEquation
            // 
            lblEquation.Location = new System.Drawing.Point(7, 5);
            lblEquation.Name = "lblEquation";
            lblEquation.Size = new System.Drawing.Size(57, 20);
            lblEquation.TabIndex = 39;
            lblEquation.Text = "E&quation:";
            lblEquation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblIndependentValues
            // 
            lblIndependentValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            lblIndependentValues.Location = new System.Drawing.Point(10, 6);
            lblIndependentValues.Name = "lblIndependentValues";
            lblIndependentValues.Size = new System.Drawing.Size(80, 27);
            lblIndependentValues.TabIndex = 39;
            lblIndependentValues.Text = "&Values:";
            lblIndependentValues.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnits
            // 
            lblUnits.Location = new System.Drawing.Point(14, 38);
            lblUnits.Name = "lblUnits";
            lblUnits.Size = new System.Drawing.Size(57, 20);
            lblUnits.TabIndex = 19;
            lblUnits.Text = "&Units:";
            lblUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblType
            // 
            lblType.Location = new System.Drawing.Point(14, 64);
            lblType.Name = "lblType";
            lblType.Size = new System.Drawing.Size(57, 20);
            lblType.TabIndex = 29;
            lblType.Text = "&Type:";
            lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstVariables
            // 
            this.lstVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVariables.ContextMenuStrip = this.mnuVariables;
            this.lstVariables.FormattingEnabled = true;
            this.lstVariables.IntegralHeight = false;
            this.lstVariables.Location = new System.Drawing.Point(3, 3);
            this.lstVariables.Name = "lstVariables";
            this.lstVariables.Size = new System.Drawing.Size(144, 296);
            this.lstVariables.TabIndex = 1;
            this.lstVariables.SelectedIndexChanged += new System.EventHandler(this.lstVariables_SelectedIndexChanged);
            // 
            // mnuVariables
            // 
            this.mnuVariables.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVariablesMoveUp,
            this.mnuVariablesMoveDown});
            this.mnuVariables.Name = "mnuVariables";
            this.mnuVariables.Size = new System.Drawing.Size(139, 48);
            // 
            // mnuVariablesMoveUp
            // 
            this.mnuVariablesMoveUp.Name = "mnuVariablesMoveUp";
            this.mnuVariablesMoveUp.Size = new System.Drawing.Size(138, 22);
            this.mnuVariablesMoveUp.Text = "Move &Up";
            this.mnuVariablesMoveUp.Click += new System.EventHandler(this.mnuVariablesMoveUp_Click);
            // 
            // mnuVariablesMoveDown
            // 
            this.mnuVariablesMoveDown.Name = "mnuVariablesMoveDown";
            this.mnuVariablesMoveDown.Size = new System.Drawing.Size(138, 22);
            this.mnuVariablesMoveDown.Text = "Move &Down";
            this.mnuVariablesMoveDown.Click += new System.EventHandler(this.mnuVariablesMoveDown_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.lstVariables);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cboType);
            this.splitContainer1.Panel2.Controls.Add(this.tabSettings);
            this.splitContainer1.Panel2.Controls.Add(lblType);
            this.splitContainer1.Panel2.Controls.Add(lblUnits);
            this.splitContainer1.Panel2.Controls.Add(lblName);
            this.splitContainer1.Panel2.Controls.Add(this.txtUnits);
            this.splitContainer1.Panel2.Controls.Add(this.txtName);
            this.splitContainer1.Panel2.Controls.Add(this.chkBehavesLikeTrials);
            this.splitContainer1.Size = new System.Drawing.Size(413, 330);
            this.splitContainer1.SplitterDistance = 150;
            this.splitContainer1.TabIndex = 100;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(97, 300);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 27);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "D&elete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(41, 300);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 27);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cboType
            // 
            this.cboType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "Dimensional",
            "Independent",
            "Dependent"});
            this.cboType.Location = new System.Drawing.Point(77, 64);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(170, 21);
            this.cboType.TabIndex = 30;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // tabSettings
            // 
            this.tabSettings.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSettings.Controls.Add(this.tabDependent);
            this.tabSettings.Controls.Add(this.tabDimensional);
            this.tabSettings.Controls.Add(this.tabIndependent);
            this.tabSettings.Location = new System.Drawing.Point(3, 91);
            this.tabSettings.Multiline = true;
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(256, 239);
            this.tabSettings.TabIndex = 4;
            // 
            // tabDependent
            // 
            this.tabDependent.Controls.Add(this.txtEquation);
            this.tabDependent.Controls.Add(lblEquation);
            this.tabDependent.Location = new System.Drawing.Point(4, 4);
            this.tabDependent.Name = "tabDependent";
            this.tabDependent.Padding = new System.Windows.Forms.Padding(3);
            this.tabDependent.Size = new System.Drawing.Size(248, 213);
            this.tabDependent.TabIndex = 0;
            this.tabDependent.Text = "Dependent";
            this.tabDependent.UseVisualStyleBackColor = true;
            // 
            // txtEquation
            // 
            this.txtEquation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEquation.Location = new System.Drawing.Point(10, 28);
            this.txtEquation.Multiline = true;
            this.txtEquation.Name = "txtEquation";
            this.txtEquation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEquation.Size = new System.Drawing.Size(230, 170);
            this.txtEquation.TabIndex = 40;
            this.txtEquation.TextChanged += new System.EventHandler(this.txtEquation_TextChanged);
            // 
            // tabDimensional
            // 
            this.tabDimensional.BackColor = System.Drawing.Color.Transparent;
            this.tabDimensional.Controls.Add(this.txtIndependentValue);
            this.tabDimensional.Controls.Add(this.btnDeleteIndependentValue);
            this.tabDimensional.Controls.Add(this.btnAddIndependentValue);
            this.tabDimensional.Controls.Add(lblIndependentValues);
            this.tabDimensional.Controls.Add(this.lstIndependentValues);
            this.tabDimensional.Location = new System.Drawing.Point(4, 4);
            this.tabDimensional.Name = "tabDimensional";
            this.tabDimensional.Padding = new System.Windows.Forms.Padding(3);
            this.tabDimensional.Size = new System.Drawing.Size(248, 213);
            this.tabDimensional.TabIndex = 1;
            this.tabDimensional.Text = "Dimensional";
            this.tabDimensional.UseVisualStyleBackColor = true;
            // 
            // txtIndependentValue
            // 
            this.txtIndependentValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIndependentValue.Location = new System.Drawing.Point(10, 187);
            this.txtIndependentValue.Name = "txtIndependentValue";
            this.txtIndependentValue.Size = new System.Drawing.Size(230, 20);
            this.txtIndependentValue.TabIndex = 43;
            this.txtIndependentValue.TextChanged += new System.EventHandler(this.txtIndependentValue_TextChanged);
            // 
            // btnDeleteIndependentValue
            // 
            this.btnDeleteIndependentValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteIndependentValue.Location = new System.Drawing.Point(190, 6);
            this.btnDeleteIndependentValue.Name = "btnDeleteIndependentValue";
            this.btnDeleteIndependentValue.Size = new System.Drawing.Size(50, 27);
            this.btnDeleteIndependentValue.TabIndex = 41;
            this.btnDeleteIndependentValue.Text = "Delete";
            this.btnDeleteIndependentValue.UseVisualStyleBackColor = true;
            this.btnDeleteIndependentValue.Click += new System.EventHandler(this.btnDeleteIndependentValue_Click);
            // 
            // btnAddIndependentValue
            // 
            this.btnAddIndependentValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddIndependentValue.Location = new System.Drawing.Point(134, 6);
            this.btnAddIndependentValue.Name = "btnAddIndependentValue";
            this.btnAddIndependentValue.Size = new System.Drawing.Size(50, 27);
            this.btnAddIndependentValue.TabIndex = 40;
            this.btnAddIndependentValue.Text = "Add";
            this.btnAddIndependentValue.UseVisualStyleBackColor = true;
            this.btnAddIndependentValue.Click += new System.EventHandler(this.btnAddIndependentValue_Click);
            // 
            // lstIndependentValues
            // 
            this.lstIndependentValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstIndependentValues.FormattingEnabled = true;
            this.lstIndependentValues.IntegralHeight = false;
            this.lstIndependentValues.Location = new System.Drawing.Point(10, 37);
            this.lstIndependentValues.Name = "lstIndependentValues";
            this.lstIndependentValues.Size = new System.Drawing.Size(230, 146);
            this.lstIndependentValues.TabIndex = 42;
            this.lstIndependentValues.SelectedIndexChanged += new System.EventHandler(this.lstIndependentValues_SelectedIndexChanged);
            // 
            // tabIndependent
            // 
            this.tabIndependent.Location = new System.Drawing.Point(4, 4);
            this.tabIndependent.Name = "tabIndependent";
            this.tabIndependent.Padding = new System.Windows.Forms.Padding(3);
            this.tabIndependent.Size = new System.Drawing.Size(248, 213);
            this.tabIndependent.TabIndex = 2;
            this.tabIndependent.Text = "Independent";
            this.tabIndependent.UseVisualStyleBackColor = true;
            // 
            // txtUnits
            // 
            this.txtUnits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnits.Location = new System.Drawing.Point(77, 38);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.Size = new System.Drawing.Size(170, 20);
            this.txtUnits.TabIndex = 20;
            this.txtUnits.TextChanged += new System.EventHandler(this.txtUnits_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(77, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(170, 20);
            this.txtName.TabIndex = 10;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // chkBehavesLikeTrials
            // 
            this.chkBehavesLikeTrials.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkBehavesLikeTrials.Location = new System.Drawing.Point(77, 86);
            this.chkBehavesLikeTrials.Name = "chkBehavesLikeTrials";
            this.chkBehavesLikeTrials.Size = new System.Drawing.Size(170, 24);
            this.chkBehavesLikeTrials.TabIndex = 35;
            this.chkBehavesLikeTrials.Text = "Behaves like Trials";
            this.chkBehavesLikeTrials.UseVisualStyleBackColor = true;
            this.chkBehavesLikeTrials.CheckedChanged += new System.EventHandler(this.chkBehavesLikeTrials_CheckedChanged);
            this.chkBehavesLikeTrials.VisibleChanged += new System.EventHandler(this.chkBehavesLikeTrials_VisibleChanged);
            // 
            // VariableEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 330);
            this.Controls.Add(this.splitContainer1);
            this.Name = "VariableEditorForm";
            this.Text = "Variables";
            this.Load += new System.EventHandler(this.VariableEditorForm_Load);
            this.mnuVariables.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabDependent.ResumeLayout(false);
            this.tabDependent.PerformLayout();
            this.tabDimensional.ResumeLayout(false);
            this.tabDimensional.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstVariables;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabControl tabSettings;
        private System.Windows.Forms.TabPage tabDependent;
        private System.Windows.Forms.TextBox txtEquation;
        private System.Windows.Forms.TabPage tabDimensional;
        private System.Windows.Forms.Button btnDeleteIndependentValue;
        private System.Windows.Forms.Button btnAddIndependentValue;
        private System.Windows.Forms.ListBox lstIndependentValues;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIndependentValue;
        private System.Windows.Forms.TextBox txtUnits;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.TabPage tabIndependent;
        private System.Windows.Forms.ContextMenuStrip mnuVariables;
        private System.Windows.Forms.ToolStripMenuItem mnuVariablesMoveUp;
        private System.Windows.Forms.ToolStripMenuItem mnuVariablesMoveDown;
        private System.Windows.Forms.CheckBox chkBehavesLikeTrials;
    }
}