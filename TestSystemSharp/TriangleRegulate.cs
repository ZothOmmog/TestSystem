using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    static class TriangleRegulate
    {
        //Вычисление коэффицента поворота
        static private double KTurn(double left_end, double right_end, double left_right, double center)
        {
            double k_delta_dist = 0.05; //Коэффицент разницы расстояний до цели, чем больше, тем позже будет включаться вычисление коэффицента поворота

            if (Math.Abs(left_end - right_end) > k_delta_dist && center != 0)
                return 1 - Math.Abs(left_end - right_end) / left_right;
            else
                return 1;
        }

        //Задание колесам скорости
        static public Wheels RegulateSpeed(double left_kol_end, double right_kol_end, Wheels wheels, double speed_max)
        {
            double k_turn = KTurn(left_kol_end, right_kol_end, wheels.WheelsDistance, wheels.Center.Y); //Коэффицент поворота

            if (left_kol_end > right_kol_end) wheels.ChangePosition(speed_max, speed_max * k_turn);
            else wheels.ChangePosition(speed_max * k_turn, speed_max);

            return wheels;
        }
    }
}
