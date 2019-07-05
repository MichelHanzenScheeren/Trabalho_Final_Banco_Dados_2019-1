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
    public partial class PaginaVenda : Form
    {
        public Venda Venda { get; set; }
        private string tipo = "Cadastro";
        private VendaServico vendaServico;
        private LivroServico livroServico;
        private CadastroServico cadastroServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;

        public PaginaVenda()
        {
            InitializeComponent();
            vendaServico = new VendaServico(new SqlConnection(connectionString));
            livroServico = new LivroServico(new SqlConnection(connectionString));
            cadastroServico = new CadastroServico(new SqlConnection(connectionString));
            dtCompra.Enabled = false;
        }

        public PaginaVenda(Venda venda) : this()
        {
            Venda = venda;
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


        ///////////////////////////////////////// NOVA VENDA ////////////////////////////////////////////
        private void BtnLivros_Click(object sender, EventArgs e)
        {
            if (cbLivros.Text == "")
                AtualizarcbLivros(livroServico.ObterTodos());
            else
            {
                List<Livro> livros = new List<Livro>();
                try
                {
                    foreach (var id in livroServico.BuscarID(Convert.ToInt32(cbLivros.Text)))
                    {
                        livros.Add(id);
                    }
                }
                catch (Exception)
                {
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
                }
                finally
                {
                    AtualizarcbLivros(livros);
                }
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

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            if (cbClientes.Text == "")
                AtualizarcbClientes(cadastroServico.ObterTodos());
            else
            {
                List<Cadastro> cadastros = new List<Cadastro>();
                try
                {
                    foreach (var id in cadastroServico.BuscarID(Convert.ToInt32(cbClientes.Text)))
                    {
                        cadastros.Add(id);
                    }
                }
                catch (Exception)
                {
                    foreach (var nome in cadastroServico.BuscarGeral("Nome", cbClientes.Text))
                    {
                        cadastros.Add(nome);
                    }
                    foreach (var cidade in cadastroServico.BuscarGeral("Cidade", cbClientes.Text))
                    {
                        cadastros.Add(cidade);
                    }
                    foreach (var estado in cadastroServico.BuscarGeral("Estado", cbClientes.Text))
                    {
                        cadastros.Add(estado);
                    }
                }
                finally
                {
                    AtualizarcbClientes(cadastros);
                }
            }
        }
        private void AtualizarcbClientes(List<Cadastro> cadastros)
        {
            cbClientes.Items.Clear();
            int contador = 0;
            if (cadastros.Count == 0)
                cbClientes.Items.Add("Nenhum resultado correspondente!");
            else
            {
                foreach (var item in cadastros)
                {
                    if (contador <= 10)
                        cbClientes.Items.Add(item.CadastroID + " - '" + item.Nome + "', CPF: " + item.CPF + ". " + item.Cidade + " - " + item.Estado);
                    contador++;
                }
            }
            cbClientes.DroppedDown = true;
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
                MessageBox.Show($"Erro no Cadastro!\n{erro.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (EstoqueVazioException erro2)
            {
                MessageBox.Show($"Erro no Cadastro!\n{erro2.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Exception)
            {
                MessageBox.Show($"O livro e/ou cliente inserido não são válidos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarCondicoes()
        {
            if (cbLivros.Text == "" || cbClientes.Text == "" || txtQuantidade.Text == "" || txtQuantidade.Text == "0")
                throw new NaoPreenchidoException("Um ou mais campos obrigatórios incompletos!");

        }

        private void Cadastrar()
        {
            string[] livro = cbLivros.Text.Split(' ');
            int idLivro = Convert.ToInt32(livro[0]);
            string[] cliente = cbClientes.Text.Split(' ');
            int idCliente = Convert.ToInt32(cliente[0]);
            AtualizarEstoque(idLivro);
            if (txtDesconto.Text == "")
                txtDesconto.Text = "0";
            Venda = new Venda(Convert.ToInt32(txtQuantidade.Text), Convert.ToDouble(txtDesconto.Text), Convert.ToDouble(txtTotal.Text), idLivro, idCliente);
            vendaServico.Gravar(Venda);
            MessageBox.Show("Venda Cadastrada!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void AtualizarEstoque(int idLivro)
        {
            Livro auxLivro = livroServico.BuscarID(idLivro).FirstOrDefault();
            auxLivro.Estoque -= Convert.ToInt32(txtQuantidade.Text);
            if (auxLivro.Estoque < 0)
                throw new EstoqueVazioException("Estoque insuficiente!");
            livroServico.Editar(auxLivro);
        }


        ///////////////////////////////////////// EDITAR VENDA ////////////////////////////////////////////
        private void FazerAlteracoes()
        {
            txtID.Visible = true;
            lblID.Visible = true;
            cbLivros.Enabled = false;
            //cbClientes.Enabled = false;
            btnSalvar.Text = "SALVAR ALTERAÇÕES";
            tipo = "Editar";
            //btnClientes.Enabled = false;
            btnLivros.Enabled = false;
            PreencherDados();
        }
        
        private void PreencherDados()
        {
            txtID.Text = Convert.ToString(Venda.VendaID);
            cbLivros.Text = Venda.Livro.LivroID + " - '" + Venda.Livro.Titulo + "', Autor: " + Venda.Livro.Autor + ". Gênero " + Venda.Livro.Tipo;
            cbClientes.Text = Venda.Cadastro.CadastroID + " - '" + Venda.Cadastro.Nome + "', CPF: " + Venda.Cadastro.CPF + ". " + Venda.Cadastro.Cidade + " - " + Venda.Cadastro.Estado;
            txtQuantidade.Text = Convert.ToString(Venda.Quantidade);
            txtDesconto.Text = Convert.ToString(Venda.Desconto);
            txtEstoque.Text = Convert.ToString(Venda.Livro.Estoque);
            txtValorUnitario.Text = Convert.ToString(Venda.Livro.Preco);
            AtualizarTotal();
        }

        private void TxtQuantidade_TextChanged(object sender, EventArgs e)
        {
            AtualizarTotal();
        }

        private void TxtDesconto_TextChanged(object sender, EventArgs e)
        {
            AtualizarTotal();
        }

        private void CbLivros_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] auxLivro = cbLivros.Text.Split(' ');
                int id = Convert.ToInt32(auxLivro[0]);
                Livro livro = livroServico.BuscarID(id).FirstOrDefault();
                txtEstoque.Text = Convert.ToString(livro.Estoque);
                txtValorUnitario.Text = Convert.ToString(livro.Preco);
                AtualizarTotal();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Um erro inesperado ocoreu!\nTente Novamente!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AtualizarTotal()
        {
            if (txtQuantidade.Text != "0" && txtQuantidade.Text != "" && txtValorUnitario.Text != "")
                txtTotal.Text = Convert.ToString(Convert.ToInt32(txtQuantidade.Text) * Convert.ToDouble(txtValorUnitario.Text));
            else
                txtTotal.Text = "";
            if (txtDesconto.Text != "0" && txtDesconto.Text != "" && txtQuantidade.Text != "0" && txtQuantidade.Text != "" && txtValorUnitario.Text != "")
                txtTotal.Text = Convert.ToString(Convert.ToDouble(txtTotal.Text) - (Convert.ToDouble(txtTotal.Text) * (Convert.ToDouble(txtDesconto.Text) / 100)));
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
            if (txtDesconto.Text.Contains(".") && ((e.KeyChar == '.') || (e.KeyChar == ',')))
                e.Handled = true;
        }


        private void Editar()
        {
            
            if (MessageBox.Show("TEM CERTEZA QUE ALTERAR ESSE REGISTRO?\n\nINFORMAÇÕES ALTERADAS NÃO PODEM SER RECUPERADAS", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                VerificarCondicoesEstoque();
                Venda.Quantidade = Convert.ToInt32(txtQuantidade.Text);
                Venda.Desconto = Convert.ToDouble(txtDesconto.Text);
                Venda.CadastroID = Convert.ToInt32(cbClientes.Text.Split(' ')[0]);
                vendaServico.Editar(Venda);
                MessageBox.Show("Alterações Salvas!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void VerificarCondicoesEstoque()
        {
            int quantidade = Convert.ToInt32(txtQuantidade.Text);
            AtualizarEstoque(Venda.Livro.LivroID, quantidade - Venda.Quantidade);
        }

        private void AtualizarEstoque(int idLivro, int quantidade)
        {
            Livro auxLivro = livroServico.BuscarID(idLivro).FirstOrDefault();
            auxLivro.Estoque -= quantidade;
            if (auxLivro.Estoque < 0)
                throw new EstoqueVazioException("Estoque insuficiente!");
            livroServico.Editar(auxLivro);
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
