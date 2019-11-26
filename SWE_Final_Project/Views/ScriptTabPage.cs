using SWE_Final_Project.Managers;
using SWE_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views {
    class ScriptTabPage: TabPage {
        public ScriptTabPage(string tabName): base(tabName) {
            // add a whole new canvas
            ScriptCanvas scriptCanvas = new ScriptCanvas(tabName);
            Controls.Add(scriptCanvas);
        }
    }
}
