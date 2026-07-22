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
        lblRootFolder = new Label();
        txtRootFolder = new TextBox();
        btnBrowseFolder = new Button();
        lblCategories = new Label();
        lstCategories = new ListBox();
        txtNewCategory = new TextBox();
        btnAddCategory = new Button();
        btnRemoveCategory = new Button();
        btnSave = new Button();
        SuspendLayout();

        // lblRootFolder
        lblRootFolder.Text = "Root Folder:";
        lblRootFolder.Location = new Point(15, 15);
        lblRootFolder.AutoSize = true;

        // txtRootFolder
        txtRootFolder.Location = new Point(15, 35);
        txtRootFolder.Size = new Size(350, 23);
        txtRootFolder.ReadOnly = true;

        // btnBrowseFolder
        btnBrowseFolder.Text = "Browse...";
        btnBrowseFolder.Location = new Point(375, 34);
        btnBrowseFolder.Size = new Size(70, 25);
        btnBrowseFolder.Click += btnBrowseFolder_Click;

        // lblCategories
        lblCategories.Text = "Categories:";
        lblCategories.Location = new Point(15, 75);
        lblCategories.AutoSize = true;

        // lstCategories
        lstCategories.Location = new Point(15, 95);
        lstCategories.Size = new Size(200, 120);

        // txtNewCategory
        txtNewCategory.Location = new Point(15, 225);
        txtNewCategory.Size = new Size(120, 23);
        txtNewCategory.PlaceholderText = "New category...";

        // btnAddCategory
        btnAddCategory.Text = "Add";
        btnAddCategory.Location = new Point(145, 224);
        btnAddCategory.Size = new Size(60, 25);
        btnAddCategory.Click += btnAddCategory_Click;

        // btnRemoveCategory
        btnRemoveCategory.Text = "Remove";
        btnRemoveCategory.Location = new Point(220, 224);
        btnRemoveCategory.Size = new Size(70, 25);
        btnRemoveCategory.Click += btnRemoveCategory_Click;

        // btnSave
        btnSave.Text = "Save";
        btnSave.Location = new Point(340, 270);
        btnSave.Size = new Size(100, 35);
        btnSave.Click += btnSave_Click;

        // SettingsForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(460, 320);
        Controls.Add(lblRootFolder);
        Controls.Add(txtRootFolder);
        Controls.Add(btnBrowseFolder);
        Controls.Add(lblCategories);
        Controls.Add(lstCategories);
        Controls.Add(txtNewCategory);
        Controls.Add(btnAddCategory);
        Controls.Add(btnRemoveCategory);
        Controls.Add(btnSave);
        Text = "Settings";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblRootFolder;
    private TextBox txtRootFolder;
    private Button btnBrowseFolder;
    private Label lblCategories;
    private ListBox lstCategories;
    private TextBox txtNewCategory;
    private Button btnAddCategory;
    private Button btnRemoveCategory;
    private Button btnSave;
}
