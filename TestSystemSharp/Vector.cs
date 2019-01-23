using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    class Vector
    {
        Point point_begin;
        Point point_end;

        //Точка одного конца вектора
        public Point PointBegin
        {
            get { return point_begin; }
            set
            {
                point_begin = value;
                TryInit();
            }
        }

        //Точка другого конца вектора
        public Point PointEnd
        {
            get { return point_end; }
            set
            {
                point_end = value;
                TryInit();
            }
        }

        //Точка вершины вектора, отложенного от начала координат
        public Point PointTopVect { get; private set; }

        //Длинна вектора
        public double Length { get; private set; }

        //Конструктор через 2 точки
        public Vector(Point PointBegin, Point PointEnd)
        {
            this.PointBegin = PointBegin;
            this.PointEnd = PointEnd;
        }

        //Конструктор через точку начала, длинну вектора и вектор, перпендикулярный данному
        public Vector(Point PointBegin, double Length, Vector v)
        {
            //Из скалярного произведения
            double x = -(Length * v.PointTopVect.Y) / v.Length;
            double y = (Length * v.PointTopVect.X) / v.Length;

            this.PointBegin = PointBegin;
            this.PointEnd = new Point(x, y) + PointBegin;
        }

        //Конструктор через вектор, коллинеарный данному, лежащий на той же прямой, что и данный и 
        //начало которого является началом данного вектора и через длинну данного вектора
        public Vector(Vector beg, double Length)
        {
            //Из скалярного произведения
            double x = (Length * beg.PointTopVect.X)/ beg.Length;
            double y = (Length * beg.PointTopVect.Y) / beg.Length;

            this.PointBegin = beg.PointBegin;
            this.PointEnd = beg.PointBegin + new Point(x, y);
        }

        //Для случая, когда изменяется точка одного из концов вектора
        public void TryInit()
        {
            if (PointBegin != null && PointEnd != null) Init();
        }

        //Инициализация координат вектора и его длинны
        public void Init()
        {
            InitPointTopVect();
            InitLength();
        }

        //Инициализация координат вектора, исходящего из начала координат
        public void InitPointTopVect()
        {
            PointTopVect = PointEnd - PointBegin;
        }

        //Инициализация длинны вектора
        private void InitLength()
        {
            Length = Math.Sqrt(Math.Pow(PointTopVect.X, 2) + Math.Pow(PointTopVect.Y, 2));
        }

        public static bool operator <(Vector v1, Vector v2)
        {
            if (v1.Length < v2.Length) return true;
            else return false;
        }

        public static bool operator >(Vector v1, Vector v2)
        {
            if (v1.Length > v2.Length) return true;
            else return false;
        }

        public static Vector operator -(Vector v)
        {
            return new Vector(v.PointEnd, v.PointBegin);
        }
    }
}