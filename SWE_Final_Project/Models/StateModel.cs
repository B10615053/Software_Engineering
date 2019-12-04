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

    // 4 types of ports
    [Serializable]
    enum PortType {
        UP, RIGHT, DOWN, LEFT, NONE
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

        // location on the script (anchor is center)
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

        // 4 types of ports (up, right, down, left) of ingoing links and outgoing links
        private Dictionary<PortType, PortModel> mPortDict = new Dictionary<PortType, PortModel> {
            { PortType.UP, new PortModel() },
            { PortType.RIGHT, new PortModel() },
            { PortType.DOWN, new PortModel() },
            { PortType.LEFT, new PortModel() }
        };

        /* ========================================= */

        // constructor
        public StateModel(StateType newStateType, Point newLoc, Size newSize, string newContent = "", string id = null) {
            mStateType = newStateType;
            mLocOnScript = newLoc;
            mSizeOnScript = newSize;
            mContentText = newContent;

            // generate unique id
            if (id == null)
                mId = Guid.NewGuid().ToString("N");
            // or set to the designated one
            else
                mId = id;
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

        // get the certain port's location on script
        public Point getLocationOfCertainPortOnScript(PortType portType) {
            Point ret;

            // the start & end locations on the script
            if (portType == PortType.UP) {
                ret = new Point(
                    LocOnScript.X,
                    LocOnScript.Y - SizeOnScript.Height / 2
                );
            }
            else if (portType == PortType.RIGHT) {
                ret = new Point(
                    LocOnScript.X + SizeOnScript.Width / 2,
                    LocOnScript.Y
                );
            }
            else if (portType == PortType.DOWN) {
                ret = new Point(
                    LocOnScript.X,
                    LocOnScript.Y + SizeOnScript.Height / 2
                );
            }
            else /* if (srcPortType == PortType.LEFT1) */ {
                ret = new Point(
                    LocOnScript.X - SizeOnScript.Width / 2,
                    LocOnScript.Y
                );
            }

            return ret;
        }

        // add a link-model at certain port of this state-model
        public void addLinkAtCertainPort(LinkModel newLinkModel, PortType atPortType, bool isOutgoing) {
            // is an outgoing link from this state
            if (isOutgoing)
                mPortDict[atPortType].addOutgoingLink(newLinkModel);
            // is an ingoing link to this state
            //else
            //    mPortDict[atPortType].addIngoingLink(newLinkModel);
        }

        // get a certain (up, right, down, left) port-model
        public PortModel getCertainPortModel(PortType portType) {
            return mPortDict[portType];
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

            mPortDict[PortType.UP].getCopiedLinks(true).ForEach(it => {
                ret += "\nfrom UP to " + it.DstStateModel.ContentText + "/" + it.DstPortType;
            });
            mPortDict[PortType.RIGHT].getCopiedLinks(true).ForEach(it => {
                ret += "\nfrom RIGHT to " + it.DstStateModel.ContentText + "/" + it.DstPortType;
            });
            mPortDict[PortType.DOWN].getCopiedLinks(true).ForEach(it => {
                ret += "\nfrom DOWN to " + it.DstStateModel.ContentText + "/" + it.DstPortType;
            });
            mPortDict[PortType.LEFT].getCopiedLinks(true).ForEach(it => {
                ret += "\nfrom LEFT to " + it.DstStateModel.ContentText + "/" + it.DstPortType;
            });

            return ret + "\n";
        }
    }
}
