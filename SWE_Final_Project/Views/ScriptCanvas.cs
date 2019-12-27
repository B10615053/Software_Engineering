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
    public class ScriptCanvas: PictureBox {
        // the script name
        private string mScriptNameAtCanvas = "";

        // the list of existed (dragged out by user) state-views
        private List<StateView> mExistedStateViewList = new List<StateView>();

        // the list of existed outgoing link-views
        private List<LinkView> mExistedOutgoingLinks = new List<LinkView>();

        // the list of existed ingoing link-views
        private List<LinkView> mExistedIngoingLinks = new List<LinkView>();

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
                    // build state-view
                    StateView stateView;
                    if (it.StateType == StateType.START)
                        stateView = new StartStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);
                    else if (it.StateType == StateType.END)
                        stateView = new EndStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);
                    else
                        stateView = new GeneralStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);

                    stateView.Id = it.Id;

                    // add link-views
                    var allPortTypes = Enum.GetValues(typeof(PortType));
                    foreach (PortType portType in allPortTypes) {
                        // add outgoing links
                        foreach (LinkModel linkModel in it.getCertainPortModel(portType).getLinks(true))
                            mExistedOutgoingLinks.Add(new LinkView(linkModel));
                        // add ingoing links
                        foreach (LinkModel linkModel in it.getCertainPortModel(portType).getLinks(false))
                            mExistedIngoingLinks.Add(new LinkView(linkModel));
                    }

                    // add state-view
                    mExistedStateViewList.Add(stateView);
                    Controls.Add(stateView);
                });

                Invalidate();
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

        // add a new link-view
        public void AddLinkView(LinkView newLinkView) {
            mExistedOutgoingLinks.Add(newLinkView);
            mExistedIngoingLinks.Add(newLinkView);
            Invalidate();
        }

        // re-set the whole script
        public void setDataByScriptModel(ScriptModel scriptModel) {
            if (scriptModel is null)
                return;

            // set the script name
            mScriptNameAtCanvas = scriptModel.Name;

            mExistedStateViewList.Clear();
            Controls.Clear();
            mExistedIngoingLinks.Clear();
            mExistedOutgoingLinks.Clear();

            scriptModel.getCopiedStateList().ForEach(it => {
                // build state-view
                StateView stateView;
                if (it.StateType == StateType.START)
                    stateView = new StartStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);
                else if (it.StateType == StateType.END)
                    stateView = new EndStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);
                else
                    stateView = new GeneralStateView(it.LocOnScript.X, it.LocOnScript.Y, it.ContentText, true);

                stateView.Id = it.Id;

                // add link-views
                var allPortTypes = Enum.GetValues(typeof(PortType));
                foreach (PortType portType in allPortTypes) {
                    // add outgoing links
                    foreach (LinkModel linkModel in it.getCertainPortModel(portType).getLinks(true))
                        mExistedOutgoingLinks.Add(new LinkView(linkModel));
                    // add ingoing links
                    foreach (LinkModel linkModel in it.getCertainPortModel(portType).getLinks(false))
                        mExistedIngoingLinks.Add(new LinkView(linkModel));
                }

                // add state-view
                mExistedStateViewList.Add(stateView);
                Controls.Add(stateView);
            });

            Invalidate();
        }

        // re-set a certain link-view data by a link-model
        public void setDataByLinkModel(LinkModel linkModel, bool isOutgoing) {
            LinkView linkView = null;

            if (isOutgoing)
                linkView = mExistedOutgoingLinks.Find(it => it.Model.Equals(linkModel));
            else
                linkView = mExistedIngoingLinks.Find(it => it.Model.Equals(linkModel));

            if (linkView is null)
                return;

            linkView.setSrcAndDstPorts(linkModel.SrcPortType, linkModel.DstPortType, makeHistory: false);
            linkView.generateLinesAndAddToSectionList();
            Invalidate();
        }

        // delete a state-view
        public void deleteStateView(StateModel stateModel) {
            // search for the designated state-view by common id w/ corresponding state-model
            foreach (StateView stateView in mExistedStateViewList)
                if (stateView.Id == stateModel.Id) {
                    mExistedStateViewList.Remove(stateView);
                    Controls.Remove(stateView);
                    break;
                }

            // re-draw
            Invalidate();
        }

        // delete a link-view
        public void deleteLinkView(LinkModel linkModel)
        {
            // search for the designated link-view by common id w/ corresponding link-model
            foreach (LinkView linkView in mExistedIngoingLinks)
                if (linkView.Model.Id == linkModel.Id)
                {
                    mExistedIngoingLinks.Remove(linkView);
                    Controls.Remove(linkView);
                    break;
                }
            foreach (LinkView linkView in mExistedOutgoingLinks)
                if (linkView.Model.Id == linkModel.Id)
                {
                    mExistedOutgoingLinks.Remove(linkView);
                    break;
                }
            // re-draw
            Invalidate();
        }

        // get a certain state-view by id
        public StateView getStateViewById(string id) => mExistedStateViewList.Find(it => it.Id == id);

        // get a certain link-view by id
        public LinkView getLinkViewById(string id) => mExistedOutgoingLinks.Find(it => it.Model.Id == id);

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

            // lounging
            else if (MouseManager.CurrentMouseAction == MouseAction.LOUNGE) {
                Cursor = Cursors.Default;
                MouseManager.coveringLinkView = null;
                MouseManager.coveringStateViewAndPort = new KeyValuePair<StateView, PortType>(null, PortType.NONE);

                // check if the mouse is currently on a certain link
                foreach (LinkView linkView in mExistedOutgoingLinks) {
                    if (linkView.LinesGphPath.IsVisible(e.Location) ||
                            linkView.TextGphPath.IsVisible(e.Location)) {
                        // ZA HANDO
                        Cursor = Cursors.Hand;
                        // set the current-covering link-view
                        MouseManager.coveringLinkView = linkView;
                        break;
                    }
                }

                if (SimulationManager.isSimulating())
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

                // invalidate the start-state-view on the shell if needs
                Program.form.getCertainStateViewOnTheShell(0).Invalidate();
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

            // left-click
            if (e.Button == MouseButtons.Left) {
                // on a certain link-view
                if (!(MouseManager.coveringLinkView is null)) {
                    // simulating
                    if (SimulationManager.isSimulating()) {
                        StateModel currentStaying = SimulationManager.getCurrentStayingStateModel();

                        bool isActivatableLink =
                            !(currentStaying is null) &&
                            !(currentStaying.getConnectedLinks() is null) &&
                            currentStaying.getConnectedLinks().Exists(l => l.Id == MouseManager.coveringLinkView.Model.Id);

                        if (isActivatableLink)
                            SimulationManager.stepOnNextState(
                                MouseManager.coveringLinkView.Model,
                                MouseManager.coveringLinkView.Model.DstStateModel
                            );
                    }

                    // not simulating, show info-panel of this link
                    else
                        Program.form.selectExistedObject(MouseManager.coveringLinkView.Model);
                }
            }

            // cancel the link addition
            else if (e.Button == MouseButtons.Right) {
                MouseManager.AddingLinkView = null;
                Invalidate();
            }
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;

            // be used for the special rendering when a certain link is covered during a simulation
            StateModel currentStaying = SimulationManager.getCurrentStayingStateModel();
            string dstStateViewId = "";

            // render the already-settled links
            mExistedOutgoingLinks.ForEach(it => {
                bool isSimulatingAndTheMouseIsCoveringThisLink =
                    SimulationManager.isSimulating() &&
                    it == MouseManager.coveringLinkView &&
                    !(currentStaying is null) &&
                    !(currentStaying.getConnectedLinks() is null) &&
                    currentStaying.getConnectedLinks().Exists(l => l.Id == MouseManager.coveringLinkView.Model.Id);

                /* draw the lines of the link */
                // is simulating and the mouse is covering a walkable link
                if (isSimulatingAndTheMouseIsCoveringThisLink) {
                    g.DrawPath(new Pen(Color.Red, 2), it.LinesGphPath);
                    dstStateViewId = it.Model.DstStateModel.Id;
                }
                // is simulating
                else if (SimulationManager.isSimulating()) {
                    Color simulatingLinkColor = SimulationManager.getSimulatingLinkColor(it.CurrentSimulatingStatus);
                    g.DrawPath(new Pen(simulatingLinkColor), it.LinesGphPath);
                }
                // is not simulating
                else
                    g.DrawPath(Pens.Black, it.LinesGphPath);

                // draw the transparent link text for mouse events
                g.FillPath(Brushes.Transparent, it.TextGphPath);

                // draw the string of the link text
                using (Font font = new Font("Consolas", 12.0F, FontStyle.Regular, GraphicsUnit.Point)) {
                    var pair = it.Model.getLeftUpCornerPositionOnScriptAndGroundSize();
                    Rectangle rect = new Rectangle(
                        pair.Key.X,
                        pair.Key.Y,
                        pair.Value.Width,
                        pair.Value.Height
                    );

                    // for aligning the text to center
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    if (isSimulatingAndTheMouseIsCoveringThisLink)
                        g.DrawString(it.Model.LinkText, font, Brushes.Red, rect, stringFormat);
                    else
                        g.DrawString(it.Model.LinkText, font, Brushes.Black, rect, stringFormat);
                }
            });

            // currently, the user is creating a new link
            if (MouseManager.CurrentMouseAction == MouseAction.CREATING_LINK) {
                g.DrawPath(Pens.DarkGray, MouseManager.AddingLinkView.LinesGphPath);
            }

            // render the state-views
            if (MouseManager.CurrentMouseAction == MouseAction.LOUNGE) {
                foreach (StateView stateView in mExistedStateViewList) {
                    // is simulating now
                    if (SimulationManager.isSimulating()) {
                        Color simulatingStateColor = SimulationManager.getSimulatingStateColor(stateView.CurrentSimulatingStatus);

                        string coveringStateViewId = "";
                        if (!(MouseManager.coveringStateViewAndPort.Key is null)) {
                            StateModel coveringStateModel = ModelManager.getStateModelByIdAtCurrentScript(MouseManager.coveringStateViewAndPort.Key.Id);
                            if (!(coveringStateModel is null)) {
                                coveringStateModel.getConnectedLinks(false, true).ForEach(ingoing => {
                                    if (SimulationManager.isCertainLinkAvailableCurrently(ingoing))
                                        coveringStateViewId = coveringStateModel.Id;
                                });
                            }
                        }

                        if (dstStateViewId == stateView.Id || coveringStateViewId == stateView.Id) {
                            g.DrawPath(new Pen(Color.Red, 2), stateView.OutlineGphPath);
                            if (!(stateView is GeneralStateView))
                                g.FillPath(new SolidBrush(Color.Red), stateView.InnerGphPath);
                        }
                        else {
                            g.DrawPath(new Pen(simulatingStateColor), stateView.OutlineGphPath);
                            if (!(stateView is GeneralStateView))
                                g.FillPath(new SolidBrush(simulatingStateColor), stateView.InnerGphPath);
                        }
                    }

                    // is NOT simulating now
                    else {
                        g.DrawPath(Pens.Black, stateView.OutlineGphPath);

                        // if it's a GENERAL, draw the transparent inner staff
                        if (stateView is GeneralStateView)
                            g.FillPath(Brushes.Transparent, stateView.InnerGphPath);
                        // else, normally drawing
                        else
                            g.FillPath(Brushes.Black, stateView.InnerGphPath);
                    }
                }
            }

            // end of on-paint
        }
    }
}
