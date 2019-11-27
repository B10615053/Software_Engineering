using SWE_Final_Project.Views.States;
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

        // script's saved file path
        private string mSavedFilePath = null;
        public string SavedFilePath { get => mSavedFilePath; set => mSavedFilePath = value; }

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

        // modify a existed state
        public void modifyState(StateView stateView) {
            StateModel toBeModifiedState = mExistedStateList.Find(it => it.Id == stateView.Id);
            if (toBeModifiedState is null)
                return;
            toBeModifiedState.setDataByStateView(stateView);
        }

        /* ========================================= */

        public override bool Equals(object obj) {
            var other = obj as ScriptModel;
            if (other == null)
                return false;

            if (!(mSavedFilePath is null) && other.mSavedFilePath is null)
                return false;
            if (mSavedFilePath is null && !(other.mSavedFilePath is null))
                return false;
            if (mSavedFilePath is null && other.mSavedFilePath is null)
                return mScriptName == other.mScriptName;
            return mScriptName == other.mScriptName && mSavedFilePath == other.mSavedFilePath;
        }

        public override int GetHashCode() {
            if (mSavedFilePath is null)
                return mScriptName.GetHashCode();
            return mScriptName.GetHashCode() ^ mSavedFilePath.GetHashCode();
        }

        public override string ToString() {
            string ret = mScriptName + "\r\n";
            mExistedStateList.ForEach(it => {
                ret += "\t" + it.ToString() + "\r\n";
            });
            return ret;
        }
    }
}
