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
        btnConfirm = new Button();
        btnCancel = new Button();
        btnSelectAll = new Button();
        btnDeselectAll = new Button();
        lblTitle = new Label();
        SuspendLayout();

        // lblTitle
        lblTitle.Text = "Select fields to include:";
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTitle.Height = 30;
        lblTitle.TextAlign = ContentAlignment.MiddleCenter;

        // clbFields
        clbFields.Dock = DockStyle.Fill;
        clbFields.CheckOnClick = true;
        clbFields.Font = new Font("Segoe UI", 10F);

        // btnSelectAll
        btnSelectAll.Text = "Select All";
        btnSelectAll.Location = new Point(10, 10);
        btnSelectAll.Size = new Size(90, 28);
        btnSelectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnSelectAll.Click += btnSelectAll_Click;

        // btnDeselectAll
        btnDeselectAll.Text = "Deselect All";
        btnDeselectAll.Location = new Point(110, 10);
        btnDeselectAll.Size = new Size(90, 28);
        btnDeselectAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDeselectAll.Click += btnDeselectAll_Click;

        // btnConfirm
        btnConfirm.Text = "Confirm";
        btnConfirm.Location = new Point(260, 10);
        btnConfirm.Size = new Size(80, 28);
        btnConfirm.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnConfirm.Click += btnConfirm_Click;

        // btnCancel
        btnCancel.Text = "Cancel";
        btnCancel.Location = new Point(350, 10);
        btnCancel.Size = new Size(80, 28);
        btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

        // FieldSelectionForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(440, 350);
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
    private Button btnConfirm;
    private Button btnCancel;
    private Button btnSelectAll;
    private Button btnDeselectAll;
    private Label lblTitle;
}
