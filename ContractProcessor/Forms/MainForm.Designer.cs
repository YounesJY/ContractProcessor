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
        dgvContracts = new DataGridView();
        btnUpload = new Button();
        btnExport = new Button();
        btnSelectFields = new Button();
        btnDelete = new Button();
        btnSettings = new Button();
        cmbFilter = new ComboBox();
        lblFilter = new Label();
        panelTop = new Panel();
        panelBottom = new Panel();

        ((System.ComponentModel.ISupportInitialize)(dgvContracts)).BeginInit();
        panelTop.SuspendLayout();
        panelBottom.SuspendLayout();
        SuspendLayout();

        // panelTop
        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 50;
        panelTop.Padding = new Padding(10);
        panelTop.Controls.Add(btnUpload);
        panelTop.Controls.Add(btnSelectFields);
        panelTop.Controls.Add(btnDelete);
        panelTop.Controls.Add(btnExport);
        panelTop.Controls.Add(btnSettings);
        panelTop.Controls.Add(lblFilter);
        panelTop.Controls.Add(cmbFilter);

        // btnUpload
        btnUpload.Text = "Upload PDFs";
        btnUpload.Location = new Point(10, 12);
        btnUpload.Size = new Size(120, 30);
        btnUpload.Click += btnUpload_Click;

        // btnSelectFields
        btnSelectFields.Text = "Select Fields";
        btnSelectFields.Location = new Point(140, 12);
        btnSelectFields.Size = new Size(120, 30);
        btnSelectFields.Click += btnSelectFields_Click;

        // btnDelete
        btnDelete.Text = "Delete";
        btnDelete.Location = new Point(270, 12);
        btnDelete.Size = new Size(80, 30);
        btnDelete.Click += btnDelete_Click;

        // btnExport
        btnExport.Text = "Export";
        btnExport.Location = new Point(360, 12);
        btnExport.Size = new Size(100, 30);
        btnExport.Click += btnExport_Click;

        // btnSettings
        btnSettings.Text = "Settings";
        btnSettings.Location = new Point(470, 12);
        btnSettings.Size = new Size(80, 30);
        btnSettings.Click += btnSettings_Click;

        // lblFilter
        lblFilter.Text = "Filter:";
        lblFilter.Location = new Point(600, 17);
        lblFilter.AutoSize = true;

        // cmbFilter
        cmbFilter.Location = new Point(640, 14);
        cmbFilter.Size = new Size(120, 28);
        cmbFilter.SelectedIndexChanged += cmbFilter_SelectedIndexChanged;

        // dgvContracts
        dgvContracts.Dock = DockStyle.Fill;
        dgvContracts.ReadOnly = true;
        dgvContracts.AllowUserToAddRows = false;
        dgvContracts.AllowUserToDeleteRows = false;
        dgvContracts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvContracts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // MainForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(dgvContracts);
        Controls.Add(panelTop);
        Text = "ContractProcessor";
        StartPosition = FormStartPosition.CenterScreen;
        Load += MainForm_Load;

        ((System.ComponentModel.ISupportInitialize)(dgvContracts)).EndInit();
        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        ResumeLayout(false);
    }

    private DataGridView dgvContracts;
    private Button btnUpload;
    private Button btnExport;
    private Button btnSelectFields;
    private Button btnDelete;
    private Button btnSettings;
    private ComboBox cmbFilter;
    private Label lblFilter;
    private Panel panelTop;
    private Panel panelBottom;
}
