using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;
using SWE_Final_Project.Managers;
using SWE_Final_Project.Views.States;
using SWE_Final_Project.Views.SubForms;
using SWE_Final_Project.Models;

namespace SWE_Final_Project.Views {
    class ScriptCanvas: PictureBox {
        private string mScriptNameAtCanvas = "";

        // constructor
        public ScriptCanvas(string scriptName, List<StateModel> stateModelList = null) : base() {
            // fill in the father container
            Dock = DockStyle.Fill;

            // allow drop operation
            AllowDrop = true;

            // to avoid flashing when invalidating
            DoubleBuffered = true;

            // set this script name
            mScriptNameAtCanvas = scriptName;

            // render the state-models if existed
            if (!(stateModelList is null)) {
                stateModelList.ForEach(it => {
                    StateView stateView;
                    if (it.StateType == StateType.START)
                        stateView = new StartStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);
                    else if (it.StateType == StateType.END)
                        stateView = new EndStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);
                    else
                        stateView = new GeneralStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);

                    Controls.Add(stateView);
                });
            }
        }

        // prompt the typing form to get some texts from user
        private string promptTypingFormAndGetTypedText() {
            // don't prompt the typing form up, return empty string (not null) directly
            if (SettingsManager.PromptTypingFormWhenCreatingNewGeneralState == false)
                return "";

            // prompt new typing-form
            TypingForm stateContentForm = new TypingForm("New State", "Please enter the content of the new state", true);
            // show and get the dialog-result
            DialogResult dialogResult = stateContentForm.ShowDialog();

            if (dialogResult == DialogResult.Cancel)
                return null;
            else
                return TypingForm.userTypedResultText;
        }

        /* methods */
        /* ==================================================================== */
        /*  events */

        // mouse entered (not dragging), change the cursor style into a cross
        protected override void OnMouseEnter(EventArgs e) {
            Cursor = Cursors.Cross;
        }

        // drag-entered
        protected override void OnDragEnter(DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Copy;
        }

        // drag-dropped, a new state-view added on current script
        protected override void OnDragDrop(DragEventArgs e) {
            //var bmp = (Bitmap) e.Data.GetData(DataFormats.Bitmap);

            // get the client point
            Point clientPoint = PointToClient(new Point(e.X, e.Y));

            // add the state at the location of client point
            StateView newStateView = null;
            switch (MouseManager.CurrentHoldingType) {
                case StateType.START:
                    newStateView = new StartStateView(clientPoint.X, clientPoint.Y, "", true);
                    break;

                case StateType.END:
                    newStateView = new EndStateView(clientPoint.X, clientPoint.Y, "", true);
                    break;

                case StateType.GENERAL:
                    // prompt the typing form to get the state content from user
                    string newStateContent = promptTypingFormAndGetTypedText();

                    // if the result is null, means that user cancels the addition of state
                    if (!(newStateContent is null))
                        newStateView = new GeneralStateView(clientPoint.X, clientPoint.Y, newStateContent, true);

                    break;
            }
            if (!(newStateView is null)) {
                // add view
                Controls.Add(newStateView);
                // add model
                ModelManager.addNewStateOnCertainScript(new StateModel(ref newStateView));

                // show info panel
                ModelManager.showInfoPanel(newStateView);
            }

            // reset both the holding type and dragging to NONE
            MouseManager.CurrentHoldingType = StateType.NONE;

            // paint on the picture-box
            //Invalidate();
        }

        // deprecated
        protected override void OnMouseDoubleClick(MouseEventArgs e) {
        }

        // click, then focus on the canvas and remove the info-panel if existed
        protected override void OnMouseClick(MouseEventArgs e) {
            // focus on the current working-on canvas
            Select();

            // remove the info-panel
            ModelManager.removeInfoPanel();
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            //g.DrawLine(Pens.Black, 0, 0, 10, 10);

        }
    }
}
