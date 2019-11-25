using SWE_Final_Project.Managers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWE_Final_Project.Views.States {
    class GeneralStateView: StateView {
        // constructor
        public GeneralStateView(int x, int y, string stateContent, bool isInstanceOnScript)
            : base(x, y, stateContent, true, isInstanceOnScript) {
            TheTag = "GENERAL";
        }

        // re-draw
        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            Color color;

            if (!mIsInstanceOnScript && mIsMouseMovingOn)
                color = Color.FromArgb(127, 0, 0, 0);
            else
                color = Color.Black;

            // draw the kuang-kuang
            g.DrawEllipse(new Pen(color), 0, 0, Size.Width - 1, Size.Height - 1);

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
