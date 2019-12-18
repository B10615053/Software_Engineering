using SWE_Final_Project.Views.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    // for checking the completeness of a certain script-model
    [Serializable]
    public enum ScriptModelCompleteness {
        // absolutely empty, or only has GENERALs
        EMPTY_OR_ONLY_HAS_GENERAL,
        // has a START w/o END
        HAS_START_BUT_NO_END,
        // has ENDs w/o START
        HAS_END_BUT_NO_START,
        // has both START and ENDs
        HAS_START_AND_END
    }

    // a mode of a script
    [Serializable]
    public class ScriptModel {
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

        // the completeness of the script-model, initially set to EMPTY
        private ScriptModelCompleteness mCompleteness = ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL;
        public ScriptModelCompleteness Completeness { get => mCompleteness; set => mCompleteness = value; }

        /* ========================================= */

        // constructor
        public ScriptModel(string scriptName, List<StateModel> stateList) {
            // set the script name
            mScriptName = scriptName is null ? "Untitled" : scriptName;

            // initially set the completeness into EMPTY
            mCompleteness = ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL;

            // add the pre-defined states if exist
            if (!(stateList is null))
                mExistedStateList.AddRange(stateList);
        }

        // copy constructor
        public ScriptModel(ScriptModel rhs): this(rhs.Name, rhs.getCopiedStateList()) {}

        // get (deep-) copied existed state list
        public List<StateModel> getCopiedStateList() {
            try {
                using (var ms = new MemoryStream()) {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, mExistedStateList);
                    ms.Position = 0;
                    return (List<StateModel>) formatter.Deserialize(ms);
                }
            } catch (Exception) {
                return null;
            }
        }

        // get a certain state-model designated by id
        public StateModel getStateModelById(string id)
            => mExistedStateList.Find(it => it.Id == id);

        // add a new state
        public void addNewState(StateModel newStateModel) {
            mExistedStateList.Add(newStateModel);

            // re-set the script-model's completeness in different cases
            if (newStateModel.StateType == StateType.START) {
                if (mCompleteness == ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL)
                    mCompleteness = ScriptModelCompleteness.HAS_START_BUT_NO_END;
                else if (mCompleteness == ScriptModelCompleteness.HAS_END_BUT_NO_START)
                    mCompleteness = ScriptModelCompleteness.HAS_START_AND_END;
            }
            else if (newStateModel.StateType == StateType.END) {
                if (mCompleteness == ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL)
                    mCompleteness = ScriptModelCompleteness.HAS_END_BUT_NO_START;
                else if (mCompleteness == ScriptModelCompleteness.HAS_START_BUT_NO_END)
                    mCompleteness = ScriptModelCompleteness.HAS_START_AND_END;
            }
        }

        // modify a existed state
        public void modifyState(StateView stateView) {
            StateModel toBeModifiedState = mExistedStateList.Find(it => it.Id == stateView.Id);
            if (toBeModifiedState is null)
                return;
            toBeModifiedState.setDataByStateView(stateView);
        }

        // check if this script has been saved at least one time or not
        public bool hasBeenSavedAtLeastOneTime() => (!(mSavedFilePath is null));

        // check if the script currently has a START state
        public bool hasStartStateOnScript ()
            => (mCompleteness == ScriptModelCompleteness.HAS_START_BUT_NO_END ||
                mCompleteness == ScriptModelCompleteness.HAS_START_AND_END);

        public bool removeState(string id) {
            foreach (var s in mExistedStateList) {
                if (s.Id == id) {
                    /* deal with the completeness of this script */
                    // if the state the user want to delete is a START state
                    if (s.StateType == StateType.START) {
                        if (mCompleteness == ScriptModelCompleteness.HAS_START_AND_END)
                            mCompleteness = ScriptModelCompleteness.HAS_END_BUT_NO_START;
                        else /* if (mCompleteness == ScriptModelCompleteness.HAS_START_BUT_NO_END) */
                            mCompleteness = ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL;
                    }
                    // if the state the user want to delete is an END state
                    else if (s.StateType == StateType.END) {
                        int numOfEndStates = mExistedStateList.FindAll(it => it.StateType == StateType.END).Count;
                        // the only one END state
                        if (numOfEndStates == 1)
                            if (mCompleteness == ScriptModelCompleteness.HAS_START_AND_END)
                                mCompleteness = ScriptModelCompleteness.HAS_START_BUT_NO_END;
                            else if (mCompleteness == ScriptModelCompleteness.HAS_END_BUT_NO_START)
                                mCompleteness = ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL;
                    }

                    // mark this script as unsaved
                    mHaveUnsavedChanges = true;
                    Program.form.MarkUnsavedScript();

                    // remove it from the state list
                    mExistedStateList.Remove(s);
                    return true;
                }
            }
            return false;
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
