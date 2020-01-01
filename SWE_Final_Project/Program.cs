using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project {
    static class Program {
        public static Form1 form;

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main() {
            void Run() {
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Start:
            try {
                form = new Form1();
                Application.Run(form);
            } catch (Exception) {
                if (!(form is null))
                    form.Close();
                goto Start;
            }
        }
    }
}
