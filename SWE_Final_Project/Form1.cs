using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SWE_Final_Project.Managers;
using SWE_Final_Project.Views;
using SWE_Final_Project.Views.States;

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
        private void saveCertainScript(int scriptTabIdx) {
            if (scriptTabIdx < 0)
                return;

            SaveFileDialog saveScriptDialog = new SaveFileDialog();

            saveScriptDialog.DefaultExt = "sms";
            saveScriptDialog.RestoreDirectory = true;

            // save the file succesfully
            if (saveScriptDialog.ShowDialog() == DialogResult.OK) {
                // TODO: save the script file
            }
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
    }
}
