using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConvexEnvelope
{
    class Geometry
    {
        public double Distance(PointF p1, PointF p2)
        { 
            float X = p2.X - p1.X;
            float Y = p2.Y - p1.Y;
            return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }

        public double Angle(Vector v1, Vector v2)
        {
            return (v1.X * v2.X + v1.Y * v2.Y) / (Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y) * Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y));
        }

    }
}
