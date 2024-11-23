

using Database.Models;
using DocumentFormat.OpenXml.Office.CustomUI;
using FormForLab.BisnesLogic;
using FormForLab.IModels.BindingModel;
using System.ComponentModel;

namespace FormForLab
{
    public partial class Category : Form
    {
        private readonly FullLogic logic;
        private BindingList<CategoryModel> categories;
        public Category()
        {
            InitializeComponent();
            logic = new();
            LoadData();
            dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns["Id"].Visible = false;
        }

        private void LoadData()
        {
            var values = logic.GetCategories();
            categories = new BindingList<CategoryModel>(values);
            dataGridView1.DataSource = categories;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                categories.Add(new CategoryModel { Name = "" });
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells["Name"];
                dataGridView1.BeginEdit(true);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранные записи?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        var value = dataGridView1.SelectedRows[0].Cells;
                        logic.DeleteCategory(new() { Id = (int)value["Id"].Value, Name = (string)value["Name"].Value });
                        LoadData();
                    }
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString()))
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
            }
            else
            {
                var category = dataGridView1.Rows[e.RowIndex];
                var i = category.Cells["Id"].Value;
                var n = category.Cells["Name"].Value;
                if ((int)i == 0)
                {
                    logic.CreateCategory(new() { Name = (string)category.Cells["Name"].Value });
                    LoadData();
                }
                else
                {
                    logic.UpdateCategory(new() { Id = (int)i, Name = (string)n });
                }
                
            }
        }
    }
}
