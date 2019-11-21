using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;
using SWE_Final_Project.Managers;

namespace SWE_Final_Project.Views {
    class ScriptCanvas: System.Windows.Forms.PictureBox {
        public ScriptCanvas(): base() {
            Dock = DockStyle.Fill;

            // to avoid flashing when invalidating
            DoubleBuffered = true;
        }

        protected override void OnMouseEnter(EventArgs e) {
            Cursor = Cursors.Cross;
        }

        protected override void OnMouseUp(MouseEventArgs e) {

        }

        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            // set the mouse status with holding-status and location
            MouseManager.setMouseStatus(MouseManager.MouseHolding.HOLDING_NEW_STATE, e.X, e.Y);

            this.Controls.Add(new StateView(e.X, e.Y, "motherfucker\nyou are an asshole"));

            // paint on the picture-box
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.DrawLine(Pens.Black, 0, 0, 10, 10);

            // if here's a new state coming
            if (MouseManager.CurrentMouseHolding == MouseManager.MouseHolding.HOLDING_NEW_STATE) {


                // after painting, reset the mouse status into LOUNGING
                MouseManager.setMouseStatus(MouseManager.MouseHolding.LOUNGING);
            }
        }
    }
}
