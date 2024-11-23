using COP_V6.NoVisualComponent;
using FormForLab.BisnesLogic;
using FormForLab.IModels.ViewModel;
using KOP_14_2;
using Laba;
using Microsoft.Extensions.DependencyInjection;
using Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormForLab.Plugins
{
    public class MainFormPlugin : IPluginsConvention
    {
        MainForm form = new();
        public string PluginName => "MainForm";

        public UserControl GetControl => form.valueTree;

        public PluginsConventionElement GetElement => new() { Id = form.valueTree.GetSelectedNode<ProductViewModel>().Id};

        public bool CreateChartDocument(PluginsConventionSaveDocument saveDocument)
        {
            var products = form.logic.GetProducts();
            if (products == null) 
                return false;

            Dictionary<string, double> vals = new();
            var cat = form.logic.GetCategories();
            if (cat == null || cat.Count == 0)
            {
                return false;
            }
            foreach (var i in cat)
            {
                vals[i.Name] = products.Where(p => p.Category == i.Name && p.Count == "Отсутствует").Count();
            }
            form.diagrampdf1.CreatePieDiagram(saveDocument.FileName, "Продукты", "Соотношение", vals);
            return true;
        }

        public bool CreateSimpleDocument(PluginsConventionSaveDocument saveDocument)
        {
            var products = form.logic.GetProducts();
            if (products == null)
                return false;

            form.excelBigText1.SaveExcel(saveDocument.FileName, "Продукты", products.Select(x => $"{x.Title}: {x.Description}").ToArray());
            return true;
        }

        public bool CreateTableDocument(PluginsConventionSaveDocument saveDocument)
        {
            var products = form.logic.GetProducts();
            if (products == null)
                return false;

            List<int> width = new(); for (int i = 0; i < products.Count; i++) { width.Add(900); };
            form.tableInDoc1.CreateTable<ProductViewModel>(new(saveDocument.FileName, new() { ("Id", 50), ("Title", 50), ("Category", 50), ("Count", 50) }, width, products));
            return true;
        }

        public bool DeleteElement(PluginsConventionElement element)
        {
            if (element.Id == -1)
                return false;
            form.logic.DeleteProduct(new() { Id = element.Id });
            form.LoadData();
            return true;
        }

        public Form GetForm(PluginsConventionElement element)
        {
            CreateProduct service = new();
            if (element.Id == -1)
            {
                return service;
            }
            else
            {
                service.Id = element.Id;
                return service;
            }

        }

        public Form GetThesaurus()
        {
            Category forma = new();
            return forma;
        }

        public void ReloadData()
        {
            form.LoadData();
        }
    }
}
