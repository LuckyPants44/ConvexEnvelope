using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConvexEnvelope
{
    class Vector
    {
        public float X { private set; get; }
        public float Y { private set; get; }
        public double distance { private set; get; }

        public Vector(PointF p1, PointF p2)
        {
            X = p2.X - p1.X;
            Y = p2.Y - p1.Y;
            distance = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }
    }
}
