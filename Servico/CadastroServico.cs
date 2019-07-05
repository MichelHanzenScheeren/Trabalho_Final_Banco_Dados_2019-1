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
    public class CadastroServico
    {
        private CadastroDAL cadastroDAL;
        public CadastroServico(SqlConnection connection)
        {
            cadastroDAL = new CadastroDAL(connection);
        }

        public void Gravar(Cadastro cadastro)
        {
            cadastroDAL.Gravar(cadastro);
        }

        public List<Cadastro> ObterTodos()
        {
            return cadastroDAL.ObterTodos();
        }

        public List<Cadastro> BuscarID(int id)
        {
            return cadastroDAL.BuscarID(id);
        }

        public List<Cadastro> BuscarGeral(string coluna, string pesquisa)
        {
            return cadastroDAL.BuscarGeral(coluna, pesquisa);
        }

        public void Editar(Cadastro cadastro)
        {
            cadastroDAL.Editar(cadastro);
        }

        public void Excluir(int id)
        {
            cadastroDAL.Excluir(id);
        }
    }
}
