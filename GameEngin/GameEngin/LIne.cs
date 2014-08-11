using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngin
{
    public class Line
    {
        public const double RAD_TO_DEG = 57.29577951;

        public PointF start;
        public PointF end;

        public Line(PointF start, PointF end)
        {
            this.start = start;
            this.end = end;
        }

        public void draw(Graphics g,Pen p)
        {
            g.DrawLine(p, start, end);
        }

        public double calcultePerpendicularLineSlope()
        {
            double sl = slope();
            if (sl == 0)
                return 0;
            return -1 / sl;
        }

        public static double calculateAnglw(PointF v1, PointF v2)
        {

            float diffy = v2.Y - v1.Y;
            float diffx = v2.X - v1.X;

            float tanAlpha = diffy / diffx;

            double alpha = Math.Atan(tanAlpha) * RAD_TO_DEG; // calculat the angle

            //if the angle in the first quadrant
            if (v2.Y < v1.Y && v2.X > v1.X)
                return alpha;
            //if the angle in the second of third quadrat
            else if ((v2.Y < v1.Y && v2.X < v1.X) ||
                    (v2.Y > v1.Y && v2.X < v1.X))
                return alpha + 180;
            else
                return alpha + 360;
        }

        public double slope()
        {
            return (end.Y - start.Y) / (end.X - start.X);
        }

        public bool isOver(PointF p)
        {
            return p.X >= start.X && p.X <= end.X;
        }

        public static PointF GetRayAcrossPoints(PointF a, PointF b, int distance)
        {

            // a. calculate the vector from b to a:
            double vectorX = b.X - a.X;
            double vectorY = b.Y - a.Y;

            // b. calculate the proportion of hypotenuse
            double factor = distance / Math.Sqrt(vectorX * vectorX + vectorY * vectorY);

            // c. factor the lengths
            vectorX *= factor;
            vectorY *= factor;

            // d. calculate and Draw the new vector,
            return new PointF((int)(a.X + vectorX), (int)(a.Y + vectorY));
        }

        public static PointF GetLineEndOfDist(PointF p,float slope, float disty)
        {
            if (slope == 0)
                return new PointF(p.X, p.Y - disty);
            float g = slope * p.X - p.Y;

            float y = p.Y - disty;
            float x = (g + y) / slope;

            return new PointF(x, y);
        }

        public static float GetLineYIntesept(PointF p1, PointF p2)
        {
            float slope1 = (p2.Y - p1.Y) / (p2.X - p1.X);

            return p1.Y - slope1 * p1.X;
        }

        public static float GetLineYIntesept(PointF p, float slope)
        {
            return p.Y - slope * p.X;
        }

        public static double CalculateSlope(PointF start, PointF end)
        {
            if (end.X - start.X == 0)
                return 0;
            return (end.Y - start.Y) / (end.X - start.X);
        }

        public static PointF FindIntersectionFast(PointF line1Start, PointF line1End, PointF line2Start, PointF line2End)
        {

            float slope1 = (line1End.Y - line1Start.Y) / (line1End.X - line1Start.X);
            float slope2 = (line2End.Y - line2Start.Y) / (line2End.X - line2Start.X);

            float yinter1 = GetLineYIntesept(line1Start, slope1);
            float yinter2 = GetLineYIntesept(line2Start, slope2);

            if (slope1 == slope2 && yinter1 != yinter2)
                return PointF.Empty;

            float x = (yinter2 - yinter1) / (slope1 - slope2);

            float y = slope1 * x + yinter1;

            return new PointF(x, y);
        }

        public static bool IsPointOnLine(PointF lineStart, PointF lineEnd, PointF point)
        {
            double slope = CalculateSlope(lineStart, lineEnd);
            float yIntersept = GetLineYIntesept(lineStart, (float)slope);

            return lineStart.Y == slope * lineStart.X + yIntersept;
        }

        public static PointF FindActuallIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
         {

            float slope1 = (p2.Y - p1.Y) / (p2.X - p1.X);
            float slope2 = (p4.Y - p3.Y) / (p4.X - p3.X);

            float yinter1 = GetLineYIntesept(p1, slope1);
            float yinter2 = GetLineYIntesept(p3, slope2);

            if (slope1 == slope2 && yinter1 != yinter2)
                return PointF.Empty;

            float x = (yinter2 - yinter1) / (slope1 - slope2);

            float y = slope1 * x + yinter1;

            PointF intersection = new PointF(x, y);

            return IsPointOnLine(p1, p2, intersection) ? intersection : PointF.Empty;
        }

        // Returns 1 if the lines intersect, otherwise 0. In addition, if the lines 
        // intersect the intersection point may be stored in the floats i_x and i_y.
        public static PointF FindSuperLineIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            float i_x = 0;
            float i_y = 0;

            float s1_x, s1_y, s2_x, s2_y;
            s1_x = p2.X - p1.X;
            s1_y = p2.Y - p1.Y;
            s2_x = p4.X - p3.X;
            s2_y = p4.Y - p3.Y;

            float s, t;
            s = (-s1_y * (p1.X - p3.X) + s1_x * (p1.Y - p3.Y)) / (-s2_x * s1_y + s1_x * s2_y);
            t = (s2_x * (p1.Y - p3.Y) - s2_y * (p1.X - p3.X)) / (-s2_x * s1_y + s1_x * s2_y);

            if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
            {
                i_x = p1.X + (t * s1_x);
                i_y = p1.Y + (t * s1_y);
                return new PointF(i_x, i_y);
            }

            return PointF.Empty; // No collision
        }

        public static PointF FindExtraIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            float A1 = p2.Y - p1.Y;
            float B1 = p1.X - p2.X;
            float C1 = A1 * p1.X + B1 * p1.Y;

            float A2 = p4.Y - p3.Y;
            float B2 = p3.X - p4.X;
            float C2 = A2 * p3.X + B2 * p3.Y;

            float det = A1 * B2 - A2 * B1;
            if (det == 0)
            {
                return PointF.Empty;
            }
            else
            {
                float x = (B2 * C1 - B1 * C2) / det;
                float y = (A1 * C2 - A2 * C1) / det;

                return new PointF(x, y);
            }
        }
    }
}
