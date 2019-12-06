using SWE_Final_Project.Models;
using SWE_Final_Project.Views;
using SWE_Final_Project.Views.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Managers {
    class ModelManager {
        private static StateInfoTableLayoutPanel mInfoPanel = null;

        // the index of current working-on script of all opened scripts
        public static int CurrentSelectedScriptIndex = -1;

        // store all opened scripts
        private static List<ScriptModel> mOpenedScriptList = new List<ScriptModel>();

        // get (deep-) copied opened script list
        public static List<ScriptModel> getCopiedScriptList() {
            using (var ms = new MemoryStream()) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, mOpenedScriptList);
                ms.Position = 0;
                return (List<ScriptModel>) formatter.Deserialize(ms);
            }
        }

        // get the designated script-model by index
        public static ScriptModel getScriptModelByIndex(int idx) {
            if (idx >= 0 && idx < mOpenedScriptList.Count)
                return mOpenedScriptList[idx];
            return null;
        }

        // get the designated state-model by id
        public static StateModel getStateModelByIdAtCurrentScript(string id) {
            if (CurrentSelectedScriptIndex < 0)
                return null;
            return mOpenedScriptList[CurrentSelectedScriptIndex].getStateModelById(id);
        }

        // add new script, and return the re-adjusted script name
        public static ScriptModel addNewScript(string newScriptName, List<StateModel> stateModels = null) {
            // deal w/ duplicated script names
            string origNewScriptName = newScriptName;
            int cnt = 2;
            while (mOpenedScriptList.Any(it => it.Name == newScriptName)) {
                newScriptName = origNewScriptName + " (" + cnt + ")";
                ++cnt;
            }

            ScriptModel newScriptModel = new ScriptModel(newScriptName, stateModels);
            mOpenedScriptList.Add(newScriptModel);

            // invalidate the start-state-view on the shell if needs
            Program.form.getCertainStateViewOnTheShell(0).Invalidate();

            return newScriptModel;
        }

        // open the saved script
        /// <summary>
        /// return true if added successfully
        /// </summary>
        /// <param name="scriptModel"></param>
        /// <returns></returns>
        public static bool openScript(ScriptModel scriptModel) {
            ScriptModel scriptModelInList = mOpenedScriptList.Find(it => it.Equals(scriptModel));

            // not in the opened script list, open it
            if (scriptModelInList is null) {
                mOpenedScriptList.Add(scriptModel);

                // invalidate the start-state-view on the shell if needs
                Program.form.getCertainStateViewOnTheShell(0).Invalidate();

                return true;
            }
            // already in the opened script list
            else
                return false;
        }

        // close the current working-on script
        public static void closeScript() {
            if (CurrentSelectedScriptIndex < 0)
                return;

            // remove the script-model from the list,
            mOpenedScriptList.RemoveAt(CurrentSelectedScriptIndex);

            // and re-adjust the current-selected-script-index
            if (CurrentSelectedScriptIndex >= mOpenedScriptList.Count)
                CurrentSelectedScriptIndex = mOpenedScriptList.Count - 1;

            // invalidate the start-state-view on the shell if needs
            Program.form.getCertainStateViewOnTheShell(0).Invalidate();
        }

        // add new state on a certain script
        public static void addNewStateOnCertainScript(StateModel newStateModel) {
            if (CurrentSelectedScriptIndex < 0)
                return;
            mOpenedScriptList[CurrentSelectedScriptIndex].addNewState(newStateModel);
            mOpenedScriptList[CurrentSelectedScriptIndex].HaveUnsavedChanges = true;
            Program.form.MarkUnsavedScript();
            // debugPrint();
        }

        // rename the current working-on script
        public static void renameScript(string newScriptName, bool isSaving) {
            if (CurrentSelectedScriptIndex < 0)
                return;
            mOpenedScriptList[CurrentSelectedScriptIndex].Name = newScriptName;
            mOpenedScriptList[CurrentSelectedScriptIndex].HaveUnsavedChanges = !isSaving;
            if (!isSaving)
                Program.form.MarkUnsavedScript();
        }

        // modify a certain state on a certain script
        public static void modifyStateOnCertainScript(StateView stateView) {
            if (CurrentSelectedScriptIndex < 0)
                return;
            mOpenedScriptList[CurrentSelectedScriptIndex].modifyState(stateView);
            mOpenedScriptList[CurrentSelectedScriptIndex].HaveUnsavedChanges = true;
            Program.form.MarkUnsavedScript();
            // debugPrint();
        }

        // add a new link between two states
        public static void addLinkBetween2StatesOnCertainScript(StateModel from, PortType fromPortType, StateModel to, PortType toPortType, LinkModel linkModel) {
            if (CurrentSelectedScriptIndex < 0)
                return;

            mOpenedScriptList[CurrentSelectedScriptIndex].getStateModelById(from.Id).addLinkAtCertainPort(linkModel, fromPortType, true);
            mOpenedScriptList[CurrentSelectedScriptIndex].getStateModelById(to.Id).addLinkAtCertainPort(linkModel, toPortType, false);

            mOpenedScriptList[CurrentSelectedScriptIndex].HaveUnsavedChanges = true;
            Program.form.MarkUnsavedScript();
        }

        // show the info-panel at the right-side of the form
        public static void showInfoPanel(PictureBox view) {
            // get the panel as the container
            Panel infoContainer = Program.form.panelInfoContainer;

            // create a new info-apnel
            if (view is StateView)
                mInfoPanel = new StateInfoTableLayoutPanel(view as StateView);
            else if (view is LinkView)
                mInfoPanel = new StateInfoTableLayoutPanel(view as LinkView);

            // remove the existed one and add the new one
            infoContainer.Controls.Clear();
            infoContainer.Controls.Add(mInfoPanel);

            // focus on the txt-show-text
            Control[] controls = mInfoPanel.Controls.Find("txtShowText", false);
            if (controls.Length == 1)
                controls[0].Select();
        }

        // remove the info-panel
        public static void removeInfoPanel() {
            if (mInfoPanel is null)
                return;

            // get the panel as the container
            Panel infoContainer = Program.form.panelInfoContainer;

            // remove
            infoContainer.Controls.Clear();
            mInfoPanel = null;
        }

        // for debugging
        public static void debugPrint() {
            mOpenedScriptList.ForEach(it => {
                Console.WriteLine(it.ToString());
                Console.WriteLine("========");
            });
            Console.WriteLine("********************");
        }
    }
}
