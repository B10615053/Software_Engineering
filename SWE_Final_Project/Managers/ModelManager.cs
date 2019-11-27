using SWE_Final_Project.Models;
using SWE_Final_Project.Views.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Managers {
    class ModelManager {
        public static int CurrentSelectedScriptIndex = -1;

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
        public static void addNewStateOnCertainScript(StateModel newStateModel) {
            if (CurrentSelectedScriptIndex < 0)
                return;
            openedScriptList[CurrentSelectedScriptIndex].addNewState(newStateModel);
            debugPrint();
        }

        // modify a certain state on a certain script
        public static void modifyStateOnCertainScript(StateView stateView) {
            if (CurrentSelectedScriptIndex < 0)
                return;
            openedScriptList[CurrentSelectedScriptIndex].modifyState(stateView);
            debugPrint();
        }

        public static void debugPrint() {
            openedScriptList.ForEach(it => {
                Console.WriteLine(it.ToString());
                Console.WriteLine("========");
            });
            Console.WriteLine("********************");
        }
    }
}
