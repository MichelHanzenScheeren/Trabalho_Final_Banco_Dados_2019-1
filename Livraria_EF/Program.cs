using Livraria_AdoNet;
using System;
using System.Windows.Forms;

namespace Livraria_EF
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PaginaInicial());
        }
    }
}
