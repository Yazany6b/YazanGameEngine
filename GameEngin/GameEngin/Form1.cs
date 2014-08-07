using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
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
            lines.Add(new Line(new PointF(150, 150), new PointF(300, 150)));
            lines.Add(new Line(new PointF(300, 150), new PointF(350, 100)));
            lines.Add(new Line(new PointF(350, 100), new PointF(400, 100)));
            lines.Add(new Line(new PointF(400, 100), new PointF(450, 150)));
            lines.Add(new Line(new PointF(450, 150), new PointF(600, 150)));

            characterLoction = new PointF(lines[0].start.X,lines[0].start.Y);
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

            

            if (attached.slope() == 0)
            {
                e.Graphics.DrawLine(Pens.Red, characterLoction.X, attached.start.Y - length, characterLoction.X, attached.start.Y);
            }
            else
            {
                PointF characterEnd = Line.GetLineEndOfDist(characterLoction, (float)attached.calcultePerpendicularLineSlope(), length);
                
                PointF intersection = Line.FindIntersectionFast(characterEnd, characterLoction, attached.start, attached.end);
                PointF lineStart = Line.GetLineEndOfDist(intersection, (float)attached.calcultePerpendicularLineSlope(), length);
                e.Graphics.DrawLine(Pens.Red, lineStart, intersection);
                characterLoction = intersection;
            }

            base.OnPaint(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right) characterLoction.X += 10;
            if (e.KeyCode == Keys.Left) characterLoction.X -= 10;

            this.Invalidate();
            base.OnKeyUp(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            int add = 2;
            new Task(() => {
                while (true)
                {
                    characterLoction.X += add;
                    Thread.Sleep(30);
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            this.Invalidate();
                        }));
                    }
                    catch (Exception)
                    {
                        break;
                    }


                    if (characterLoction.X < 0 || characterLoction.X > lines[lines.Count - 1].end.X)
                        add *= -1;
                }
            }).Start();
            base.OnMouseDoubleClick(e);
        }
    }
}
