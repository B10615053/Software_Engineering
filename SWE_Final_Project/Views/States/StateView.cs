using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SWE_Final_Project.Views.States {
    abstract class StateView: PictureBox {
        // is the state textable, START & END are not, but general state is textable
        protected readonly bool mIsTextable;

        // is the item let user to drag or the instance on the script
        protected readonly bool mIsInstanceOnScript;

        private string mTag;
        public string TheTag { get => mTag; set => mTag = value; }

        // state content text
        private string mStateContent = "";

        // =======

        // for calculating the state size
        private static readonly int ENLARGING_RATIO = 6;

        // size of START & END
        public static readonly Size UNTEXTABLE_STATE_SIZE = new Size(25, 25);


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

        // set the state content string
        public void setStateContent(string newStateContent) {
            // un-textable, return directly
            if (mIsTextable == false)
                return;

            // set the content and do re-sizing
            resizeStateByContent(newStateContent.Trim());

            // set the location
            relocateState(Location.X, Location.Y);
        }

        // re-size the state once the content text has changed
        protected void resizeStateByContent(string newStateContent) {
            // set the state content
            mStateContent = newStateContent.Trim();

            // calculate the state size
            int stateWidth = mStateContent.Length * ENLARGING_RATIO;
            int stateHeight = mStateContent.Split('\n').Length * 3 * ENLARGING_RATIO;

            // set the size
            Size = new Size(stateWidth, stateHeight);

            Console.WriteLine("W: {0}, H: {1}", stateWidth, stateHeight);

            // re-draw
            Invalidate();
        }

        // re-size the state once the content text has changed
        protected void resizeStateBySize(Size newSize) {
            // set the size
            Size = newSize;

            // re-draw
            Invalidate();
        }

        // re-locate the state
        public void relocateState(int leftUpCornerX, int leftUpCornerY, bool areParamsAtCorner = true) {
            if (areParamsAtCorner)
                Location = new Point((int) ((float) leftUpCornerX - (Size.Width / 2.0F)),
                    (int) ((float) leftUpCornerY - (Size.Height / 2.0F)));
            else
                Location = new Point(leftUpCornerX, leftUpCornerY);
        }

        /* methods */
        /* ==================================================================== */
        /*  events */

        // (mouse) double-click event
        protected override void OnDoubleClick(EventArgs e) {
            if (mIsTextable == false)
                return;

            // TODO: prompt a msg box to let user type the content text
        }

        // mouse-down event
        protected override void OnMouseDown(MouseEventArgs e) {
            if (mIsInstanceOnScript == false) {
                Bitmap pic = new Bitmap(ClientSize.Width, ClientSize.Height);
                DrawToBitmap(pic, Bounds);

                if (pic == null)
                    return;

                DoDragDrop(pic, DragDropEffects.Copy);
            }
        }

        // on-paint event
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.DrawRectangle(Pens.Black, 0, 0, Size.Width - 1, Size.Height - 1);
        }
    }
}
