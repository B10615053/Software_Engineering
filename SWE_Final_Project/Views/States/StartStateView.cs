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
    public class StartStateView: StateView {
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

            // check if the current working-on script already has a START state or not
            bool alreadyHasStartState = ModelManager.CurrentSelectedScriptIndex >= 0 &&
                                            ModelManager
                                                .getScriptModelByIndex()
                                                .hasStartStateOnScript();

            // select the color of this start-state-view
            if (!mIsInstanceOnScript && mIsMouseMovingOn && !alreadyHasStartState)
                color = Color.FromArgb(255, 100, 100, 100);
            else
                color = Color.Black;

            // render the start-state-view
            g.FillEllipse(new SolidBrush(color), 0, 0, Size.Width - 1, Size.Height - 1);

            // if already has a START state, render the forbidden sign on the shell
            if (alreadyHasStartState && !mIsInstanceOnScript) {
                Pen pen4ForbiddenSign = new Pen(Color.Red, 5);
                g.DrawLine(pen4ForbiddenSign, 0, 0, Size.Width - 1, Size.Height - 1);
                g.DrawLine(pen4ForbiddenSign, Size.Width - 1, 0, 0, Size.Height - 1);
            }
        }
    }
}
