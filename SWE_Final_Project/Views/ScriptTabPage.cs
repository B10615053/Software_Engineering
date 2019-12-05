using SWE_Final_Project.Managers;
using SWE_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views {
    public class ScriptTabPage: TabPage {
        private ScriptCanvas mScriptCanvas;
        public ScriptCanvas TheScriptCanvas { get => mScriptCanvas; }

        public ScriptTabPage(string tabName, List<StateModel> stateModelList = null): base(tabName) {
            // add a whole new canvas
            mScriptCanvas = new ScriptCanvas(tabName, stateModelList);
            Controls.Add(mScriptCanvas);
        }
    }
}
