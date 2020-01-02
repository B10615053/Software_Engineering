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

        // get a certain instance state-view
        public StateView getCertainInstanceStateViewById(string id)
            => ((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas.getStateViewById(id);

        // get a certain instance link-view
        public LinkView getCertainInstanceLinkViewById(string id)
            => ((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas.getLinkViewById(id);

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
            if (scriptModel == null)
                return;

            // get state-model list
            List<StateModel> stateModels = scriptModel.getCopiedStateList();
            if (stateModels == null) {
                new AlertForm("Alert", "Something is wrong, I can feel it.").ShowDialog();
                return;
            }

            // create a new tab-page
            ScriptTabPage tabPage = new ScriptTabPage(scriptModel.Name, stateModels);
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
        public bool saveCertainScript(int scriptTabIdx = -1) {
            if (scriptTabIdx == -1)
                scriptTabIdx = scriptsTabControl.SelectedIndex;

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
                    throw;
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
                //try {
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
                //} catch (Exception) {
                //    new AlertForm("Error happened", "Unfortunately, errors happened when loading the file. Operation failed.").ShowDialog();
                //    throw;
                //    return false;
                //}

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

        public bool deleteLinkView(LinkModel linkModel)
        {
            if (SimulationManager.checkSimulating())
                return false;

            ScriptTabPage tabPage = (ScriptTabPage)scriptsTabControl.SelectedTab;
            ScriptCanvas scriptCanvas = tabPage.TheScriptCanvas;

            scriptCanvas.deleteLinkView(linkModel);
            return true;
        }

        public void invalidateCanvasAtCurrentScript(ScriptModel scriptModel = null) {
            ScriptCanvas canvas = (scriptsTabControl.SelectedTab as ScriptTabPage).TheScriptCanvas;

            if (!(scriptModel is null))
                canvas.setDataByScriptModel(scriptModel);

            getCertainStateViewOnTheShell(0).Invalidate();

            canvas.Invalidate();
        }

        // get the rich-text-box which is used to show logs
        public RichTextBox getCmdLogoutBox() => rtxtCmdOutput;

        // do screenshot
        public void doScreenshot(Control control, string msgWhenScreenshottedSuccessfully = "Screenshot saved.") {
            Bitmap screenshot = new Bitmap(control.Width, control.Height);
            Rectangle formRect = new Rectangle(0, 0, control.Width, control.Height);

            control.DrawToBitmap(screenshot, formRect);

            SaveFileDialog screenshotSaveDialog = new SaveFileDialog();
            screenshotSaveDialog.DefaultExt = "jpg";
            screenshotSaveDialog.RestoreDirectory = true;
            screenshotSaveDialog.FileName = "";

            if (screenshotSaveDialog.ShowDialog() == DialogResult.OK) {
                screenshot.Save(
                    screenshotSaveDialog.FileName,
                    System.Drawing.Imaging.ImageFormat.Jpeg
                );
                new AlertForm("Alert", msgWhenScreenshottedSuccessfully).ShowDialog();
            }
        }
        
        // set operations which need at least a script exists
        public void setOperationsWhichNeedScriptExists(bool isEnabled) {
            // save script
            saveToolStripMenuItem.Enabled = isEnabled;
            // export as an image
            saveAsToolStripMenuItem.Enabled = isEnabled;
            // simulation start
            stepByStepToolStripMenuItem.Enabled = isEnabled;
            // simulation end
            stopRunningToolStripMenuItem.Enabled = isEnabled;
            // screenshot at the current working script (w/ & w/o script name)
            currentWorkingScriptToolStripMenuItem.Enabled = isEnabled;
        }

        public Point getCanvasLocation() {
            return scriptsTabControl.Location;
        }

        // do validation of the current working-on state machine
        public void startOrStopValidation() {
            if (scriptsTabControl.TabCount <= 0 || ModelManager.CurrentSelectedScriptIndex < 0)
                return;

            // stop the validation
            if (SimulationManager.isSimulating())
                SimulationManager.stopSimulation();

            // start a validation
            else {
                DialogResult dialogResult = new TypingForm("Pre-set branches", "Please enter the input sequence.\r\n\r\nEach input must be seperated by a single \',\'", false).ShowDialog();
                if (dialogResult == DialogResult.OK) {
                    // start the simulation w/ the type of VALIDATION
                    SimulationManager.startSimulation(
                        SimulationType.VALIDATION,
                        TypingForm.userTypedResultText.Split(',').ToList()
                    );
                }
            }
        }

        // clear all existed objects in cbb's
        public void clearExistedObjects() {
            cbbExistedStates.Items.Clear();
            cbbExistedLinks.Items.Clear();

            lblExistedStates.Text = "States (0)";
            lblExistedLinks.Text = "Links (0)";
        }

        // re-load the states and links' data to cbb's
        public void reloadExistedObjects(List<StateModel> states = null, List<LinkModel> links = null) {
            clearExistedObjects();

            if (!(states is null))
                states.ForEach(it => cbbExistedStates.Items.Add(it));
            if (!(links is null))
                links.ForEach(it => cbbExistedLinks.Items.Add(it));

            lblExistedStates.Text = "States (" + cbbExistedStates.Items.Count.ToString() + ")";
            lblExistedLinks.Text = "Links (" + cbbExistedLinks.Items.Count.ToString() + ")";

            cbbExistedStates.SelectedIndex = cbbExistedStates.Items.Count - 1;
            cbbExistedLinks.SelectedIndex = cbbExistedLinks.Items.Count - 1;
        }

        // add the object to certain cbb: state
        public void addExistedObject(StateModel state) {
            if (cbbExistedStates.Items.Contains(state) == false)
                cbbExistedStates.Items.Add(state);
            lblExistedStates.Text = "States (" + cbbExistedStates.Items.Count.ToString() + ")";
            cbbExistedStates.SelectedItem = state;
        }

        // add the object to certain cbb: link
        public void addExistedObject(LinkModel link) {
            if (cbbExistedLinks.Items.Contains(link) == false)
                cbbExistedLinks.Items.Add(link);
            lblExistedLinks.Text = "Links (" + cbbExistedLinks.Items.Count.ToString() + ")";
            cbbExistedLinks.SelectedItem = link;
        }

        // remove the object from certain cbb: state
        public void removeExistedObject(StateModel state) {
            if (cbbExistedStates.Items.Contains(state))
                cbbExistedStates.Items.Remove(state);
            lblExistedStates.Text = "States (" + cbbExistedStates.Items.Count.ToString() + ")";
            cbbExistedStates.SelectedIndex = cbbExistedStates.Items.Count - 1;
        }

        // remove the object to certain cbb: link
        public void removeExistedObject(LinkModel link) {
            if (cbbExistedLinks.Items.Contains(link))
                cbbExistedLinks.Items.Remove(link);
            lblExistedLinks.Text = "Links (" + cbbExistedLinks.Items.Count.ToString() + ")";
            cbbExistedLinks.SelectedIndex = cbbExistedLinks.Items.Count - 1;
        }

        // update the object in cbb: state
        public void updateExistedObject(StateModel state) {
            if (state is null)
                return;

            int idxInCbb = cbbExistedStates.Items.IndexOf(state);
            cbbExistedStates.Items.RemoveAt(idxInCbb);
            cbbExistedStates.Items.Insert(idxInCbb, state);
            cbbExistedStates.SelectedIndex = idxInCbb;
        }

        // update the object in cbb: link
        public void updateExistedObject(LinkModel link) {
            if (link is null)
                return;

            int idxInCbb = cbbExistedLinks.Items.IndexOf(link);
            cbbExistedLinks.Items.RemoveAt(idxInCbb);
            cbbExistedLinks.Items.Insert(idxInCbb, link);
            cbbExistedLinks.SelectedIndex = idxInCbb;
        }

        // select the object in cbb: state
        public void selectExistedObject(StateModel state) {
            if (state is null ||
                    cbbExistedStates is null ||
                    cbbExistedStates.Items is null ||
                    scriptsTabControl is null ||
                    scriptsTabControl.SelectedTab is null)
                return;

            cbbExistedStates.SelectedIndex = cbbExistedStates.Items.IndexOf(state);

            ScriptCanvas canvas = ((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas;
            if (canvas is null)
                return;

            StateView stateView = canvas.getStateViewById(state.Id);
            if (stateView is null)
                return;

            ModelManager.showInfoPanel(stateView);
            //((ScriptTabPage)scriptsTabControl.SelectedTab).TheScriptCanvas.translateToState(stateView);
        }

        // select the object in cbb: link
        public void selectExistedObject(LinkModel link) {
            if (link is null ||
                    cbbExistedLinks is null ||
                    cbbExistedLinks.Items is null ||
                    scriptsTabControl is null ||
                    scriptsTabControl.SelectedTab is null)
                return;

            cbbExistedLinks.SelectedIndex = cbbExistedLinks.Items.IndexOf(link);

            ScriptCanvas canvas = ((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas;
            if (canvas is null)
                return;

            LinkView linkView = canvas.getLinkViewById(link.Id);
            if (linkView is null)
                return;

            ModelManager.showInfoPanel(linkView);
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
            // F11: screenshots
            if (e.KeyCode == Keys.F11) {
                // Ctrl + F11: current working-on script w/ name
                if (e.Modifiers == Keys.Control) {
                    if (scriptsTabControl is null || scriptsTabControl.SelectedTab is null)
                        new AlertForm("No script opened", "There is NO any scripts opened.").ShowDialog();
                    else
                        doScreenshot(scriptsTabControl);
                }

                // Shift + F11: current working-on script w/o name
                else if (e.Modifiers == Keys.Shift) {
                    if (scriptsTabControl is null || scriptsTabControl.SelectedTab is null)
                        new AlertForm("No script opened", "There is NO any scripts opened.").ShowDialog();
                    else
                        doScreenshot(((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas);
                }

                // F11: the whole window
                else
                    doScreenshot(this);
            }

            // other cases
            else {
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

                    // Ctrl + Z: undo
                    else if (e.KeyCode == Keys.Z) {
                        if (SimulationManager.isSimulating() == false)
                            ModelManager.undo(scriptsTabControl.SelectedIndex);
                        else
                            SimulationManager.backToCertainStateByIdx(SimulationManager.getRouteLength() - 2);
                    }

                    // Ctrl + Y: redo
                    else if (e.KeyCode == Keys.Y) {
                        if (SimulationManager.checkSimulating() == false)
                            ModelManager.redo(scriptsTabControl.SelectedIndex);
                    }

                    // Ctrl + W: close the current working-on script
                    else if (e.KeyCode == Keys.W) {
                        if (scriptsTabControl.TabPages.Count > 0 && ModelManager.CurrentSelectedScriptIndex >= 0) {
                            // has unsaved changes in the current working-on script
                            if (ModelManager.getScriptModelByIndex(scriptsTabControl.SelectedIndex).HaveUnsavedChanges) {
                                AlertForm alertForm = new AlertForm("Alert", "The script has been modified and it's unsaved. Do you want to save it?", true, true, true);
                                DialogResult result = alertForm.ShowDialog();

                                bool doesSaveSuccessfully = true;

                                // don't do closing
                                if (result == DialogResult.Cancel)
                                    return;
                                // yes, do saving, then closing
                                else if (result == DialogResult.Yes)
                                    doesSaveSuccessfully = Program.form.saveCertainScript(scriptsTabControl.SelectedIndex);

                                // not actually saving
                                if (!doesSaveSuccessfully)
                                    return;
                            }

                            // close the script
                            ModelManager.closeScript();
                            scriptsTabControl.TabPages.RemoveAt(scriptsTabControl.SelectedIndex);
                        }
                    }

                    // Ctrl + R: rename the current working-on script
                    else if (e.KeyCode == Keys.R) {
                        if (scriptsTabControl.TabPages.Count > 0 && ModelManager.CurrentSelectedScriptIndex >= 0) {
                            DialogResult result = new TypingForm("Rename the script", "Type the new title for your script.", false).ShowDialog();
                            if (result == DialogResult.OK) {
                                ModelManager.renameScript(TypingForm.userTypedResultText, false);
                                scriptsTabControl.SelectedTab.Text = TypingForm.userTypedResultText + "*";
                            }
                        }
                    }

                    // Ctrl + V: paste the copied/cutted view
                    else if (e.KeyCode == Keys.V) {
                        if (HistoryManager.hasBufferedStateModel()) {
                            // paste GENERAL
                            if (HistoryManager.BufferedStateModel.StateType == StateType.GENERAL) {
                                ((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas.AddStateView(new GeneralStateView(
                                    HistoryManager.BufferedStateModel.LocOnScript.X,
                                    HistoryManager.BufferedStateModel.LocOnScript.Y,
                                    HistoryManager.BufferedStateModel.ContentText,
                                    true
                                ), HistoryManager.BufferedStateModel.Id,
                                   HistoryManager.BufferedStateModel.BackgroundArgb,
                                   HistoryManager.BufferedStateModel.TextArgb
                                );
                            }

                            // paste END
                            else if (HistoryManager.BufferedStateModel.StateType == StateType.END) {
                                ((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas.AddStateView(new EndStateView(
                                    HistoryManager.BufferedStateModel.LocOnScript.X,
                                    HistoryManager.BufferedStateModel.LocOnScript.Y,
                                    HistoryManager.BufferedStateModel.ContentText,
                                    true
                                ), HistoryManager.BufferedStateModel.Id);
                            }

                            // paste START only if the previous action is Ctrl + X
                            else if (HistoryManager.BufferedStateModel.StateType == StateType.START) {
                                if (ModelManager.getCurrentWorkingScript().Completeness == ScriptModelCompleteness.EMPTY_OR_ONLY_HAS_GENERAL || ModelManager.getCurrentWorkingScript().Completeness == ScriptModelCompleteness.HAS_END_BUT_NO_START) {
                                    ((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas.AddStateView(new StartStateView(
                                        HistoryManager.BufferedStateModel.LocOnScript.X,
                                        HistoryManager.BufferedStateModel.LocOnScript.Y,
                                        HistoryManager.BufferedStateModel.ContentText,
                                        true
                                    ), HistoryManager.BufferedStateModel.Id);
                                }
                            }
                        }
                    }
                }

                // F5 pressed: start/stop a simulation (not include a validation)
                else if (e.KeyCode == Keys.F5) {
                    if (SimulationManager.isSimulating()) {
                        if (SimulationManager.CurrentSimulationType == SimulationType.STEP_BY_STEP)
                            SimulationManager.stopSimulation();
                    }
                    else
                        SimulationManager.startSimulation(SimulationType.STEP_BY_STEP);
                }

                // F6 pressed: validate the state machine
                else if (e.KeyCode == Keys.F6)
                    startOrStopValidation();

                // Ctrl + Shift + S: export the current script as an image in JPG format
                else if (e.Modifiers == (Keys.Control | Keys.Shift) && e.KeyCode == Keys.S) {
                    if (scriptsTabControl is null || scriptsTabControl.SelectedTab is null)
                        new AlertForm("No script opened", "There is NO any scripts opened.").ShowDialog();
                    else
                        doScreenshot(((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas, "Exported successfully!");
                }
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
            
            // re-load into cbb's if there's any remained scripts
            if (ModelManager.CurrentSelectedScriptIndex >= 0)
                Program.form.reloadExistedObjects(
                    ModelManager.getCurrentWorkingScript().getStateList(),
                    ModelManager.getCurrentWorkingScript().getAllLinksInWholeScript()
                );
            // clear all cbb's if no any opened scripts
            else
                Program.form.clearExistedObjects();
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

        // simulation stop
        private void StopRunningToolStripMenuItem_Click(object sender, EventArgs e) {
            if (SimulationManager.CurrentSimulationType == SimulationType.STEP_BY_STEP)
                SimulationManager.stopSimulation();
        }

        // screenshot feature: the whole window
        private void WholeWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            doScreenshot(this);
        }

        // screenshot feature: the current working-on script w/ script name
        private void WithScriptNameToolStripMenuItem_Click(object sender, EventArgs e) {
            if (scriptsTabControl is null || scriptsTabControl.SelectedTab is null)
                new AlertForm("No script opened", "There is NO any scripts opened.").ShowDialog();
            else
                doScreenshot(scriptsTabControl);
        }

        // screenshot feature: the current working-on script w/o script name
        private void WithoutScriptNameToolStripMenuItem_Click(object sender, EventArgs e) {
            if (scriptsTabControl is null || scriptsTabControl.SelectedTab is null)
                new AlertForm("No script opened", "There is NO any scripts opened.").ShowDialog();
            else
                doScreenshot(((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas);
        }

        // key-down event for form1
        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            
        }

        // export the current working-on script as an image of JPG
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (scriptsTabControl is null || scriptsTabControl.SelectedTab is null)
                new AlertForm("No script opened", "There is NO any scripts opened.").ShowDialog();
            else
                doScreenshot(((ScriptTabPage) scriptsTabControl.SelectedTab).TheScriptCanvas, "Exported successfully!");
        }

        // selected-index-changed at cbb-existed-states
        private void CbbExistedStates_SelectedIndexChanged(object sender, EventArgs e) {
            selectExistedObject(cbbExistedStates.SelectedItem as StateModel);
        }

        private void CbbExistedLinks_SelectedIndexChanged(object sender, EventArgs e) {
            selectExistedObject(cbbExistedLinks.SelectedItem as LinkModel);
        }

        private void scriptsTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        // validate the current working-on state machine
        private void ValidateToolStripMenuItem_Click(object sender, EventArgs e) {
            startOrStopValidation();
        }

        // show user tips
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e) {
            new AlertForm(
                "User tips",
                "Feature\t\t\tOperation\t\t\t\t\t\tShortcut\r\n" +
                "===============================================================================================\r\n" +
                "File system\r\n" +
                "Create\t\t\tToolbar -> File -> New script\t\t\t\tCtrl, N\r\n" +
                "Load\t\t\tToolbar -> File -> Open script\t\t\t\tCtrl, O\r\n" +
                "Save\t\t\tToolbar -> File -> Save script\t\t\t\tCtrl, S\r\n" +
                "Export\t\t\tToolbar -> File -> Export as JPG\t\t\tCtrl, Shift, S\r\n" +
                "\r\n" +
                "Simulation\r\n" +
                "Start\t\t\tToolbar -> Run -> Start\t\t\t\t\tF5\r\n" +
                "Stop\t\t\tToolbar -> Run -> Stop\t\t\t\t\tF5\r\n" +
                "\r\n" +
                "Validation\r\n" +
                "Start\t\t\tToolbar -> Validation\t\t\t\t\tF6\r\n" +
                "\r\n" +
                "Screenshot\r\n" +
                "Whole window\t\tToolbar -> Screenshot -> Whole window\t\t\tF11\r\n" +
                "Script w/ name\t\tToolbar -> Screenshot -> Script -> With name\t\tCtrl, F11\r\n" +
                "Script w/o name\t\tToolbar -> Screenshot -> Script -> Without name\t\tShift, F11\r\n"
            ).Show();
        }
    }
}
