using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    [Serializable]
    enum DirectionType {
        TO_UP,
        TO_RIGHT,
        TO_DOWN,
        TO_LEFT,
        SLASHED,
        LITERALLY_SAME_POINT
    }

    // the line-model
    [Serializable]
    class LineModel {
        // the direction-type of this line
        private DirectionType mDirectionType = DirectionType.LITERALLY_SAME_POINT;
        public DirectionType Direction { get => mDirectionType; }

        // src location on script
        private Point mSrcLocOnScript = new Point();
        public Point SrcLocOnScript { get => mSrcLocOnScript; set => mSrcLocOnScript = value; }

        // dst location on script
        private Point mDstLocOnScript = new Point();
        public Point DstLocOnScript { get => mDstLocOnScript; set => mDstLocOnScript = value; }

        /* ================================ */

        // constructor
        public LineModel(Point srcOnScript, Point dstOnScript) {
            mSrcLocOnScript = new Point(srcOnScript.X, srcOnScript.Y);
            mDstLocOnScript = new Point(dstOnScript.X, dstOnScript.Y);

            setDirectionType();
        }

        // constructor
        public LineModel(int srcX, int srcY, int dstX, int dstY):
            this(new Point(srcX, srcY), new Point(dstX, dstY)) { }

        // set the direction-type according to the src & dst points
        private void setDirectionType() {
            // start point XY
            int sptX = mSrcLocOnScript.X;
            int sptY = mSrcLocOnScript.Y;
            // end point XY
            int eptX = mDstLocOnScript.X;
            int eptY = mDstLocOnScript.Y;

            // they're the same point
            if (sptX == eptX && sptY == eptY)
                mDirectionType = DirectionType.LITERALLY_SAME_POINT;
            // it's a vertical line
            else if (sptX == eptX) {
                if (sptY < eptY)
                    mDirectionType = DirectionType.TO_DOWN;
                else
                    mDirectionType = DirectionType.TO_UP;
            }
            // it's a horizontal line
            else if (sptY == eptY) {
                if (sptX < eptX)
                    mDirectionType = DirectionType.TO_RIGHT;
                else
                    mDirectionType = DirectionType.TO_LEFT;
            }
            // it's a slashed line
            else
                mDirectionType = DirectionType.SLASHED;
        }

        // check if the line is vertical or not
        public bool IsVertical() {
            return mSrcLocOnScript.X == mDstLocOnScript.X;
        }

        // check if the line is horizontal or not
        public bool IsHorizontal() {
            return mSrcLocOnScript.Y == mDstLocOnScript.Y;
        }
    }
}
