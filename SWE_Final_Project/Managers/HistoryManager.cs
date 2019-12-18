using SWE_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Managers {
    // be used to manage the history versions of all opened scripts
    public class HistoryManager {
        // the history versions stacks of all opened scripts
        private static List<KeyValuePair<Stack<ScriptModel>, Stack<ScriptModel>>>
            mScriptsHistoryStacks = new List<KeyValuePair<Stack<ScriptModel>, Stack<ScriptModel>>>();

        // when creating or opening a script, start it's history record
        public static void startScriptHistory(ScriptModel newScriptModel) {
            if (newScriptModel == null)
                return;

            // the "done" stack
            Stack<ScriptModel> newDoneStack = new Stack<ScriptModel>();
            newDoneStack.Push(new ScriptModel(newScriptModel));

            // the "undone" stack
            Stack<ScriptModel> newUndoneStack = new Stack<ScriptModel>();

            // merge them into a key-value-pair and add it to the history-stack
            mScriptsHistoryStacks.Add(
                new KeyValuePair<Stack<ScriptModel>, Stack<ScriptModel>>(newDoneStack, newUndoneStack)
            );
        }

        // completely close and remove a certain script's histories by its index
        public static void closeAndRemoveScriptHistoryByIdx(int scriptIndex = -1) {
            if (scriptIndex == -1)
                scriptIndex = ModelManager.CurrentSelectedScriptIndex;
            if (scriptIndex >= 0 && scriptIndex < mScriptsHistoryStacks.Count)
                mScriptsHistoryStacks.RemoveAt(scriptIndex);
        }

        // the user do a change at a certain script
        public static void Do(ScriptModel doneScriptModel, int scriptIndex = -1) {
            if (scriptIndex == -1)
                scriptIndex = ModelManager.CurrentSelectedScriptIndex;

            // push the model into the "done" stack
            if (scriptIndex >= 0 && scriptIndex < mScriptsHistoryStacks.Count) {
                mScriptsHistoryStacks[scriptIndex].Key.Push(new ScriptModel(doneScriptModel));
                clearUndoneStack(scriptIndex);

                debugPrint();
            }
        }

        // the user undo a change at a certain script
        public static ScriptModel Undo(int scriptIndex = -1) {
            if (scriptIndex == -1)
                scriptIndex = ModelManager.CurrentSelectedScriptIndex;

            if (scriptIndex >= 0 && scriptIndex < mScriptsHistoryStacks.Count) {
                var pair = mScriptsHistoryStacks[scriptIndex];

                // check if the "done" stack has any history record or not before undoing
                if (pair.Key.Count > 1) {
                    ScriptModel undoneScript = pair.Key.Pop();
                    pair.Value.Push(new ScriptModel(undoneScript));

                    debugPrint();

                    return new ScriptModel(pair.Key.Peek());
                }
            }

            return null;
        }

        // the user redo a change at a certain script
        public static ScriptModel Redo(int scriptIndex = -1) {
            if (scriptIndex == -1)
                scriptIndex = ModelManager.CurrentSelectedScriptIndex;

            if (scriptIndex >= 0 && scriptIndex < mScriptsHistoryStacks.Count) {
                var pair = mScriptsHistoryStacks[scriptIndex];

                // check if the "undone" stack has any history record or not before redoing
                if (pair.Value.Count > 0) {
                    ScriptModel redoneScript = pair.Value.Pop();
                    pair.Key.Push(new ScriptModel(redoneScript));

                    return new ScriptModel(pair.Key.Peek());
                }
            }

            return null;
        }

        // clear the "undone" stack
        private static void clearUndoneStack(int scriptIndex = -1) {
            if (scriptIndex == -1)
                scriptIndex = ModelManager.CurrentSelectedScriptIndex;
            if (scriptIndex >= 0 && scriptIndex < mScriptsHistoryStacks.Count)
                mScriptsHistoryStacks[scriptIndex].Value.Clear();
        }

        // for debugging
        private static void debugPrint() {
            Console.WriteLine("DEBUG START]]");
            mScriptsHistoryStacks.ForEach(pair => {
                Console.WriteLine("done stack:");
                foreach (ScriptModel script in pair.Key)
                    Console.WriteLine(script.ToString());
                Console.WriteLine("undone stack:");
                foreach (ScriptModel script in pair.Value)
                    Console.WriteLine(script.ToString());
                Console.WriteLine("=================================");
            });
        }
    }
}
