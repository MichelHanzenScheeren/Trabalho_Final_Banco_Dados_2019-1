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
    public class LivroServico
    {
        private LivroDAL livroDAL;
        public LivroServico(SqlConnection connection)
        {
            livroDAL = new LivroDAL(connection);
        }

        public void Gravar(Livro livro)
        {
            livroDAL.Gravar(livro);
        }

        public List<Livro> ObterTodos()
        {
            return livroDAL.ObterTodos();
        }

        public List<Livro> BuscarID(int id)
        {
            return livroDAL.BuscarID(id);
        }

        public void Editar(Livro livro)
        {
            livroDAL.Editar(livro);
        }

        public void Excluir(int id)
        {
            livroDAL.Excluir(id);
        }

        public List<Livro> BuscarGeral(string coluna, string pesquisa)
        {
            return livroDAL.BuscarGeral(coluna, pesquisa);
        }
    }
}
