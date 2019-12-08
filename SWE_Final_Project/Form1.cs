using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWE_Final_Project.Managers;
using SWE_Final_Project.Models;
using SWE_Final_Project.Views;
using SWE_Final_Project.Views.States;
using SWE_Final_Project.Views.SubForms;

namespace SWE_Final_Project {
    public partial class Form1: Form {
        private List<StateView> mStateViewListOnTheShell = new List<StateView>();

        public Form1() {
            InitializeComponent();
            buildUpStatesList();
        }

        // put the 3 types of states on the left side of GUI
        private void buildUpStatesList() {
            int panelW = statesListPanel.Width;

            int locX = panelW / 2;

            StateView startStateView = new StartStateView(locX, 50, "", false);
            statesListPanel.Controls.Add(startStateView);
            mStateViewListOnTheShell.Add(startStateView);

            StateView endStateView = new EndStateView(locX, 140, "", false);
            statesListPanel.Controls.Add(endStateView);
            mStateViewListOnTheShell.Add(endStateView);

            StateView generalStateView = new GeneralStateView(locX, 240, "", false);
            statesListPanel.Controls.Add(generalStateView);
            mStateViewListOnTheShell.Add(generalStateView);
        }

        // get a certain on-the-shell state-view
        public StateView getCertainStateViewOnTheShell(int idx) {
            if (idx >= 0 && idx < mStateViewListOnTheShell.Count)
                return mStateViewListOnTheShell[idx];
            return null;
        }

        // add a new script
        private void addNewScript() {
            if (SimulationManager.checkSimulating())
                return;

            // add new script to model-manager
            ScriptModel adjustedScriptModel = ModelManager.addNewScript("Untitled");

            // add new tab-page
            addNewTabPage(adjustedScriptModel, true);
        }

        // add new tab-page of a script
        private void addNewTabPage(ScriptModel scriptModel, bool shouldMarkAsUnsaved) {
            // create a new tab-page
            ScriptTabPage tabPage = new ScriptTabPage(scriptModel.Name, scriptModel.getCopiedStateList());
            scriptsTabControl.TabPages.Add(tabPage);

            // select the new script
            scriptsTabControl.SelectedIndex = scriptsTabControl.TabPages.Count - 1;

            // mark as unsaved if needs
            if (shouldMarkAsUnsaved)
                MarkUnsavedScript();

            // set the selected script index
            ModelManager.CurrentSelectedScriptIndex = scriptsTabControl.SelectedIndex;
        }

        // save the script at the current tab
        public bool saveCertainScript(int scriptTabIdx) {
            if (scriptTabIdx < 0 || SimulationManager.checkSimulating())
                return false;

            // get the current tab text w/o asterisk
            string currentTabTextWithoutAsterisk = scriptsTabControl.SelectedTab.Text.ToString();
            currentTabTextWithoutAsterisk = currentTabTextWithoutAsterisk.TrimEnd('*');

            // get the script-model
            ScriptModel scriptModel = ModelManager.getScriptModelByIndex();

            // if it's the twice or more times to save the file,
            // no need to prompt a save-dialog
            if (scriptModel.hasBeenSavedAtLeastOneTime()) {
                // have unsaved changes
                if (scriptModel.HaveUnsavedChanges) {
                    // build the new path, including path, file name, and extension
                    int lastIdxOfSeperator = scriptModel.SavedFilePath.LastIndexOf('\\');
                    string newPath;
                    if (lastIdxOfSeperator >= 0)
                        newPath = scriptModel.SavedFilePath.Substring(0, lastIdxOfSeperator + 1) + currentTabTextWithoutAsterisk + ".sms";
                    else
                        newPath = currentTabTextWithoutAsterisk + ".sms";

                    // store the file path to the script-model
                    scriptModel.SavedFilePath = newPath;
                    scriptModel.HaveUnsavedChanges = false;

                    // serialize
                    SerializationManager.serialize(scriptModel, newPath);

                    // trim the asterisk at the end
                    scriptsTabControl.SelectedTab.Text = scriptsTabControl.SelectedTab.Text.TrimEnd('*');

                    return true;
                }

                return false;
            }



            /* prompt a save-dialog */

            // create a save-file-dialog
            SaveFileDialog saveScriptDialog = new SaveFileDialog();

            saveScriptDialog.DefaultExt = "sms";
            saveScriptDialog.RestoreDirectory = true;
            saveScriptDialog.FileName = currentTabTextWithoutAsterisk;

            // save the file succesfully
            if (saveScriptDialog.ShowDialog() == DialogResult.OK) {
                // get the file name with the path and an extension "sms"
                string fileNameWithPath = saveScriptDialog.FileName;
                if (!fileNameWithPath.EndsWith(".sms"))
                    fileNameWithPath += ".sms";

                // the pre-saving file path, possible as null
                string origPathOrNull = scriptModel.SavedFilePath;

                // do serialization for saving the file
                try {
                    // store the file path to the script-model
                    scriptModel.SavedFilePath = fileNameWithPath;
                    scriptModel.HaveUnsavedChanges = false;

                    // get the file name w/o path and extension
                    int lastIdxOfDot = fileNameWithPath.LastIndexOf('.');
                    int lastIdxOfSeperator = fileNameWithPath.LastIndexOf('\\');
                    string fileNameWithoutPathAndExt = fileNameWithPath.Substring(lastIdxOfSeperator + 1, lastIdxOfDot - lastIdxOfSeperator - 1);

                    // rename the script
                    ModelManager.renameScript(fileNameWithoutPathAndExt, true);
                    scriptsTabControl.SelectedTab.Text = fileNameWithoutPathAndExt;

                    // serialize
                    SerializationManager.serialize(scriptModel, fileNameWithPath);
                } catch (Exception) {
                    new AlertForm("Error happened", "Unfortunately, errors happened when saving the file. Operation failed.").ShowDialog();
                    scriptModel.SavedFilePath = origPathOrNull;
                    return false;
                }

                return true;
            }

            // no saving
            return false;
        }

        // show dialog and open the script
        private bool showDialogAndOpenScriptFromDisk() {
            if (SimulationManager.checkSimulating())
                return false;

            // create a open-file-dialog
            OpenFileDialog openScriptDialog = new OpenFileDialog();

            openScriptDialog.Filter = "sms files (*.sms)|*.sms";
            openScriptDialog.RestoreDirectory = true;

            // show the dialog and result a designated-by-user file
            if (openScriptDialog.ShowDialog() == DialogResult.OK) {
                try {
                    // get the script-model by de-serializing
                    ScriptModel scriptModel = SerializationManager.Deserialize<ScriptModel>(openScriptDialog.FileName);

                    // open the designated script-model,
                    // if the script is already in the opened script list
                    if (ModelManager.openScript(scriptModel) == false) {
                        new AlertForm("Alert", "The designated script is already in the opened script list.").ShowDialog();
                        return false;
                    }

                    // add new tab-page for this script
                    addNewTabPage(scriptModel, false);
                } catch (Exception) {
                    new AlertForm("Error happened", "Unfortunately, errors happened when loading the file. Operation failed.").ShowDialog();
                    return false;
                }

                return true;
            }

            // no opening
            return false;
        }

        // mark the current working-on script as unsaved
        public void MarkUnsavedScript() {
            // add the prefix '*' to the text of the current selected tab
            if (scriptsTabControl.SelectedTab.Text.EndsWith("*") == false)
                scriptsTabControl.SelectedTab.Text += "*";
        }

        public void AddLinkViewAtCurrentScript(LinkView newLinkView) {
            ScriptTabPage tabPage = (ScriptTabPage) scriptsTabControl.SelectedTab;
            ScriptCanvas scriptCanvas = tabPage.TheScriptCanvas;

            scriptCanvas.AddLinkView(newLinkView);
        }

        public void adjustLinkViewAtCurrentScript(LinkModel linkModel, bool isOutgoingLink) {
            ScriptTabPage tabPage = (ScriptTabPage) scriptsTabControl.SelectedTab;
            ScriptCanvas scriptCanvas = tabPage.TheScriptCanvas;

            scriptCanvas.setDataByLinkModel(linkModel, isOutgoingLink);
        }

        public bool deleteStateView(StateModel stateModel) {
            if (SimulationManager.checkSimulating())
                return false;

            ScriptTabPage tabPage = (ScriptTabPage) scriptsTabControl.SelectedTab;
            ScriptCanvas scriptCanvas = tabPage.TheScriptCanvas;

            scriptCanvas.deleteStateView(stateModel);
            return true;
        }

        public void invalidateCanvasAtCurrentScript() {
            (scriptsTabControl.SelectedTab as ScriptTabPage).TheScriptCanvas.Invalidate();
        }

        /* ============================================================== */
        /* user events */

        // add the whole new script by clicking the new button at tool-strip
        private void NewScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            addNewScript();
        }

        // save the script by clicking the save button at tool-strip
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveCertainScript(scriptsTabControl.SelectedIndex);
        }

        // open the script which is saved to the disk by clicking the open button at tool-strip
        private void OpenScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            showDialogAndOpenScriptFromDisk();
        }

        // click on the settings of prompting typing form when creating a new general state
        // to let user type the content texts of the new state
        private void PromptTypingFormWhenCreatingGeneralStateToolStripMenuItem_Click(object sender, EventArgs e) {
            // switch the current state of checked
            bool currentState = SettingsManager.PromptTypingFormWhenCreatingNewGeneralState;

            if (currentState)
                promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.CheckState = CheckState.Unchecked;
            else
                promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.CheckState = CheckState.Checked;

            SettingsManager.PromptTypingFormWhenCreatingNewGeneralState = !currentState;
        }

        // user keyboard actions at a certain script
        private void ScriptsTabControl_KeyDown(object sender, KeyEventArgs e) {
            // Ctrl pressed
            if (e.Modifiers == Keys.Control) {
                // Ctrl + N: new script
                if (e.KeyCode == Keys.N)
                    addNewScript();

                // Ctrl + S: save script
                if (e.KeyCode == Keys.S)
                    saveCertainScript(scriptsTabControl.SelectedIndex);

                // Ctrl + O: open script
                else if (e.KeyCode == Keys.O)
                    showDialogAndOpenScriptFromDisk();
            }
        }

        // switch between scripts
        private void ScriptsTabControl_SelectedIndexChanged(object sender, EventArgs e) {
            // set the index of current selected script
            ModelManager.CurrentSelectedScriptIndex = scriptsTabControl.SelectedIndex;

            // remove the info-panel if existed
            ModelManager.removeInfoPanel();

            // invalidate the start-state-view on the shell if needs
            Program.form.getCertainStateViewOnTheShell(0).Invalidate();
        }

        // mouse-up event on the tab-control
        private void ScriptsTabControl_MouseUp(object sender, MouseEventArgs e) {
            if (SimulationManager.checkSimulating())
                return;

            // check if the right mouse button was pressed
            if (e.Button == MouseButtons.Right) {
                // iterate through all the tab pages
                for (int i = 0; i < scriptsTabControl.TabCount; i++) {
                    // get their rectangle area and check if it contains the mouse cursor
                    Rectangle r = scriptsTabControl.GetTabRect(i);

                    if (r.Contains(e.Location)) {
                        // select the clicked-on tab-page
                        scriptsTabControl.SelectedIndex = i;

                        // set the index
                        ModelManager.CurrentSelectedScriptIndex = scriptsTabControl.SelectedIndex;

                        // show the context-menu
                        ScriptLabelContextMenu cm = new ScriptLabelContextMenu(scriptsTabControl);
                        cm.Show(scriptsTabControl, new Point(e.X, e.Y), LeftRightAlignment.Right);
                    }
                }
            }
        }

        // simulation start: step-by-step
        private void StepByStepToolStripMenuItem_Click(object sender, EventArgs e) {
            SimulationManager.startSimulation(SimulationType.STEP_BY_STEP);
        }

        // simulation start: run through
        private void RunThroughToolStripMenuItem_Click(object sender, EventArgs e) {
            SimulationManager.startSimulation(SimulationType.RUN_THROUGH);
        }
    }
}
