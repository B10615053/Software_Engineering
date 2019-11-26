using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWE_Final_Project.Models;

namespace SWE_Final_Project.Managers {
    // for managing the status of the mouse
    class MouseManager {

        static private StateType mCurrentHoldingType = StateType.NONE;
        internal static StateType CurrentHoldingType { get => mCurrentHoldingType; set => mCurrentHoldingType = value; }
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
