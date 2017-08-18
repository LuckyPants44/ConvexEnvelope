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
            ConvexMethod();
        }

        void ConvexMethod()
        {
            DrawFigure();
        }

        void DrawFigure()
        {
            Graphics gr = Graphics.FromImage(bmp);
            Pen DotPen = new Pen(Brushes.Red, 4);
            Pen LinePen = new Pen(Brushes.Green, 2);
            for (int i = 0; i < points.Count()-1; i++)
            {
                gr.DrawRectangle(DotPen, new Rectangle(new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)),new Size(2,2)));
                gr.DrawRectangle(DotPen, new Rectangle(new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i+1].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i+1].Y * scale)), new Size(2, 2)));
                gr.DrawLine(LinePen, new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i].Y * scale)), new Point(Convert.ToInt32(pictureBox.Width / 2 + points[i + 1].X * scale), Convert.ToInt32(pictureBox.Height / 2 - points[i + 1].Y * scale)));
            }
            pictureBox.Image = bmp;
        }
    }
}
