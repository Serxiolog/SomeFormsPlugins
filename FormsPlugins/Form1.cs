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

            // Проходим по каждому плагину и создаем для него пункт меню
            foreach (var plugin in plugins)
            {
                var pluginMenuItem = new ToolStripMenuItem(plugin.Key); // plugin.Key - это PluginName

                // Привязываем обработчик события для каждого пункта меню
                pluginMenuItem.Click += (sender, e) =>
                {
                    // Получаем UserControl от плагина
                    var control = plugin.Value.GetControl;

                    // Очищаем panelControl и добавляем новый контрол
                    panelControl.Controls.Clear();
                    control.Dock = DockStyle.Fill; // Заполняем весь panelControl
                    panelControl.Controls.Add(control);
                    _selectedPlugin = plugin.Key;
                    ActionsToolStripMenuItem.Enabled = true;
                    DocsToolStripMenuItem.Enabled = true;
                };

                // Добавляем пункт меню в меню "Компоненты"
                componentsMenuItem.DropDownItems.Add(pluginMenuItem);
            }

            // Добавляем меню "Компоненты" в menuStrip
            menuStrip.Items.Add(componentsMenuItem);
        }

        private Dictionary<string, IPluginsConvention> LoadPlugins()
        {
            // Создаем словарь для хранения плагинов по их имени
            var plugins = new Dictionary<string, IPluginsConvention>();

            // Путь к папке с плагинами
            string pluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");

            // Проверяем, что папка существует
            if (!Directory.Exists(pluginsPath))
            {
                Directory.CreateDirectory(pluginsPath); // Создаем папку, если она не существует
            }

            // Получаем все DLL из папки
            string[] pluginFiles = Directory.GetFiles(pluginsPath, "*.dll");

            // Загружаем каждую DLL
            foreach (string pluginFile in pluginFiles)
            {
                try
                {
                    // Загружаем сборку
                    var assembly = Assembly.LoadFrom(pluginFile);

                    // Ищем все типы, которые реализуют интерфейс IPluginsConvention
                    var pluginTypes = assembly.GetTypes()
                                              .Where(t => typeof(IPluginsConvention).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                    // Для каждого найденного типа создаем экземпляр и добавляем в словарь
                    foreach (var type in pluginTypes)
                    {
                        // Создаем экземпляр плагина
                        var pluginInstance = (IPluginsConvention)Activator.CreateInstance(type);

                        // Добавляем в словарь по имени плагина
                        plugins.Add(pluginInstance.PluginName, pluginInstance);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Создаем меню компонентов
            CreatePluginsMenu(plugins);

            // Возвращаем словарь плагинов
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
                MessageBox.Show("Нет выбранного элемента", "Ошибка",
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
            if (MessageBox.Show("Удалить выбранный элемент", "Удаление",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            var element = _plugins[_selectedPlugin].GetElement;
            if (element == null)
            {
                MessageBox.Show("Нет выбранного элемента", "Ошибка",
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
                    MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при создании документа",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при создании документа",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Документ сохранен", "Создание документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка при создании документа",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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