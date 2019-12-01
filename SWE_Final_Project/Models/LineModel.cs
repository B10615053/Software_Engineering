using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    [Serializable]
    class LineModel {
        // src location on script
        private Point mSrcLocOnScript = new Point();
        public Point SrcLocOnScript { get => mSrcLocOnScript; set => mSrcLocOnScript = value; }

        // dst location on script
        private Point mDstLocOnScript = new Point();
        public Point DstLocOnScript { get => mDstLocOnScript; set => mDstLocOnScript = value; }

        /* ================================ */

        // constructor
        public LineModel(Point srcOnScript, Point dstOnScript) {
            mSrcLocOnScript = new Point(srcOnScript.X, srcOnScript.Y);
            mDstLocOnScript = new Point(dstOnScript.X, dstOnScript.Y);
        }
    }
}
