using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static System.Math;

namespace Individual_task1
{
    public partial class Form1 : Form
    {
        Bitmap pic;
        Graphics graph;
        List<Point> points1 = new List<Point>();
        Pen pen = new Pen(Color.Black, 2);
        public Form1()
        {
            InitializeComponent();
            pic = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = pic;
            graph = Graphics.FromImage(pictureBox1.Image);
            graph.Clear(Color.White);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graph.Clear(Color.White);
            points1.Clear();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            points1.Add(new Point(e.X, e.Y));
            pic.SetPixel(e.X, e.Y, Color.Black);
            graph.DrawEllipse(pen, (float)(e.X-1), (float)(e.Y-1), 2, 2);
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            int leftx = int.MaxValue, lefty = int.MinValue;
            Point left = points1[0], right = points1[0];
            List<Point> points = new List<Point>(points1);

            foreach (var p in points) {
                //Console.WriteLine(p);
                if (leftx > p.X)
                {
                    leftx = p.X; lefty = p.Y;
                    left = p;
                }
                if (leftx == p.X)
                {
                    if (lefty < p.Y)
                    {
                        left = p;
                        lefty = p.Y;
                    }
                }
            }
            int rightx = int.MinValue, righty = int.MaxValue;
            foreach (var p in points)
            {
                //Console.WriteLine(p);
                if (rightx < p.X)
                {
                    rightx = p.X; righty = p.Y;
                    right = p;
                }
                if (leftx == p.X)
                    righty = Min(righty, p.Y);
            }
            //graph.DrawLine(pen, leftx, lefty, rightx, righty);
            pictureBox1.Refresh();
            List<Point> leftl = new List<Point>();
            List<Point> rightl = new List<Point>();
            foreach (var p in points)
            {
                if (p == left || p == right)
                    continue;
                if (where_dot(left, right, p))
                    leftl.Add(p);
                else
                    rightl.Add(p);
            }
            //(int, int) moda = (0, 1);
            //bool flag = true;
            Pen bpen = new Pen(Color.Blue, 5);
            Pen rpen = new Pen(Color.Red, 5);
            /*foreach (var p in leftl)
                graph.DrawEllipse(bpen, (float)(p.X - 2.5), (float)(p.Y - 2.5), 5, 5);
            foreach (var p in rightl)
                graph.DrawEllipse(rpen, (float)(p.X - 2.5), (float)(p.Y - 2.5), 5, 5);*/
            leftl.Add(new Point(leftx, lefty)); leftl.Add(new Point(rightx, righty));
            rightl.Add(new Point(leftx, lefty)); rightl.Add(new Point(rightx, righty));
            leftl.Sort((Point x, Point y) =>
                { return (x.X).CompareTo(y.X); });
            rightl.Sort((Point x, Point y) =>
            { return (x.X).CompareTo(y.X); });

            /*for (int i=0;i< rightl.Count;i++)
            {
                Font f = new Font(Font,FontStyle.Bold);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                Font drawFont = new Font("Arial", 16);
                graph.DrawString(""+i, drawFont, drawBrush, rightl[i].X, rightl[i].Y);
            }
            for (int i = 0; i < leftl.Count; i++)
            {
                Font f = new Font(Font, FontStyle.Bold);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                Font drawFont = new Font("Arial", 16);
                graph.DrawString("" + i, drawFont, drawBrush, leftl[i].X, leftl[i].Y);
            }
            pictureBox1.Refresh();*/
            int j = 0;
            while(j!=leftl.Count-2)
            {
                if (!where_dot(leftl[j], leftl[j + 2], leftl[j + 1]))
                {
                    leftl.Remove(leftl[j + 1]);
                    if (j > 0)
                        j--;
                }
                else
                {
                    j++;
                }
            }
            for(int i = 0; i < leftl.Count - 1; i++)
            {
                graph.DrawLine(pen, leftl[i], leftl[i + 1]);
            }
            j = 0;
            while (j != rightl.Count - 2)
            {
                if (where_dot(rightl[j], rightl[j + 2], rightl[j + 1]))
                {
                    rightl.Remove(rightl[j + 1]);
                    if (j > 0)
                        j--;
                }
                else
                {
                    j++;
                }
            }
            for (int i = 0; i < rightl.Count - 1; i++)
            {
                graph.DrawLine(pen, rightl[i], rightl[i + 1]);
            }
            pictureBox1.Refresh();

        }
        public bool where_dot(Point a, Point b, Point c)
        {
            int xa = a.X, ya = a.Y, xb = b.X, yb = b.Y, xc = c.X, yc = c.Y;
            if ((yc - ya) * (xb - xa) - (xc - xa) * (yb - ya) <= 0)
                return true;
            else
                return false;
        }        
    }
}
