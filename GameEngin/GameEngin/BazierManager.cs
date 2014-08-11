using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngin
{
    public class BazierManager
    {

        public PointF startPoint;
        public PointF endPoint;
        public PointF firstHandler;
        public PointF secondHandler;


        public PointF lineStart;
        public PointF lineEnd;

        private TerrienArea area;

        public BazierManager()
        {

        }

        public void Draw(Graphics g,Pen p)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawBezier(p, startPoint, firstHandler, secondHandler, endPoint);
            //g.DrawLine(Pens.Red, firstHandler, secondHandler);
            g.DrawLine(Pens.Yellow, firstHandler, startPoint);
            g.DrawLine(Pens.Yellow, secondHandler, endPoint);

            g.DrawLine(Pens.OrangeRed,lineStart, lineEnd);

            if (area != null)
            {
                area.Draw(g, Brushes.Red, new SizeF(4,4));
                PointF [] intesections = area.Intersection(lineStart, lineEnd);
                foreach (var intersection in intesections)
                {
                    if (!intersection.IsEmpty) g.FillEllipse(Brushes.Black, new RectangleF(intersection.X - 3, intersection.Y, 6, 6));
                }
                
            }

        }

        public void CalaculateBezier()
        {
            area = new TerrienArea(startPoint, firstHandler, secondHandler, endPoint);
            area.Update();
        }

        public void setLocation(PointF loc, int index)
        {
            switch (index)
            {
                case 1:
                    startPoint = loc;
                    break;

                case 2:
                    firstHandler = loc;
                    break;
                default:
                case 3:
                    secondHandler = loc;
                    break;
                case 4:
                    endPoint = loc;
                    break;
                case 5:
                    lineStart = loc;
                    break;
                case 6:
                    lineEnd = loc;
                    break;
            }
        }


    }
}
