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

        // be used only during the simulation, the current simulating status
        private SimulatingLinkStatus mCurrentSimulatingStatus = SimulatingLinkStatus.VISITABLE;
        public SimulatingLinkStatus CurrentSimulatingStatus {
            get => mCurrentSimulatingStatus;
            set {
                mCurrentSimulatingStatus = value;
                Program.form.invalidateCanvasAtCurrentScript();
            }
        }

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
        public void setDestinationOnlyWhenAdding() {
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

        // set the link-text
        public void setLinkText(string newLinkText) {
            mModel.LinkText = newLinkText;
            Program.form.invalidateCanvasAtCurrentScript();
            ModelManager.modifyLinkOnCertainScript(this, true);
        }

        public void setSrcAndDst(StateModel src, StateModel dst, bool makeHistory) {
            Model.SrcStateModel = src;
            Model.DstStateModel = dst;
            Program.form.invalidateCanvasAtCurrentScript();
            ModelManager.modifyLinkOnCertainScript(this, makeHistory);
        }

        // set the ports of source & destination
        public void setSrcAndDstPorts(PortType srcPortType = PortType.NONE, PortType dstPortType = PortType.NONE, bool makeHistory = true) {
            Model.setSrcAndDstPorts(srcPortType, dstPortType);
            ModelManager.modifyLinkOnCertainScript(this, makeHistory);
        }

        // draw on the designated graphics-path
        private void addToGraphicsPath() {
            mLinesGphPath.Reset();
            foreach (LineModel line in mModel.SectionList) {
                mLinesGphPath.AddLine(line.SrcLocOnScript, line.DstLocOnScript);
            }

            mTextGphPath.Reset();
            using (Font font = new Font("Consolas", 12.0F, FontStyle.Regular, GraphicsUnit.Point)) {
                var pair = mModel.getLeftUpCornerPositionOnScriptAndGroundSize();
                Rectangle rect = new Rectangle(
                    pair.Key.X,
                    pair.Key.Y,
                    pair.Value.Width,
                    pair.Value.Height
                );

                // for aligning the text to center
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                if (mModel.LinkText != null) {
                    mTextGphPath.AddString(mModel.LinkText, new FontFamily("Consolas"), (int) FontStyle.Regular, 23.0F, rect, stringFormat);
                    RectangleF linkTextRect = mTextGphPath.GetBounds();

                    mTextGphPath.Reset();
                    mTextGphPath.AddRectangle(linkTextRect);
                }
            }
        }

        /* ================ */

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            addToGraphicsPath();
        }

        // delete myself from the canvas and the model
        public void deleteThisLink()
        {
            // remove the view
            if (Program.form.deleteLinkView(mModel) == false)
                return;

            // remove the model
            ModelManager.removeLinkModelAtCurrentScript(mModel, true);

            // invalidate the start-state-view on the shell if needs
            // Program.form.getCertainStateViewOnTheShell(0).Invalidate();
        }
    }
}
