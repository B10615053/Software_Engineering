using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;
using SWE_Final_Project.Managers;
using SWE_Final_Project.Views.States;
using SWE_Final_Project.Views.SubForms;

namespace SWE_Final_Project.Views {
    class ScriptCanvas: PictureBox {
        // constructor
        public ScriptCanvas(): base() {
            // fill in the father container
            Dock = DockStyle.Fill;

            // allow drop operation
            AllowDrop = true;

            // to avoid flashing when invalidating
            DoubleBuffered = true;
        }

        private string promptTypingFormAndGetTypedText() {
            // prompt new typing-form
            TypingForm stateContentForm = new TypingForm("New State", "Please enter the content of the new state", true);
            // show and get the dialog-result
            DialogResult dialogResult = stateContentForm.ShowDialog();

            if (dialogResult == DialogResult.Cancel)
                return null;
            else
                return TypingForm.userTypedResultText;
        }

        /* methods */
        /* ==================================================================== */
        /*  events */

        // mouse entered (not dragging), change the cursor style into a cross
        protected override void OnMouseEnter(EventArgs e) {
            Cursor = Cursors.Cross;
        }

        // drag-entered
        protected override void OnDragEnter(DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Copy;
        }

        // drag-dropped, a new state-view added on current script
        protected override void OnDragDrop(DragEventArgs e) {
            //var bmp = (Bitmap) e.Data.GetData(DataFormats.Bitmap);

            // get the client point
            Point clientPoint = PointToClient(new Point(e.X, e.Y));

            // add the state at the location of client point
            switch (MouseManager.CurrentHoldingType) {
                case HoldingType.START:
                    Controls.Add(new StartStateView(clientPoint.X, clientPoint.Y, "", true));
                    break;

                case HoldingType.END:
                    Controls.Add(new EndStateView(clientPoint.X, clientPoint.Y, "", true));
                    break;

                case HoldingType.GENERAL:
                    // prompt the typing form to get the state content from user
                    string newStateContent = promptTypingFormAndGetTypedText();
                    // if the result is null, means that user cancels the addition of state
                    if (!(newStateContent is null))
                        Controls.Add(new GeneralStateView(clientPoint.X, clientPoint.Y, newStateContent, true));
                    break;
            }

            // reset the holding type to NONE
            MouseManager.CurrentHoldingType = HoldingType.NONE;

            // paint on the picture-box
            //Invalidate();
        }

        // deprecated
        protected override void OnMouseDoubleClick(MouseEventArgs e) {
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            //g.DrawLine(Pens.Black, 0, 0, 10, 10);

        }
    }
}
