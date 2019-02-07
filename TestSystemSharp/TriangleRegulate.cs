using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TestSystemSharp
{
    class TriangleRegulate
    {
        public Point End { get; private set; }
        public Vector LeftWheelEndVect { get; private set; }
        public Vector RightWheelEndVect { get; private set; }
        public Vector LeftRightVect { get; private set; }
        public Vector CenterEndVect { get; private set; }

        public TriangleRegulate(Point End, Wheels wheels)
        {
            Init(End, wheels);
        }

        private void Init(Point End, Wheels wheels)
        {
            this.End = End;
            ChangeVector(wheels);
        }

        private void ChangeVector(Wheels wheels)
        {
            LeftWheelEndVect = new Vector(wheels.LeftWheel, End);
            RightWheelEndVect = new Vector(wheels.RightWheel, End);
            LeftRightVect = new Vector(wheels.LeftWheel, wheels.RightWheel);
            CenterEndVect = new Vector(wheels.Center, End);
        }

        static private double MinMax(double num, double min, double max)
        {
            if (num > max) return max;
            if (num < min) return min;
            return num;
        }

        //Вычисление коэффицента поворота
        private double KTurn()
        {
            double k_delta_dist = 0.05; //Коэффицент разницы расстояний до цели, чем больше, тем позже будет включаться вычисление коэффицента поворота
            double k_harsh = 0.7; //Коэффицент резкости поворота, чем больше, тем более реким будет поворот
            double k_turn_ret = 0.9; //Возвращает этот коэффицент, если расстояния от колёс равны, т.к. мы не знаем сзади или спереди цель

            if (Math.Abs(LeftWheelEndVect.Length - RightWheelEndVect.Length) > k_delta_dist)
                return 1 - MinMax(k_harsh * Math.Abs(LeftWheelEndVect.Length - RightWheelEndVect.Length) / LeftRightVect.Length, 0, 1);
            else
                return k_turn_ret;
        }

        //Задание колесам скорости
        public Wheels RegulateSpeed(Wheels wheels, double speed_max)
        {
            ChangeVector(wheels);

            if (LeftWheelEndVect.Length > RightWheelEndVect.Length) wheels.ChangePosition(speed_max, speed_max * KTurn());
            else wheels.ChangePosition(speed_max * KTurn(), speed_max);

            return wheels;
        }
    }
}
