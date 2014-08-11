using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngin
{
    public class Terrian
    {
        private GraphicsPath path = new GraphicsPath();

        
        public Terrian()
        {

        }

        public GraphicsPath Path
        {
            get { return path; }
        }

        public Line GetCollidedLine()
        {
            PointF [] points = path.PathPoints;
            for (int i = 1; i < path.PathPoints.Length; i++)
            {
                Line line = new Line(points[i - 1], points[i]);
            }

            return null;
        }

        public void Draw(Graphics g,Pen p)
        {
            g.DrawPath(p, path);
        }

        /*computes intersection between a cubic spline and a line segment*/

    }
}
