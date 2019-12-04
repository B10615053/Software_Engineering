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
using System.Drawing.Drawing2D;

namespace SWE_Final_Project.Views {
    class ScriptCanvas: PictureBox {
        // the script name
        private string mScriptNameAtCanvas = "";

        // the list of existed (dragged out by user) state-views
        private List<StateView> mExistedStateViewList = new List<StateView>();

        /* ==================================================================== */

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

        // re-draw when mouse moving and is dragging existed state-views
        protected override void OnMouseMove(MouseEventArgs e) {
            // when the mouse is dragging an existed state-view
            // if (MouseManager.isDraggingExistedStateView)
            if (MouseManager.CurrentMouseAction == MouseAction.DRAGGING_EXISTED_STATE_VIEW)
                Invalidate();

            // when the mouse is adding a new link (arrow)
            else if (MouseManager.CurrentMouseAction == MouseAction.CREATING_LINK) {
                MouseManager.AddingLinkView.adjustLinesByChangingEndLocOnScript(e.Location);
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
        }

        // mouse entered (not dragging)
        protected override void OnMouseEnter(EventArgs e) {
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
                // add model
                ModelManager.addNewStateOnCertainScript(new StateModel(ref newStateView));

                // add view
                Controls.Add(newStateView);
                mExistedStateViewList.Add(newStateView);

                // show info panel
                ModelManager.showInfoPanel(newStateView);
            }

            // reset both the holding type and dragging to NONE
            MouseManager.CurrentHoldingType = StateType.NONE;

            // paint on the picture-box
            Invalidate();
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

            // cancel the link adding
            if (e.Button == MouseButtons.Right) {
                MouseManager.AddingLinkView = null;
                Invalidate();
            }
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;

            // if (MouseManager.isDraggingExistedStateView == false) {
            if (MouseManager.CurrentMouseAction == MouseAction.LOUNGE) {
                foreach (StateView stateView in mExistedStateViewList) {
                    g.DrawPath(Pens.Black, stateView.OutlineGphPath);
                    g.FillPath(Brushes.Black, stateView.InnerGphPath);
                }
            }

            // currently, the user is creating a new link (arrow)
            if (MouseManager.CurrentMouseAction == MouseAction.CREATING_LINK) {
                g.DrawPath(Pens.DarkGray, MouseManager.AddingLinkView.LinesGphPath);
            }
        }
    }
}
