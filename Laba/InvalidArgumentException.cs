using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba
{
    [Serializable]
    internal class InvalidArgumentException : Exception
    {
        public InvalidArgumentException() : base("Входные данные не соответствуют шаблону") { }
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, Exception exception) : base(message, exception) { }
    }
}
