using SWE_Final_Project.Views.States;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    // 3 kinds of state
    [Serializable]
    enum StateType {
        START, END, GENERAL, NONE
    }

    // a model of a state
    [Serializable]
    class StateModel {
        // type of state
        private StateType mStateType = StateType.NONE;
        public StateType StateType { get => mStateType; }

        // location on the script (anchor is left-up corner)
        private Point mLocOnScript = new Point();
        public Point LocOnScript { get => mLocOnScript; set => mLocOnScript = value; }

        // size on the script
        private Size mSizeOnScript = new Size();
        public Size SizeOnScript { get => mSizeOnScript; set => mSizeOnScript = value; }

        // content of the state
        private string mContentText = "";
        public string ContentText {
            get => mContentText;
            set {
                if (mStateType == StateType.GENERAL)
                    mContentText = value;
            }
        }

        /* ========================================= */

        // constructor
        public StateModel(StateType newStateType, Point newLoc, Size newSize, string newContent = "") {
            mStateType = newStateType;
            mLocOnScript = newLoc;
            mSizeOnScript = newSize;
            mContentText = newContent;
        }

        // constructor
        public StateModel(StateView stateView) {
            if (stateView is StartStateView)
                mStateType = StateType.START;
            else if (stateView is EndStateView)
                mStateType = StateType.END;
            else
                mStateType = StateType.GENERAL;

            mLocOnScript = new Point(stateView.Location.X, stateView.Location.Y);
            mSizeOnScript = new Size(stateView.Size.Width, stateView.Size.Height);
            mContentText = stateView.StateContent;
        }

        /* ========================================= */

        public override bool Equals(object obj) {
            var other = obj as StateModel;
            if (other == null)
                return false;

            return mStateType == other.mStateType
                && mLocOnScript.X == other.mLocOnScript.X && mLocOnScript.Y == other.mLocOnScript.Y
                && mSizeOnScript.Width == other.mSizeOnScript.Width && mSizeOnScript.Height == other.mSizeOnScript.Height
                && mContentText == other.mContentText;
        }

        public override int GetHashCode() {
            return mStateType.GetHashCode()
                ^ mLocOnScript.GetHashCode()
                ^ mSizeOnScript.GetHashCode()
                ^ mContentText.GetHashCode();
        }
    }
}
