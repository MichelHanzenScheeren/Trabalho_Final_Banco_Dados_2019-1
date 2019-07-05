using Modelo.Exception;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Livraria_AdoNet
{
    public partial class PaginaInicial : Form
    {
        public PaginaInicial()
        {
            InitializeComponent();
           
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            MsgFecharPrograma fechar = new MsgFecharPrograma();
            fechar.ShowDialog();
            if(fechar.Resultado == "OK")
                this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnCadastros_Click(object sender, EventArgs e)
        {
            try
            {
                var exibirCadastros = new ExibirCadastros();
                this.Visible = false;
                exibirCadastros.ShowDialog();
                this.Visible = true;
            }
            catch (FecharException)
            {
                this.Close();
            }
        }

        private void BtnLivros_Click(object sender, EventArgs e)
        {
            try
            {
                var exibirLivro = new ExibirLivro();
                this.Visible = false;
                exibirLivro.ShowDialog();
                this.Visible = true;
            }
            catch (FecharException)
            {
                this.Close();
            }
        }

        private void BtnCompras_Click(object sender, EventArgs e)
        {
            try
            {
                var exibirCompras = new ExibirCompras();
                this.Visible = false;
                exibirCompras.ShowDialog();
                this.Visible = true;
            }
            catch (FecharException)
            {
                this.Close();
            }
            
        }

        private void BtnVendas_Click(object sender, EventArgs e)
        {
            try
            {
                var exibirVenda = new ExibirVenda();
                this.Visible = false;
                exibirVenda.ShowDialog();
                this.Visible = true;
            }
            catch (FecharException)
            {
                this.Close();
            }
        }

        //////////////////////////////// Mover form ao clicar ///////////////////////////////////////////
        bool ClickMouse;
        Point LocalClicado;
        private void Top_MouseMove(object sender, MouseEventArgs e)
        {
            if (ClickMouse)
                this.Location = new Point(Cursor.Position.X - LocalClicado.X, Cursor.Position.Y - LocalClicado.Y);
        }
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
    }
}
