using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SWE_Final_Project.Managers;
using SWE_Final_Project.Models;
using System.Drawing.Drawing2D;
using SWE_Final_Project.Views.SubForms;

namespace SWE_Final_Project.Views.States {
    public abstract class StateView: PictureBox {
        // the same as corresponding state-model
        private string mId = "";
        public string Id { get => mId; set => mId = value; }

        // is the state textable, START & END are not, but general state is textable
        protected readonly bool mIsTextable;

        // is the item let user to drag or the instance on the script
        protected readonly bool mIsInstanceOnScript;

        // is the cursor moving on this state-view currently
        protected bool mIsMouseMovingOn = false;

        // the tag of view, e.g., "START", "END", "GENERAL"
        private string mTag;
        public string TheTag { get => mTag; set => mTag = value; }

        // state content text
        private string mStateContent = "";
        internal string StateContent { get => mStateContent; }

        // the outline of state-view for drawing on the canvas (script)
        protected GraphicsPath mOutlineGphPath = new GraphicsPath();
        internal GraphicsPath OutlineGphPath { get => mOutlineGphPath; }

        // the inner stuff of state-view for drawing on the canvas (script)
        protected GraphicsPath mInnerGphPath = new GraphicsPath();
        internal GraphicsPath InnerGphPath { get => mInnerGphPath; }

        // positions of 4 types of ports
        private Dictionary<PortType, Point> mPortPosDict = new Dictionary<PortType, Point>();

        // the location of the script (not the view!) when the mouse clicking down
        private int mMouseDownCanvasLocX = 0;
        private int mMouseDownCanvasLocY = 0;

        // be used only during the simulation, the current simulating status
        private SimulatingStateStatus mCurrentSimulatingStatus = SimulatingStateStatus.VISITABLE;
        public SimulatingStateStatus CurrentSimulatingStatus {
            get => mCurrentSimulatingStatus;
            set {
                mCurrentSimulatingStatus = value;
                Program.form.invalidateCanvasAtCurrentScript();
            }
        }

        /* ================================================= */

        // for calculating the state size, H and V stand for horizontal and vertical respectively
        private static readonly int ENLARGING_RATIO_H = 7;
        private static readonly int ENLARGING_RATIO_V = ENLARGING_RATIO_H * 4;

        // the size of START & END
        public static readonly Size UNTEXTABLE_STATE_SIZE = new Size(40, 40);

        // the smallest size of GENERAL
        private static readonly Size SMALLEST_GENERAL_STATE_SIZE = new Size(100, 75);

        // the radius of the circle to hint the user he/she can add arrow (link)
        private static readonly int ADDING_ARROW_HINT_RADIUS = 12;
        private static readonly int SQD_HINT_RADIUS = ADDING_ARROW_HINT_RADIUS * ADDING_ARROW_HINT_RADIUS;

        /* ================================================= */

        // constructor
        public StateView(int x, int y, string stateContent, bool isTextable, bool isInstance) {
            // is textable or not
            mIsTextable = isTextable;

            // is instance or not
            mIsInstanceOnScript = isInstance;

            // set the state content
            if (mIsTextable)
                setStateContent(stateContent, false);
            // set the certain state size since it's un-textable
            else
                resizeStateBySize(UNTEXTABLE_STATE_SIZE, false);

            // set the location
            relocateState(x, y, updateModel: false);

            // set the ports positions
            resetPortPositions();

            // set the background color to transparent
            BackColor = Color.Transparent;

            // re-draw
            Invalidate();
        }

        /* ================================================= */

        // set the state content string
        public void setStateContent(string newStateContent, bool updateModel = true) {
            // un-textable, return directly
            if (mIsTextable == false)
                return;

            // set the content and do re-sizing
            resizeStateByContent(newStateContent.Trim(), false);

            // set the location
            relocateState(Location.X, Location.Y, false, updateModel);
        }

        // re-size the state once the content text has changed
        protected void resizeStateByContent(string newStateContent, bool updateModel = true) {
            // set the state content
            mStateContent = newStateContent.Trim();

            // calculate the state size
            int stateWidth = mStateContent.Length * ENLARGING_RATIO_H;
            int stateHeight = mStateContent.Split('\n').Length * ENLARGING_RATIO_V;

            if (stateWidth < SMALLEST_GENERAL_STATE_SIZE.Width)
                stateWidth = SMALLEST_GENERAL_STATE_SIZE.Width;
            if (stateHeight < SMALLEST_GENERAL_STATE_SIZE.Height)
                stateHeight = SMALLEST_GENERAL_STATE_SIZE.Height;

            // set the size
            Size = new Size(stateWidth, stateHeight);

            // set the ports positions
            resetPortPositions();

            // changes at data model
            ModelManager.modifyStateOnCertainScript(this, updateModel);

            // re-draw
            Invalidate();
        }

        // re-size the state once the content text has changed
        protected void resizeStateBySize(Size newSize, bool updateModel = true) {
            // set the size
            Size = newSize;

            // set the ports positions
            resetPortPositions();

            // changes at data model
            ModelManager.modifyStateOnCertainScript(this, updateModel);

            // re-draw
            Invalidate();
        }

        // re-set the positions of 4 types of ports
        public void resetPortPositions() {
            // up port
            if (mPortPosDict.ContainsKey(PortType.UP))
                mPortPosDict[PortType.UP] = new Point(Size.Width / 2, 0);
            else
                mPortPosDict.Add(PortType.UP, new Point(Size.Width / 2, 0));

            // right port
            if (mPortPosDict.ContainsKey(PortType.RIGHT))
                mPortPosDict[PortType.RIGHT] = new Point(Size.Width, Size.Height / 2);
            else
                mPortPosDict.Add(PortType.RIGHT, new Point(Size.Width, Size.Height / 2));

            // down port
            if (mPortPosDict.ContainsKey(PortType.DOWN))
                mPortPosDict[PortType.DOWN] = new Point(Size.Width / 2, Size.Height);
            else
                mPortPosDict.Add(PortType.DOWN, new Point(Size.Width / 2, Size.Height));

            // left port
            if (mPortPosDict.ContainsKey(PortType.LEFT))
                mPortPosDict[PortType.LEFT] = new Point(0, Size.Height / 2);
            else
                mPortPosDict.Add(PortType.LEFT, new Point(0, Size.Height / 2));
        }

        // re-locate the state
        public void relocateState(int x, int y, bool areParamsAtCorner = true, bool updateModel = true) {
            /* if the are-params-at-corner parameter is false,
             * means that the x and y parameters are at the center
             */

            if (areParamsAtCorner)
                Location = new Point((int) ((float) x - (Size.Width / 2.0F)),
                    (int) ((float) y - (Size.Height / 2.0F)));
            else
                Location = new Point(x, y);

            // changes at data model
            ModelManager.modifyStateOnCertainScript(this, updateModel);
        }

        // get the position of a certain port
        public Point getPortPosition(PortType portType) {
            return mPortPosDict[portType];
        }

        // delete myself from the canvas and the model
        public void deleteThisState() {
            // remove the view
            if (Program.form.deleteStateView(ModelManager.getStateModelByIdAtCurrentScript(Id)) == false)
                return;
            List<LinkModel> ret = new List<LinkModel>();
            ret = ModelManager.getAllLinkInState(Id);
            foreach(var s in ret)
            {
                Program.form.deleteLinkView(s);
            }
            // remove the model
            ModelManager.removeStateModelByIDAtCurrentScript(Id);

            // invalidate the start-state-view on the shell if needs
            Program.form.getCertainStateViewOnTheShell(0).Invalidate();
        }

        // draw on the designated graphics-path
        abstract protected void addToGraphicsPath();

        // calculate the squared distance between 2 points
        private static int calcSquaredDistance(Point a, Point b) {
            return ((a.X - b.X) * (a.X - b.X)) + ((a.Y - b.Y) * (a.Y - b.Y));
        }

        /* methods */
        /* ==================================================================== */
        /*  events */

        // click on an instance on scripts, show the info panel of this state-view
        protected override void OnMouseClick(MouseEventArgs e) {
            if (mIsInstanceOnScript)
            {
                //ModelManager.showInfoPanel(this);
                Focus();
            }
        }

        // keyboard key-down events
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            e.Handled = true;

            // delete a certain state
            if (e.KeyData.Equals(Keys.Delete))
            {
                deleteThisState();
            }
        }

        // mouse entered, set is-mouse-moving-on to true, and re-draw
        protected override void OnMouseEnter(EventArgs e) {
            if (SimulationManager.isSimulating())
                return;

            mIsMouseMovingOn = true;

            // set the current holding-type of state-views
            if (mIsInstanceOnScript == false) {
                if (this is StartStateView)
                    MouseManager.CurrentHoldingType = StateType.START;
                else if (this is EndStateView)
                    MouseManager.CurrentHoldingType = StateType.END;
                else if (this is GeneralStateView)
                    MouseManager.CurrentHoldingType = StateType.GENERAL;
                else
                    MouseManager.CurrentHoldingType = StateType.NONE;
            }

            Invalidate();
        }

        // dragging a certain state-view if mouse is down
        protected override void OnMouseMove(MouseEventArgs e) {
            // e.X and e.Y is the mouse position of state-view
            // need to calc the mouse position of the whole canvas (script)
            int canvasX = Location.X + e.X;
            int canvasY = Location.Y + e.Y;

            // if it's an instance on the script (not the state-view on the left-bar)
            if (mIsInstanceOnScript) {
                // ZA HANDO
                Cursor = Cursors.Hand;

                // no use for now
                if (mOutlineGphPath.IsOutlineVisible(canvasX, canvasY, Pens.Black)) {
                    
                }
                // the mouse in the inner stuff
                else if (mOutlineGphPath.IsVisible(canvasX, canvasY)) {
                    // mouse is at the inner stuff of this state-view
                    // -> do moving (relocating the dragged state-view)
                    if (MouseManager.CurrentMouseAction == MouseAction.DRAGGING_EXISTED_STATE_VIEW) {
                        Point newLoc = new Point(
                            Location.X + e.X - MouseManager.posOnStateViewX + Size.Width / 2,
                            Location.Y + e.Y - MouseManager.posOnStateViewY + Size.Height / 2
                        );
                        // failed
                        //newLoc = ModelManager.getAlignedLocWithOtherStates(
                        //    newLoc.X,
                        //    newLoc.Y,
                        //    Size.Width,
                        //    Size.Height
                        //);

                        relocateState(newLoc.X, newLoc.Y, updateModel: false);
                    }

                    // mouse is on the 4 borders of this state-view
                    // -> do linking (adding arrow)
                    else {
                        if (!(this is EndStateView) || MouseManager.CurrentMouseAction == MouseAction.CREATING_LINK)
                            Cursor = Cursors.Cross;

                        // calculate squared distances among 4 ports
                        int upSqdDis = calcSquaredDistance(e.Location, mPortPosDict[PortType.UP]);
                        int rightSqdDis = calcSquaredDistance(e.Location, mPortPosDict[PortType.RIGHT]);
                        int downSqdDis = calcSquaredDistance(e.Location, mPortPosDict[PortType.DOWN]);
                        int leftSqdDis = calcSquaredDistance(e.Location, mPortPosDict[PortType.LEFT]);

                        if (upSqdDis < SQD_HINT_RADIUS)
                            MouseManager.coveringStateViewAndPort = new KeyValuePair<StateView, PortType>(this, PortType.UP);
                        else if (rightSqdDis < SQD_HINT_RADIUS)
                            MouseManager.coveringStateViewAndPort = new KeyValuePair<StateView, PortType>(this, PortType.RIGHT);
                        else if (downSqdDis < SQD_HINT_RADIUS)
                            MouseManager.coveringStateViewAndPort = new KeyValuePair<StateView, PortType>(this, PortType.DOWN);
                        else if (leftSqdDis < SQD_HINT_RADIUS)
                            MouseManager.coveringStateViewAndPort = new KeyValuePair<StateView, PortType>(this, PortType.LEFT);
                        else {
                            MouseManager.coveringStateViewAndPort = new KeyValuePair<StateView, PortType>(this, PortType.NONE);
                            Cursor = Cursors.Hand;
                        }

                        // re-draw
                        Invalidate();
                    }
                }
                
                // if it's creating a link
                if (MouseManager.CurrentMouseAction == MouseAction.CREATING_LINK) {
                    MouseManager.AddingLinkView.adjustLinesByChangingEndLocOnScript(
                        new Point(Location.X + e.X, Location.Y + e.Y)
                    );
                }
            }
        }

        // mouse leaved, set is-mouse-moving-on to false, and re-draw
        protected override void OnMouseLeave(EventArgs e) {
            if (SimulationManager.isSimulating())
                return;

            mIsMouseMovingOn = false;
            Invalidate();
        }

        // drag a new/exsited state-view (out),
        // or create a new link, or cancel the adding (not settled) link
        protected override void OnMouseDown(MouseEventArgs e) {
            // set the clicking down location of the script (not the view!)
            mMouseDownCanvasLocX = Location.X + e.X;
            mMouseDownCanvasLocY = Location.Y + e.Y;

            // left click
            if (e.Button == MouseButtons.Left) {
                // dragging new state-view
                if (mIsInstanceOnScript == false && SimulationManager.isSimulating() == false) {
                    if (this is StartStateView && ModelManager.getScriptModelByIndex() != null && ModelManager.getScriptModelByIndex().hasStartStateOnScript());
                    else {
                        Bitmap pic = new Bitmap(ClientSize.Width, ClientSize.Height);
                        DrawToBitmap(pic, Bounds);

                        if (pic == null)
                            return;

                        DoDragDrop(pic, DragDropEffects.Copy);
                    }
                }

                // dragging existed state-view, or adding/settling link, or simulating
                else {
                    if (MouseManager.coveringStateViewAndPort.Value == PortType.NONE) {
                        // simulating
                        if (SimulationManager.isSimulating()) {
                            if (mIsInstanceOnScript) {
                                StateModel model = ModelManager.getStateModelByIdAtCurrentScript(Id);

                                // this is the state in the route
                                if (SimulationManager.isCertainStateInRoute(model)) {
                                    SimulationManager.backToCerainState(model);
                                }

                                // not in the route, then check if this state is available currently
                                else {
                                    List<LinkModel> ingoingLinks = model.getConnectedLinks(false, true);
                                    ingoingLinks.ForEach(ingoing => {
                                        if (SimulationManager.isCertainLinkAvailableCurrently(ingoing)) {
                                            SimulationManager.stepOnNextState(
                                                ingoing,
                                                ingoing.DstStateModel
                                            );
                                        }
                                    });
                                }
                            }
                        }

                        // dragging existed state-view
                        else {
                            if (MouseManager.CurrentMouseAction != MouseAction.CREATING_LINK) {
                                MouseManager.CurrentMouseAction = MouseAction.DRAGGING_EXISTED_STATE_VIEW;
                                MouseManager.posOnStateViewX = e.X;
                                MouseManager.posOnStateViewY = e.Y;
                            }
                        }
                    }

                    // adding link
                    else {
                        if (SimulationManager.isSimulating())
                            return;

                        // create the link (not settled)
                        if (MouseManager.CurrentMouseAction != MouseAction.CREATING_LINK) {
                            // an END is NOT allowed to have any outgoing links
                            if (!(this is EndStateView)) {
                                StateModel stateModel = ModelManager.getStateModelByIdAtCurrentScript(mId);
                                if (!(stateModel is null)) {
                                    LinkModel newLinkModel = new LinkModel(
                                        stateModel,
                                        null,
                                        MouseManager.coveringStateViewAndPort.Value,
                                        MouseManager.coveringStateViewAndPort.Value
                                    );
                                    LinkView newLinkView = new LinkView(newLinkModel);

                                    MouseManager.CurrentMouseAction = MouseAction.CREATING_LINK;
                                    MouseManager.AddingLinkView = newLinkView;
                                }
                            }
                        }

                        // settle the link down
                        else {
                            // prompt the typing-form to let user type the link texts
                            DialogResult dialogResult = new TypingForm("New link", "Please enter the text of this link.", false).ShowDialog();

                            // confirmed that the user certainly wanna add this link
                            if (dialogResult == DialogResult.OK) {
                                // set the destination state-view
                                MouseManager.AddingLinkView.setDestinationOnlyWhenAdding();

                                // set the link-text
                                MouseManager.AddingLinkView.Model.LinkText = TypingForm.userTypedResultText;

                                // add link into model
                                ModelManager.addLinkBetween2StatesOnCertainScript(
                                    MouseManager.AddingLinkView.Model.SrcStateModel,
                                    MouseManager.AddingLinkView.Model.SrcPortType,
                                    MouseManager.AddingLinkView.Model.DstStateModel,
                                    MouseManager.AddingLinkView.Model.DstPortType,
                                    MouseManager.AddingLinkView.Model
                                );

                                // show the info-panel for this link-view
                                ModelManager.showInfoPanel(MouseManager.AddingLinkView);

                                // remove the adding-link-view (and set the mouse-action back to LOUNGE)
                                Program.form.AddLinkViewAtCurrentScript(MouseManager.AddingLinkView);
                                MouseManager.AddingLinkView = null;
                            }
                        }
                    }
                }
            }

            // right click
            else if (e.Button == MouseButtons.Right) {
                MouseManager.AddingLinkView = null;
                Invalidate();
            }
        }

        // dropped (not dragging)
        protected override void OnMouseUp(MouseEventArgs e) {
            addToGraphicsPath();

            int lx = Location.X;
            int ly = Location.Y;

            if (MouseManager.CurrentMouseAction == MouseAction.DRAGGING_EXISTED_STATE_VIEW) {
                relocateState(
                    Location.X + e.X - MouseManager.posOnStateViewX + Size.Width / 2,
                    Location.Y + e.Y - MouseManager.posOnStateViewY + Size.Height / 2,
                    updateModel: lx + e.X != mMouseDownCanvasLocX || ly + e.Y != mMouseDownCanvasLocY
                );

                MouseManager.CurrentMouseAction = MouseAction.LOUNGE;
            }

            // if the both locations when clicking down and when up are the same point,
            // which means that the mouse didn't move during down and up,
            // show the info-panel
            if (lx + e.X == mMouseDownCanvasLocX && ly + e.Y == mMouseDownCanvasLocY)
                Program.form.selectExistedObject(ModelManager.getStateModelByIdAtCurrentScript(Id));
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            // render the link-adding hints
            if (mIsInstanceOnScript && mIsMouseMovingOn) {
                SolidBrush portHintBrush = new SolidBrush(Color.FromArgb(255, 255, 14, 29));

                // check if this view is an end-state-view or not
                bool isEndState = this is EndStateView;

                if (!isEndState || MouseManager.CurrentMouseAction == MouseAction.CREATING_LINK) {
                    switch (MouseManager.coveringStateViewAndPort.Value) {
                        case PortType.UP:
                            e.Graphics.FillEllipse(
                                portHintBrush,
                                mPortPosDict[PortType.UP].X - ADDING_ARROW_HINT_RADIUS / 2,
                                mPortPosDict[PortType.UP].Y - ADDING_ARROW_HINT_RADIUS / 2,
                                ADDING_ARROW_HINT_RADIUS,
                                ADDING_ARROW_HINT_RADIUS
                            );
                            break;

                        case PortType.RIGHT:
                            e.Graphics.FillEllipse(
                                portHintBrush,
                                mPortPosDict[PortType.RIGHT].X - ADDING_ARROW_HINT_RADIUS / 2,
                                mPortPosDict[PortType.RIGHT].Y - ADDING_ARROW_HINT_RADIUS / 2,
                                ADDING_ARROW_HINT_RADIUS,
                                ADDING_ARROW_HINT_RADIUS
                            );
                            break;

                        case PortType.DOWN:
                            e.Graphics.FillEllipse(
                                portHintBrush,
                                mPortPosDict[PortType.DOWN].X - ADDING_ARROW_HINT_RADIUS / 2,
                                mPortPosDict[PortType.DOWN].Y - ADDING_ARROW_HINT_RADIUS / 2,
                                ADDING_ARROW_HINT_RADIUS,
                                ADDING_ARROW_HINT_RADIUS
                            );
                            break;

                        case PortType.LEFT:
                            e.Graphics.FillEllipse(
                                portHintBrush,
                                mPortPosDict[PortType.LEFT].X - ADDING_ARROW_HINT_RADIUS / 2,
                                mPortPosDict[PortType.LEFT].Y - ADDING_ARROW_HINT_RADIUS / 2,
                                ADDING_ARROW_HINT_RADIUS,
                                ADDING_ARROW_HINT_RADIUS
                            );
                            break;
                    }
                }
            }

            addToGraphicsPath();
        }
    }
}
