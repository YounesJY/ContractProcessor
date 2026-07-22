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
        clbFields = new CheckedListBox();
        btnConfirm = new Guna.UI2.WinForms.Guna2Button();
        btnCancel = new Guna.UI2.WinForms.Guna2Button();
        btnSelectAll = new Guna.UI2.WinForms.Guna2Button();
        btnDeselectAll = new Guna.UI2.WinForms.Guna2Button();
        lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
        SuspendLayout();

        // lblTitle
        lblTitle.Text = "Select fields to include:";
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        lblTitle.Height = 40;
        lblTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
        lblTitle.ForeColor = System.Drawing.Color.FromArgb(50, 50, 60);

        // clbFields
        clbFields.Dock = DockStyle.Fill;
        clbFields.CheckOnClick = true;
        clbFields.Font = new System.Drawing.Font("Segoe UI", 10F);
        clbFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

        // btnSelectAll
        btnSelectAll.Text = "Select All";
        btnSelectAll.Location = new Point(10, 10);
        btnSelectAll.Size = new Size(100, 32);
        btnSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSelectAll.FillColor = System.Drawing.Color.FromArgb(0, 122, 204);
        btnSelectAll.Font = new System.Drawing.Font("Segoe UI", 9F);
        btnSelectAll.ForeColor = System.Drawing.Color.White;
        btnSelectAll.BorderRadius = 6;
        btnSelectAll.Click += btnSelectAll_Click;

        // btnDeselectAll
        btnDeselectAll.Text = "Deselect All";
        btnDeselectAll.Location = new Point(120, 10);
        btnDeselectAll.Size = new Size(100, 32);
        btnDeselectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDeselectAll.FillColor = System.Drawing.Color.FromArgb(108, 117, 125);
        btnDeselectAll.Font = new System.Drawing.Font("Segoe UI", 9F);
        btnDeselectAll.ForeColor = System.Drawing.Color.White;
        btnDeselectAll.BorderRadius = 6;
        btnDeselectAll.Click += btnDeselectAll_Click;

        // btnConfirm
        btnConfirm.Text = "Confirm";
        btnConfirm.Location = new Point(280, 10);
        btnConfirm.Size = new Size(90, 32);
        btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnConfirm.FillColor = System.Drawing.Color.FromArgb(40, 167, 69);
        btnConfirm.Font = new System.Drawing.Font("Segoe UI", 9F);
        btnConfirm.ForeColor = System.Drawing.Color.White;
        btnConfirm.BorderRadius = 6;
        btnConfirm.Click += btnConfirm_Click;

        // btnCancel
        btnCancel.Text = "Cancel";
        btnCancel.Location = new Point(380, 10);
        btnCancel.Size = new Size(90, 32);
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.FillColor = System.Drawing.Color.FromArgb(220, 53, 69);
        btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
        btnCancel.ForeColor = System.Drawing.Color.White;
        btnCancel.BorderRadius = 6;
        btnCancel.Click += btnCancel_Click;

        // FieldSelectionForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(480, 400);
        BackColor = System.Drawing.Color.White;
        Controls.Add(clbFields);
        Controls.Add(lblTitle);
        Controls.Add(btnSelectAll);
        Controls.Add(btnDeselectAll);
        Controls.Add(btnConfirm);
        Controls.Add(btnCancel);
        Text = "Select Fields";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        ResumeLayout(false);
    }

    private CheckedListBox clbFields;
    private Guna.UI2.WinForms.Guna2Button btnConfirm;
    private Guna.UI2.WinForms.Guna2Button btnCancel;
    private Guna.UI2.WinForms.Guna2Button btnSelectAll;
    private Guna.UI2.WinForms.Guna2Button btnDeselectAll;
    private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
}
