using System;

namespace Modelo.Exception
{
    public class EstoqueVazioException : ApplicationException
    {
        public EstoqueVazioException(string mensagem) : base(mensagem)
        { }
    }
}
