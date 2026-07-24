namespace ContractProcessor.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        dgvContracts = new Guna.UI2.WinForms.Guna2DataGridView();
        btnUpload = new Guna.UI2.WinForms.Guna2Button();
        btnViewText = new Guna.UI2.WinForms.Guna2Button();
        btnManualExtract = new Guna.UI2.WinForms.Guna2Button();
        btnExport = new Guna.UI2.WinForms.Guna2Button();
        btnSelectFields = new Guna.UI2.WinForms.Guna2Button();
        btnDelete = new Guna.UI2.WinForms.Guna2Button();
        btnSettings = new Guna.UI2.WinForms.Guna2Button();
        cmbFilter = new Guna.UI2.WinForms.Guna2ComboBox();
        lblFilter = new Guna.UI2.WinForms.Guna2HtmlLabel();
        panelTop = new Guna.UI2.WinForms.Guna2Panel();
        ((System.ComponentModel.ISupportInitialize)dgvContracts).BeginInit();
        panelTop.SuspendLayout();
        SuspendLayout();
        // 
        // dgvContracts
        // 
        dgvContracts.AllowUserToAddRows = false;
        dgvContracts.AllowUserToDeleteRows = false;
        dgvContracts.BackgroundColor = Color.Silver;
        dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 240, 245);
        dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dataGridViewCellStyle1.ForeColor = Color.White;
        dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
        dgvContracts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        dgvContracts.ColumnHeadersHeight = 15;
        dgvContracts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.LightGray;
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(71, 69, 94);
        dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(220, 235, 255);
        dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(71, 69, 94);
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
        dgvContracts.DefaultCellStyle = dataGridViewCellStyle2;
        dgvContracts.Dock = DockStyle.Fill;
        dgvContracts.GridColor = Color.DimGray;
        dgvContracts.Location = new Point(0, 60);
        dgvContracts.Name = "dgvContracts";
        dgvContracts.ReadOnly = true;
        dgvContracts.RowHeadersVisible = false;
        dgvContracts.Size = new Size(860, 460);
        dgvContracts.TabIndex = 0;
        dgvContracts.ThemeStyle.BackColor = Color.Silver;
        dgvContracts.ThemeStyle.GridColor = Color.DimGray;
        dgvContracts.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(50, 50, 60);
        dgvContracts.ThemeStyle.HeaderStyle.ForeColor = Color.White;
        dgvContracts.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvContracts.ThemeStyle.ReadOnly = true;
        dgvContracts.ThemeStyle.RowsStyle.BackColor = Color.LightGray;
        dgvContracts.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
        dgvContracts.ThemeStyle.RowsStyle.Height = 25;
        dgvContracts.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
        // 
        // btnUpload
        // 
        btnUpload.BorderRadius = 6;
        btnUpload.CustomizableEdges = customizableEdges1;
        btnUpload.FillColor = Color.FromArgb(0, 122, 204);
        btnUpload.Font = new Font("Segoe UI", 9F);
        btnUpload.ForeColor = Color.White;
        btnUpload.Location = new Point(10, 14);
        btnUpload.Name = "btnUpload";
        btnUpload.ShadowDecoration.CustomizableEdges = customizableEdges2;
        btnUpload.Size = new Size(120, 32);
        btnUpload.TabIndex = 0;
        btnUpload.Text = "Upload PDFs";
        btnUpload.Click += btnUpload_Click;
        // 
        // btnViewText
        // 
        btnViewText.BorderRadius = 6;
        btnViewText.FillColor = Color.FromArgb(111, 66, 193);
        btnViewText.Font = new Font("Segoe UI", 9F);
        btnViewText.ForeColor = Color.White;
        btnViewText.Location = new Point(140, 14);
        btnViewText.Name = "btnViewText";
        btnViewText.Size = new Size(100, 32);
        btnViewText.TabIndex = 7;
        btnViewText.Text = "View Text";
        btnViewText.Click += btnViewText_Click;
        // 
        // btnManualExtract
        // 
        btnManualExtract.BorderRadius = 6;
        btnManualExtract.FillColor = Color.FromArgb(255, 193, 7);
        btnManualExtract.Font = new Font("Segoe UI", 9F);
        btnManualExtract.ForeColor = Color.FromArgb(50, 50, 60);
        btnManualExtract.Location = new Point(250, 14);
        btnManualExtract.Name = "btnManualExtract";
        btnManualExtract.Size = new Size(120, 32);
        btnManualExtract.TabIndex = 8;
        btnManualExtract.Text = "Manual Extract";
        btnManualExtract.Click += btnManualExtract_Click;
        // 
        // btnExport
        // 
        btnExport.BorderRadius = 6;
        btnExport.CustomizableEdges = customizableEdges3;
        btnExport.FillColor = Color.FromArgb(40, 167, 69);
        btnExport.Font = new Font("Segoe UI", 9F);
        btnExport.ForeColor = Color.White;
        btnExport.Location = new Point(570, 14);
        btnExport.Name = "btnExport";
        btnExport.Size = new Size(80, 32);
        btnExport.TabIndex = 3;
        btnExport.Text = "Export";
        btnExport.Click += btnExport_Click;
        // 
        // btnSelectFields
        // 
        btnSelectFields.BorderRadius = 6;
        btnSelectFields.CustomizableEdges = customizableEdges5;
        btnSelectFields.FillColor = Color.FromArgb(80, 80, 90);
        btnSelectFields.Font = new Font("Segoe UI", 9F);
        btnSelectFields.ForeColor = Color.White;
        btnSelectFields.Location = new Point(380, 14);
        btnSelectFields.Name = "btnSelectFields";
        btnSelectFields.Size = new Size(100, 32);
        btnSelectFields.TabIndex = 1;
        btnSelectFields.Text = "Select Fields";
        btnSelectFields.Click += btnSelectFields_Click;
        // 
        // btnDelete
        // 
        btnDelete.BorderRadius = 6;
        btnDelete.CustomizableEdges = customizableEdges7;
        btnDelete.FillColor = Color.FromArgb(220, 53, 69);
        btnDelete.Font = new Font("Segoe UI", 9F);
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(490, 14);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(70, 32);
        btnDelete.TabIndex = 2;
        btnDelete.Text = "Delete";
        btnDelete.Click += btnDelete_Click;
        // 
        // btnSettings
        // 
        btnSettings.BorderRadius = 6;
        btnSettings.CustomizableEdges = customizableEdges9;
        btnSettings.FillColor = Color.FromArgb(108, 117, 125);
        btnSettings.Font = new Font("Segoe UI", 9F);
        btnSettings.ForeColor = Color.White;
        btnSettings.Location = new Point(660, 14);
        btnSettings.Name = "btnSettings";
        btnSettings.Size = new Size(80, 32);
        btnSettings.TabIndex = 4;
        btnSettings.Text = "Settings";
        btnSettings.Click += btnSettings_Click;
        // 
        // cmbFilter
        // 
        cmbFilter.BackColor = Color.Transparent;
        cmbFilter.BorderRadius = 6;
        cmbFilter.CustomizableEdges = customizableEdges11;
        cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
        cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFilter.FocusedColor = Color.Empty;
        cmbFilter.Font = new Font("Segoe UI", 9F);
        cmbFilter.ForeColor = Color.FromArgb(68, 88, 112);
        cmbFilter.ItemHeight = 30;
        cmbFilter.Location = new Point(645, 14);
        cmbFilter.Name = "cmbFilter";
        cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges12;
        cmbFilter.Size = new Size(130, 36);
        cmbFilter.TabIndex = 6;
        cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;
        // 
        // lblFilter
        // 
        lblFilter.BackColor = Color.Transparent;
        lblFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblFilter.Location = new Point(600, 18);
        lblFilter.Name = "lblFilter";
        lblFilter.Size = new Size(35, 17);
        lblFilter.TabIndex = 5;
        lblFilter.Text = "Filter:";
        // 
        // panelTop
        // 
        panelTop.Controls.Add(btnUpload);
        panelTop.Controls.Add(btnViewText);
        panelTop.Controls.Add(btnManualExtract);
        panelTop.Controls.Add(btnSelectFields);
        panelTop.Controls.Add(btnDelete);
        panelTop.Controls.Add(btnExport);
        panelTop.Controls.Add(btnSettings);
        panelTop.Controls.Add(lblFilter);
        panelTop.Controls.Add(cmbFilter);
        panelTop.CustomizableEdges = customizableEdges13;
        panelTop.Dock = DockStyle.Top;
        panelTop.FillColor = Color.FromArgb(240, 240, 245);
        panelTop.Location = new Point(0, 0);
        panelTop.Name = "panelTop";
        panelTop.Padding = new Padding(10);
        panelTop.ShadowDecoration.CustomizableEdges = customizableEdges14;
        panelTop.Size = new Size(860, 60);
        panelTop.TabIndex = 1;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(860, 520);
        Controls.Add(dgvContracts);
        Controls.Add(panelTop);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "ContractProcessor";
        Load += MainForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvContracts).EndInit();
        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        ResumeLayout(false);
    }

    private Guna.UI2.WinForms.Guna2DataGridView dgvContracts;
    private Guna.UI2.WinForms.Guna2Button btnUpload;
    private Guna.UI2.WinForms.Guna2Button btnViewText;
    private Guna.UI2.WinForms.Guna2Button btnManualExtract;
    private Guna.UI2.WinForms.Guna2Button btnExport;
    private Guna.UI2.WinForms.Guna2Button btnSelectFields;
    private Guna.UI2.WinForms.Guna2Button btnDelete;
    private Guna.UI2.WinForms.Guna2Button btnSettings;
    private Guna.UI2.WinForms.Guna2ComboBox cmbFilter;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblFilter;
    private Guna.UI2.WinForms.Guna2Panel panelTop;
}
