using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngin
{
    public class TerrienArea
    {

        public const int THRESHOLD = 3;
        public const int DETAILS = 128;

        public PointF StartPoint { get; set; }
        public PointF ControlPoint1 { get; set; }
        public PointF ControlPoint2 { get; set; }
        public PointF EndPoint { get; set; }

        private PointF[] points;

        public TerrienArea(PointF start, PointF control1, PointF control2, PointF end)
        {
            StartPoint = start;
            EndPoint = end;
            ControlPoint1 = control1;
            ControlPoint2 = control2;
        }

        private void CalculateBezierPoints()
        {
            points = new PointF[DETAILS];

            for (int i = 0; i < points.Length; i++)
            {
                float t = i / (float)points.Length;
                points[i] = CaclulatePointAtT(t);
            }
        }


        private PointF CaclulatePointAtT(float t)
        {

            float Ax = ((1 - t) * StartPoint.X) + (t * ControlPoint1.X);
            float Ay = ((1 - t) * StartPoint.Y) + (t * ControlPoint1.Y);
            float Bx = ((1 - t) * ControlPoint1.X) + (t * ControlPoint2.X);
            float By = ((1 - t) * ControlPoint1.Y) + (t * ControlPoint2.Y);
            float Cx = ((1 - t) * ControlPoint2.X) + (t * EndPoint.X);
            float Cy = ((1 - t) * ControlPoint2.Y) + (t * EndPoint.Y);

            float Dx = ((1 - t) * Ax) + (t * Bx);
            float Dy = ((1 - t) * Ay) + (t * By);

            float Ex = ((1 - t) * Bx) + (t * Cx);
            float Ey = ((1 - t) * By) + (t * Cy);

            float Px = ((1 - t) * Dx) + (t * Ex);
            float Py = ((1 - t) * Dy) + (t * Ey);

            return new PointF(Px, Py);
        }

        public PointF [] Intersection(PointF lineStart, PointF lineEnd)
        {
            List<PointF> intersections = new List<PointF>();
            for (int i = 1; i < points.Length; i++)
            {
                PointF intersection = Line.FindSuperLineIntersection(points[i - 1], points[i], lineStart, lineEnd);

                if (!intersection.IsEmpty)
                {
                    intersections.Add(intersection);
                }
            }

            return intersections.ToArray<PointF>();
        }

        public static double Distance(PointF p1, PointF p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        public void Update()
        {
            CalculateBezierPoints();
        }

        public void Draw(Graphics g, Brush brush,SizeF size)
        {
           /* foreach (var item in points)
            {
                g.FillEllipse(brush, new RectangleF(new PointF(item.X - size.Width / 2, item.Y - size.Height / 2), size));
            }*/
        }

    }
}
