using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    [Serializable]
    public class LinkModel {
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

        /* ============================== */

        private static readonly int ARROW_SIGN_OFFSET_PARALEL = 12;
        private static readonly int ARROW_SIGN_OFFSET_CHUIZHI = 6;

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

        // adjust the locations of lines that comprised the link
        public void adjustLines() {
            SectionList.Clear();

            // start point XY
            int sptX = StartLocOnScript.X;
            int sptY = StartLocOnScript.Y;
            // end point XY
            int eptX = EndLocOnScript.X;
            int eptY = EndLocOnScript.Y;

            // vertical distance
            int disVert = Math.Abs(sptY - eptY);
            // horizontal distance
            int disHori = Math.Abs(sptX - eptX);

            // start & end are the same point, no need for lines
            if (sptX == eptX && sptY == eptY)
                return;
            // need a single vertical or horizontal line
            else if (sptX == eptX || sptY == eptY)
                SectionList.Add(new LineModel(sptX, sptY, eptX, eptY));
            // need 3 lines
            else {
                // vertical distance > horizontal distance
                if (disVert > disHori) {
                    int midPtY = (sptY + eptY) / 2;
                    SectionList.Add(new LineModel(sptX, sptY, sptX, midPtY));
                    SectionList.Add(new LineModel(sptX, midPtY, eptX, midPtY));
                    SectionList.Add(new LineModel(eptX, midPtY, eptX, eptY));
                }
                // vertical distance < horizontal distance
                else {
                    int midPtX = (sptX + eptX) / 2;
                    SectionList.Add(new LineModel(sptX, sptY, midPtX, sptY));
                    SectionList.Add(new LineModel(midPtX, sptY, midPtX, eptY));
                    SectionList.Add(new LineModel(midPtX, eptY, eptX, eptY));
                }
            }

            // add 2 more short lines as an arrow sign
            LineModel lastLine = SectionList.Last();
            // to up
            if (lastLine.Direction == DirectionType.TO_UP) {
                SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_CHUIZHI, eptY + ARROW_SIGN_OFFSET_PARALEL));
                SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_CHUIZHI, eptY + ARROW_SIGN_OFFSET_PARALEL));
            }
            // to right
            else if (lastLine.Direction == DirectionType.TO_RIGHT) {
                SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_PARALEL, eptY - ARROW_SIGN_OFFSET_CHUIZHI));
                SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_PARALEL, eptY + ARROW_SIGN_OFFSET_CHUIZHI));
            }
            // to down
            else if (lastLine.Direction == DirectionType.TO_DOWN) {
                SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_CHUIZHI, eptY - ARROW_SIGN_OFFSET_PARALEL));
                SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_CHUIZHI, eptY - ARROW_SIGN_OFFSET_PARALEL));
            }
            // to left
            else if (lastLine.Direction == DirectionType.TO_LEFT) {
                SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_PARALEL, eptY - ARROW_SIGN_OFFSET_CHUIZHI));
                SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_PARALEL, eptY + ARROW_SIGN_OFFSET_CHUIZHI));
            }
        }

        /* ========================================= */

        public override bool Equals(object obj) {
            var other = obj as LinkModel;
            if (other == null)
                return false;
            return mId == other.mId;
        }

        public override int GetHashCode() {
            return mId.GetHashCode();
        }

        public override string ToString() {
            return "[" + SrcStateModel.ContentText + " -> " + DstStateModel.ContentText + "]";
        }
    }
}
