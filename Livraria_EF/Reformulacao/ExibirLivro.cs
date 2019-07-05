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
    public partial class ExibirLivro : Form
    {
        private string ultimaPesquisa = null;
        private string filtro = null;
        private LivroServico livroServico;
        private VendaServico vendaServico;
        private CompraServico compraServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;

        public ExibirLivro()
        {
            InitializeComponent();
            livroServico = new LivroServico(new SqlConnection(connectionString));
            vendaServico = new VendaServico(new SqlConnection(connectionString));
            compraServico = new CompraServico(new SqlConnection(connectionString));
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
            PaginaLivro paginaLivro = new PaginaLivro();
            this.Visible = false;
            paginaLivro.ShowDialog();
            this.Visible = true;
            AtualizarDataGrid();
        }

        private Livro LivroSelecionado()
        {
            try
            {
                int id = Convert.ToInt32(dgvCadastros.CurrentRow.Cells[0].Value.ToString());
                return livroServico.BuscarID(id).FirstOrDefault();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Selecione um livro primeiro!", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                return null;
            }
        }

        private void BtnbtnEditar_Click(object sender, EventArgs e)
        {
            Livro livro = LivroSelecionado();
            if (livro != null)
            {
                PaginaLivro paginaLivro = new PaginaLivro(livro);
                this.Visible = false;
                paginaLivro.ShowDialog();
                this.Visible = true;
                if (paginaLivro.Situacao == "Concluído")
                    AtualizarDataGrid();
            }
        }

        private void BtnApagar_Click(object sender, EventArgs e)
        {
            Livro livro = LivroSelecionado();
            if (livro != null)
            {
                if (MessageBox.Show("TEM CERTEZA QUE DESEJA APAGAR ESSE REGISTRO?\n\nESSA AÇÂO NÂO PODE SER DESFEITA!\n\nINFORMAÇÕES DE COMPRAS E VENDAS RELACIONADAS A ESTE LIVRO TAMBÉM SERÃO APAGADOS!", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    foreach (var item in vendaServico.BuscarGeral("Venda.LivroID", Convert.ToString(livro.LivroID)))
                    {
                        vendaServico.Excluir(item.VendaID);
                    }
                    foreach (var item in compraServico.BuscarGeral("Compra.LivroID", Convert.ToString(livro.LivroID)))
                    {
                        compraServico.Excluir(item.CompraID);
                    }
                    livroServico.Excluir(livro.LivroID);
                    MessageBox.Show("Registro Apagado!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarDataGrid();
                }
            }
        }

        private void AtualizarDataGrid()
        {
            if (ultimaPesquisa == "Todos")
                PreencherDataGrid(livroServico.ObterTodos());
            else
            {
                cbCriterio.Text = filtro;
                cbPesquisa.Text = ultimaPesquisa;
                Pesquisar();
            }
        }

        private void BtnExibirTodos_Click(object sender, EventArgs e)
        {
            PreencherDataGrid(livroServico.ObterTodos());
            ultimaPesquisa = "Todos";
        }

        private void PreencherDataGrid(List<Livro> livros)
        {
            int contLinha = 0, contadorResultados = 0;
            dgvCadastros.Rows.Clear();
            foreach (var item in livros)
            {
                contLinha = dgvCadastros.Rows.Add();
                dgvCadastros.Rows[contLinha].Cells[0].Value = item.LivroID;
                dgvCadastros.Rows[contLinha].Cells[1].Value = item.Tipo;
                dgvCadastros.Rows[contLinha].Cells[2].Value = item.Titulo;
                dgvCadastros.Rows[contLinha].Cells[3].Value = item.Autor;
                dgvCadastros.Rows[contLinha].Cells[4].Value = item.Editora;
                dgvCadastros.Rows[contLinha].Cells[5].Value = item.Estoque;
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
                PreencherDataGrid(livroServico.BuscarID(Convert.ToInt32(cbPesquisa.Text)));
            else if (cbCriterio.Text == "Tipo")
                PreencherDataGrid(livroServico.BuscarGeral("Tipo", cbPesquisa.Text));
            else if (cbCriterio.Text == "Título")
                PreencherDataGrid(livroServico.BuscarGeral("Titulo", cbPesquisa.Text));
            else if (cbCriterio.Text == "Autor(a)")
                PreencherDataGrid(livroServico.BuscarGeral("Autor", cbPesquisa.Text));
            else
                PreencherDataGrid(livroServico.BuscarGeral("Editora", cbPesquisa.Text));
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
