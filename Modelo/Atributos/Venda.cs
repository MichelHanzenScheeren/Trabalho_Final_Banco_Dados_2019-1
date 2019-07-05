using System;

namespace Modelo
{
    public class Venda
    {
        public int VendaID { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public double Desconto { get; set; }
        public double Total { get; set; }

        public int LivroID { get; set; }
        public virtual Livro Livro { get; set; }

        public int CadastroID { get; set; }
        public virtual Cadastro Cadastro { get; set; }

        public Venda() { }

        public Venda(int quantidade, double desconto, double total, int livroID, int cadastroID)
        {
            Quantidade = quantidade;
            Desconto = desconto;
            Total = total;
            LivroID = livroID;
            CadastroID = cadastroID;

            Data = DateTime.Now;
        }
    }
}
