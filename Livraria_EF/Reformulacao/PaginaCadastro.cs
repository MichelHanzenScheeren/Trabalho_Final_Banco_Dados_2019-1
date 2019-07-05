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
    public partial class PaginaCadastro : Form
    {
        public Cadastro Cadastro { get; set; }
        public string Situacao = "Pendente";
        private string tipo = "Cadastro";
        private CadastroServico cadastroServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;

        public PaginaCadastro()
        {
            InitializeComponent();
            cadastroServico = new CadastroServico(new SqlConnection(connectionString));
        }

        public PaginaCadastro(Cadastro cadastro) : this()
        {
            Cadastro = cadastro;
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
            if (txtNome.Text == "" || txtRua.Text == "" || txtNumeroCasa.Text == "" || txtCidade.Text == "" || mbEstado.Text == "" 
            || cbSexo.Text == "" || cbTipoCadastro.Text == "" || mbCpf.Text == "   .   .   -" || mbTelefone.Text == "+  (  )     -")
                throw new NaoPreenchidoException("Um ou mais campos obrigatórios incompletos!");

        }

        private void Cadastrar()
        {
            string sexo;
            if (cbSexo.Text == "Masculino")
                sexo = "M";
            else
                sexo = "F";
            Cadastro = new Cadastro(cbTipoCadastro.Text, txtNome.Text, sexo, mbCpf.Text, mbTelefone.Text, txtRua.Text, txtNumeroCasa.Text, txtCidade.Text, mbEstado.Text, Convert.ToDateTime(dtNascimento.Text));
            cadastroServico.Gravar(Cadastro);
            Situacao = "Concluído";
            MessageBox.Show("Cadastro Concluído!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtID.Text = Convert.ToString(Cadastro.CadastroID);
            txtNome.Text = Cadastro.Nome;
            txtRua.Text = Cadastro.Rua;
            txtNumeroCasa.Text = Cadastro.NumeroCasa;
            txtCidade.Text = Cadastro.Cidade;

            if(Cadastro.Sexo == "M")
                cbSexo.SelectedItem = "Masculino";
            else
                cbSexo.SelectedItem = "Feminino";
            cbTipoCadastro.SelectedItem = Cadastro.Tipo;
            mbCpf.Text = Cadastro.CPF;
            mbEstado.Text = Cadastro.Estado;
            mbTelefone.Text = Cadastro.Telefone;
            dtNascimento.Text = Cadastro.Nascimento.ToShortDateString();
        }

        private void Editar()
        {
            if (MessageBox.Show("TEM CERTEZA QUE ALTERAR ESSE REGISTRO?\n\nINFORMAÇÕES ALTERADAS NÃO PODEM SER RECUPERADAS", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Cadastro.Nome = txtNome.Text;
                Cadastro.Rua = txtRua.Text;
                Cadastro.NumeroCasa = txtNumeroCasa.Text;
                Cadastro.Cidade = txtCidade.Text;
                string sexo;
                if (cbSexo.Text == "Masculino")
                    sexo = "M";
                else
                    sexo = "F";
                Cadastro.Sexo = sexo;
                Cadastro.Tipo = cbTipoCadastro.Text;
                Cadastro.CPF = mbCpf.Text;
                Cadastro.Estado = mbEstado.Text;
                Cadastro.Telefone = mbTelefone.Text;
                Cadastro.Nascimento = Convert.ToDateTime(dtNascimento.Text);
                cadastroServico.Editar(Cadastro);
                Situacao = "Concluído";
                MessageBox.Show("Alterações Salvas!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
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
