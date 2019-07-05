using System;

namespace Modelo
{
    public class Livro
    {
        public int LivroID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editora { get; set; }
        public int NumeroPaginas { get; set; }
        public string Tipo { get; set; }
        public double Preco { get; set; }
        public int Estoque { get; set; }
        public DateTime DataLancamento { get; set; }


        public Livro()   { }

        public Livro(string titulo, string autor, string editora, int numeroPaginas, string tipo, double preco, int estoque, DateTime dataLancamento)
        {
            Titulo = titulo;
            Autor = autor;
            Editora = editora;
            NumeroPaginas = numeroPaginas;
            Tipo = tipo;
            Preco = preco;
            Estoque = estoque;
            DataLancamento = dataLancamento;

        }
    }
}
