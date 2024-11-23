using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba
{
    public class DiagInfo
    {
        public string Title { get; set; } = string.Empty;
        public List<(int, int)> nums { get; set; } = new();
    }
}
