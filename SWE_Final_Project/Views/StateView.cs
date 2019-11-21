using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SWE_Final_Project.Views {
    class StateView: System.Windows.Forms.PictureBox {
        private string mStateContent = "";

        private int mContentWidth = 0;
        private int mContentHeight = 0;

        private const int ENLARGING_RATE = 4;

        public StateView(int x, int y, string stateContent) {
            mStateContent = stateContent.Trim();

            mContentWidth = mStateContent.Length * ENLARGING_RATE;
            mContentHeight = mStateContent.Split('\n').Length * 3 * ENLARGING_RATE;

            Console.WriteLine("W: {0}, H: {1}", mContentWidth, mContentHeight);

            this.Location = new Point((int) ((float) x - (mContentWidth / 2.0F)),
                (int) ((float) y - (mContentHeight / 2.0F)));
            Size = new Size(mContentWidth, mContentHeight);
            BackColor = Color.Transparent;

            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.DrawRectangle(Pens.Black, 0, 0, mContentWidth - 1, mContentHeight - 1);
        }
    }
}
