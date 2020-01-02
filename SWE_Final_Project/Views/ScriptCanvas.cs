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
        static private int TRANSLATION_STEP = 10;
        static private int SCALING_STEP = 3;

        // the script name
        private string mScriptNameAtCanvas = "";

        // the list of existed (dragged out by user) state-views
        private List<StateView> mExistedStateViewList = new List<StateView>();

        // the list of existed outgoing link-views
        private List<LinkView> mExistedOutgoingLinks = new List<LinkView>();

        // the list of existed ingoing link-views
        private List<LinkView> mExistedIngoingLinks = new List<LinkView>();

        // check if the mouse-right is down or not currently
        private bool mIsMouseRightDown = false;

        // check if the shift-key is down or not currently
        private bool mIsShiftDown = false;

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

        // add a new state-view
        public void AddStateView(StateView newStateView, string id = null, int bgArgb = 0, int textArgb = 0) {
            // add model
            ModelManager.addNewStateOnCertainScript(new StateModel(ref newStateView, id, bgArgb, textArgb));

            // add view
            Controls.Add(newStateView);
            mExistedStateViewList.Add(newStateView);

            // show info panel
            ModelManager.showInfoPanel(newStateView);

            // invalidate the start-state-view on the shell if needs
            Program.form.getCertainStateViewOnTheShell(0).Invalidate();
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
        
        // not yet implement
        public void translateToState(StateView srcStateView)
        {
            foreach (StateView stateView in mExistedStateViewList)
            {
                
                    int newX = stateView.Location.X - srcStateView.Location.X + Size.Width / 2;
                    int newY = stateView.Location.Y - srcStateView.Location.Y + Size.Height / 2;
                    stateView.relocateState(newX, newY, false);
                stateView.OutlineGphPath.Reset();
                
            }
            foreach (LinkView linkView in mExistedIngoingLinks)
            {

            }
            Invalidate();
        }
        
        // translate the all state and link in this script
        private void translateUpdate(Keys direction) {
            foreach (StateView stateView in mExistedStateViewList) {
                if (direction == Keys.Right)
                    stateView.relocateState(stateView.Location.X - TRANSLATION_STEP, stateView.Location.Y, false, false);
                else if (direction == Keys.Left)
                    stateView.relocateState(stateView.Location.X + TRANSLATION_STEP, stateView.Location.Y, false, false);
                else if (direction == Keys.Up)
                    stateView.relocateState(stateView.Location.X, stateView.Location.Y + TRANSLATION_STEP, false, false);
                else if (direction == Keys.Down)
                    stateView.relocateState(stateView.Location.X, stateView.Location.Y - TRANSLATION_STEP, false, false);
            }

            foreach(LinkView linkView in mExistedIngoingLinks) {
                if (direction == Keys.Right)
                    linkView.translate(-SCALING_STEP, 0);
                else if (direction == Keys.Left)
                    linkView.translate(SCALING_STEP, 0);
                else if (direction == Keys.Up)
                    linkView.translate(0, SCALING_STEP);
                else if (direction == Keys.Down)
                    linkView.translate(0, -SCALING_STEP);
            }

            Refresh();
        }
        
        // scale the all state and link in this script
        private float scaleScript = 1.0f;
        public float currentScale = 1.0f;

        public void scaleUpdate(bool scale, double x, double y)
        {
            if (scale)
                scaleScript = 1.2f;
            else
                scaleScript = 1 / 1.1f;

            currentScale *= scaleScript;

            if (currentScale >= 1.0f)
            {
                foreach (StateView stateView in mExistedStateViewList)
                {
                    double newX = scaleScript * ((double)stateView.Location.X - x) + x;
                    double newY = scaleScript * ((double)stateView.Location.Y - y) + y;
                    stateView.Size = new Size((int)(stateView.Size.Width * scaleScript), (int)(stateView.Size.Height * scaleScript));
                    stateView.relocateState((int)newX, (int)newY, false);
                    stateView.resetPortPositions();
                }
                foreach (LinkView linkView in mExistedIngoingLinks)
                {
                    linkView.scale(x, y, scaleScript);
                    double newX = scaleScript * ((double)linkView.Location.X - x) + x;
                    double newY = scaleScript * ((double)linkView.Location.Y - y) + y;
                    linkView.Location = new Point((int)newX, (int)newY);
                    
                }
                //foreach (LinkView linkView in mExistedOutgoingLinks)
                //{
                //    linkView.scale(x, y, scaleScript);
                //}
            }
            else
                currentScale = 1.0f;
            Refresh();
        }
        
        // get a certain link-view by id
        public LinkView getLinkViewById(string id) => mExistedOutgoingLinks.Find(it => it.Model.Id == id);

        /* methods */
        /* ==================================================================== */
        /*  events */

        // re-draw when keyDown right, left, up, down
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                translateUpdate(e.KeyCode);
            }

            else if (e.Modifiers == Keys.Shift || e.KeyCode == Keys.Shift)
                mIsShiftDown = true;

            //else if (e.KeyCode == Keys.T)
            //    translateToState(mExistedStateViewList[0]);
            Invalidate();
        }

        // key-up
        protected override void OnKeyUp(KeyEventArgs e) {
            mIsShiftDown = false;
        }

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

        // mouse-down, certainly used to check if the mouse-button is mouse-right
        protected override void OnMouseDown(MouseEventArgs e) {
            if (e.Button == MouseButtons.Right)
                mIsMouseRightDown = true;
        }

        // mouse-up, disable the boolean of m-is-mouse-right-down
        protected override void OnMouseUp(MouseEventArgs e) {
            mIsMouseRightDown = false;
        }

        // mouse entered (not dragging)
        protected override void OnMouseEnter(EventArgs e) {

        }
        
        // re-draw when mousewheel
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            // mouse-right is down -> do scaling
            if (mIsMouseRightDown) {
                if (e.Delta > 0)
                    scaleUpdate(true, (int)e.X, (int)e.Y);
                else if (e.Delta < 0)
                    scaleUpdate(false, (int)e.X, (int)e.Y);
            }

            // mouse-right is not down -> do translating
            else {
                // shift-key is down -> go left/right
                if (mIsShiftDown) {
                    if (e.Delta > 0)
                        translateUpdate(Keys.Left);
                    else if (e.Delta < 0)
                        translateUpdate(Keys.Right);
                }

                // shift-key is not down -> go up/down
                else {
                    if (e.Delta > 0)
                        translateUpdate(Keys.Up);
                    else if (e.Delta < 0)
                        translateUpdate(Keys.Down);
                }
            }

            Invalidate();
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
                // resize the state with current scale 
                newStateView.Size = new Size((int)(newStateView.Size.Width * currentScale), 
                    (int)(newStateView.Size.Height * currentScale ));
                newStateView.resetPortPositions();

                // add the state-view
                AddStateView(newStateView);
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
                if (isSimulatingAndTheMouseIsCoveringThisLink)
                {
                    g.DrawPath(new Pen(Color.Red, 2), it.LinesGphPath);
                    dstStateViewId = it.Model.DstStateModel.Id;
                }
                // is simulating
                else if (SimulationManager.isSimulating())
                {
                    Color simulatingLinkColor = SimulationManager.getSimulatingLinkColor(it.CurrentSimulatingStatus);
                    g.DrawPath(new Pen(simulatingLinkColor), it.LinesGphPath);
                }
                // is not simulating
                else
                {
                    for (int i = 0; i < it.LinesGphPath.PathPoints.Length; ++i)
                    {
                        PointF pt = it.LinesGphPath.PathPoints[i];
                        it.LinesGphPath.PathPoints[i] = new PointF(pt.X * (float)scaleScript, pt.Y * (float)scaleScript);
                    }
                    g.DrawPath(Pens.Black, it.LinesGphPath);
                }

                // draw the transparent link text for mouse events
                g.FillPath(Brushes.Transparent, it.TextGphPath);

                // draw the string of the link text
                using (Font font = new Font("Consolas", 12.0F, FontStyle.Regular, GraphicsUnit.Point)) {
                    // get the pair of point at left-up corner & size
                    var pair = it.Model.getLeftUpCornerPositionOnScriptAndGroundSize();

                    // calculate the enlarged textbox-rectangle
                    const float R = 500.0F;
                    int w = (int) (pair.Value.Width * R);
                    int h = (int) (pair.Value.Height * R);
                    int x = (int) ((pair.Key.X + (pair.Value.Width / 2.0F)) - (w / 2.0F));
                    int y = (int) ((pair.Key.Y + (pair.Value.Height / 2.0F)) - (h / 2.0F));

                    // create the rectangle for locating the text
                    Rectangle rect = new Rectangle(
                        x + it.Model.LinkTextOffsetX,
                        y + it.Model.LinkTextOffsetY,
                        w,
                        h
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
