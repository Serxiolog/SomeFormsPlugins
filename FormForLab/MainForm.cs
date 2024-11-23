using FormForLab.BisnesLogic;
using FormForLab.IModels.ViewModel;


namespace FormForLab
{
    public partial class MainForm : Form
    {
        internal readonly FullLogic logic;
        private List<ProductViewModel> products = new();
        private bool first = true;
        public MainForm()
        {
            InitializeComponent();
            logic = new();
            LoadData();
        }
        internal void LoadData()
        {
            valueTree.ClearTree();
            valueTree.Hierarchy = new() { "Category", "Count", "Id", "Title" };
            var values = logic.GetProducts();
            if (values == null || values.Count == 0)
                return;
            products = values!.ToList();
            foreach (var i in values)
            {
                valueTree.AddObject(i);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProduct service = new();
            if (service is CreateProduct form)
            {
                form.ShowDialog();
                LoadData();
            }
        }

        private void ���������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category service = new();
            if (service is Category form)
            {
                form.ShowDialog();
                LoadData();
            }
        }

        private void ��������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductViewModel item = new();
            try
            {
                item = valueTree.GetSelectedNode<ProductViewModel>();
                if (item == null)
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CreateProduct service = new();
            if (service is CreateProduct form)
            {
                form.Id = item.Id;
                form.ShowDialog();
                LoadData();
            }
        }

        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductViewModel item = new();
            try
            {
                item = valueTree.GetSelectedNode<ProductViewModel>();
                if (item == null)
                    return;
                logic.DeleteProduct(new() { Id = item.Id });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadData();
        }

        private void �������ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (SaveFileDialog folderBrowserDialog = new())
            {
                folderBrowserDialog.InitialDirectory = "c:\\";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.FileName;
                    excelBigText1.SaveExcel(filePath, "��������", products.Select(x => $"{x.Title}: {x.Description}").ToArray());
                }
            }
            MessageBox.Show("���� ������");
        }

        private void �������WordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (SaveFileDialog folderBrowserDialog = new())
            {
                folderBrowserDialog.InitialDirectory = "c:\\";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.FileName;
                    List<int> width = new(); for (int i = 0; i < products.Count; i++) { width.Add(900); };
                    tableInDoc1.CreateTable<ProductViewModel>(new(filePath, new() { ("Id", 50), ("Title", 50), ("Category", 50), ("Count", 50) }, width, products));

                }
            }
            MessageBox.Show("���� ������");

        }

        private void �������PdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (SaveFileDialog folderBrowserDialog = new())
            {
                folderBrowserDialog.InitialDirectory = "c:\\";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.FileName;
                    Dictionary<string, double> vals = new();
                    var cat = logic.GetCategories();
                    if (cat == null || cat.Count == 0)
                    {
                        MessageBox.Show("�������� ���������");
                        return;
                    }
                    foreach (var i in cat)
                    {
                        vals[i.Name] = products.Where(p => p.Category == i.Name && p.Count == "�����������").Count();
                    }
                    diagrampdf1.CreatePieDiagram(filePath, "��������", "�����������", vals);
                }
                
            }
            MessageBox.Show("���� ������");
            
        }

        private void Check_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        �������ToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.U:
                        ��������ToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.D:
                        �������ToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.S:
                        �������ExcelToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.T:
                        �������WordToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.C:
                        �������PdfToolStripMenuItem_Click(sender, e);
                        break;
                }
            }
        }
    }

}
