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
    public partial class ExibirVenda : Form
    {
        private string ultimaPesquisa = null;
        private string filtro = null;
        private VendaServico vendaServico;
        private LivroServico livroServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;

        public ExibirVenda()
        {
            InitializeComponent();
            vendaServico = new VendaServico(new SqlConnection(connectionString));
            livroServico = new LivroServico(new SqlConnection(connectionString));
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
            PaginaVenda paginaVenda = new PaginaVenda();
            this.Visible = false;
            paginaVenda.ShowDialog();
            this.Visible = true;
            PreencherDataGrid(vendaServico.ObterTodos());
        }

        private Venda VendaSelecionada()
        {
            try
            {
                int id = Convert.ToInt32(dgvCadastros.CurrentRow.Cells[0].Value.ToString());
                return vendaServico.BuscarID(id).FirstOrDefault();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Selecione uma venda primeiro!", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                return null;
            }
        }

        private void BtnbtnEditar_Click(object sender, EventArgs e)
        {
            Venda venda = VendaSelecionada();
            if(venda != null)
            {
                PaginaVenda paginaVenda = new PaginaVenda(venda);
                this.Visible = false;
                paginaVenda.ShowDialog();
                this.Visible = true;
                AtualizarDataGrid();
            }
        }

        private void BtnApagar_Click(object sender, EventArgs e)
        {
            Venda venda = VendaSelecionada();
            if (venda != null)
            {
                if (MessageBox.Show("TEM CERTEZA QUE DESEJA CANCELAR E APAGAR ESSE REGISTRO?\n\nOBS:\nESSA AÇÂO NÂO PODE SER DESFEITA!\nO ESTOQUE ENVOLVIDO SERÁ ATUALIZADO!", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    AtualizarEstoque(venda);
                    vendaServico.Excluir(venda.VendaID);
                    MessageBox.Show("Registro Apagado!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarDataGrid();
               
                }
            }
        }

        private void AtualizarEstoque(Venda venda)
        {
            Livro livro = livroServico.BuscarID(venda.Livro.LivroID).FirstOrDefault();
            livro.Estoque += venda.Quantidade;
            livroServico.Editar(livro);
        }

        private void AtualizarDataGrid()
        {
            if (ultimaPesquisa == "Todos")
                PreencherDataGrid(vendaServico.ObterTodos());
            else
            {
                cbCriterio.Text = filtro;
                cbPesquisa.Text = ultimaPesquisa;
                Pesquisar();
            }
        }

        private void BtnExibirTodos_Click(object sender, EventArgs e)
        {
            PreencherDataGrid(vendaServico.ObterTodos());
            ultimaPesquisa = "Todos";
        }

        private void PreencherDataGrid(List<Venda> vendas)
        {
            int contLinha = 0, contadorResultados = 0;
            dgvCadastros.Rows.Clear();
            foreach (var item in vendas)
            {
                contLinha = dgvCadastros.Rows.Add();
                dgvCadastros.Rows[contLinha].Cells[0].Value = item.VendaID;
                dgvCadastros.Rows[contLinha].Cells[1].Value = item.Data.ToShortDateString();
                dgvCadastros.Rows[contLinha].Cells[2].Value = item.Livro.Titulo + ", de " + item.Livro.Autor;
                dgvCadastros.Rows[contLinha].Cells[3].Value = item.Cadastro.Nome + ". CPF: " + item.Cadastro.CPF;
                dgvCadastros.Rows[contLinha].Cells[4].Value = item.Quantidade;
                contadorResultados++;
            }
            if (contadorResultados == 0)
                MessageBox.Show("Nenhuma correspondência encontrada!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.None);
            else
                dgvCadastros.AutoResizeColumns();
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
                PreencherDataGrid(vendaServico.BuscarID(Convert.ToInt32(cbPesquisa.Text)));
            else if (cbCriterio.Text == "Data")
                PreencherDataGrid(vendaServico.BuscarGeral("Data", cbPesquisa.Text));
            else if (cbCriterio.Text == "Livro")
            {
                List<Venda> vendas = new List<Venda>();
                foreach (var titulo in vendaServico.BuscarGeral("Titulo", cbPesquisa.Text))
                {
                    vendas.Add(titulo);
                }
                foreach (var autor in vendaServico.BuscarGeral("Autor", cbPesquisa.Text))
                {
                    vendas.Add(autor);
                }
                foreach (var editor in vendaServico.BuscarGeral("Editora", cbPesquisa.Text))
                {
                    vendas.Add(editor);
                }
                PreencherDataGrid(vendas);
            }
            else
            {
                List<Venda> vendas = new List<Venda>();
                foreach (var nome in vendaServico.BuscarGeral("Nome", cbPesquisa.Text))
                {
                    vendas.Add(nome);
                }
                foreach (var cpf in vendaServico.BuscarGeral("CPF", cbPesquisa.Text))
                {
                    vendas.Add(cpf);
                }
                foreach (var cidade in vendaServico.BuscarGeral("Cidade", cbPesquisa.Text))
                {
                    vendas.Add(cidade);
                }
                foreach (var estado in vendaServico.BuscarGeral("Estado", cbPesquisa.Text))
                {
                    vendas.Add(estado);
                }
                PreencherDataGrid(vendas);
            }
            ultimaPesquisa = cbPesquisa.Text;
            filtro = cbCriterio.Text;
        }

        private void CbCriterio_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (cbCriterio.Text == "Data")
            {
                cbPesquisa.Text = DateTime.Now.ToShortDateString();
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
