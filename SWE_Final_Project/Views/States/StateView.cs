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

namespace SWE_Final_Project.Views.States {
    abstract class StateView: PictureBox {
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

        /* ================================================= */

        // for calculating the state size, H and V stand for horizontal and vertical respectively
        private static readonly int ENLARGING_RATIO_H = 7;
        private static readonly int ENLARGING_RATIO_V = ENLARGING_RATIO_H * 4;

        // size of START & END
        public static readonly Size UNTEXTABLE_STATE_SIZE = new Size(25, 25);

        private static readonly Size SMALLEST_GENERAL_STATE_SIZE = new Size(75, 50);

        /* ================================================= */

        // constructor
        public StateView(int x, int y, string stateContent, bool isTextable, bool isInstance) {
            // is textable or not
            mIsTextable = isTextable;

            // is instance or not
            mIsInstanceOnScript = isInstance;

            // set the state content
            if (mIsTextable)
                setStateContent(stateContent);
            // set the certain state size since it's un-textable
            else
                resizeStateBySize(UNTEXTABLE_STATE_SIZE);

            // set the location
            relocateState(x, y);

            // set the background color to transparent
            BackColor = Color.Transparent;

            // re-draw
            Invalidate();
        }

        /* ================================================= */

        // set the state content string
        public void setStateContent(string newStateContent) {
            // un-textable, return directly
            if (mIsTextable == false)
                return;

            // set the content and do re-sizing
            resizeStateByContent(newStateContent.Trim());

            // set the location
            relocateState(Location.X, Location.Y, false);
        }

        // re-size the state once the content text has changed
        protected void resizeStateByContent(string newStateContent) {
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

            // Console.WriteLine("W: {0}, H: {1}", stateWidth, stateHeight);

            // changes at data model
            ModelManager.modifyStateOnCertainScript(this);

            // re-draw
            Invalidate();
        }

        // re-size the state once the content text has changed
        protected void resizeStateBySize(Size newSize) {
            // set the size
            Size = newSize;

            // changes at data model
            ModelManager.modifyStateOnCertainScript(this);

            // re-draw
            Invalidate();
        }

        // re-locate the state
        public void relocateState(int x, int y, bool areParamsAtCorner = true) {
            /* if the are-params-at-corner parameter is false,
             * means that the x and y parameters are at the center
             */

            if (areParamsAtCorner)
                Location = new Point((int) ((float) x - (Size.Width / 2.0F)),
                    (int) ((float) y - (Size.Height / 2.0F)));
            else
                Location = new Point(x, y);

            // changes at data model
            ModelManager.modifyStateOnCertainScript(this);
        }

        // draw on the designated graphics-path
        abstract protected void addToGraphicsPath();

        /* methods */
        /* ==================================================================== */
        /*  events */

        // click on an instance on scripts, show the info panel of this state-view
        protected override void OnMouseClick(MouseEventArgs e) {
            if (mIsInstanceOnScript) {
                ModelManager.showInfoPanel(this);
                MouseManager.selectedStateView = this;
            }
        }

        // mouse entered, set is-mouse-moving-on to true, and re-draw
        protected override void OnMouseEnter(EventArgs e) {
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

                // mouse is on the outline of this state-view
                // -> do linking (adding arrow)
                if (mOutlineGphPath.IsOutlineVisible(canvasX, canvasY, Pens.Black)) {
                    // TODO: link between states
                }

                // mouse is at the inner stuff of this state-view
                // -> do moving
                else if (mOutlineGphPath.IsVisible(canvasX, canvasY)) {
                    // relocate the dragged state-view
                    if (MouseManager.isDraggingExistedStateView) {
                        relocateState(
                            Location.X + e.X - MouseManager.posOnStateViewX + Size.Width / 2,
                            Location.Y + e.Y - MouseManager.posOnStateViewY + Size.Height / 2
                        );
                    }
                }

            }
        }

        // mouse leaved, set is-mouse-moving-on to false, and re-draw
        protected override void OnMouseLeave(EventArgs e) {
            mIsMouseMovingOn = false;
            Invalidate();
        }

        // drag a new state-view out
        protected override void OnMouseDown(MouseEventArgs e) {
            // dragging new state-view
            if (mIsInstanceOnScript == false) {
                Bitmap pic = new Bitmap(ClientSize.Width, ClientSize.Height);
                DrawToBitmap(pic, Bounds);

                if (pic == null)
                    return;

                DoDragDrop(pic, DragDropEffects.Copy);
            }
            // dragging existed state-view
            else {
                MouseManager.isDraggingExistedStateView = true;
                MouseManager.posOnStateViewX = e.X;
                MouseManager.posOnStateViewY = e.Y;
            }
        }

        // dropped (not dragging)
        protected override void OnMouseUp(MouseEventArgs e) {
            addToGraphicsPath();
            MouseManager.isDraggingExistedStateView = false;
        }

        protected override void OnPaint(PaintEventArgs e) {
            addToGraphicsPath();
        }
    }
}
