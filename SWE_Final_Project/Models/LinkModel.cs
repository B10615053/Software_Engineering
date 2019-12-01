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

        // the radius of rounded corners
        private int mCornerRadius;

        // the arrow ends at a certain state-model
        private StateModel mDestination;
        public StateModel DstStateModel { get => mDestination; set => mDestination = value; }

        /* ========================================= */

        // constructor
        public LinkModel(StateModel dst, int cornerRadius = 5) {
            mId = Guid.NewGuid().ToString("N");

            mCornerRadius = cornerRadius;

            mDestination = dst;

            mSectionList = new List<LineModel>();
        }
    }
}
