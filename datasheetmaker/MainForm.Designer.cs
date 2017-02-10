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
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExportRawData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExportFormattedDatasheet = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDataEditVariables = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDataSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDataAutomaticallyUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDataUpdateNow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpFormattingUnits = new System.Windows.Forms.ToolStripMenuItem();
            this.dtaGrid = new System.Windows.Forms.DataGridView();
            this.diagOpen = new System.Windows.Forms.OpenFileDialog();
            this.diagSave = new System.Windows.Forms.SaveFileDialog();
            this.diagExport = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtaGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuData,
            this.mnuHelp});
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
            this.mnuFileSave,
            this.mnuFileSeparator1,
            this.mnuFileExport});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(146, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(146, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSeparator1
            // 
            this.mnuFileSeparator1.Name = "mnuFileSeparator1";
            this.mnuFileSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExportRawData,
            this.mnuFileExportFormattedDatasheet});
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(146, 22);
            this.mnuFileExport.Text = "E&xport";
            this.mnuFileExport.Click += new System.EventHandler(this.mnuFileExport_Click);
            // 
            // mnuFileExportRawData
            // 
            this.mnuFileExportRawData.Name = "mnuFileExportRawData";
            this.mnuFileExportRawData.Size = new System.Drawing.Size(184, 22);
            this.mnuFileExportRawData.Text = "&Raw Data";
            this.mnuFileExportRawData.Click += new System.EventHandler(this.mnuFileExportRawData_Click);
            // 
            // mnuFileExportFormattedDatasheet
            // 
            this.mnuFileExportFormattedDatasheet.Name = "mnuFileExportFormattedDatasheet";
            this.mnuFileExportFormattedDatasheet.Size = new System.Drawing.Size(184, 22);
            this.mnuFileExportFormattedDatasheet.Text = "&Formatted Datasheet";
            this.mnuFileExportFormattedDatasheet.Click += new System.EventHandler(this.mnuFileExportFormattedDatasheet_Click);
            // 
            // mnuData
            // 
            this.mnuData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDataEditVariables,
            this.mnuDataSeparator1,
            this.mnuDataAutomaticallyUpdate,
            this.mnuDataUpdateNow});
            this.mnuData.Name = "mnuData";
            this.mnuData.Size = new System.Drawing.Size(43, 20);
            this.mnuData.Text = "&Data";
            // 
            // mnuDataEditVariables
            // 
            this.mnuDataEditVariables.Name = "mnuDataEditVariables";
            this.mnuDataEditVariables.Size = new System.Drawing.Size(189, 22);
            this.mnuDataEditVariables.Text = "&Edit Variables";
            this.mnuDataEditVariables.Click += new System.EventHandler(this.mnuDataEditVariables_Click);
            // 
            // mnuDataSeparator1
            // 
            this.mnuDataSeparator1.Name = "mnuDataSeparator1";
            this.mnuDataSeparator1.Size = new System.Drawing.Size(186, 6);
            // 
            // mnuDataAutomaticallyUpdate
            // 
            this.mnuDataAutomaticallyUpdate.Checked = true;
            this.mnuDataAutomaticallyUpdate.CheckOnClick = true;
            this.mnuDataAutomaticallyUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuDataAutomaticallyUpdate.Name = "mnuDataAutomaticallyUpdate";
            this.mnuDataAutomaticallyUpdate.Size = new System.Drawing.Size(189, 22);
            this.mnuDataAutomaticallyUpdate.Text = "&Automatically Update";
            this.mnuDataAutomaticallyUpdate.Click += new System.EventHandler(this.mnuDataAutomaticallyUpdate_Click);
            // 
            // mnuDataUpdateNow
            // 
            this.mnuDataUpdateNow.Name = "mnuDataUpdateNow";
            this.mnuDataUpdateNow.Size = new System.Drawing.Size(189, 22);
            this.mnuDataUpdateNow.Text = "&Update Now";
            this.mnuDataUpdateNow.Click += new System.EventHandler(this.mnuDataUpdateNow_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpFormattingUnits});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpFormattingUnits
            // 
            this.mnuHelpFormattingUnits.Name = "mnuHelpFormattingUnits";
            this.mnuHelpFormattingUnits.Size = new System.Drawing.Size(163, 22);
            this.mnuHelpFormattingUnits.Text = "Formatting &Units";
            this.mnuHelpFormattingUnits.Click += new System.EventHandler(this.mnuHelpFormattingUnits_Click);
            // 
            // dtaGrid
            // 
            this.dtaGrid.AllowDrop = true;
            this.dtaGrid.AllowUserToAddRows = false;
            this.dtaGrid.AllowUserToDeleteRows = false;
            this.dtaGrid.AllowUserToResizeRows = false;
            this.dtaGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dtaGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtaGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtaGrid.Location = new System.Drawing.Point(0, 24);
            this.dtaGrid.Name = "dtaGrid";
            this.dtaGrid.RowHeadersVisible = false;
            this.dtaGrid.Size = new System.Drawing.Size(616, 379);
            this.dtaGrid.TabIndex = 1;
            this.dtaGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dtaGrid_CellFormatting);
            this.dtaGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtaGrid_CellValueChanged);
            this.dtaGrid.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
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
            // diagExport
            // 
            this.diagExport.Filter = "Raw Data (*.csv)|*.csv|Formatted Data (*.csv)|*.csv|Formatted Data (*.txt)|*.txt";
            this.diagExport.FileOk += new System.ComponentModel.CancelEventHandler(this.diagExport_FileOk);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 403);
            this.Controls.Add(this.dtaGrid);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Datasheet Maker";
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.MainForm_PreviewKeyDown);
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
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpFormattingUnits;
        private System.Windows.Forms.ToolStripSeparator mnuFileSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExport;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExportRawData;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExportFormattedDatasheet;
        private System.Windows.Forms.SaveFileDialog diagExport;
        private System.Windows.Forms.ToolStripSeparator mnuDataSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuDataAutomaticallyUpdate;
        private System.Windows.Forms.ToolStripMenuItem mnuDataUpdateNow;
    }
}

