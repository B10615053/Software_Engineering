using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SWE_Final_Project.Managers;
using System.Drawing.Drawing2D;

namespace SWE_Final_Project.Views.States {
    class StartStateView: StateView {
        // constructor
        public StartStateView(int x, int y, string stateContent, bool isInstanceOnScript)
            : base(x, y, stateContent, false, isInstanceOnScript) {
            TheTag = "START";
            addToGraphicsPath();
        }

        // draw on the designated graphics-path
        protected override void addToGraphicsPath() {
            // no need for visualization,
            // but still draw the outline since the mouse-outline detection will need it
            mOutlineGphPath.Reset();
            mOutlineGphPath.AddEllipse(
                Location.X,
                Location.Y,
                Size.Width - 1,
                Size.Height - 1
            );

            mInnerGphPath.Reset();
            mInnerGphPath.AddEllipse(
                Location.X,
                Location.Y,
                Size.Width - 1,
                Size.Height - 1
            );
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Color color;

            if (!mIsInstanceOnScript && mIsMouseMovingOn)
                color = Color.FromArgb(255, 100, 100, 100);
            else
                color = Color.Black;

            g.FillEllipse(new SolidBrush(color), 0, 0, Size.Width - 1, Size.Height - 1);
        }
    }
}
