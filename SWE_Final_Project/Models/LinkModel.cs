using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    [Serializable]
    class LinkModel {
        // unique id of every arrow-model
        private string mId;
        public string Id { get => mId; }

        // the partial sections of this link-model
        private List<LineModel> mSectionList;
        public List<LineModel> SectionList { get => mSectionList; set => mSectionList = value; }

        // the radius of rounded corners
        private int mCornerRadius;
        public int CornerRadius { get => mCornerRadius; set => mCornerRadius = value; }

        // link text
        private string mLinkText;
        public string LinkText { get => mLinkText; set => mLinkText = value; }

        // the arrow starts at a certain state-model
        private StateModel mSource;
        public StateModel SrcStateModel { get => mSource; set => mSource = value; }

        // the arrow ends at a certain state-model
        private StateModel mDestination;
        public StateModel DstStateModel { get => mDestination; set => mDestination = value; }

        // the port-type of the source state-model
        private PortType mSrcPortType;
        public PortType SrcPortType { get => mSrcPortType; set => mSrcPortType = value; }

        // the port-type of the destination state-model
        private PortType mDstPortType;
        public PortType DstPortType { get => mDstPortType; set => mDstPortType = value; }

        // the start point on the script
        private Point mStartLocOnScript = new Point();
        public Point StartLocOnScript { get => mStartLocOnScript; set => mStartLocOnScript = value; }

        // the end point on the script
        private Point mEndLocOnScript = new Point();
        public Point EndLocOnScript { get => mEndLocOnScript; set => mEndLocOnScript = value; }

        /* ========================================= */

        // constructor
        public LinkModel(StateModel src, StateModel dst, PortType srcPortType, PortType dstPortType, int cornerRadius = 5, string id = null) {
            // give a unique id
            if (id == null)
                mId = Guid.NewGuid().ToString("N");
            else
                mId = id;

            // the radius of arrow's corners
            mCornerRadius = cornerRadius;

            // the source state-model
            mSource = src;
            // the destination state-model
            mDestination = dst;

            // the source port-type
            mSrcPortType = srcPortType;
            // the destination port-type
            mDstPortType = dstPortType;

            // the start & end locations on the script
            mStartLocOnScript = src.getLocationOfCertainPortOnScript(srcPortType);
            mEndLocOnScript = src.getLocationOfCertainPortOnScript(srcPortType);

            // there could be many sections within a single link
            mSectionList = new List<LineModel>();
        }

        /* ========================================= */

        public override string ToString() {
            return "[" + SrcStateModel.ContentText + " -> " + DstStateModel.ContentText + "]";
        }
    }
}
