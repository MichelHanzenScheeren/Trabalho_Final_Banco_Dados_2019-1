using System;
using System.Collections.Generic;

namespace Modelo
{
    public class Cadastro
    {
        public int CadastroID { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Rua { get; set; }
        public string NumeroCasa { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }


        public DateTime Nascimento { get; set; }

        public virtual List<Venda> Vendas { get; set; }

        public Cadastro() { }

        public Cadastro(string tipo, string nome, string sexo, string cPF, string telefone, string rua, string numeroCasa, string cidade, string estado, DateTime nascimento)
        {
            Tipo = tipo;
            Nome = nome;
            Sexo = sexo;
            CPF = cPF;
            Telefone = telefone;
            Rua = rua;
            NumeroCasa = numeroCasa;
            Cidade = cidade;
            Estado = estado;
            Nascimento = nascimento;

        }
    }
}
