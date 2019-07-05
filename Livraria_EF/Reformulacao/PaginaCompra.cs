using Modelo;
using Modelo.Exception;
using Servico;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Livraria_AdoNet
{
    public partial class PaginaCompra : Form
    {
        public Compra Compra { get; set; }
        private string tipo = "Cadastro";
        private CompraServico compraServico;
        private LivroServico livroServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;

        public PaginaCompra()
        {
            InitializeComponent();
            compraServico = new CompraServico(new SqlConnection(connectionString));
            livroServico = new LivroServico(new SqlConnection(connectionString));
            dtCompra.Enabled = false;
        }

        public PaginaCompra(Compra compra) : this()
        {
            Compra = compra;
            FazerAlteracoes();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            MsgFecharPrograma fechar = new MsgFecharPrograma();
            fechar.ShowDialog();
            if (fechar.Resultado == "OK")
                throw new FecharException();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnLivros_Click(object sender, EventArgs e)
        {
            if (cbLivros.Text == "")
                AtualizarcbLivros(livroServico.ObterTodos());
            else
            {
                List<Livro> livros = new List<Livro>();
                foreach (var titulo in livroServico.BuscarGeral("Titulo", cbLivros.Text))
                {
                    livros.Add(titulo);
                }
                foreach (var autor in livroServico.BuscarGeral("Autor", cbLivros.Text))
                {
                    livros.Add(autor);
                }
                foreach (var editor in livroServico.BuscarGeral("Editora", cbLivros.Text))
                {
                    livros.Add(editor);
                }
                AtualizarcbLivros(livros);
            }
        }

        private void AtualizarcbLivros(List<Livro> livros)
        {
            cbLivros.Items.Clear();
            int contador = 0;
            if (livros.Count == 0)
                cbLivros.Items.Add("Nenhum resultado correspondente!");
            else
            {
                foreach (var item in livros)
                {
                    if (contador <= 10)
                        cbLivros.Items.Add(item.LivroID + " - '" + item.Titulo + "', Autor: " + item.Autor + ". Gênero " + item.Tipo);
                    contador++;
                }
            }
            cbLivros.DroppedDown = true;
        }


        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                VerificarCondicoes();
                if (tipo == "Cadastro")
                    Cadastrar();
                else
                   Editar();
            }
            catch (NaoPreenchidoException erro)
            {
                MessageBox.Show($"Erro no Cadastro!\n {erro.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(EstoqueVazioException erro2)
            {
                MessageBox.Show($"Erro no Cadastro!\n {erro2.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(System.Exception)
            {
                MessageBox.Show($"O livro inserido não é válido!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarCondicoes()
        {
            if (cbLivros.Text == "" || txtTotal.Text == "")
                throw new NaoPreenchidoException("Um ou mais campos obrigatórios incompletos!");

        }

        private void Cadastrar()
        {
            string[] livro = cbLivros.Text.Split(' ');
            int id = Convert.ToInt32(livro[0]);
            Compra = new Compra(Convert.ToInt32(txtQuantidade.Text), Convert.ToDouble(txtValorUnitario.Text),  id);
            compraServico.Gravar(Compra);
            AtualizarEstoque(id);
            MessageBox.Show("Compra Cadastrado!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void AtualizarEstoque(int id)
        {
            Livro livro = livroServico.BuscarID(id).FirstOrDefault();
            livro.Estoque += Convert.ToInt32(txtQuantidade.Text);
            if (livro.Estoque < 0)
                throw new EstoqueVazioException("Estoque Insuficiente para completar a operação!");
            livroServico.Editar(livro);
        }

        private void FazerAlteracoes()
        {
            txtID.Visible = true;
            lblID.Visible = true;
            cbLivros.Enabled = false;
            btnLivros.Enabled = false;
            btnSalvar.Text = "SALVAR ALTERAÇÕES";
            tipo = "Editar";
            PreencherDados();
        }
        
        private void PreencherDados()
        {
            txtID.Text = Convert.ToString(Compra.CompraID);
            cbLivros.Text = Compra.Livro.LivroID + " - '" + Compra.Livro.Titulo + "', Autor: " + Compra.Livro.Autor + ". Gênero " + Compra.Livro.Tipo;
            txtQuantidade.Text = Convert.ToString(Compra.Quantidade);
            txtValorUnitario.Text = Convert.ToString(Compra.PrecoUnidade);
            dtCompra.Text = Compra.Data.ToShortDateString();
        }

        private void Editar()
        {
            
            if (MessageBox.Show("TEM CERTEZA QUE DESEJA ALTERAR ESSE LIVRO?\n\nINFORMAÇÕES ALTERADAS NÃO PODEM SER RECUPERADAS", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                VerificarCondicoesEstoque();
                Compra.Quantidade = Convert.ToInt32(txtQuantidade.Text);
                Compra.PrecoUnidade = Convert.ToDouble(txtValorUnitario.Text);
                compraServico.Editar(Compra);
                MessageBox.Show("Alterações Salvas!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void VerificarCondicoesEstoque()
        {
            int quantidade = Convert.ToInt32(txtQuantidade.Text);
            AtualizarEstoque(Compra.Livro.LivroID, quantidade - Compra.Quantidade);
  
        }

        private void AtualizarEstoque(int id, int quantidade)
        {
            Livro livro = livroServico.BuscarID(id).FirstOrDefault();
            livro.Estoque += quantidade;
            if (livro.Estoque < 0)
                throw new EstoqueVazioException("Estoque Insuficiente para completar a operação!");
            livroServico.Editar(livro);
        }


        private void TxtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void TxtValorUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != ','))
                e.Handled = true;
            if (e.KeyChar == ',')
                e.KeyChar = '.';
            if (txtValorUnitario.Text.Contains(".") && ((e.KeyChar == '.') || (e.KeyChar == ',')))
                e.Handled = true;
        }

        private void TxtQuantidade_TextChanged(object sender, EventArgs e)
        {
            AtualizarTotal();
        }
        private void TxtValorUnitario_TextChanged(object sender, EventArgs e)
        {
            AtualizarTotal();
        }        private void AtualizarTotal()
        {
            if (txtValorUnitario.Text != "" && txtQuantidade.Text != "")
                txtTotal.Text = Convert.ToString(Convert.ToInt32(txtQuantidade.Text) * Convert.ToDouble(txtValorUnitario.Text));
            else
                txtTotal.Text = "";
        }

        //////////////////////////////// Mover form ao clicar ///////////////////////////////////////////
        bool ClickMouse;
        Point LocalClicado;
        private void Top_MouseUp(object sender, MouseEventArgs e)
        {
            ClickMouse = false;
        }
        private void Top_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            ClickMouse = true;
            LocalClicado = e.Location;
        }
        private void Top_MouseMove(object sender, MouseEventArgs e)
        {
            if (ClickMouse)
                this.Location = new Point(Cursor.Position.X - LocalClicado.X, Cursor.Position.Y - LocalClicado.Y);
        }

        
    }
}
