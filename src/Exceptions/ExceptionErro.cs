using System;

namespace code_eduspace_api.Exceptions
{
    public class ExceptionError : Exception
    {
        public ExceptionError(string message) : base(message) { }
    }
}