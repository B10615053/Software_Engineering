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
    public enum StateType {
        START, END, GENERAL, NONE
    }

    // 4 types of ports
    [Serializable]
    public enum PortType {
        UP, RIGHT, DOWN, LEFT, NONE
    }

    // a model of a state
    [Serializable]
    public class StateModel {
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
        private PortModel mUpPortModel = new PortModel();
        private PortModel mRightPortModel = new PortModel();
        private PortModel mDownPortModel = new PortModel();
        private PortModel mLeftPortModel = new PortModel();

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
            // set the state-type
            if (stateView is StartStateView)
                mStateType = StateType.START;
            else if (stateView is EndStateView)
                mStateType = StateType.END;
            else
                mStateType = StateType.GENERAL;

            // set the location on the script
            mLocOnScript = new Point(
                stateView.Location.X + (stateView.Size.Width / 2) + 1,
                stateView.Location.Y + (stateView.Size.Height / 2) + 1
            );

            // set the size on the script
            mSizeOnScript = new Size(stateView.Size.Width, stateView.Size.Height);

            // set the content text
            mContentText = stateView.StateContent;

            // set proper positions of links on the script
            var portDict = new Dictionary<PortType, PortModel> {
                { PortType.UP, mUpPortModel },
                { PortType.RIGHT, mRightPortModel },
                { PortType.DOWN, mDownPortModel },
                { PortType.LEFT, mLeftPortModel }
            };
            foreach (KeyValuePair<PortType, PortModel> entry in portDict) {
                List<LinkModel> outgoingLinks = entry.Value.getLinks(true);
                List<LinkModel> ingoingLinks = entry.Value.getLinks(false);

                // set positions of outgoing links on the script
                outgoingLinks.ForEach(outgoingLink => {
                    outgoingLink.StartLocOnScript = new Point(
                        stateView.Location.X + stateView.getPortPosition(entry.Key).X,
                        stateView.Location.Y + stateView.getPortPosition(entry.Key).Y
                    );
                    Program.form.adjustLinkViewAtCurrentScript(outgoingLink, true);
                });
                // set positions ingoing links on the script
                ingoingLinks.ForEach(ingoingLink => {
                    ingoingLink.EndLocOnScript = new Point(
                        stateView.Location.X + stateView.getPortPosition(entry.Key).X,
                        stateView.Location.Y + stateView.getPortPosition(entry.Key).Y
                    );
                    Program.form.adjustLinkViewAtCurrentScript(ingoingLink, false);
                });
            }
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
            switch (atPortType) {
                case PortType.UP:
                    mUpPortModel.addLink(newLinkModel, isOutgoing); break;
                case PortType.RIGHT:
                    mRightPortModel.addLink(newLinkModel, isOutgoing); break;
                case PortType.DOWN:
                    mDownPortModel.addLink(newLinkModel, isOutgoing); break;
                case PortType.LEFT:
                    mLeftPortModel.addLink(newLinkModel, isOutgoing); break;
            }
        }

        // get a certain (up, right, down, left) port-model
        public PortModel getCertainPortModel(PortType portType) {
            switch (portType) {
                case PortType.UP:
                    return mUpPortModel;
                case PortType.RIGHT:
                    return mRightPortModel;
                case PortType.DOWN:
                    return mDownPortModel;
                case PortType.LEFT:
                    return mLeftPortModel;
                default:
                    return mUpPortModel;
            }
        }

        // for simulation, get all outgoing links from this state
        public List<LinkModel> getAllOutgoingLinks() {
            List<LinkModel> ret = new List<LinkModel>();

            ret.AddRange(mUpPortModel.getLinks(true));
            ret.AddRange(mRightPortModel.getLinks(true));
            ret.AddRange(mDownPortModel.getLinks(true));
            ret.AddRange(mLeftPortModel.getLinks(true));

            return ret;
        }

        // change the port of a certain link, e.g., from LEFT changes to DOWN
        public void changePortOfCertainLink(LinkModel linkModel, PortType fromPortType, PortType toPortType, bool isOutgoing) {
            if (fromPortType == toPortType)
                return;

            // find the designated port-models
            PortModel fromPortModel;
            switch (fromPortType) {
                case PortType.UP: fromPortModel = mUpPortModel; break;
                case PortType.RIGHT: fromPortModel = mRightPortModel; break;
                case PortType.DOWN: fromPortModel = mDownPortModel; break;
                default: fromPortModel = mLeftPortModel; break;
            }
            PortModel toPortModel;
            switch (toPortType) {
                case PortType.UP: toPortModel = mUpPortModel; break;
                case PortType.RIGHT: toPortModel = mRightPortModel; break;
                case PortType.DOWN: toPortModel = mDownPortModel; break;
                default: toPortModel = mLeftPortModel; break;
            }

            // remove the link
            fromPortModel.getLinks(isOutgoing).Remove(linkModel);

            // re-add the link
            if (isOutgoing)
                toPortModel.addOutgoingLink(linkModel);
            else
                toPortModel.addIngoingLink(linkModel);
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

            mUpPortModel.getLinks(true).ForEach(it => {
                ret += "\nfrom UP to " + it.DstStateModel.ContentText + ":" + it.DstPortType;
            });
            mRightPortModel.getLinks(true).ForEach(it => {
                ret += "\nfrom RIGHT to " + it.DstStateModel.ContentText + ":" + it.DstPortType;
            });
            mDownPortModel.getLinks(true).ForEach(it => {
                ret += "\nfrom DOWN to " + it.DstStateModel.ContentText + ":" + it.DstPortType;
            });
            mLeftPortModel.getLinks(true).ForEach(it => {
                ret += "\nfrom LEFT to " + it.DstStateModel.ContentText + ":" + it.DstPortType;
            });

            return ret + "\n";
        }
    }
}
