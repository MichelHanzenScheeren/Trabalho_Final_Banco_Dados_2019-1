using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    public class LivroDAL
    {
        private SqlConnection connection;
        public LivroDAL(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Gravar(Livro livro)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Livro (Titulo, Autor, Editora, NumeroPaginas,Tipo, Preco, Estoque, DataLancamento) " +
                "values (@titulo, @autor, @editora, @numeroPaginas,@tipo, @preco, @estoque, @dataLancamento)";
            command.Parameters.AddWithValue("@titulo", livro.Titulo);
            command.Parameters.AddWithValue("@autor", livro.Autor);
            command.Parameters.AddWithValue("@editora", livro.Editora);
            command.Parameters.AddWithValue("@numeroPaginas", livro.NumeroPaginas);
            command.Parameters.AddWithValue("@tipo", livro.Tipo);
            command.Parameters.AddWithValue("@preco", livro.Preco);
            command.Parameters.AddWithValue("@estoque", livro.Estoque);
            command.Parameters.AddWithValue("@dataLancamento", livro.DataLancamento);
            command.ExecuteNonQuery();

            this.connection.Close();
        }

        public List<Livro> ObterTodos()
        {
            List<Livro> livros = new List<Livro>();
            var command = new SqlCommand("Select LivroID, Titulo, Autor, Editora, Tipo, Estoque from Livro", connection);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var livro = new Livro();
                    livro.LivroID = reader.GetInt32(0);
                    livro.Titulo = reader.GetString(1);
                    livro.Autor = reader.GetString(2);
                    livro.Editora = reader.GetString(3);
                    livro.Tipo = reader.GetString(4);
                    livro.Estoque = reader.GetInt32(5);
                    livros.Add(livro);
                }
            }
            connection.Close();
            return livros;
        }

        public void Editar(Livro livro)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Livro set Titulo=@titulo, Autor=@autor, Editora=@editora, NumeroPaginas=@numeroPaginas,Tipo=@tipo, Preco=@preco, Estoque=@estoque, DataLancamento=@dataLancamento where LivroID=@LivroID";
            command.Parameters.AddWithValue("@titulo", livro.Titulo);
            command.Parameters.AddWithValue("@autor", livro.Autor);
            command.Parameters.AddWithValue("@editora", livro.Editora);
            command.Parameters.AddWithValue("@numeroPaginas", livro.NumeroPaginas);
            command.Parameters.AddWithValue("@tipo", livro.Tipo);
            command.Parameters.AddWithValue("@preco", livro.Preco);
            command.Parameters.AddWithValue("@estoque", livro.Estoque);
            command.Parameters.AddWithValue("@dataLancamento", livro.DataLancamento);
            command.Parameters.AddWithValue("@LivroID", livro.LivroID);
            command.ExecuteNonQuery();

            this.connection.Close();
        }

        public List<Livro> BuscarID(int id)
        {
            List<Livro> livros = new List<Livro>();
            var command = new SqlCommand("Select LivroID, Titulo, Autor, Editora, NumeroPaginas,Tipo, Preco, Estoque, DataLancamento from Livro where LivroID = @ID", connection);
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var livro = new Livro();
                    livro.LivroID = reader.GetInt32(0);
                    livro.Titulo = reader.GetString(1);
                    livro.Autor = reader.GetString(2);
                    livro.Editora = reader.GetString(3);
                    livro.NumeroPaginas = reader.GetInt32(4);
                    livro.Tipo = reader.GetString(5);
                    livro.Preco = Convert.ToDouble(reader.GetDecimal(6));
                    livro.Estoque = reader.GetInt32(7);
                    livro.DataLancamento = reader.GetDateTime(8);
                    livros.Add(livro);
                }
            }
            connection.Close();
            return livros;
        }

        public List<Livro> BuscarGeral(string coluna, string pesquisa)
        {
            List<Livro> livros = new List<Livro>();
            var command = new SqlCommand($"Select LivroID, Titulo, Autor, Editora, Tipo, Estoque from Livro where {coluna} LIKE @pesquisa", connection);
            command.Parameters.AddWithValue("@pesquisa", "%" + pesquisa + "%");
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var livro = new Livro();
                    livro.LivroID = reader.GetInt32(0);
                    livro.Titulo = reader.GetString(1);
                    livro.Autor = reader.GetString(2);
                    livro.Editora = reader.GetString(3);
                    livro.Tipo = reader.GetString(4);
                    livro.Estoque = reader.GetInt32(5);
                    livros.Add(livro);
                }
            }
            connection.Close();
            return livros;
        }

        public void Excluir(int id)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE from Livro where LivroID=@ID";
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            this.connection.Close();

        }

    }
}
