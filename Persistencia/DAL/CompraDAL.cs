using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    public class CompraDAL
    {
        private SqlConnection connection;
        public CompraDAL(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Gravar(Compra compra)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Compra (Data, Quantidade, PrecoUnidade, LivroID) " +
                "values (@data, @quantidade, @precoUnidade, @livroID)";
            command.Parameters.AddWithValue("@data", compra.Data);
            command.Parameters.AddWithValue("@quantidade", compra.Quantidade);
            command.Parameters.AddWithValue("@precoUnidade", compra.PrecoUnidade);
            command.Parameters.AddWithValue("@livroID", compra.LivroID);
            command.ExecuteNonQuery();

            this.connection.Close();
        }

        public List<Compra> ObterTodos()
        {
            List<Compra> compras = new List<Compra>();
            var command = new SqlCommand("Select Livro.LivroID, Titulo, Autor, Editora, Estoque, Data, Quantidade, PrecoUnidade, CompraID from Compra INNER JOIN Livro on Livro.LivroID = Compra.LivroID", connection);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var compra = new Compra();
                    compra.Livro = new Livro();
                    compra.Livro.LivroID = reader.GetInt32(0);
                    compra.Livro.Titulo = reader.GetString(1);
                    compra.Livro.Autor = reader.GetString(2);
                    compra.Livro.Editora = reader.GetString(3);
                    compra.Livro.Estoque = reader.GetInt32(4);
                    compra.Data = reader.GetDateTime(5);
                    compra.Quantidade = reader.GetInt32(6);
                    compra.PrecoUnidade = Convert.ToDouble(reader.GetDecimal(7));
                    compra.CompraID = reader.GetInt32(8);

                    compras.Add(compra);
                }
            }
            connection.Close();
            return compras;
        }

        public List<Compra> BuscarID(int id)
        {
            List<Compra> compras = new List<Compra>();
            var command = new SqlCommand("Select Livro.LivroID, Titulo, Autor, Editora, Estoque, Data, Quantidade, PrecoUnidade, CompraID from Compra INNER JOIN Livro on Livro.LivroID = Compra.LivroID where CompraID = @ID", connection);
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var compra = new Compra();
                    compra.Livro = new Livro();
                    compra.Livro.LivroID = reader.GetInt32(0);
                    compra.Livro.Titulo = reader.GetString(1);
                    compra.Livro.Autor = reader.GetString(2);
                    compra.Livro.Editora = reader.GetString(3);
                    compra.Livro.Estoque = reader.GetInt32(4);
                    compra.Data = reader.GetDateTime(5);
                    compra.Quantidade = reader.GetInt32(6);
                    compra.PrecoUnidade = Convert.ToDouble(reader.GetDecimal(7));
                    compra.CompraID = reader.GetInt32(8);

                    compras.Add(compra);
                }
            }
            connection.Close();
            return compras;
        }

        public List<Compra> BuscarGeral(string coluna, string pesquisa)
        {
            List<Compra> compras = new List<Compra>();
            SqlCommand command;
            if (coluna == "Data")
                command = new SqlCommand($"Select Livro.LivroID, Titulo, Autor, Editora, Estoque, Data, Quantidade, PrecoUnidade, CompraID from Compra INNER JOIN Livro on Livro.LivroID = Compra.LivroID where CONVERT(nvarchar(10),  {coluna}, 103) LIKE @pesquisa", connection);
            else
                command = new SqlCommand($"Select Livro.LivroID, Titulo, Autor, Editora, Estoque, Data, Quantidade, PrecoUnidade, CompraID from Compra INNER JOIN Livro on Livro.LivroID = Compra.LivroID where {coluna} LIKE @pesquisa", connection);
            command.Parameters.AddWithValue("@pesquisa", "%" + pesquisa + "%");
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var compra = new Compra();
                    compra.Livro = new Livro();
                    compra.Livro.LivroID = reader.GetInt32(0);
                    compra.Livro.Titulo = reader.GetString(1);
                    compra.Livro.Autor = reader.GetString(2);
                    compra.Livro.Editora = reader.GetString(3);
                    compra.Livro.Estoque = reader.GetInt32(4);
                    compra.Data = reader.GetDateTime(5);
                    compra.Quantidade = reader.GetInt32(6);
                    compra.PrecoUnidade = Convert.ToDouble(reader.GetDecimal(7));
                    compra.CompraID = reader.GetInt32(8);

                    compras.Add(compra);
                }
            }
            connection.Close();
            return compras;
        }

        public void Editar(Compra compra)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Compra set Quantidade=@quantidade, PrecoUnidade=@precoUnidade where CompraID=@CompraID";
            command.Parameters.AddWithValue("@quantidade", compra.Quantidade);
            command.Parameters.AddWithValue("@PrecoUnidade", compra.PrecoUnidade);
            command.Parameters.AddWithValue("@CompraID", compra.CompraID);
            command.ExecuteNonQuery();

            this.connection.Close();
        }

        public void Excluir(int id)
        {

            this.connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE from Compra where CompraID=@ID";
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            this.connection.Close();
        }
    }
}
