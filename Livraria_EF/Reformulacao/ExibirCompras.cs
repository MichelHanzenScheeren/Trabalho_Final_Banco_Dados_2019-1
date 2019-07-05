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
    public partial class ExibirCompras : Form
    {
        private string ultimaPesquisa = null;
        private string filtro = null;
        private CompraServico compraServico;
        private LivroServico livroServico;
        string connectionString = ConfigurationManager.ConnectionStrings["CS_ADO_NET"].ConnectionString;
        public ExibirCompras()
        {
            InitializeComponent();
            compraServico = new CompraServico(new SqlConnection(connectionString));
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
            PaginaCompra paginaCompra = new PaginaCompra();
            this.Visible = false;
            paginaCompra.ShowDialog();
            this.Visible = true;
            AtualizarDataGrid();
        }

        private Compra CompraSelecionada()
        {
            try
            {
                int id = Convert.ToInt32(dgvCadastros.CurrentRow.Cells[0].Value.ToString());
                return compraServico.BuscarID(id).FirstOrDefault();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Selecione uma compra primeiro!", "Info", MessageBoxButtons.OK, MessageBoxIcon.None);
                return null;
            }
        }

        private void BtnbtnEditar_Click(object sender, EventArgs e)
        {
            Compra compra = CompraSelecionada();
            if (compra != null)
            {
                PaginaCompra paginaCompra = new PaginaCompra(compra);
                this.Visible = false;
                paginaCompra.ShowDialog();
                this.Visible = true;
                AtualizarDataGrid();
            }
        }

        private void BtnApagar_Click(object sender, EventArgs e)
        {
            Compra compra = CompraSelecionada();
            if (compra != null)
            {
                if (MessageBox.Show("TEM CERTEZA QUE DESEJA CANCELAR E APAGAR ESSE REGISTRO?\n\nOBS:\nESSA AÇÃO NÃO PODE SER DESFEITA!\nO ESTOQUE ENVOLVIDO SERÁ ATUALIZADO!", "ATENÇÃO!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    try
                    {
                        AtualizarEstoque(compra);
                        compraServico.Excluir(compra.CompraID);
                        MessageBox.Show("Registro Apagado!", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AtualizarDataGrid();

                    }
                    catch (EstoqueVazioException erro)
                    {
                        MessageBox.Show($"Erro na operação!\n {erro.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AtualizarEstoque(Compra compra)
        {
            Livro livro = livroServico.BuscarID(compra.Livro.LivroID).FirstOrDefault();
            if (livro.Estoque < compra.Quantidade)
                throw new EstoqueVazioException("Estoque insuficiente!");
            livro.Estoque -= compra.Quantidade;
            livroServico.Editar(livro);
        }

        private void AtualizarDataGrid()
        {
            if (ultimaPesquisa == "Todos")
                PreencherDataGrid(compraServico.ObterTodos());
            else
            {
                cbCriterio.Text = filtro;
                cbPesquisa.Text = ultimaPesquisa;
                Pesquisar();
            }
        }

        private void BtnExibirTodos_Click(object sender, EventArgs e)
        {
            PreencherDataGrid(compraServico.ObterTodos());
            ultimaPesquisa = "Todos";
        }

        private void PreencherDataGrid(List<Compra> compras)
        {
            int contLinha = 0, contadorResultados = 0;
            dgvCadastros.Rows.Clear();
            foreach (var item in compras)
            {
                contLinha = dgvCadastros.Rows.Add();
                dgvCadastros.Rows[contLinha].Cells[0].Value = item.CompraID;
                dgvCadastros.Rows[contLinha].Cells[1].Value = item.Data.ToShortDateString();
                dgvCadastros.Rows[contLinha].Cells[2].Value = item.Livro.Titulo + ", de " + item.Livro.Autor;
                dgvCadastros.Rows[contLinha].Cells[3].Value = "R$ " + item.PrecoUnidade;
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
                PreencherDataGrid(compraServico.BuscarID(Convert.ToInt32(cbPesquisa.Text)));
            else if (cbCriterio.Text == "Data")
                PreencherDataGrid(compraServico.BuscarGeral("Data", cbPesquisa.Text));
            else if (cbCriterio.Text == "Livro")
            {
                List<Compra> compras = new List<Compra>();
                foreach (var titulo in compraServico.BuscarGeral("Titulo", cbPesquisa.Text))
                {
                    compras.Add(titulo);
                }
                foreach (var autor in compraServico.BuscarGeral("Autor", cbPesquisa.Text))
                {
                    compras.Add(autor);
                }
                foreach (var editor in compraServico.BuscarGeral("Editora", cbPesquisa.Text))
                {
                    compras.Add(editor);
                }
                PreencherDataGrid(compras);
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
