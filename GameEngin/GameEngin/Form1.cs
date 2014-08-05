using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngin
{
    public partial class Form1 : Form
    {
        List<Line> lines = new List<Line>();

        PointF characterLoction;

        float length = 10;
        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            CreateLines();
        }

        private void CreateLines()
        {
            lines.Add(new Line(new PointF(0, 100), new PointF(50, 100)));
            lines.Add(new Line(new PointF(50, 100), new PointF(100, 100)));
            lines.Add(new Line(new PointF(100, 100), new PointF(150, 150)));
            lines.Add(new Line(new PointF(150, 150), new PointF(500, 150)));

            characterLoction = lines[0].start;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            List<Line> over = new List<Line>();
            foreach (var item in lines)
            {
                item.draw(e.Graphics, Pens.Black);

                if (item.isOver(characterLoction))
                {
                    over.Insert(0, item);
                }
            }

            if (over.Count == 0)
                return;

            Line attached = over[0];

            double slope = Line.calculateAnglw(attached.start, attached.end);

            e.Graphics.FillEllipse(Brushes.Blue,new RectangleF(characterLoction,new SizeF(3,3)));

            GraphicsPath path = new GraphicsPath();
            path.AddLine(new PointF(characterLoction.X, characterLoction.Y - length), characterLoction);

            Matrix mat = new Matrix();

            mat.RotateAt((float)slope, characterLoction, MatrixOrder.Prepend);

            path.Transform(mat);

            PointF ray = Line.GetRayAcrossPoints(path.PathPoints[0], path.PathPoints[path.PointCount - 1],1000);

            PointF intersection = Line.FindIntersectionFast(new PointF(characterLoction.X,characterLoction.Y - length), ray, attached.start, attached.end);

            PointF lstart = Line.GetLineEndOfDist(intersection, (float)Line.CalculateSlope(path.PathPoints[0], path.PathPoints[path.PointCount - 1]), length);

            if (intersection.IsEmpty)
            {
                e.Graphics.DrawLine(Pens.Red, new PointF(characterLoction.X, attached.start.Y), new PointF(characterLoction.X, attached.start.Y - length));
            }
            else
            {
                e.Graphics.DrawLine(Pens.Red, lstart, intersection);
            }
            path.Dispose();

            base.OnPaint(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right) characterLoction.X += 10;
            if (e.KeyCode == Keys.Left) characterLoction.X -= 10;

            this.Invalidate();
            base.OnKeyUp(e);
        }
    }
}
