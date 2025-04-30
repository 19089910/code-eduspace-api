using System;

namespace code_eduspace_api.Exceptions
{
    public class ExceptionErro : Exception
    {
        public ExceptionErro(string mensagem) : base(mensagem) { }
    }
}