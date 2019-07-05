using Modelo;
using Modelo.Exception;
using Servico;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Livraria_AdoNet
{
    public partial class PaginaLivro : Form
    {
        public Livro Livro { get; set; }
        private string tipo = "Cadastro";
        public string Situacao = "Pendente";
        private LivroServico livroServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;

        public PaginaLivro()
        {
            InitializeComponent();
            livroServico = new LivroServico(new SqlConnection(connectionString));
        }

        public PaginaLivro(Livro livro) : this()
        {
            Livro = livro;
            tipo = "Editar";
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
        }

        private void VerificarCondicoes()
        {
            if (txtTitulo.Text == "" || txtAutor.Text == "" || txtEditor.Text == "" || cbTipo.Text == "" || txtPaginas.Text == "" || txtPreco.Text == "")
                throw new NaoPreenchidoException("Um ou mais campos obrigatórios incompletos!");

        }

        private void Cadastrar()
        {
            Livro = new Livro(txtTitulo.Text, txtAutor.Text, txtEditor.Text, Convert.ToInt32(txtPaginas.Text), cbTipo.Text, Convert.ToDouble(txtPreco.Text), 0, Convert.ToDateTime(dtLancamento.Text));
            livroServico.Gravar(Livro);
            MessageBox.Show("Livro Cadastrado!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }


        private void FazerAlteracoes()
        {
            txtID.Visible = true;
            lblID.Visible = true;
            btnSalvar.Text = "SALVAR ALTERAÇÕES";
            tipo = "Editar";
            PreencherDados();
        }

        private void PreencherDados()
        {
            txtID.Text = Convert.ToString(Livro.LivroID);
            txtTitulo.Text = Livro.Titulo;
            txtAutor.Text = Livro.Autor;
            txtEditor.Text = Livro.Editora;
            cbTipo.SelectedItem = Livro.Tipo;
            dtLancamento.Text = Convert.ToString(Livro.DataLancamento);
            txtPaginas.Text = Convert.ToString(Livro.NumeroPaginas);
            txtPreco.Text = Convert.ToString(Livro.Preco);
        }

        private void Editar()
        {
            if (MessageBox.Show("TEM CERTEZA QUE ALTERAR ESSE LIVRO?\n\nINFORMAÇÕES ALTERADAS NÃO PODEM SER RECUPERADAS", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Livro.Titulo = txtTitulo.Text;
                Livro.Autor = txtAutor.Text;
                Livro.Editora = txtEditor.Text;
                Livro.Tipo = cbTipo.Text;
                Livro.DataLancamento = Convert.ToDateTime(dtLancamento.Text);
                Livro.NumeroPaginas = Convert.ToInt32(txtPaginas.Text);
                Livro.Preco = Convert.ToDouble(txtPreco.Text);
                livroServico.Editar(Livro);
                Situacao = "Concluído";
                MessageBox.Show("Alterações Salvas!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void TxtPaginas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        private void TxtPreco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (!char.IsControl(e.KeyChar)) && (e.KeyChar != '.') && (e.KeyChar != ','))
                e.Handled = true;
            if(e.KeyChar == ',')
                e.KeyChar = '.';
            if (txtPreco.Text.Contains(".") && ((e.KeyChar == '.') || (e.KeyChar == ',')))
                e.Handled = true;
            
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
