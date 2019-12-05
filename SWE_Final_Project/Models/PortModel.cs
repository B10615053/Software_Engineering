using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    [Serializable]
    public class PortModel {
        // outgoing links
        private List<LinkModel> mOutgoingLinks;

        // ingoing links
        private List<LinkModel> mIngoingLinks;

        // constructor
        public PortModel() {
            mOutgoingLinks = new List<LinkModel>();
            mIngoingLinks = new List<LinkModel>();
        }

        // add outgoing link
        public void addOutgoingLink(LinkModel newOutgoingLinkModel) {
            mOutgoingLinks.Add(newOutgoingLinkModel);
        }

        // add ingoing link
        public void addIngoingLink(LinkModel newIngoingLinkModel) {
            mIngoingLinks.Add(newIngoingLinkModel);
        }

        // add outgoing or ingoing link
        public void addLink(LinkModel newLinkModel, bool isOutgoing) {
            if (isOutgoing)
                addOutgoingLink(newLinkModel);
            else
                addIngoingLink(newLinkModel);
        }

        // get links
        public List<LinkModel> getLinks(bool isOutgoing) {
            return isOutgoing ? mOutgoingLinks : mIngoingLinks;
        }
    }
}
