using System;
using System.Windows.Forms;

namespace MasterOS
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Para personalizar a configura��o do aplicativo, como definir configura��es de DPI alta ou fonte padr�o,
            // veja https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Master_os()); // Corrigido para instanciar Form1
        }
    }
}
