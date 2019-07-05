using System;

namespace Modelo
{
    public class Compra
    {
        public int CompraID { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public double PrecoUnidade { get; set; }

        public int LivroID { get; set; }
        public virtual Livro Livro { get; set; }

        public Compra() { }

        public Compra(int quantidade, double precoUnidade, int livroID)
        {
            Quantidade = quantidade;
            PrecoUnidade = precoUnidade;
            LivroID = livroID;

            Data = DateTime.Now;
        }
    }
}
