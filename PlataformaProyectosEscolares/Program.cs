using System;
using System.Windows.Forms;
using PlataformaProyectosEscolares.Forms;

namespace PlataformaProyectosEscolares
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
