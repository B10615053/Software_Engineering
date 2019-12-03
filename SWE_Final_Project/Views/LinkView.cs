using SWE_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views {
    class LinkView: PictureBox {
        // the corresponding link-model
        protected LinkModel mModel;

        // the gp for drawing lines
        protected GraphicsPath mLinesGphPath = new GraphicsPath();
        internal GraphicsPath LinesGphPath { get => mLinesGphPath; }

        // the gp for drawing text
        protected GraphicsPath mTextGphPath = new GraphicsPath();
        internal GraphicsPath TextGphPath { get => mTextGphPath; }

        /* ============================== */

        // constructor
        public LinkView(LinkModel linkModel) {
            mModel = linkModel;
            Invalidate();
        }

        // draw on the designated graphics-path
        private void addToGraphicsPath() {
            foreach (LineModel line in mModel.SectionList) {
                mLinesGphPath.AddLine(line.SrcLocOnScript, line.DstLocOnScript);
            }
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            addToGraphicsPath();
        }
    }
}
