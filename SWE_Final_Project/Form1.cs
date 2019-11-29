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
        public Form1() {
            InitializeComponent();
            buildUpStatesList();
        }

        // put the 3 types of states on the left side of GUI
        private void buildUpStatesList() {
            int panelW = statesListPanel.Width;
            int panelH = statesListPanel.Height;

            int locX = panelW / 2;

            StateView startStateView = new StartStateView(locX, 50, "", false);
            statesListPanel.Controls.Add(startStateView);

            StateView endStateView = new EndStateView(locX, 140, "", false);
            statesListPanel.Controls.Add(endStateView);

            StateView generalStateView = new GeneralStateView(locX, 240, "", false);
            statesListPanel.Controls.Add(generalStateView);
        }

        // save the script at the current tab
        public bool saveCertainScript(int scriptTabIdx) {
            if (scriptTabIdx < 0)
                return false;

            // get the current tab text w/o asterisk
            string currentTabTextWithoutAsterisk = scriptsTabControl.SelectedTab.Text.ToString();
            currentTabTextWithoutAsterisk = currentTabTextWithoutAsterisk.TrimEnd('*');

            // get the script-model
            ScriptModel scriptModel = ModelManager.getScriptModelByIndex(scriptsTabControl.SelectedIndex);

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

                    Console.WriteLine("new path: \'" + newPath + "\'");

                    // serialize
                    SerializationManager.serialize(scriptModel, newPath);

                    // trim the asterisk at the end
                    scriptsTabControl.SelectedTab.Text = scriptsTabControl.SelectedTab.Text.TrimEnd('*');

                    // store the file path to the script-model
                    scriptModel.SavedFilePath = newPath;

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

                // do serialization for saving the file
                try {
                    // serialize
                    SerializationManager.serialize(scriptModel, fileNameWithPath);

                    // get the file name w/o path and extension
                    int lastIdxOfDot = fileNameWithPath.LastIndexOf('.');
                    int lastIdxOfSeperator = fileNameWithPath.LastIndexOf('\\');
                    string fileNameWithoutPathAndExt = fileNameWithPath.Substring(lastIdxOfSeperator + 1, lastIdxOfDot - lastIdxOfSeperator - 1);

                    // rename the script
                    ModelManager.renameScript(fileNameWithoutPathAndExt, true);
                    scriptsTabControl.SelectedTab.Text = fileNameWithoutPathAndExt;

                    // store the file path to the script-model
                    scriptModel.SavedFilePath = fileNameWithPath;
                } catch (Exception) {
                    new AlertForm("Error happened", "Unfortunately, errors happened when saving the file. Operation failed.").ShowDialog();
                }

                return true;
            }

            // no saving
            return false;
        }

        // mark the current working-on script as unsaved
        public void MarkUnsavedScript() {
            if (scriptsTabControl.SelectedTab.Text.EndsWith("*") == false)
                scriptsTabControl.SelectedTab.Text += "*";
        }

        // ==============================================================
        // user events

        // add the whole new script
        private void NewScriptToolStripMenuItem_Click(object sender, EventArgs e) {
            // add new script to model-manager
            string adjustedScriptName = ModelManager.addNewScript("Untitled");

            // add new tab-page
            ScriptTabPage tabPage = new ScriptTabPage(adjustedScriptName);
            scriptsTabControl.TabPages.Add(tabPage);
            // mark as unsaved
            scriptsTabControl.TabPages[scriptsTabControl.TabPages.Count - 1].Text += "*";

            // set the selected script index
            ModelManager.CurrentSelectedScriptIndex = scriptsTabControl.SelectedIndex;
        }

        // click the save-script button
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) {
            saveCertainScript(scriptsTabControl.SelectedIndex);
        }

        // Ctrl + S at a certain script
        private void ScriptsTabControl_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
                saveCertainScript(scriptsTabControl.SelectedIndex);
        }

        // click on the settings of prompting typing form when creating a new general state
        // to let user type the content of the new state
        private void PromptTypingFormWhenCreatingGeneralStateToolStripMenuItem_Click(object sender, EventArgs e) {
            // switch the current state of checked
            bool currentState = SettingsManager.PromptTypingFormWhenCreatingNewGeneralState;

            if (currentState)
                promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.CheckState = CheckState.Unchecked;
            else
                promptTypingFormWhenCreatingGeneralStateToolStripMenuItem.CheckState = CheckState.Checked;

            SettingsManager.PromptTypingFormWhenCreatingNewGeneralState = !currentState;
        }

        // switch between scripts
        private void ScriptsTabControl_SelectedIndexChanged(object sender, EventArgs e) {
            ModelManager.CurrentSelectedScriptIndex = scriptsTabControl.SelectedIndex;
        }

        // mouse-up event on the tab-control
        private void ScriptsTabControl_MouseUp(object sender, MouseEventArgs e) {
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
    }
}
