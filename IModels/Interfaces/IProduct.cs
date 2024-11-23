using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormForLab.IModels.Interfaces
{
    public interface IProduct : IId
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int? Count { get; set; }
    }
}
