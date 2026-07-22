namespace ContractProcessor.Forms;

public partial class FieldSelectionForm : Form
{
    public List<string> SelectedFields { get; private set; } = new();

    public FieldSelectionForm(List<string> availableFields, List<string> currentSelection)
    {
        InitializeComponent();
        PopulateFields(availableFields, currentSelection);
    }

    private void PopulateFields(List<string> availableFields, List<string> currentSelection)
    {
        clbFields.Items.Clear();
        foreach (var field in availableFields)
        {
            var index = clbFields.Items.Add(field, currentSelection.Contains(field));
        }
    }

    private void btnConfirm_Click(object sender, EventArgs e)
    {
        SelectedFields = clbFields.CheckedItems
            .Cast<string>()
            .ToList();
        DialogResult = DialogResult.OK;
        Close();
    }

    private void btnSelectAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < clbFields.Items.Count; i++)
            clbFields.SetItemChecked(i, true);
    }

    private void btnDeselectAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < clbFields.Items.Count; i++)
            clbFields.SetItemChecked(i, false);
    }
}
