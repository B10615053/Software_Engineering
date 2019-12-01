using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    [Serializable]
    class PortModel {
        // outgoing links
        private List<LinkModel> mOutgoingLinks;

        // ingoing links
        private List<LinkModel> mIngoingLinks;

        // constructor
        public PortModel() {
            mOutgoingLinks = new List<LinkModel>();
            mIngoingLinks = new List<LinkModel>();
        }
    }
}
