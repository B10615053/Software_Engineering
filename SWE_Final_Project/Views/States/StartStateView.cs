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
        public StartStateView(int x, int y, string stateContent, bool isInstanceOnScript)
            : base(x, y, stateContent, false, isInstanceOnScript) {
            TheTag = "START";
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            Color color;

            if (!mIsInstanceOnScript && mIsMouseMovingOn)
                color = Color.FromArgb(127, 0, 0, 0);
            else
                color = Color.Black;

            g.FillEllipse(new SolidBrush(color), 0, 0, Size.Width - 1, Size.Height - 1);
        }
    }
}
