using FormForLab.IModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormForLab.IModels.BindingModel
{
    public class CategoryModel : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
