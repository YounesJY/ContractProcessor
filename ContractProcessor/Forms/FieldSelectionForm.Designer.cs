namespace ContractProcessor.Forms;

partial class FieldSelectionForm
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
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
        clbFields = new CheckedListBox();
        btnConfirm = new Guna.UI2.WinForms.Guna2Button();
        btnCancel = new Guna.UI2.WinForms.Guna2Button();
        btnSelectAll = new Guna.UI2.WinForms.Guna2Button();
        btnDeselectAll = new Guna.UI2.WinForms.Guna2Button();
        lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
        SuspendLayout();
        // 
        // clbFields
        // 
        clbFields.BorderStyle = BorderStyle.FixedSingle;
        clbFields.CheckOnClick = true;
        clbFields.Dock = DockStyle.Bottom;
        clbFields.Font = new Font("Segoe UI", 10F);
        clbFields.Location = new Point(0, 58);
        clbFields.Name = "clbFields";
        clbFields.Size = new Size(480, 342);
        clbFields.TabIndex = 0;
        // 
        // btnConfirm
        // 
        btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnConfirm.BorderRadius = 6;
        btnConfirm.CustomizableEdges = customizableEdges1;
        btnConfirm.FillColor = Color.FromArgb(40, 167, 69);
        btnConfirm.Font = new Font("Segoe UI", 9F);
        btnConfirm.ForeColor = Color.White;
        btnConfirm.Location = new Point(280, 10);
        btnConfirm.Name = "btnConfirm";
        btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
        btnConfirm.Size = new Size(90, 32);
        btnConfirm.TabIndex = 4;
        btnConfirm.Text = "Confirm";
        btnConfirm.Click += btnConfirm_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.BorderRadius = 6;
        btnCancel.CustomizableEdges = customizableEdges3;
        btnCancel.FillColor = Color.FromArgb(220, 53, 69);
        btnCancel.Font = new Font("Segoe UI", 9F);
        btnCancel.ForeColor = Color.White;
        btnCancel.Location = new Point(380, 10);
        btnCancel.Name = "btnCancel";
        btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges4;
        btnCancel.Size = new Size(90, 32);
        btnCancel.TabIndex = 5;
        btnCancel.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        // 
        // btnSelectAll
        // 
        btnSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSelectAll.BorderRadius = 6;
        btnSelectAll.CustomizableEdges = customizableEdges5;
        btnSelectAll.FillColor = Color.FromArgb(0, 122, 204);
        btnSelectAll.Font = new Font("Segoe UI", 9F);
        btnSelectAll.ForeColor = Color.White;
        btnSelectAll.Location = new Point(10, 10);
        btnSelectAll.Name = "btnSelectAll";
        btnSelectAll.ShadowDecoration.CustomizableEdges = customizableEdges6;
        btnSelectAll.Size = new Size(100, 32);
        btnSelectAll.TabIndex = 2;
        btnSelectAll.Text = "Select All";
        btnSelectAll.Click += btnSelectAll_Click;
        // 
        // btnDeselectAll
        // 
        btnDeselectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDeselectAll.BorderRadius = 6;
        btnDeselectAll.CustomizableEdges = customizableEdges7;
        btnDeselectAll.FillColor = Color.FromArgb(108, 117, 125);
        btnDeselectAll.Font = new Font("Segoe UI", 9F);
        btnDeselectAll.ForeColor = Color.White;
        btnDeselectAll.Location = new Point(120, 10);
        btnDeselectAll.Name = "btnDeselectAll";
        btnDeselectAll.ShadowDecoration.CustomizableEdges = customizableEdges8;
        btnDeselectAll.Size = new Size(100, 32);
        btnDeselectAll.TabIndex = 3;
        btnDeselectAll.Text = "Deselect All";
        btnDeselectAll.Click += btnDeselectAll_Click;
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.Transparent;
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(50, 50, 60);
        lblTitle.Location = new Point(0, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(480, 22);
        lblTitle.TabIndex = 1;
        lblTitle.Text = "Select fields to include:";
        lblTitle.TextAlignment = ContentAlignment.MiddleCenter;
        // 
        // FieldSelectionForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(480, 400);
        Controls.Add(clbFields);
        Controls.Add(lblTitle);
        Controls.Add(btnSelectAll);
        Controls.Add(btnDeselectAll);
        Controls.Add(btnConfirm);
        Controls.Add(btnCancel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FieldSelectionForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Select Fields";
        ResumeLayout(false);
        PerformLayout();
    }

    private CheckedListBox clbFields;
    private Guna.UI2.WinForms.Guna2Button btnConfirm;
    private Guna.UI2.WinForms.Guna2Button btnCancel;
    private Guna.UI2.WinForms.Guna2Button btnSelectAll;
    private Guna.UI2.WinForms.Guna2Button btnDeselectAll;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
}
