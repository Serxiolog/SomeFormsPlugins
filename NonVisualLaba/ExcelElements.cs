using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba
{
    public class ExcelElements
    { 
        public string bigTitle {  get; set; } = string.Empty;
        public List<(string, string)> titles { get; set; } = new();
    }
}
