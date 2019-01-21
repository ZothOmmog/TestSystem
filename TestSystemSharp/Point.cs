using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double X, double Y) { Init(X, Y); }

        public void Init(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        static public Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        static public Point operator -(Point p)
        {
            return new Point(-p.X, -p.Y);
        }

        static public Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", X, Y);
        }
    }
}