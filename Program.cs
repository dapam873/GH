using System;
using System.Windows.Forms;

//using System.Threading.Tasks; // nnn
//using Squirrel;// nnn

namespace GH
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GH());
        }
    }
}
