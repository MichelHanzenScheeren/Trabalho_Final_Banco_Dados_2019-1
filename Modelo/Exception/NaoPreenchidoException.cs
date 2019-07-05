using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Exception
{
    public class NaoPreenchidoException : ApplicationException
    {
        public NaoPreenchidoException(string mensagem) : base(mensagem)
        { }
    }
}
