using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    // a mode of a script
    [Serializable]
    class ScriptModel {
        // script's name
        private string mScriptName = "Untitled";
        public string Name { get => mScriptName; set => mScriptName = value; }

        // existed states
        private List<StateModel> mExistedStateList = new List<StateModel>();

        // there's any unsaved change or not
        private bool mHaveUnsavedChanges = true;
        public bool HaveUnsavedChanges { get => mHaveUnsavedChanges; set => mHaveUnsavedChanges = value; }

        /* ========================================= */

        // constructor
        public ScriptModel(string scriptName, List<StateModel> stateList) {
            mScriptName = scriptName is null ? "Untitled" : scriptName;

            if (!(stateList is null))
                mExistedStateList.AddRange(stateList);
        }

        // get (deep-) copied existed state list
        public List<StateModel> getCopiedStateList() {
            using (var ms = new MemoryStream()) {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, mExistedStateList);
                ms.Position = 0;
                return (List<StateModel>) formatter.Deserialize(ms);
            }
        }

        // add a new state
        public void addNewState(StateModel newStateModel) {
            mExistedStateList.Add(newStateModel);
        }

        /* ========================================= */

        public override bool Equals(object obj) {
            var other = obj as ScriptModel;
            if (other == null)
                return false;

            return mScriptName == other.mScriptName;
        }

        public override int GetHashCode() {
            return mScriptName.GetHashCode();
        }
    }
}
