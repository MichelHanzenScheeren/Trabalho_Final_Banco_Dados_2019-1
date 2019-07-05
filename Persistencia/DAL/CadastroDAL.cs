using Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    public class CadastroDAL
    {
        private SqlConnection connection;
        public CadastroDAL(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void Gravar(Cadastro cadastro)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Cadastro (Tipo, Nome, Sexo, CPF,Telefone, Rua, NumeroCasa,Cidade, Estado,  DataNascimento) " +
                "values (@tipo, @nome, @sexo, @cPF,@telefone, @rua, @numeroCasa, @cidade, @estado,  @dataNascimento)";
            command.Parameters.AddWithValue("@tipo", cadastro.Tipo);
            command.Parameters.AddWithValue("@nome", cadastro.Nome);
            command.Parameters.AddWithValue("@sexo", cadastro.Sexo);
            command.Parameters.AddWithValue("@cPF", cadastro.CPF);
            command.Parameters.AddWithValue("@telefone", cadastro.Telefone);
            command.Parameters.AddWithValue("@rua", cadastro.Rua);
            command.Parameters.AddWithValue("@numeroCasa", cadastro.NumeroCasa);
            command.Parameters.AddWithValue("@cidade", cadastro.Cidade);
            command.Parameters.AddWithValue("@estado", cadastro.Estado);
            command.Parameters.AddWithValue("@dataNascimento", cadastro.Nascimento);
            command.ExecuteNonQuery();

            this.connection.Close();
            
        }

        public List<Cadastro> ObterTodos()
        {
            List<Cadastro> cadastros = new List<Cadastro>();
            var command = new SqlCommand("Select CadastroID, Tipo, Nome, Telefone, Rua, NumeroCasa, Cidade, Estado, CPF from Cadastro", connection);
            connection.Open();
            using(SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var cadastro = new Cadastro();
                    cadastro.CadastroID = reader.GetInt32(0);
                    cadastro.Tipo = reader.GetString(1);
                    cadastro.Nome = reader.GetString(2);
                    cadastro.Telefone = reader.GetString(3);
                    cadastro.Rua = reader.GetString(4);
                    cadastro.NumeroCasa = reader.GetString(5);
                    cadastro.Cidade = reader.GetString(6);
                    cadastro.Estado = reader.GetString(7);
                    cadastro.CPF = reader.GetString(8);
                    cadastros.Add(cadastro);
                }
            }
            connection.Close();
            return cadastros;
        }

        public List<Cadastro> BuscarID(int id)
        {
            List <Cadastro> cadastros = new List<Cadastro>();
            var command = new SqlCommand("Select CadastroID, Tipo, Nome, sexo, CPF, Telefone, Rua, NumeroCasa, Cidade, Estado, DataNascimento from Cadastro where CadastroID = @ID", connection);
            command.Parameters.AddWithValue("@ID", id);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cadastro = new Cadastro();
                    cadastro.CadastroID = reader.GetInt32(0);
                    cadastro.Tipo = reader.GetString(1);
                    cadastro.Nome = reader.GetString(2);
                    cadastro.Sexo = reader.GetString(3);
                    cadastro.CPF = reader.GetString(4);
                    cadastro.Telefone = reader.GetString(5);
                    cadastro.Rua = reader.GetString(6);
                    cadastro.NumeroCasa = reader.GetString(7);
                    cadastro.Cidade = reader.GetString(8);
                    cadastro.Estado = reader.GetString(9);
                    cadastro.Nascimento = reader.GetDateTime(10);
                    cadastros.Add(cadastro);
                }
            }
            connection.Close();
            return cadastros;
        }

        public List<Cadastro> BuscarGeral(string coluna, string pesquisa)
        {
            List<Cadastro> cadastros = new List<Cadastro>();
            var command = new SqlCommand($"Select CadastroID, Tipo, Nome, Telefone, Rua, NumeroCasa, Cidade, Estado, CPF from Cadastro where {coluna} LIKE @pesquisa", connection);
            command.Parameters.AddWithValue("@pesquisa","%" + pesquisa + "%");
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var cadastro = new Cadastro();
                    cadastro.CadastroID = reader.GetInt32(0);
                    cadastro.Tipo = reader.GetString(1);
                    cadastro.Nome = reader.GetString(2);
                    cadastro.Telefone = reader.GetString(3);
                    cadastro.Rua = reader.GetString(4);
                    cadastro.NumeroCasa = reader.GetString(5);
                    cadastro.Cidade = reader.GetString(6);
                    cadastro.Estado = reader.GetString(7);
                    cadastro.CPF = reader.GetString(8);
                    cadastros.Add(cadastro);
                }
            }
            connection.Close();
            return cadastros;
        }

        public void Editar(Cadastro cadastro)
        {
         
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Cadastro set Tipo=@tipo, Nome=@nome, Sexo=@sexo, CPF=@cPF,Telefone=@telefone, Rua=@rua, NumeroCasa=@numeroCasa,Cidade=@cidade, Estado=@estado,  DataNascimento=@dataNascimento where CadastroID=@cadastroID";
            command.Parameters.AddWithValue("@tipo", cadastro.Tipo);
            command.Parameters.AddWithValue("@nome", cadastro.Nome);
            command.Parameters.AddWithValue("@sexo", cadastro.Sexo);
            command.Parameters.AddWithValue("@cPF", cadastro.CPF);
            command.Parameters.AddWithValue("@telefone", cadastro.Telefone);
            command.Parameters.AddWithValue("@rua", cadastro.Rua);
            command.Parameters.AddWithValue("@numeroCasa", cadastro.NumeroCasa);
            command.Parameters.AddWithValue("@cidade", cadastro.Cidade);
            command.Parameters.AddWithValue("@estado", cadastro.Estado);
            command.Parameters.AddWithValue("@dataNascimento", cadastro.Nascimento);
            command.Parameters.AddWithValue("@cadastroID", cadastro.CadastroID);
            command.ExecuteNonQuery();

            this.connection.Close();
            
        }

        public void Excluir(int id)
        {
            this.connection.Open();

            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE from Cadastro where CadastroID=@ID";
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            this.connection.Close();

        }
    }
}