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

                /*---------------------------------------------------------------------------
        Returns Array of intersection Points on a quadratic curve.
        An empty Array is returned if no intersection is found.
 
        Parameters:
        A		-Start Point of a segment to make intersection.
        B		-End Point of a segment to make intersection.
        sp		-Start Point of curve.
        cp		-Control Point of curve.
        ep		-End Point of curve.
        rez		-The resolution of curve tests
        Note: Uses another function for testing the intersections:
 
        http://keith-hair.net/blog/2008/08/04/find-intersection-point-of-two-lines-in-as3/
 
        ----------------------------------------------------------------------------*/
        List<PointF> lineToQCurve_Intersect(PointF A,PointF B,PointF sp,PointF cp,PointF ep,int rez =80)
        {
	        //rez less than 2 is almost "Pointless" LOL.
	        var low =2;
	        var high =99; //100 causes infinite loop.
	        rez=Math.Min(Math.Max(Math.Min(low,high),rez),high);
	        var t =0;
	        var ft =0;
	        var n =99/rez;
	        var C=new PointF();
	        var D=new PointF();
	        var L=new PointF();
	        PointF ip;
	        List<PointF> a = new List<PointF>();
	        //test possible segment intersections in a loop.
	        var z=100;
	        while (z > -1)
	        {
		        t=z/100;
		        C.X=(float)(Math.Pow(1-t,2)*sp.X+2*(1-t)*t*cp.X+Math.Pow(t,2)*ep.X);
		        C.Y=(float)(Math.Pow(1-t,2)*sp.Y+2*(1-t)*t*cp.Y+Math.Pow(t,2)*ep.Y);
		        D=L;//Connect start to last end point
                L = new PointF(C.X,C.Y);
		        if (z == 100) {
			        D= new PointF(ep.X,ep.Y);
		        }

		        ip=Line.FindIntersectionFast(A,B,C,D);
		        
                if (ip != null) {
			        a.Add(ip);
			        if(a.Count == 2){
				        break;
			        }
		        }
		        z-=n;
	        }

	        return a;
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

            manager.Draw(e.Graphics, new Pen(Color.Green,2));

            if (true) return;
            e.Graphics.DrawBezier(Pens.Green, new Point(400, 400), new Point(450, 350), new Point(500, 340), new Point(600, 450));

            PointF [] curve = new PointF[] { new PointF(100, 200), new PointF(300, 100), new PointF(500, 200) };
            PointF [] line = new PointF[]{new PointF(200, 50), new PointF(350, 300)};

            e.Graphics.DrawCurve(Pens.Green, curve);
            e.Graphics.DrawLines(Pens.Blue, line);

            GraphicsPath ph = new GraphicsPath();

            ph.AddCurve(curve);

            foreach (var item in curve)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(150,180,30,40)), new RectangleF(item.X - 3,item.Y - 3,6,6));
            }

            foreach (var item in line)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(150, 180, 30, 40)), new RectangleF(item.X - 3, item.Y - 3, 6, 6));
            }

            List<PointF> p = lineToQCurve_Intersect(line[0], line[1], curve[0], curve[1], curve[2], 20);


            foreach (var item in p)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(150, 40, 30, 180)), new RectangleF(item.X - 3, item.Y - 3, 6, 6));
            }

            foreach (var item in ph.PathPoints)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(150, 40, 180, 30)), new RectangleF(item.X - 3, item.Y - 3, 6, 6));
            }

            if (true) return;
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

        BazierManager manager = new BazierManager();
        Point activePoint = Point.Empty;
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
    }
}
