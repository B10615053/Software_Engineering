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

        // current mouse-covering link-view
        internal static LinkView coveringLinkView = null;

        // when adding a new link (not complete)
        private static LinkView mAddingLinkView = null;
        internal static LinkView AddingLinkView {
            get => mAddingLinkView;
            set {
                mAddingLinkView = value;
                if (mAddingLinkView is null)
                    CurrentMouseAction = MouseAction.LOUNGE;
            }
        }
    }
}
