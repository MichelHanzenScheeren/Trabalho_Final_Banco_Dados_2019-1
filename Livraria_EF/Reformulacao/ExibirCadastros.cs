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
    public partial class ExibirCadastros : Form
    {
        
        private string ultimaPesquisa = null;
        private string filtro = null;
        private CadastroServico cadastroServico;
        private VendaServico vendaServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;
        public ExibirCadastros()
        {
            InitializeComponent();
            cadastroServico = new CadastroServico(new SqlConnection(connectionString));
            vendaServico = new VendaServico(new SqlConnection(connectionString));
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

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            PaginaCadastro paginaCadastro = new PaginaCadastro();
            this.Visible = false;
            paginaCadastro.ShowDialog();
            this.Visible = true;
        }

        private Cadastro CadastroSelecionado()
        {
            try
            {
                int id = Convert.ToInt32(dgvCadastros.CurrentRow.Cells[0].Value.ToString());
                return cadastroServico.BuscarID(id).FirstOrDefault();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Selecione um cadastro primeiro!", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                return null;
            }
        }

        private void BtnbtnEditar_Click(object sender, EventArgs e)
        {
            Cadastro cadastro = CadastroSelecionado();
            if (cadastro != null)
            {
                PaginaCadastro paginaCadastro = new PaginaCadastro(cadastro);
                this.Visible = false;
                paginaCadastro.ShowDialog();
                this.Visible = true;
                if (paginaCadastro.Situacao == "Concluído")
                    AtualizarDataGrid();
            }
        }

        private void BtnApagar_Click(object sender, EventArgs e)
        {
            Cadastro cadastro = CadastroSelecionado();
            if (cadastro != null)
            {
                if (MessageBox.Show("TEM CERTEZA QUE DESEJA APAGAR ESSE REGISTRO?\n\nESSA AÇÂO NÂO PODE SER DESFEITA!\nREGISTROS RELACIONADOS AO CLIENTE TAMBÉM SERÃO APAGADOS!", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    foreach (var item in vendaServico.BuscarGeral("Venda.CadastroID", Convert.ToString(cadastro.CadastroID)))
                    {
                        vendaServico.Excluir(item.VendaID);
                    }
                    cadastroServico.Excluir(cadastro.CadastroID);
                    MessageBox.Show("Registro Apagado!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarDataGrid();
                }
            }
        }

        private void AtualizarDataGrid()
        {
            if (ultimaPesquisa == "Todos")
                PreencherDataGrid(cadastroServico.ObterTodos());
            else
            {
                cbCriterio.Text = filtro;
                cbPesquisa.Text = ultimaPesquisa;
                Pesquisar();
            }
        }

        private void BtnExibirTodos_Click(object sender, EventArgs e)
        {
            PreencherDataGrid(cadastroServico.ObterTodos());
            ultimaPesquisa = "Todos";
        }

        private void PreencherDataGrid(List<Cadastro> cadastros)
        {
            int contLinha = 0, contadorResultados = 0;
            dgvCadastros.Rows.Clear();
            foreach (var item in cadastros)
            {
                contLinha = dgvCadastros.Rows.Add();
                dgvCadastros.Rows[contLinha].Cells[0].Value = item.CadastroID;
                dgvCadastros.Rows[contLinha].Cells[1].Value = item.Tipo;
                dgvCadastros.Rows[contLinha].Cells[2].Value = item.Nome;
                dgvCadastros.Rows[contLinha].Cells[3].Value = item.Rua + ", " + item.NumeroCasa + ". " + item.Cidade + " - " + item.Estado;
                dgvCadastros.Rows[contLinha].Cells[4].Value = item.Telefone;
                contadorResultados++;
                dgvCadastros.AutoResizeColumns();
            }            
            if (contadorResultados == 0)
               MessageBox.Show("Nenhuma correspondência encontrada! Verifique a pesquisa ou filtro utilizado!", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                VerificarCondicoes();
                Pesquisar();
            }
            catch (NaoPreenchidoException erro)
            {
                MessageBox.Show($"Erro na pesquisa!\n {erro.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SystemException)
            {
                MessageBox.Show("Não foi possível concluir a pesquisa!\n Verifique o conteúdo inserido e tente novamente!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VerificarCondicoes()
        {
            if (cbCriterio.Text == "" || cbPesquisa.Text == "")
                throw new NaoPreenchidoException("Um ou mais campos obrigatórios estão em branco ou incompletos!");
        }

        private void Pesquisar()
        {
            if (cbCriterio.Text == "ID")
                PreencherDataGrid(cadastroServico.BuscarID(Convert.ToInt32(cbPesquisa.Text)));
            else if (cbCriterio.Text == "Tipo")
                PreencherDataGrid(cadastroServico.BuscarGeral("Tipo", cbPesquisa.Text));
            else if (cbCriterio.Text == "Nome")
                PreencherDataGrid(cadastroServico.BuscarGeral("Nome", cbPesquisa.Text));
            else if (cbCriterio.Text == "Endereço")
            {
                List<Cadastro> cadastrosEndereco = new List<Cadastro>();
                foreach (var rua in cadastroServico.BuscarGeral("Rua", cbPesquisa.Text))
                {
                    cadastrosEndereco.Add(rua);
                }
                foreach (var cidade in cadastroServico.BuscarGeral("Cidade", cbPesquisa.Text))
                {
                    cadastrosEndereco.Add(cidade);
                }
                foreach (var estado in cadastroServico.BuscarGeral("Estado", cbPesquisa.Text))
                {
                    cadastrosEndereco.Add(estado);
                }
                PreencherDataGrid(cadastrosEndereco);
            }
            else
                PreencherDataGrid(cadastroServico.BuscarGeral("Telefone", cbPesquisa.Text));

            ultimaPesquisa = cbPesquisa.Text;
            filtro = cbCriterio.Text;
        }

        private void CbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCriterio.Text == "Tipo")
            {
                cbPesquisa.Text = "";
                cbPesquisa.DropDownStyle = ComboBoxStyle.DropDownList;
                cbPesquisa.FlatStyle = FlatStyle.Flat;
                cbPesquisa.DroppedDown = true;
            }
            else
            {
                cbPesquisa.DropDownStyle = ComboBoxStyle.Simple;
                cbPesquisa.FlatStyle = FlatStyle.System;
            }   
        }

        //////////////////////////////// Mover form ao clicar ///////////////////////////////////////////
        bool ClickMouse;
        Point LocalClicado;
        private void Top_MouseUp_1(object sender, MouseEventArgs e)
        {
            ClickMouse = false;
        }
        private void Top_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            ClickMouse = true;
            LocalClicado = e.Location;
        }
        private void Top_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (ClickMouse)
                this.Location = new Point(Cursor.Position.X - LocalClicado.X, Cursor.Position.Y - LocalClicado.Y);
        }
    }
}
