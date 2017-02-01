namespace datasheetmaker
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDataEditVariables = new System.Windows.Forms.ToolStripMenuItem();
            this.dtaGrid = new System.Windows.Forms.DataGridView();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.diagOpen = new System.Windows.Forms.OpenFileDialog();
            this.diagSave = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuData});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(616, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen,
            this.mnuFileSave});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuData
            // 
            this.mnuData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDataEditVariables});
            this.mnuData.Name = "mnuData";
            this.mnuData.Size = new System.Drawing.Size(43, 20);
            this.mnuData.Text = "&Data";
            // 
            // mnuDataEditVariables
            // 
            this.mnuDataEditVariables.Name = "mnuDataEditVariables";
            this.mnuDataEditVariables.Size = new System.Drawing.Size(143, 22);
            this.mnuDataEditVariables.Text = "&Edit Variables";
            this.mnuDataEditVariables.Click += new System.EventHandler(this.mnuDataEditVariables_Click);
            // 
            // dtaGrid
            // 
            this.dtaGrid.AllowUserToAddRows = false;
            this.dtaGrid.AllowUserToDeleteRows = false;
            this.dtaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtaGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtaGrid.Location = new System.Drawing.Point(0, 24);
            this.dtaGrid.Name = "dtaGrid";
            this.dtaGrid.RowHeadersVisible = false;
            this.dtaGrid.Size = new System.Drawing.Size(616, 379);
            this.dtaGrid.TabIndex = 1;
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // diagOpen
            // 
            this.diagOpen.Filter = "Datasheet Maker Sheet Files (*.datasheetmakerdatasheet)|*.datasheetmakerdatasheet" +
    "";
            this.diagOpen.FileOk += new System.ComponentModel.CancelEventHandler(this.diagOpen_FileOk);
            // 
            // diagSave
            // 
            this.diagSave.Filter = "Datasheet Maker Datasheet Files (*.datasheetmakerdatasheet)|*.datasheetmakerdatas" +
    "heet";
            this.diagSave.FileOk += new System.ComponentModel.CancelEventHandler(this.diagSave_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 403);
            this.Controls.Add(this.dtaGrid);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Datasheet Maker";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuData;
        private System.Windows.Forms.ToolStripMenuItem mnuDataEditVariables;
        private System.Windows.Forms.DataGridView dtaGrid;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.OpenFileDialog diagOpen;
        private System.Windows.Forms.SaveFileDialog diagSave;
    }
}

