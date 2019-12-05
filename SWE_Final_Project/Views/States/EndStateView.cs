using SWE_Final_Project.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views.States {
    public class EndStateView: StateView {

        // the gap between inner circle and outer circle
        private const int INNER_CIRCLE_GAP_THICKNESS = 2;

        // constructor
        public EndStateView(int x, int y, string stateContent, bool isInstanceOnScript)
            : base(x, y, stateContent, false, isInstanceOnScript) {
            TheTag = "END";
            addToGraphicsPath();
        }

        // draw on the designated graphics-path
        protected override void addToGraphicsPath() {
            mOutlineGphPath.Reset();
            mOutlineGphPath.AddEllipse(
                Location.X,
                Location.Y,
                Size.Width - 1,
                Size.Height - 1
            );

            mInnerGphPath.Reset();
            mInnerGphPath.AddEllipse(
                Location.X + INNER_CIRCLE_GAP_THICKNESS, // x at left-up corner
                Location.Y + INNER_CIRCLE_GAP_THICKNESS, // y at left-up corner
                Size.Width - 1 - (INNER_CIRCLE_GAP_THICKNESS * 2),
                Size.Height - 1 - (INNER_CIRCLE_GAP_THICKNESS * 2)
            );
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Color color;

            if (!mIsInstanceOnScript && mIsMouseMovingOn)
                color = Color.FromArgb(127, 0, 0, 0);
            else
                color = Color.Black;

            g.DrawEllipse(new Pen(color), 0, 0, Size.Width - 1, Size.Height - 1);
            g.FillEllipse(
                new SolidBrush(color),
                INNER_CIRCLE_GAP_THICKNESS, // x at left-up corner
                INNER_CIRCLE_GAP_THICKNESS, // y at left-up corner
                Size.Width - 1 - (INNER_CIRCLE_GAP_THICKNESS * 2),
                Size.Height - 1 - (INNER_CIRCLE_GAP_THICKNESS * 2)
            );
        }
    }
}
