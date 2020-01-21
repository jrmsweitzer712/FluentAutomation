using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentFramework.Core.Exceptions
{
    public class ConsoleErrorException : Exception
    {
        public ConsoleErrorException(string message) 
            : base(message) { }

    }
}
