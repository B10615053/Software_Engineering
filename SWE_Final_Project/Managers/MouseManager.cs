using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE_Final_Project.Models;
using SWE_Final_Project.Views;
using SWE_Final_Project.Views.States;

namespace SWE_Final_Project.Managers {
    enum MouseAction {
        DRAGGING_EXISTED_STATE_VIEW,
        CREATING_LINK,
        LOUNGE
    }

    // for managing the status of the mouse
    class MouseManager {

        private static StateType mCurrentHoldingType = StateType.NONE;
        internal static StateType CurrentHoldingType { get => mCurrentHoldingType; set => mCurrentHoldingType = value; }

        private static MouseAction mCurrentMouseAction = MouseAction.LOUNGE;
        internal static MouseAction CurrentMouseAction { get => mCurrentMouseAction; set => mCurrentMouseAction = value; }

        // internal static bool isDraggingExistedStateView = false;
        internal static int posOnStateViewX = 0;
        internal static int posOnStateViewY = 0;

        // current mouse-covering state-view and which port is covered
        internal static KeyValuePair<StateView, PortType> coveringStateViewAndPort = new KeyValuePair<StateView, PortType>(null, PortType.NONE);

        // TODO: incomplete feature
        internal static StateView selectedStateView = null;

        // when adding a new link (not complete)
        private static LinkView mAddingLinkView = null;
        internal static LinkView AddingLinkView { get => mAddingLinkView; set => mAddingLinkView = value; }
    }

    /*
    class MouseManager {
        // the enum for checking the mouse is holding something or not
        public enum MouseHolding {
            HOLDING_NEW_STATE,
            HOLDING_EXISTED_STATE,
            // LOUNGING means holding nothing
            LOUNGING
        }

        // the current mouse-holding
        static private MouseHolding mCurrentMouseHolding = MouseHolding.LOUNGING;
        static public MouseHolding CurrentMouseHolding {
            get { return mCurrentMouseHolding; }
        }

        // the current x-axis of mouse location
        static private int mMouseX = 0;
        static public int MouseX {
            get { return mMouseX; }
        }

        // the current y-axis of mouse location
        static private int mMouseY = 0;
        static public int MouseY {
            get { return mMouseY; }
        }

        // public method for setting the current mouse status w/ holding something or not and the location
        static public void setMouseStatus(MouseHolding mouseHolding, int x = 0, int y = 0) {
            mCurrentMouseHolding = mouseHolding;
            mMouseX = x;
            mMouseY = y;
        }
    }
    */
}
