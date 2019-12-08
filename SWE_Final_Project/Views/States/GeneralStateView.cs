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
    public class GeneralStateView: StateView {
        // constructor
        public GeneralStateView(int x, int y, string stateContent, bool isInstanceOnScript)
            : base(x, y, stateContent, true, isInstanceOnScript) {
            TheTag = "GENERAL";
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

            // draw the string of state content
            //mInnerGphPath.Reset();
            //using (Font font = new Font("Consolas", 12.0F, FontStyle.Regular, GraphicsUnit.Point)) {
            //    Rectangle rect = new Rectangle(Location.X, Location.Y, Size.Width, Size.Height);

            //    // for aligning the text to center
            //    StringFormat stringFormat = new StringFormat();
            //    stringFormat.Alignment = StringAlignment.Center;
            //    stringFormat.LineAlignment = StringAlignment.Center;

            //    mInnerGphPath.AddString(StateContent, new FontFamily("Consolas"), (int) FontStyle.Regular, 14.0F, rect, stringFormat);
            //}
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

            if (SimulationManager.isSimulating() == false) {
                // draw the kuang-kuang
                g.DrawEllipse(new Pen(color), 0, 0, Size.Width - 1, Size.Height - 1);
            }

            // draw the string of state content
            using (Font font = new Font("Consolas", 12.0F, FontStyle.Regular, GraphicsUnit.Point)) {
                Rectangle rect = new Rectangle(0, 0, Size.Width, Size.Height);

                // for aligning the text to center
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                g.DrawString(StateContent, font, new SolidBrush(color), rect, stringFormat);
            }
        }
    }
}
