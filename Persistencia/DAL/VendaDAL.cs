using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    public class VendaDAL
    {
        private SqlConnection connection;
        public VendaDAL(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Gravar(Venda venda)
        {
            try
            {
                this.connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "insert into Venda (Data, Quantidade, Desconto,Total, LivroID, CadastroID) " +
                    "values (@data, @quantidade, @desconto,@total, @livroID, @cadastroID)";
                command.Parameters.AddWithValue("@data", venda.Data);
                command.Parameters.AddWithValue("@quantidade", venda.Quantidade);
                command.Parameters.AddWithValue("@desconto", venda.Desconto);
                command.Parameters.AddWithValue("@total", venda.Total);
                command.Parameters.AddWithValue("@livroID", venda.LivroID);
                command.Parameters.AddWithValue("@cadastroID", venda.CadastroID);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            finally
            {
                this.connection.Close();
            }
        }

        public List<Venda> ObterTodos()
        {
            List<Venda> vendas = new List<Venda>();
            var command = new SqlCommand("Select Livro.LivroID, Titulo, Autor, Editora, Estoque, Venda.Data, Quantidade, Desconto,Total, VendaID, Cadastro.CadastroID, Nome, CPF, Cidade, Estado from Venda INNER JOIN Livro on Livro.LivroID = Venda.LivroID INNER JOIN Cadastro on Cadastro.CadastroID = Venda.CadastroID ", connection);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var venda = new Venda();
                    venda.Livro = new Livro();
                    venda.Cadastro = new Cadastro();
                    venda.Livro.LivroID = reader.GetInt32(0);
                    venda.Livro.Titulo = reader.GetString(1);
                    venda.Livro.Autor = reader.GetString(2);
                    venda.Livro.Editora = reader.GetString(3);
                    venda.Livro.Estoque = reader.GetInt32(4);
                    venda.Data = reader.GetDateTime(5);
                    venda.Quantidade = reader.GetInt32(6);
                    venda.Desconto = Convert.ToDouble(reader.GetDecimal(7));
                    venda.Total = Convert.ToDouble(reader.GetDecimal(8));
                    venda.VendaID = reader.GetInt32(9);
                    venda.Cadastro.CadastroID = reader.GetInt32(10);
                    venda.Cadastro.Nome = reader.GetString(11);
                    venda.Cadastro.CPF = reader.GetString(12);
                    venda.Cadastro.Cidade = reader.GetString(13);
                    venda.Cadastro.Estado = reader.GetString(14);

                    vendas.Add(venda);
                }
            }
            connection.Close();
            return vendas;
        }

        public List<Venda> BuscarID(int id)
        {
            List<Venda> vendas = new List<Venda>();
            var command = new SqlCommand("Select Livro.LivroID, Titulo, Autor, Editora, Estoque,Preco, Venda.Data, Quantidade, Desconto,Total, VendaID, Cadastro.CadastroID, Nome, CPF, Cidade, Estado from Venda INNER JOIN Livro on Livro.LivroID = Venda.LivroID INNER JOIN Cadastro on Cadastro.CadastroID = Venda.CadastroID where VendaID = @ID", connection);
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var venda = new Venda();
                    venda.Livro = new Livro();
                    venda.Cadastro = new Cadastro();
                    venda.Livro.LivroID = reader.GetInt32(0);
                    venda.Livro.Titulo = reader.GetString(1);
                    venda.Livro.Autor = reader.GetString(2);
                    venda.Livro.Editora = reader.GetString(3);
                    venda.Livro.Estoque = reader.GetInt32(4);
                    venda.Livro.Preco = Convert.ToDouble(reader.GetDecimal(5));
                    venda.Data = reader.GetDateTime(6);
                    venda.Quantidade = reader.GetInt32(7);
                    venda.Desconto = Convert.ToDouble(reader.GetDecimal(8));
                    venda.Total = Convert.ToDouble(reader.GetDecimal(9));
                    venda.VendaID = reader.GetInt32(10);
                    venda.Cadastro.CadastroID = reader.GetInt32(11);
                    venda.Cadastro.Nome = reader.GetString(12);
                    venda.Cadastro.CPF = reader.GetString(13);
                    venda.Cadastro.Cidade = reader.GetString(14);
                    venda.Cadastro.Estado = reader.GetString(15);

                    vendas.Add(venda);
                }
            }
            connection.Close();
            return vendas;
        }

        public List<Venda> BuscarGeral(string coluna, string pesquisa)
        {
            List<Venda> vendas = new List<Venda>();
            SqlCommand command;
            if (coluna == "Data")
                command = new SqlCommand($"Select Livro.LivroID, Titulo, Autor, Editora, Estoque, Venda.Data, Quantidade, Desconto,Total, VendaID, Cadastro.CadastroID, Nome, CPF, Cidade, Estado from Venda INNER JOIN Livro on Livro.LivroID = Venda.LivroID INNER JOIN Cadastro on Cadastro.CadastroID = Venda.CadastroID where CONVERT(nvarchar(10),  {coluna}, 103) LIKE @pesquisa", connection);
            else
                command = new SqlCommand($"Select Livro.LivroID, Titulo, Autor, Editora, Estoque, Venda.Data, Quantidade, Desconto,Total, VendaID, Cadastro.CadastroID, Nome, CPF, Cidade, Estado from Venda INNER JOIN Livro on Livro.LivroID = Venda.LivroID INNER JOIN Cadastro on Cadastro.CadastroID = Venda.CadastroID where {coluna} LIKE @pesquisa", connection);
            command.Parameters.AddWithValue("@pesquisa", "%" + pesquisa + "%");
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var venda = new Venda();
                    venda.Livro = new Livro();
                    venda.Cadastro = new Cadastro();
                    venda.Livro.LivroID = reader.GetInt32(0);
                    venda.Livro.Titulo = reader.GetString(1);
                    venda.Livro.Autor = reader.GetString(2);
                    venda.Livro.Editora = reader.GetString(3);
                    venda.Livro.Estoque = reader.GetInt32(4);
                    venda.Data = reader.GetDateTime(5);
                    venda.Quantidade = reader.GetInt32(6);
                    venda.Desconto = Convert.ToDouble(reader.GetDecimal(7));
                    venda.Total = Convert.ToDouble(reader.GetDecimal(8));
                    venda.VendaID = reader.GetInt32(9);
                    venda.Cadastro.CadastroID = reader.GetInt32(10);
                    venda.Cadastro.Nome = reader.GetString(11);
                    venda.Cadastro.CPF = reader.GetString(12);
                    venda.Cadastro.Cidade = reader.GetString(13);
                    venda.Cadastro.Estado = reader.GetString(14);

                    vendas.Add(venda);
                }
            }
            connection.Close();
            return vendas;
        }

        public void Editar(Venda venda)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Venda set Quantidade=@quantidade, Desconto=@desconto, CadastroID=@cadastroID where VendaID=@vendaID";
            command.Parameters.AddWithValue("@quantidade", venda.Quantidade);
            command.Parameters.AddWithValue("@desconto", venda.Desconto);
            command.Parameters.AddWithValue("@cadastroID", venda.CadastroID);
            command.Parameters.AddWithValue("@VendaID", venda.VendaID);
            command.ExecuteNonQuery();

            this.connection.Close();
        }

        public void Excluir(int id)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE from Venda where VendaID=@ID";
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();
            
            this.connection.Close();
        }
    }
}
