using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ConvexEnvelope
{
    public partial class Form1 : Form
    {
        int scale = 40;
        List<PointF> points;
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            pictureBox.Image = bmp;
            DrawGrid();
        }

        private void DrawGrid()
        {
            Graphics gr = Graphics.FromImage(bmp);
            Pen axesPen = new Pen(Brushes.Blue, 4);
            //Axes
            gr.DrawLine(axesPen, new Point(0, pictureBox.Height / 2), new Point(pictureBox.Width, pictureBox.Height / 2));
            gr.DrawLine(axesPen, new Point(pictureBox.Width / 2, 0), new Point(pictureBox.Width / 2, pictureBox.Height));
            //Grid
            for (int i = scale; i < pictureBox.Width / 2; i += scale)
            {
                gr.DrawLine(Pens.Black, new Point(pictureBox.Width / 2 + i, 0), new Point(pictureBox.Width / 2 + i, pictureBox.Height));
                gr.DrawLine(Pens.Black, new Point(pictureBox.Width / 2 - i, 0), new Point(pictureBox.Width / 2 - i, pictureBox.Height));
            }
            for (int i = scale; i < pictureBox.Height / 2; i += scale)
            {
                gr.DrawLine(Pens.Black, new Point(0, pictureBox.Height / 2 + i), new Point(pictureBox.Width, pictureBox.Height / 2 + i));
                gr.DrawLine(Pens.Black, new Point(0, pictureBox.Height / 2 - i), new Point(pictureBox.Width, pictureBox.Height / 2 - i));
            }
            pictureBox.Image = bmp;
        }

        private void InsertPoints_Click(object sender, EventArgs e)
        {
            string[] buf;
            points = new List<PointF>();
            try
            {
                FileStream file = new FileStream("C://Users//1//Desktop//Лабы Быкова//6//ConvexEnvelope//ConvexEnvelope//Points.txt", FileMode.Open);
                StreamReader sr = new StreamReader(file);
                buf = sr.ReadToEnd().Split(' ', '\n', '\r');
                sr.Close();
                for (int i = 0; i < buf.Length - 1; i += 3)
                {
                    points.Add(new PointF(float.Parse(buf[i]), float.Parse(buf[i + 1])));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка открытия файла!" + ex.Message);
            }
            DrawPoints();
            ConvexMethod();
        }

        void DrawPoints()
        {
            Graphics gr = Graphics.FromImage(bmp);
            Pen DotPen = new Pen(Brushes.Red, 4);
            for (int i = 0; i < points.Count; i++)
            {
                if (i != points.Count - 1)
                {
                    gr.DrawRectangle(DotPen, new Rectangle(new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)), new Size(2, 2)));
                }
                else
                {
                    gr.DrawRectangle(DotPen, new Rectangle(new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)), new Size(2, 2)));
                }
                pictureBox.Image = bmp;
            }
        }

        void ConvexMethod()
        {
            Stack<PointF> result = new Stack<PointF>();
            Geometry g = new Geometry();
            PointF a = new PointF();
            PointF b = new PointF();
            PointF c = new PointF();
            //Выбираем самую левую из нижних
            PointF lowLeft = new PointF();
            for(int i = 0; i < points.Count; i++)
            {
                if (i != 0)
                {
                    if (lowLeft.Y > points[i].Y)
                    {
                        lowLeft = points[i];
                        points[i] = points[0];
                        points[0] = lowLeft;
                    }
                    if (lowLeft.Y == points[i].Y)
                    {
                        if(lowLeft.X > points[i].X)
                        {
                            lowLeft = points[i];
                            points[i] = points[0];
                            points[0] = lowLeft;
                        }
                    }
                }
                else
                {
                    lowLeft = points[i];
                }
            }
            //Сортируем по полярному углу
            for(int i = 0; i < points.Count; i++)
            {
                for(int j = 0; j < points.Count; j++)
                {
                    Vector v = new Vector(points[0], new PointF(points[0].X+5,points[0].Y));
                    Vector v1 = new Vector(points[0], points[i]);
                    Vector v2 = new Vector(points[0], points[j]);

                    if (g.Angle(v,v1) > (g.Angle(v,v2)))
                    {
                        PointF buf = new PointF();
                        buf = points[j];
                        points[j] = points[i];
                        points[i] = buf;
                    }
                }
            }
            result.Push(points[0]);
            result.Push(points[1]);
            for(int i = 2; i < points.Count; i++)
            {
                b = result.Pop();   //Последний элемент
                a = result.Peek();  //Предпоследний
                result.Push(b);
                c = points[i];
                Vector u = new Vector(a, b);
                Vector v = new Vector(b, c);
                //Если не левый поворот, то 
                while (!(u.X * v.Y - u.Y * v.X >= 0))
                {
                    result.Pop();

                    b = result.Pop();   //Последний элемент
                    a = result.Peek();  //Предпоследний
                    result.Push(b);

                    c = points[i];
                    u = new Vector(a, b);
                    v = new Vector(b, c);
                }
                result.Push(points[i]);
            }
            points = result.ToList<PointF>();
            DrawConvexFigure();
        }

        void DrawConvexFigure()
        {
            Graphics gr = Graphics.FromImage(bmp);
            Pen LinePen = new Pen(Brushes.Green, 2);
            for (int i = 0; i < points.Count; i++)
            {  
                if (i != points.Count - 1)
                {
                    //gr.DrawLine(LinePen, new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i+1].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i+1].Y * scale)), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[0].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[0].Y * scale)));
                    gr.DrawLine(LinePen, new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i + 1].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i + 1].Y * scale)));
                }
                else
                {
                    gr.DrawLine(LinePen, new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[0].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[0].Y * scale)));
                }
                pictureBox.Image = bmp;
            }
        }
    }
}
