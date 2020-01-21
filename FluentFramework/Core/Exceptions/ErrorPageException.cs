using System;

namespace FluentFramework.Core.Exceptions
{
    public class ErrorPageException: Exception
    {
        public ErrorPageException(string message) 
            : base(message) { }
    }
}
