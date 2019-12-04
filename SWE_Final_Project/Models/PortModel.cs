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

        // add outgoing link
        public void addOutgoingLink(LinkModel newOutgoingLinkModel) {
            mOutgoingLinks.Add(newOutgoingLinkModel);
        }

        // add ingoing link
        public void addIngoingLink(LinkModel newIngoingLinkModel) {
            mOutgoingLinks.Add(newIngoingLinkModel);
        }

        // get (deep-) copied links
        public List<LinkModel> getCopiedLinks(bool isOutgoing) {
            //return isOutgoing ? mOutgoingLinks : mIngoingLinks;
            return mOutgoingLinks;
            //using (var ms = new MemoryStream()) {
            //    var formatter = new BinaryFormatter();
            //    formatter.Serialize(ms, isOutgoing ? mOutgoingLinks : mIngoingLinks);
            //    ms.Position = 0;
            //    return (List<LinkModel>) formatter.Deserialize(ms);
            //}
        }
    }
}
