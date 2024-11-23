using FormForLab.IModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormForLab.IModels.FullModel
{
    public class ProductModel : IProduct
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId {  get; set; }
        public int? Count { get; set; }

    }
}
