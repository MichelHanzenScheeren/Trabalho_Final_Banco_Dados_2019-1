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
    public class CompraServico
    {
        private CompraDAL compraDAL;
        public CompraServico(SqlConnection connection)
        {
            compraDAL = new CompraDAL(connection);
        }

        public void Gravar(Compra compra)
        {
            compraDAL.Gravar(compra);
        }

        public List<Compra> ObterTodos()
        {
            return compraDAL.ObterTodos();
        }

        public List<Compra> BuscarID(int id)
        {
            return compraDAL.BuscarID(id);
        }

        public void Editar(Compra compra)
        {
            compraDAL.Editar(compra);
        }

        public void Excluir(int id)
        {
            compraDAL.Excluir(id);
        }

        public List<Compra> BuscarGeral(string coluna, string pesquisa)
        {
            return compraDAL.BuscarGeral(coluna, pesquisa);
        }
    }
}
