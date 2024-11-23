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

        private void ñîçäàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProduct service = new();
            if (service is CreateProduct form)
            {
                form.ShowDialog();
                LoadData();
            }
        }

        private void ñïèñîêÊàòåãîğèéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category service = new();
            if (service is Category form)
            {
                form.ShowDialog();
                LoadData();
            }
        }

        private void èçìåíèòüToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void óäàëèòüToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void ñîçäàòüExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = "";
            using (SaveFileDialog folderBrowserDialog = new())
            {
                folderBrowserDialog.InitialDirectory = "c:\\";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.FileName;
                    excelBigText1.SaveExcel(filePath, "Ïğîäóêòû", products.Select(x => $"{x.Title}: {x.Description}").ToArray());
                }
            }
            MessageBox.Show("Ôàéë ñîçäàí");
        }

        private void ñîçäàòüWordToolStripMenuItem_Click(object sender, EventArgs e)
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
            MessageBox.Show("Ôàéë ñîçäàí");

        }

        private void ñîçäàòüPdfToolStripMenuItem_Click(object sender, EventArgs e)
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
                        MessageBox.Show("Ñîçäàéòå êàòåãîğèè");
                        return;
                    }
                    foreach (var i in cat)
                    {
                        vals[i.Name] = products.Where(p => p.Category == i.Name && p.Count == "Îòñóòñòâóåò").Count();
                    }
                    diagrampdf1.CreatePieDiagram(filePath, "Ïğîäóêòû", "Ñîîòíîøåíèå", vals);
                }
                
            }
            MessageBox.Show("Ôàéë ñîçäàí");
            
        }

        private void Check_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        ñîçäàòüToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.U:
                        èçìåíèòüToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.D:
                        óäàëèòüToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.S:
                        ñîçäàòüExcelToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.T:
                        ñîçäàòüWordToolStripMenuItem_Click(sender, e);
                        break;
                    case Keys.C:
                        ñîçäàòüPdfToolStripMenuItem_Click(sender, e);
                        break;
                }
            }
        }
    }

}
