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
        // unique id of every state-model
        private string mId;
        public string Id { get => mId; }

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

            // generate unique id
            mId = Guid.NewGuid().ToString("N");
        }

        // constructor
        public StateModel(ref StateView stateView) {
            setDataByStateView(stateView);

            // generate unique id
            mId = Guid.NewGuid().ToString("N");
            // assign to the state-view
            stateView.Id = mId;
        }

        // set the state-model data by a state-view
        public void setDataByStateView(StateView stateView) {
            if (stateView is StartStateView)
                mStateType = StateType.START;
            else if (stateView is EndStateView)
                mStateType = StateType.END;
            else
                mStateType = StateType.GENERAL;

            mLocOnScript = new Point(
                stateView.Location.X + (stateView.Size.Width / 2) + 1,
                stateView.Location.Y + (stateView.Size.Height / 2) + 1
            );
            mSizeOnScript = new Size(stateView.Size.Width, stateView.Size.Height);
            mContentText = stateView.StateContent;
        }

        /* ========================================= */

        public override bool Equals(object obj) {
            var other = obj as StateModel;
            if (other == null)
                return false;
            return mId == other.mId;
        }

        public override int GetHashCode() {
            return mId.GetHashCode();
        }

        public override string ToString() {
            string ret = "";
            if (mStateType == StateType.START)
                ret += "START";
            else if (mStateType == StateType.END)
                ret += " END ";
            else if (mStateType == StateType.GENERAL)
                ret += "GNRAL";
            ret += "|" + mContentText + "|" + mLocOnScript.ToString();
            return ret;
        }
    }
}
