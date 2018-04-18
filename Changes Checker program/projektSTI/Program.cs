using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektSTI
{
    static class Program
    {

        public static LoginForm LoginForm;
        //public static MainForm MainForm;

        /// <summary>
        /// Hlavní vstupní bod aplikace.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                   LoginForm = new LoginForm();
                   LoginForm.StartPosition = FormStartPosition.CenterScreen;
                   Application.Run(LoginForm);

             //MainForm = new MainForm();
             //Application.Run(MainForm);
        }
    }
}
