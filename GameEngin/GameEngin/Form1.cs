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

        BazierManager manager = new BazierManager();
        Point activePoint = Point.Empty;

        float length = 10;

        RectangleF rect = RectangleF.Empty;

        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            CreateLines();

            BazierHandler();
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

        public void BazierHandler()
        {
            manager.startPoint = new PointF(300, 500);
            manager.endPoint = new PointF(600, 500);

            manager.firstHandler = new PointF(400, 300);
            manager.secondHandler = new PointF(500, 400);

            manager.lineStart = new PointF(350, 250);
            manager.lineEnd = new PointF(700, 260);

            startPanel.Location = Point.Round(manager.startPoint);
            endPanel.Location = Point.Round(manager.endPoint);
            handler1Panel.Location = Point.Round(manager.firstHandler);
            handler2Panel.Location = Point.Round(manager.secondHandler);
            lineStartPanel.Location = Point.Round(manager.lineStart);
            lineEndPanel.Location = Point.Round(manager.lineEnd);

            manager.CalaculateBezier();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!rect.IsEmpty)
            {
                e.Graphics.FillRectangle(Brushes.Blue, rect);
            }
            manager.Draw(e.Graphics, new Pen(Color.Green,2));
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {

            base.OnKeyUp(e);
        }

        private void FallenRectAnimation()
        {
            PointF[] pfs = manager.area.points;
            List<PointF> collided = new List<PointF>();
            
            float times = 0;
            new Task(() =>
            {
                while (true)
                {
                    rect.Y += 5;

                    PointF chk = manager.startPoint.Y < manager.endPoint.Y ? manager.startPoint : manager.endPoint;

                    if (rect.Y > chk.Y)
                    {
                        for (int i = 0; i < pfs.Length; i++)
                        {
                            if (pfs[i].X >= rect.X && pfs[i].X < rect.X + rect.Width && Math.Abs(pfs[i].Y - (rect.Y + rect.Height)) < 10)
                            {
                                times += Math.Abs(pfs[i].Y - (rect.Y + rect.Height));
                                pfs[i].Y = rect.Y + rect.Height + 1;

                            }
                        }
                    }

                    Thread.Sleep(50);

                    Invalidate();

                    if (times > 100)
                        break;
                }
            }).Start();
        }

        public void HeartBeatsAnimation()
        {
            PointF[] pfs = manager.area.points;
            new Task(() =>
            {
                while (true)
                {
                    for (int i = 0; i < pfs.Length; i+= 9)
			        {

			        }

                    //PointF characterEnd = Line.GetLineEndOfDist(characterLoction, (float)(new Line().calcultePerpendicularLineSlope(), length));

                    Thread.Sleep(50);

                    Invalidate();
                }
            }).Start();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            rect = new RectangleF(e.X, 0, 50, 50);
            FallenRectAnimation();
            base.OnMouseDoubleClick(e);
        }

        private void startPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (activePoint.IsEmpty)
                {
                    activePoint = e.Location;
                }
                else
                {
                    ((Panel)sender).Location = new Point(((Panel)sender).Location.X + (e.X - activePoint.X), ((Panel)sender).Location.Y + (e.Y - activePoint.Y));

                    manager.setLocation(((Panel)sender).Location, ((Panel)sender).TabIndex);
                    manager.CalaculateBezier();
                    this.Invalidate();

                }
            }
        }

        private void startPanel_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void handler2Panel_MouseUp(object sender, MouseEventArgs e)
        {
            activePoint = Point.Empty;
        }

        Point formDown = Point.Empty;
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            formDown = Point.Empty;
            this.Cursor = Cursors.Default;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (formDown.IsEmpty)
                {
                    formDown = e.Location;
                    this.Cursor = Cursors.Hand;
                }
                else
                {
                    int diffx = e.X - formDown.X;
                    int diffy = e.Y - formDown.Y;
                    formDown = e.Location;

                    foreach (Control control in Controls)
                    {
                        control.Location = new Point(control.Location.X + diffx, control.Location.Y + diffy);
                        manager.setLocation(control.Location, control.TabIndex);
                        manager.CalaculateBezier();
                        this.Invalidate();
                    }

                    this.Invalidate();
                }
            }
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                manager.DrawAreaPoints = !manager.DrawAreaPoints;
                this.Invalidate();
            }
        }
    }
}
