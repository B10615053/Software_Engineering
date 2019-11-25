using SWE_Final_Project.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views.States {
    class EndStateView: StateView {
        // constructor
        public EndStateView(int x, int y, string stateContent, bool isInstanceOnScript)
            : base(x, y, stateContent, false, isInstanceOnScript) {
            TheTag = "END";
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            Color color;

            if (!mIsInstanceOnScript && mIsMouseMovingOn)
                color = Color.FromArgb(127, 0, 0, 0);
            else
                color = Color.Black;

            const int INNER_CIRCLE_GAP_THICKNESS = 2;

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
