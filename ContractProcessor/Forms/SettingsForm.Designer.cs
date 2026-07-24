namespace ContractProcessor.Forms;

partial class SettingsForm
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
        lblRootFolder = new Guna.UI2.WinForms.Guna2HtmlLabel();
        txtRootFolder = new Guna.UI2.WinForms.Guna2TextBox();
        btnBrowseFolder = new Guna.UI2.WinForms.Guna2Button();
        lblCategories = new Guna.UI2.WinForms.Guna2HtmlLabel();
        lstCategories = new ListBox();
        txtNewCategory = new Guna.UI2.WinForms.Guna2TextBox();
        btnAddCategory = new Guna.UI2.WinForms.Guna2Button();
        btnRemoveCategory = new Guna.UI2.WinForms.Guna2Button();
        btnSave = new Guna.UI2.WinForms.Guna2Button();
        lblAI = new Guna.UI2.WinForms.Guna2HtmlLabel();
        chkUseAI = new Guna.UI2.WinForms.Guna2ToggleSwitch();
        lblModel = new Guna.UI2.WinForms.Guna2HtmlLabel();
        cmbModel = new Guna.UI2.WinForms.Guna2ComboBox();
        lblCloudAI = new Guna.UI2.WinForms.Guna2HtmlLabel();
        chkUseCloudAI = new Guna.UI2.WinForms.Guna2ToggleSwitch();
        lblCloudApiKey = new Guna.UI2.WinForms.Guna2HtmlLabel();
        txtOpenRouterApiKey = new Guna.UI2.WinForms.Guna2TextBox();
        lblCloudModel = new Guna.UI2.WinForms.Guna2HtmlLabel();
        cmbCloudModel = new Guna.UI2.WinForms.Guna2ComboBox();
        SuspendLayout();

        // lblRootFolder
        lblRootFolder.Text = "Root Folder:";
        lblRootFolder.Location = new Point(20, 18);
        lblRootFolder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

        // txtRootFolder
        txtRootFolder.Location = new Point(20, 42);
        txtRootFolder.Size = new Size(380, 32);
        txtRootFolder.ReadOnly = true;
        txtRootFolder.BorderRadius = 6;
        txtRootFolder.Font = new System.Drawing.Font("Segoe UI", 9F);

        // btnBrowseFolder
        btnBrowseFolder.Text = "Browse...";
        btnBrowseFolder.Location = new Point(410, 42);
        btnBrowseFolder.Size = new Size(80, 32);
        btnBrowseFolder.FillColor = System.Drawing.Color.FromArgb(0, 122, 204);
        btnBrowseFolder.Font = new System.Drawing.Font("Segoe UI", 9F);
        btnBrowseFolder.ForeColor = System.Drawing.Color.White;
        btnBrowseFolder.BorderRadius = 6;
        btnBrowseFolder.Click += btnBrowseFolder_Click;

        // lblCategories
        lblCategories.Text = "Categories:";
        lblCategories.Location = new Point(20, 88);
        lblCategories.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

        // lstCategories
        lstCategories.Location = new Point(20, 112);
        lstCategories.Size = new Size(220, 130);
        lstCategories.Font = new System.Drawing.Font("Segoe UI", 9F);
        lstCategories.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

        // txtNewCategory
        txtNewCategory.Location = new Point(20, 255);
        txtNewCategory.Size = new Size(140, 32);
        txtNewCategory.PlaceholderText = "New category...";
        txtNewCategory.BorderRadius = 6;
        txtNewCategory.Font = new System.Drawing.Font("Segoe UI", 9F);

        // btnAddCategory
        btnAddCategory.Text = "Add";
        btnAddCategory.Location = new Point(170, 255);
        btnAddCategory.Size = new Size(60, 32);
        btnAddCategory.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
        btnAddCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
        btnAddCategory.ForeColor = System.Drawing.Color.White;
        btnAddCategory.BorderRadius = 6;
        btnAddCategory.Click += btnAddCategory_Click;

        // btnRemoveCategory
        btnRemoveCategory.Text = "Remove";
        btnRemoveCategory.Location = new Point(240, 255);
        btnRemoveCategory.Size = new Size(70, 32);
        btnRemoveCategory.FillColor = System.Drawing.Color.FromArgb(220, 53, 69);
        btnRemoveCategory.Font = new System.Drawing.Font("Segoe UI", 9F);
        btnRemoveCategory.ForeColor = System.Drawing.Color.White;
        btnRemoveCategory.BorderRadius = 6;
        btnRemoveCategory.Click += btnRemoveCategory_Click;

        // btnSave
        btnSave.Text = "Save";
        btnSave.Location = new Point(350, 420);
        btnSave.Size = new Size(120, 38);
        btnSave.FillColor = System.Drawing.Color.FromArgb(0, 122, 204);
        btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        btnSave.ForeColor = System.Drawing.Color.White;
        btnSave.BorderRadius = 6;
        btnSave.Click += btnSave_Click;

        // lblAI
        lblAI.Text = "AI Extraction (Ollama):";
        lblAI.Location = new Point(20, 310);
        lblAI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

        // chkUseAI
        chkUseAI.Location = new Point(200, 312);
        chkUseAI.Size = new Size(50, 25);
        chkUseAI.Checked = true;

        // lblModel
        lblModel.Text = "Model:";
        lblModel.Location = new Point(280, 312);
        lblModel.Font = new System.Drawing.Font("Segoe UI", 9F);

        // cmbModel
        cmbModel.Location = new Point(340, 308);
        cmbModel.Size = new Size(150, 32);
        cmbModel.Font = new System.Drawing.Font("Segoe UI", 9F);
        cmbModel.BorderRadius = 6;
        cmbModel.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbModel.Items.AddRange(new object[] { "llama3.2", "mistral", "phi3", "gemma2" });
        cmbModel.SelectedIndex = 0;

        // lblCloudAI
        lblCloudAI.Text = "Cloud AI Fallback (OpenRouter):";
        lblCloudAI.Location = new Point(20, 360);
        lblCloudAI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

        // chkUseCloudAI
        chkUseCloudAI.Location = new Point(280, 362);
        chkUseCloudAI.Size = new Size(50, 25);
        chkUseCloudAI.Checked = false;

        // lblCloudApiKey
        lblCloudApiKey.Text = "API Key:";
        lblCloudApiKey.Location = new Point(20, 395);
        lblCloudApiKey.Font = new System.Drawing.Font("Segoe UI", 9F);

        // txtOpenRouterApiKey
        txtOpenRouterApiKey.Location = new Point(20, 418);
        txtOpenRouterApiKey.Size = new Size(470, 32);
        txtOpenRouterApiKey.PlaceholderText = "Enter OpenRouter API key (get from openrouter.ai)";
        txtOpenRouterApiKey.BorderRadius = 6;
        txtOpenRouterApiKey.Font = new System.Drawing.Font("Segoe UI", 9F);
        txtOpenRouterApiKey.UseSystemPasswordChar = true;

        // lblCloudModel
        lblCloudModel.Text = "Model:";
        lblCloudModel.Location = new Point(20, 455);
        lblCloudModel.Font = new System.Drawing.Font("Segoe UI", 9F);

        // cmbCloudModel
        cmbCloudModel.Location = new Point(80, 451);
        cmbCloudModel.Size = new Size(410, 32);
        cmbCloudModel.Font = new System.Drawing.Font("Segoe UI", 9F);
        cmbCloudModel.BorderRadius = 6;
        cmbCloudModel.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbCloudModel.Items.AddRange(new object[] { 
            "meta-llama/llama-3.1-8b-instruct",
            "mistralai/mistral-7b-instruct",
            "microsoft/phi-3-mini-128k-instruct",
            "qwen/qwen-2-7b-instruct",
            "google/gemma-2-9b-it",
            "openai/gpt-4o-mini",
            "deepseek/deepseek-chat",
            "anthropic/claude-3.5-sonnet"
        });
        cmbCloudModel.SelectedIndex = 0;

        // SettingsForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(510, 510);
        BackColor = System.Drawing.Color.White;
        Controls.Add(lblRootFolder);
        Controls.Add(txtRootFolder);
        Controls.Add(btnBrowseFolder);
        Controls.Add(lblCategories);
        Controls.Add(lstCategories);
        Controls.Add(txtNewCategory);
        Controls.Add(btnAddCategory);
        Controls.Add(btnRemoveCategory);
        Controls.Add(btnSave);
        Controls.Add(lblAI);
        Controls.Add(chkUseAI);
        Controls.Add(lblModel);
        Controls.Add(cmbModel);
        Controls.Add(lblCloudAI);
        Controls.Add(chkUseCloudAI);
        Controls.Add(lblCloudApiKey);
        Controls.Add(txtOpenRouterApiKey);
        Controls.Add(lblCloudModel);
        Controls.Add(cmbCloudModel);
        Text = "Settings";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        ResumeLayout(false);
        PerformLayout();
    }

    private Guna.UI2.WinForms.Guna2HtmlLabel lblRootFolder;
    private Guna.UI2.WinForms.Guna2TextBox txtRootFolder;
    private Guna.UI2.WinForms.Guna2Button btnBrowseFolder;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblCategories;
    private ListBox lstCategories;
    private Guna.UI2.WinForms.Guna2TextBox txtNewCategory;
    private Guna.UI2.WinForms.Guna2Button btnAddCategory;
    private Guna.UI2.WinForms.Guna2Button btnRemoveCategory;
    private Guna.UI2.WinForms.Guna2Button btnSave;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblAI;
    private Guna.UI2.WinForms.Guna2ToggleSwitch chkUseAI;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblModel;
    private Guna.UI2.WinForms.Guna2ComboBox cmbModel;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblCloudAI;
    private Guna.UI2.WinForms.Guna2ToggleSwitch chkUseCloudAI;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblCloudApiKey;
    private Guna.UI2.WinForms.Guna2TextBox txtOpenRouterApiKey;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblCloudModel;
    private Guna.UI2.WinForms.Guna2ComboBox cmbCloudModel;
}
