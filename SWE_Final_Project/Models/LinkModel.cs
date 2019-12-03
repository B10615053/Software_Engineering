using System;
using System.Collections.Generic;
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

        // the arrow ends at a certain state-model
        private StateModel mDestination;
        public StateModel DstStateModel { get => mDestination; set => mDestination = value; }

        /* ========================================= */

        // constructor
        public LinkModel(StateModel dst, int cornerRadius = 5) {
            // give a unique id
            mId = Guid.NewGuid().ToString("N");

            // the radius of arrow's corners
            mCornerRadius = cornerRadius;

            // the destination state-model
            mDestination = dst;
            
            // there could be many sections within a single link
            mSectionList = new List<LineModel>();
        }
    }
}
