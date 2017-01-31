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
            System.Windows.Forms.Label lblName;
            System.Windows.Forms.Label lblEquation;
            System.Windows.Forms.Label lblIndependentValues;
            System.Windows.Forms.Label lblUnits;
            this.lstVariables = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkIndependent = new System.Windows.Forms.CheckBox();
            this.txtEquation = new System.Windows.Forms.TextBox();
            this.tabSettings = new System.Windows.Forms.TabControl();
            this.tabDependent = new System.Windows.Forms.TabPage();
            this.tabIndependent = new System.Windows.Forms.TabPage();
            this.lstIndependentValues = new System.Windows.Forms.ListBox();
            this.btnDeleteIndependentValue = new System.Windows.Forms.Button();
            this.btnAddIndependentValue = new System.Windows.Forms.Button();
            this.txtIndependentValue = new System.Windows.Forms.TextBox();
            this.txtUnits = new System.Windows.Forms.TextBox();
            lblName = new System.Windows.Forms.Label();
            lblEquation = new System.Windows.Forms.Label();
            lblIndependentValues = new System.Windows.Forms.Label();
            lblUnits = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.tabDependent.SuspendLayout();
            this.tabIndependent.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVariables
            // 
            this.lstVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVariables.FormattingEnabled = true;
            this.lstVariables.Location = new System.Drawing.Point(12, 12);
            this.lstVariables.Name = "lstVariables";
            this.lstVariables.Size = new System.Drawing.Size(138, 277);
            this.lstVariables.TabIndex = 0;
            this.lstVariables.SelectedIndexChanged += new System.EventHandler(this.lstVariables_SelectedIndexChanged);
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
            this.splitContainer1.Panel2.Controls.Add(this.tabSettings);
            this.splitContainer1.Panel2.Controls.Add(this.chkIndependent);
            this.splitContainer1.Panel2.Controls.Add(lblUnits);
            this.splitContainer1.Panel2.Controls.Add(lblName);
            this.splitContainer1.Panel2.Controls.Add(this.txtUnits);
            this.splitContainer1.Panel2.Controls.Add(this.txtName);
            this.splitContainer1.Size = new System.Drawing.Size(447, 334);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(44, 295);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(50, 27);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(100, 295);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(50, 27);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(77, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(191, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            lblName.Location = new System.Drawing.Point(14, 12);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(57, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Name:";
            lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkIndependent
            // 
            this.chkIndependent.AutoSize = true;
            this.chkIndependent.Location = new System.Drawing.Point(77, 64);
            this.chkIndependent.Name = "chkIndependent";
            this.chkIndependent.Size = new System.Drawing.Size(86, 17);
            this.chkIndependent.TabIndex = 2;
            this.chkIndependent.Text = "Independent";
            this.chkIndependent.UseVisualStyleBackColor = true;
            this.chkIndependent.CheckedChanged += new System.EventHandler(this.chkIndependent_CheckedChanged);
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
            this.txtEquation.Size = new System.Drawing.Size(251, 184);
            this.txtEquation.TabIndex = 3;
            // 
            // lblEquation
            // 
            lblEquation.Location = new System.Drawing.Point(7, 5);
            lblEquation.Name = "lblEquation";
            lblEquation.Size = new System.Drawing.Size(57, 20);
            lblEquation.TabIndex = 1;
            lblEquation.Text = "Equation:";
            lblEquation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabSettings
            // 
            this.tabSettings.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSettings.Controls.Add(this.tabDependent);
            this.tabSettings.Controls.Add(this.tabIndependent);
            this.tabSettings.Location = new System.Drawing.Point(3, 87);
            this.tabSettings.Multiline = true;
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.SelectedIndex = 0;
            this.tabSettings.Size = new System.Drawing.Size(277, 244);
            this.tabSettings.TabIndex = 4;
            // 
            // tabDependent
            // 
            this.tabDependent.Controls.Add(this.txtEquation);
            this.tabDependent.Controls.Add(lblEquation);
            this.tabDependent.Location = new System.Drawing.Point(4, 4);
            this.tabDependent.Name = "tabDependent";
            this.tabDependent.Padding = new System.Windows.Forms.Padding(3);
            this.tabDependent.Size = new System.Drawing.Size(269, 218);
            this.tabDependent.TabIndex = 0;
            this.tabDependent.Text = "Dependent";
            this.tabDependent.UseVisualStyleBackColor = true;
            // 
            // tabIndependent
            // 
            this.tabIndependent.Controls.Add(this.txtIndependentValue);
            this.tabIndependent.Controls.Add(this.btnDeleteIndependentValue);
            this.tabIndependent.Controls.Add(this.btnAddIndependentValue);
            this.tabIndependent.Controls.Add(lblIndependentValues);
            this.tabIndependent.Controls.Add(this.lstIndependentValues);
            this.tabIndependent.Location = new System.Drawing.Point(4, 4);
            this.tabIndependent.Name = "tabIndependent";
            this.tabIndependent.Padding = new System.Windows.Forms.Padding(3);
            this.tabIndependent.Size = new System.Drawing.Size(269, 218);
            this.tabIndependent.TabIndex = 1;
            this.tabIndependent.Text = "Independent";
            this.tabIndependent.UseVisualStyleBackColor = true;
            // 
            // lstIndependentValues
            // 
            this.lstIndependentValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstIndependentValues.FormattingEnabled = true;
            this.lstIndependentValues.Location = new System.Drawing.Point(10, 37);
            this.lstIndependentValues.Name = "lstIndependentValues";
            this.lstIndependentValues.Size = new System.Drawing.Size(251, 134);
            this.lstIndependentValues.TabIndex = 0;
            this.lstIndependentValues.SelectedIndexChanged += new System.EventHandler(this.lstIndependentValues_SelectedIndexChanged);
            // 
            // btnDeleteIndependentValue
            // 
            this.btnDeleteIndependentValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteIndependentValue.Location = new System.Drawing.Point(211, 6);
            this.btnDeleteIndependentValue.Name = "btnDeleteIndependentValue";
            this.btnDeleteIndependentValue.Size = new System.Drawing.Size(50, 27);
            this.btnDeleteIndependentValue.TabIndex = 3;
            this.btnDeleteIndependentValue.Text = "Delete";
            this.btnDeleteIndependentValue.UseVisualStyleBackColor = true;
            this.btnDeleteIndependentValue.Click += new System.EventHandler(this.btnDeleteIndependentValue_Click);
            // 
            // btnAddIndependentValue
            // 
            this.btnAddIndependentValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddIndependentValue.Location = new System.Drawing.Point(155, 6);
            this.btnAddIndependentValue.Name = "btnAddIndependentValue";
            this.btnAddIndependentValue.Size = new System.Drawing.Size(50, 27);
            this.btnAddIndependentValue.TabIndex = 2;
            this.btnAddIndependentValue.Text = "Add";
            this.btnAddIndependentValue.UseVisualStyleBackColor = true;
            this.btnAddIndependentValue.Click += new System.EventHandler(this.btnAddIndependentValue_Click);
            // 
            // lblIndependentValues
            // 
            lblIndependentValues.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            lblIndependentValues.Location = new System.Drawing.Point(10, 6);
            lblIndependentValues.Name = "lblIndependentValues";
            lblIndependentValues.Size = new System.Drawing.Size(101, 27);
            lblIndependentValues.TabIndex = 1;
            lblIndependentValues.Text = "Values:";
            lblIndependentValues.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtIndependentValue
            // 
            this.txtIndependentValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIndependentValue.Location = new System.Drawing.Point(10, 192);
            this.txtIndependentValue.Name = "txtIndependentValue";
            this.txtIndependentValue.Size = new System.Drawing.Size(251, 20);
            this.txtIndependentValue.TabIndex = 4;
            this.txtIndependentValue.TextChanged += new System.EventHandler(this.txtIndependentValue_TextChanged);
            // 
            // txtUnits
            // 
            this.txtUnits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUnits.Location = new System.Drawing.Point(77, 38);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.Size = new System.Drawing.Size(191, 20);
            this.txtUnits.TabIndex = 0;
            this.txtUnits.TextChanged += new System.EventHandler(this.txtUnits_TextChanged);
            // 
            // lblUnits
            // 
            lblUnits.Location = new System.Drawing.Point(14, 38);
            lblUnits.Name = "lblUnits";
            lblUnits.Size = new System.Drawing.Size(57, 20);
            lblUnits.TabIndex = 1;
            lblUnits.Text = "Units:";
            lblUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // VariableEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 334);
            this.Controls.Add(this.splitContainer1);
            this.Name = "VariableEditorForm";
            this.Text = "Variables";
            this.Load += new System.EventHandler(this.VariableEditorForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabDependent.ResumeLayout(false);
            this.tabDependent.PerformLayout();
            this.tabIndependent.ResumeLayout(false);
            this.tabIndependent.PerformLayout();
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
        private System.Windows.Forms.TabPage tabIndependent;
        private System.Windows.Forms.Button btnDeleteIndependentValue;
        private System.Windows.Forms.Button btnAddIndependentValue;
        private System.Windows.Forms.ListBox lstIndependentValues;
        private System.Windows.Forms.CheckBox chkIndependent;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtIndependentValue;
        private System.Windows.Forms.TextBox txtUnits;
    }
}