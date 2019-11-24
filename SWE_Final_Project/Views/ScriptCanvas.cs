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

namespace SWE_Final_Project.Views {
    class ScriptCanvas: PictureBox {
        public ScriptCanvas(): base() {
            // fill in the father container
            Dock = DockStyle.Fill;

            // allow drop operation
            AllowDrop = true;

            // to avoid flashing when invalidating
            DoubleBuffered = true;
        }

        protected override void OnMouseEnter(EventArgs e) {
            Cursor = Cursors.Cross;
        }

        protected override void OnDragEnter(DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
                e.Effect = DragDropEffects.Copy;
        }

        protected override void OnDragDrop(DragEventArgs e) {
            //var bmp = (Bitmap) e.Data.GetData(DataFormats.Bitmap);

            // get the client point
            Point clientPoint = PointToClient(new Point(e.X, e.Y));

            // add the state at the location of client point
            if (MouseManager.CurrentHoldingType == HoldingType.START)
                Controls.Add(new StartStateView(clientPoint.X, clientPoint.Y, "", true));

            // reset the holding type to NONE
            MouseManager.CurrentHoldingType = HoldingType.NONE;

            // paint on the picture-box
            Invalidate();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            Controls.Add(new StartStateView(e.X, e.Y, "", true));

            // paint on the picture-box
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            //g.DrawLine(Pens.Black, 0, 0, 10, 10);

        }
    }
}
