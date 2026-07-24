namespace ContractProcessor.Forms;

partial class ManualExtractForm
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
        txtPdfText = new TextBox();
        panelFields = new Panel();
        btnConfirm = new Guna.UI2.WinForms.Guna2Button();
        btnCancel = new Guna.UI2.WinForms.Guna2Button();
        lblLeft = new Label();
        lblRight = new Label();
        splitContainer = new SplitContainer();
        ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
        splitContainer.SuspendLayout();
        SuspendLayout();

        // splitContainer
        splitContainer.Dock = DockStyle.Fill;
        splitContainer.SplitterWidth = 6;

        // lblLeft
        lblLeft.Text = "PDF Text (read only):";
        lblLeft.Dock = DockStyle.Top;
        lblLeft.Height = 25;
        lblLeft.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblLeft.ForeColor = Color.FromArgb(50, 50, 60);
        lblLeft.BackColor = Color.FromArgb(240, 240, 245);
        lblLeft.TextAlign = ContentAlignment.MiddleLeft;
        lblLeft.Padding = new Padding(5, 0, 0, 0);

        // txtPdfText
        txtPdfText.Dock = DockStyle.Fill;
        txtPdfText.Multiline = true;
        txtPdfText.ReadOnly = true;
        txtPdfText.ScrollBars = ScrollBars.Both;
        txtPdfText.Font = new Font("Consolas", 9F);
        txtPdfText.BackColor = Color.White;
        txtPdfText.BorderStyle = BorderStyle.FixedSingle;

        // lblRight
        lblRight.Text = "Extract Fields (fill in manually):";
        lblRight.Dock = DockStyle.Top;
        lblRight.Height = 25;
        lblRight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblRight.ForeColor = Color.FromArgb(50, 50, 60);
        lblRight.BackColor = Color.FromArgb(240, 240, 245);
        lblRight.TextAlign = ContentAlignment.MiddleLeft;
        lblRight.Padding = new Padding(5, 0, 0, 0);

        // panelFields
        panelFields.Dock = DockStyle.Fill;
        panelFields.AutoScroll = true;
        panelFields.BackColor = Color.White;
        panelFields.Padding = new Padding(5);

        // Buttons panel
        var panelButtons = new Panel();
        panelButtons.Dock = DockStyle.Bottom;
        panelButtons.Height = 50;
        panelButtons.BackColor = Color.FromArgb(245, 245, 250);

        // btnConfirm
        btnConfirm.Text = "Confirm";
        btnConfirm.Location = new Point(200, 10);
        btnConfirm.Size = new Size(100, 32);
        btnConfirm.FillColor = Color.FromArgb(40, 167, 69);
        btnConfirm.Font = new Font("Segoe UI", 9F);
        btnConfirm.ForeColor = Color.White;
        btnConfirm.BorderRadius = 6;
        btnConfirm.Click += btnConfirm_Click;

        // btnCancel
        btnCancel.Text = "Cancel";
        btnCancel.Location = new Point(310, 10);
        btnCancel.Size = new Size(80, 32);
        btnCancel.FillColor = Color.FromArgb(220, 53, 69);
        btnCancel.Font = new Font("Segoe UI", 9F);
        btnCancel.ForeColor = Color.White;
        btnCancel.BorderRadius = 6;
        btnCancel.Click += btnCancel_Click;

        panelButtons.Controls.Add(btnConfirm);
        panelButtons.Controls.Add(btnCancel);

        // Left panel: text viewer
        var leftPanel = new Panel();
        leftPanel.Dock = DockStyle.Fill;
        leftPanel.Controls.Add(txtPdfText);
        leftPanel.Controls.Add(lblLeft);

        // Right panel: fields
        var rightPanel = new Panel();
        rightPanel.Dock = DockStyle.Fill;
        rightPanel.Controls.Add(panelFields);
        rightPanel.Controls.Add(lblRight);

        splitContainer.Panel1.Controls.Add(leftPanel);
        splitContainer.Panel2.Controls.Add(rightPanel);

        // ManualExtractForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(950, 600);
        BackColor = Color.White;
        Controls.Add(splitContainer);
        Controls.Add(panelButtons);
        Text = "Manual Field Extraction";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(800, 500);

        ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
        splitContainer.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TextBox txtPdfText;
    private Panel panelFields;
    private Guna.UI2.WinForms.Guna2Button btnConfirm;
    private Guna.UI2.WinForms.Guna2Button btnCancel;
    private Label lblLeft;
    private Label lblRight;
    private SplitContainer splitContainer;
}
