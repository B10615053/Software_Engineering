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
    class LinkView: PictureBox {
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

        private static readonly int ARROW_SIGN_OFFSET_PARALEL = 12;
        private static readonly int ARROW_SIGN_OFFSET_CHUIZHI = 6;

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
            mModel.SectionList.Clear();

            // start point XY
            int sptX = mModel.StartLocOnScript.X;
            int sptY = mModel.StartLocOnScript.Y;
            // end point XY
            int eptX = mModel.EndLocOnScript.X;
            int eptY = mModel.EndLocOnScript.Y;

            // vertical distance
            int disVert = Math.Abs(sptY - eptY);
            // horizontal distance
            int disHori = Math.Abs(sptX - eptX);

            // start & end are the same point, no need for lines
            if (sptX == eptX && sptY == eptY)
                return;
            // need a single vertical or horizontal line
            else if (sptX == eptX || sptY == eptY)
                mModel.SectionList.Add(new LineModel(sptX, sptY, eptX, eptY));
            // need 3 lines
            else {
                // vertical distance > horizontal distance
                if (disVert > disHori) {
                    int midPtY = (sptY + eptY) / 2;
                    mModel.SectionList.Add(new LineModel(sptX, sptY, sptX, midPtY));
                    mModel.SectionList.Add(new LineModel(sptX, midPtY, eptX, midPtY));
                    mModel.SectionList.Add(new LineModel(eptX, midPtY, eptX, eptY));
                }
                // vertical distance < horizontal distance
                else {
                    int midPtX = (sptX + eptX) / 2;
                    mModel.SectionList.Add(new LineModel(sptX, sptY, midPtX, sptY));
                    mModel.SectionList.Add(new LineModel(midPtX, sptY, midPtX, eptY));
                    mModel.SectionList.Add(new LineModel(midPtX, eptY, eptX, eptY));
                }
            }

            // add 2 more short lines as an arrow sign
            LineModel lastLine = mModel.SectionList.Last();
            // to up
            if (lastLine.Direction == DirectionType.TO_UP) {
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_CHUIZHI, eptY + ARROW_SIGN_OFFSET_PARALEL));
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_CHUIZHI, eptY + ARROW_SIGN_OFFSET_PARALEL));
            }
            // to right
            else if (lastLine.Direction == DirectionType.TO_RIGHT) {
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_PARALEL, eptY - ARROW_SIGN_OFFSET_CHUIZHI));
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_PARALEL, eptY + ARROW_SIGN_OFFSET_CHUIZHI));
            }
            // to down
            else if (lastLine.Direction == DirectionType.TO_DOWN) {
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX - ARROW_SIGN_OFFSET_CHUIZHI, eptY - ARROW_SIGN_OFFSET_PARALEL));
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_CHUIZHI, eptY - ARROW_SIGN_OFFSET_PARALEL));
            }
            // to left
            else if (lastLine.Direction == DirectionType.TO_LEFT) {
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_PARALEL, eptY - ARROW_SIGN_OFFSET_CHUIZHI));
                mModel.SectionList.Add(new LineModel(eptX, eptY, eptX + ARROW_SIGN_OFFSET_PARALEL, eptY + ARROW_SIGN_OFFSET_CHUIZHI));
            }

            // mModel.SectionList.Add(new LineModel(mModel.StartLocOnScript, mModel.EndLocOnScript));
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
