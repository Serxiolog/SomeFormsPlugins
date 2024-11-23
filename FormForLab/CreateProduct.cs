using FormForLab.BisnesLogic;
using FormForLab.IModels.BindingModel;
using FormForLab.IModels.FullModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormForLab
{
    public partial class CreateProduct : Form
    {
        private List<CategoryModel>? categories;
        private readonly FullLogic logic;
        private int? id;
        public int Id { set { id = value; } }
        public CreateProduct()
        {
            InitializeComponent();
            logic = new();
            LoadData();
        }

        private void LoadData()
        {
            categories = logic.GetCategories();
            if (categories == null)
            {
                return;
            }
            comboBoxCustom1.Clear();
            foreach (var i in categories)
            {
                comboBoxCustom1.Add(i.Name);
            }

        }
        private void LoadProduct()
        {
            if (id != null)
            {
                var product = logic.GetProduct((int)id);
                textBox1.Text = product.Title;
                textBox2.Text = product.Description;
                int cnt;
                if (!int.TryParse(product.Count, out cnt))
                {
                    TextBox3.Value = null;
                }
                else
                {
                    TextBox3.Value = cnt;
                }
                comboBoxCustom1.selectedValue = product.Category;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (categories?.Count == 0)
            {
                MessageBox.Show("Несуществует категорий");
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text)
                || string.IsNullOrEmpty(comboBoxCustom1.selectedValue))
            {
                MessageBox.Show("Некорректные данные");
                return;
            }
            try
            {
                var cnt = (int?)TextBox3.Value;
                if (id == null)
                {
                   
                    int categoryId = categories.FirstOrDefault(c => c.Name == comboBoxCustom1.selectedValue)!.Id;
                    var product = new ProductModel() { Title = textBox1.Text, Count = cnt, Description = textBox2.Text, CategoryId = categoryId };
                    logic.CreateProduct(product);
                }
                else
                {
                    int categoryId = categories.FirstOrDefault(c => c.Name == comboBoxCustom1.selectedValue)!.Id;
                    var product = new ProductModel() { Title = textBox1.Text, Count = cnt, Description = textBox2.Text, CategoryId = categoryId, Id = (int)id };
                    logic.UpdateProduct(product);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void CreateProduct_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
