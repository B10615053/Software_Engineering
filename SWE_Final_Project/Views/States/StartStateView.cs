using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SWE_Final_Project.Managers;

namespace SWE_Final_Project.Views.States {
    class StartStateView: StateView {
        // constructor
        public StartStateView(int x, int y, string stateContent, bool isInstanceOnScript): base(x, y, stateContent, false, isInstanceOnScript) {
            TheTag = "START";
        }

        // on-paint event
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.FillEllipse(new SolidBrush(Color.Black), 0, 0, Size.Width - 1, Size.Height - 1);
        }

        // on-mouse-move event, set the cursor style
        protected override void OnMouseMove(MouseEventArgs e) {
            if (mIsInstanceOnScript)
                Cursor = Cursors.Hand;
        }

        // on-mouse-down event, set the mouse holding type is START
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            if (mIsInstanceOnScript == false)
                MouseManager.CurrentHoldingType = HoldingType.START;
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            if (mIsInstanceOnScript) {
                Panel infoContainer = Program.form.panelInfoContainer;
                StateInfoTableLayoutPanel infoLayout = new StateInfoTableLayoutPanel(this);

                infoContainer.Controls.Clear();
                infoContainer.Controls.Add(infoLayout);
            }
        }
    }
}
