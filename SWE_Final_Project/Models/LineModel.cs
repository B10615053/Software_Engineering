using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Final_Project.Models {
    [Serializable]
    public enum DirectionType {
        TO_UP,
        TO_RIGHT,
        TO_DOWN,
        TO_LEFT,
        SLASHED,
        LITERALLY_THE_SAME_POINT
    }

    // the line-model
    [Serializable]
    public class LineModel {
        public double radian = 0;

        // the direction-type of this line
        private DirectionType mDirectionType = DirectionType.LITERALLY_THE_SAME_POINT;
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

            setRadian();
        }

        // constructor
        public LineModel(int srcX, int srcY, int dstX, int dstY):
            this(new Point(srcX, srcY), new Point(dstX, dstY)) { }

        // set the direction-type according to the src & dst points
        private void setRadian() {
            // start point XY
            int sptX = mSrcLocOnScript.X;
            int sptY = mSrcLocOnScript.Y;
            // end point XY
            int eptX = mDstLocOnScript.X;
            int eptY = mDstLocOnScript.Y;

            // Radian equal π/2(90) or 3π/2(270)
            if (eptX == sptX)
            {
                // Point vertical down
                if (eptY > sptY)
                    radian = 3 * Math.PI / 2.0;
                // Point vertical up
                else
                    radian = Math.PI / 2.0;
            }
            // Other degree
            else
            {
                double slope = Convert.ToDouble(eptY - sptY) / Convert.ToDouble(eptX - sptX);
                radian = Math.Atan(slope);
                // First quadrant(0 ~ 90)
                if (radian >= 0 && (eptX < sptX) && (eptY <= sptY))
                {
                    radian += 0;
                }
                // Second quadrant(90 ~ 180)
                else if(radian <= 0 && (eptX > sptX) && (eptY <= sptY))
                {
                    radian += Math.PI;
                }
                // Third quadrant(180 ~ 270)
                else if (radian > 0 && (eptX > sptX) && (eptY > sptY))
                {
                    radian += Math.PI;
                }
                // Fourth quadrant(270 ~ 360)
                else if (radian < 0 && (eptX < sptX) && (eptY > sptY))
                {
                    radian += 2 * Math.PI;
                }
            }
            //Console.WriteLine("The " + radian + " is equal to " + (180.0 / Math.PI) * radian);

            // they're the same point
            if (sptX == eptX && sptY == eptY)
                mDirectionType = DirectionType.LITERALLY_THE_SAME_POINT;
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
