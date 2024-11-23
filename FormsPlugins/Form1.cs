using Plugins;
using System.Reflection;
using System.Windows.Forms;

namespace FormsPlugins
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, IPluginsConvention> _plugins;
        private string _selectedPlugin;
        public Form1()
        {
            InitializeComponent();
            _plugins = LoadPlugins();
            _selectedPlugin = string.Empty;
        }

        private void CreatePluginsMenu(Dictionary<string, IPluginsConvention> plugins)
        {

            var componentsMenuItem = ControlsStripMenuItem;
            componentsMenuItem.DropDownItems.Clear();

            // �������� �� ������� ������� � ������� ��� ���� ����� ����
            foreach (var plugin in plugins)
            {
                var pluginMenuItem = new ToolStripMenuItem(plugin.Key); // plugin.Key - ��� PluginName

                // ����������� ���������� ������� ��� ������� ������ ����
                pluginMenuItem.Click += (sender, e) =>
                {
                    // �������� UserControl �� �������
                    var control = plugin.Value.GetControl;

                    // ������� panelControl � ��������� ����� �������
                    panelControl.Controls.Clear();
                    control.Dock = DockStyle.Fill; // ��������� ���� panelControl
                    panelControl.Controls.Add(control);
                    _selectedPlugin = plugin.Key;
                    ActionsToolStripMenuItem.Enabled = true;
                    DocsToolStripMenuItem.Enabled = true;
                };

                // ��������� ����� ���� � ���� "����������"
                componentsMenuItem.DropDownItems.Add(pluginMenuItem);
            }

            // ��������� ���� "����������" � menuStrip
            menuStrip.Items.Add(componentsMenuItem);
        }

        private Dictionary<string, IPluginsConvention> LoadPlugins()
        {
            // ������� ������� ��� �������� �������� �� �� �����
            var plugins = new Dictionary<string, IPluginsConvention>();

            // ���� � ����� � ���������
            string pluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

            // ���������, ��� ����� ����������
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath); // ������� �����, ���� ��� �� ����������
            }

            // �������� ��� DLL �� �����
            string[] pluginFiles = Directory.GetFiles(pluginsPath, "*.dll");

            // ��������� ������ DLL
            foreach (string pluginFile in pluginFiles)
            {
                try
                {
                    // ��������� ������
                    var assembly = Assembly.LoadFrom(pluginFile);

                    // ���� ��� ����, ������� ��������� ��������� IPluginsConvention
                    var pluginTypes = assembly.GetTypes()
                                              .Where(t => typeof(IPluginsConvention).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    // ��� ������� ���������� ���� ������� ��������� � ��������� � �������
                    foreach (var type in pluginTypes)
                    {
                        // ������� ��������� �������
                        var pluginInstance = (IPluginsConvention)Activator.CreateInstance(type);

                        // ��������� � ������� �� ����� �������
                        plugins.Add(pluginInstance.PluginName, pluginInstance);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // ������� ���� �����������
            CreatePluginsMenu(plugins);

            // ���������� ������� ��������
            return plugins;
        }
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedPlugin) || !_plugins.ContainsKey(_selectedPlugin))
            {
                return;
            }
            if (!e.Control)
            {
                return;
            }
            switch (e.KeyCode)
            {
                case Keys.I:
                    ShowThesaurus();
                    break;
                case Keys.A:
                    AddNewElement();
                    break;
                case Keys.U:
                    UpdateElement();
                    break;
                case Keys.D:
                    DeleteElement();
                    break;
                case Keys.S:
                    CreateSimpleDoc();
                    break;
                case Keys.T:
                    CreateTableDoc();
                    break;
                case Keys.C:
                    CreateChartDoc();
                    break;
            }
        }
        private void ShowThesaurus()
        {
            _plugins[_selectedPlugin].GetThesaurus()?.Show();
        }
        private void AddNewElement()
        {
            var form = _plugins[_selectedPlugin].GetForm(new PluginsConventionElement() { Id = -1 });
            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }
        private void UpdateElement()
        {
            var element = _plugins[_selectedPlugin].GetElement;
            if (element == null)
            {
                MessageBox.Show("��� ���������� ��������", "������",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var form = _plugins[_selectedPlugin].GetForm(element);
            if (form != null && form.ShowDialog() == DialogResult.OK)
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }
        private void DeleteElement()
        {
            if (MessageBox.Show("������� ��������� �������", "��������",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            var element = _plugins[_selectedPlugin].GetElement;
            if (element == null)
            {
                MessageBox.Show("��� ���������� ��������", "������",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_plugins[_selectedPlugin].DeleteElement(element))
            {
                _plugins[_selectedPlugin].ReloadData();
            }
        }
        private void CreateSimpleDoc()
        {
            using var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (_plugins[_selectedPlugin].CreateSimpleDocument(new PluginsConventionSaveDocument() { FileName = dialog.FileName }))
                {
                    MessageBox.Show("�������� ��������", "�������� ���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("������ ��� �������� ���������",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CreateTableDoc()
        {
            using var dialog = new SaveFileDialog { Filter = "doc|*.docx" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (_plugins[_selectedPlugin].CreateTableDocument(new PluginsConventionSaveDocument() { FileName = dialog.FileName }))
                {
                    MessageBox.Show("�������� ��������", "�������� ���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("������ ��� �������� ���������",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CreateChartDoc()
        {
            using var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (_plugins[_selectedPlugin].CreateChartDocument(new PluginsConventionSaveDocument() { FileName = dialog.FileName }))
                {
                    MessageBox.Show("�������� ��������", "�������� ���������", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("������ ��� �������� ���������",
                    "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void ThesaurusToolStripMenuItem_Click(object sender, EventArgs e) => ShowThesaurus();
        private void AddElementToolStripMenuItem_Click(object sender, EventArgs e) => AddNewElement();
        private void UpdElementToolStripMenuItem_Click(object sender, EventArgs e) => UpdateElement();
        private void DelElementToolStripMenuItem_Click(object sender, EventArgs e) => DeleteElement();
        private void SimpleDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateSimpleDoc();
        private void TableDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateTableDoc();
        private void ChartDocToolStripMenuItem_Click(object sender, EventArgs e) => CreateChartDoc();
    }
}