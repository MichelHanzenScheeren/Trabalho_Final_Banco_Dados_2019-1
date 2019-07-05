using Modelo;
using Persistencia.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico
{
    public class VendaServico
    {
        private VendaDAL vendaDAL;
        public VendaServico(SqlConnection connection)
        {
            vendaDAL = new VendaDAL(connection);
        }

        public void Gravar(Venda venda)
        {
            vendaDAL.Gravar(venda);
        }

        public List<Venda> ObterTodos()
        {
            return vendaDAL.ObterTodos();
        }

        public List<Venda> BuscarID(int id)
        {
            return vendaDAL.BuscarID(id);
        }

        public void Editar(Venda venda)
        {
            vendaDAL.Editar(venda);
        }

        public void Excluir(int id)
        {
            vendaDAL.Excluir(id);
        }

        public List<Venda> BuscarGeral(string coluna, string pesquisa)
        {
            return vendaDAL.BuscarGeral(coluna, pesquisa);
        }
    }
}
