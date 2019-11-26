using SWE_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Managers {
    class ModelManager {
        // store all opened scripts
        private static List<ScriptModel> openedScriptList = new List<ScriptModel>();

        // add new script, and return the re-adjusted script name
        public static string addNewScript(string newScriptName, List<StateModel> stateModels = null) {
            // deal w/ duplicated script names
            string origNewScriptName = newScriptName;
            int cnt = 2;
            while (openedScriptList.Any(it => it.Name == newScriptName)) {
                newScriptName = origNewScriptName + " (" + cnt + ")";
                ++cnt;
            }

            ScriptModel newScriptModel = new ScriptModel(newScriptName, stateModels);
            openedScriptList.Add(newScriptModel);

            return newScriptName;
        }

        // add new state on a certain script
        public static void addNewStateOnCertainScript(string scriptName, StateModel newStateModel) {
            openedScriptList.Find(it => it.Name == scriptName).addNewState(newStateModel);
        }
    }
}
