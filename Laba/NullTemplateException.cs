using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Laba
{
    [Serializable]
    internal class NullTemplateException : Exception
    {
        public NullTemplateException() : base("Отсутствует шаблон") { }
        public NullTemplateException(string message) : base(message) { }
        public NullTemplateException(string message, Exception exception) : base(message, exception) { }
    }
}
