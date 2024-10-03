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
            // Para personalizar a configuração do aplicativo, como definir configurações de DPI alta ou fonte padrão,
            // veja https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Master_os()); // Corrigido para instanciar Form1
        }
    }
}
