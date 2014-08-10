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
            }
        }


    }
}
