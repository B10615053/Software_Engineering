using SWE_Final_Project.Managers;
using SWE_Final_Project.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views {
    public class LinkView: PictureBox {
        // the corresponding link-model
        protected LinkModel mModel;
        internal LinkModel Model { get => mModel; }

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
            Location = new Point(mModel.StartLocOnScript.X, mModel.StartLocOnScript.Y);

            adjustLinesByChangingEndLocOnScript(new Point(mModel.EndLocOnScript.X, mModel.EndLocOnScript.Y));

            Invalidate();
        }

        // adjust the lines that comprised the link arrow by changing the end location on the script
        public void adjustLinesByChangingEndLocOnScript(Point newEndLocOnScript) {
            // set the end-loc-on-script
            mModel.EndLocOnScript = new Point(newEndLocOnScript.X, newEndLocOnScript.Y);

            // set the new size
            int w = Math.Abs(mModel.StartLocOnScript.X - mModel.EndLocOnScript.X);
            int h = Math.Abs(mModel.StartLocOnScript.Y - mModel.EndLocOnScript.Y);
            Size = new Size(w, h);

            // set the location?

            // add line-models and draw on graphics-path
            generateLinesAndAddToSectionList();
        }

        // according to the src & dst locations, generate the lines and add them to section-list
        public void generateLinesAndAddToSectionList() {
            mModel.adjustLines();
            addToGraphicsPath();
        }

        // after clicked on an another state-view, set the destination of this link
        public void setDestination() {
            if (MouseManager.coveringStateViewAndPort.Key == null
                    || MouseManager.coveringStateViewAndPort.Value == PortType.NONE)
                return;

            // set the destination state-model
            mModel.DstStateModel = ModelManager.getStateModelByIdAtCurrentScript(MouseManager.coveringStateViewAndPort.Key.Id);

            // set the port-type of the destination state-model
            mModel.DstPortType = MouseManager.coveringStateViewAndPort.Value;

            // set the end location on script
            mModel.EndLocOnScript = mModel.DstStateModel.getLocationOfCertainPortOnScript(mModel.DstPortType);
        }

        // draw on the designated graphics-path
        private void addToGraphicsPath() {
            mLinesGphPath.Reset();
            foreach (LineModel line in mModel.SectionList) {
                mLinesGphPath.AddLine(line.SrcLocOnScript, line.DstLocOnScript);
            }
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            addToGraphicsPath();
        }

        /* ================ */


    }
}
