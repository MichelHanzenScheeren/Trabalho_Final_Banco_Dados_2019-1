using System;
using System.Windows.Forms;

namespace Livraria_AdoNet
{
    public partial class MsgFecharPrograma : Form
    {
        public string Resultado = "Cancelar";
        public MsgFecharPrograma()
        {
            InitializeComponent();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Resultado = "OK";
            this.Close();
        }
    }
}
