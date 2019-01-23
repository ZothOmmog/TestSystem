using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace TestSystemSharp
{
    class Program
    {
        //Вычисление коэффицента поворота
        static double KTurn(double left_end, double right_end, double left_right, double center)
        {
            double k_rast = 0.5; //Коэффицент учета расстояния до цели, чем больше, тем более плавный будет поворот
            if ((left_end - left_right < 200 || Math.Abs(left_end - right_end) < 3) && center != 0)
                return 1 - Math.Abs(left_end - right_end) / left_right;
            else return 0.3;
        }


        static void Test(double wheel_distance, Point end, double k_turn, double speed_max, int iterations)
        {
            Wheels wheels = new Wheels(wheel_distance);
            Vector left_kol_end = new Vector(wheels.LeftWheel, end);
            Vector right_kol_end = new Vector(wheels.RightWheel, end);
            Vector center_kol_end;

            StreamWriter sw = new StreamWriter(new FileStream("data.txt", FileMode.Create));
            
            sw.WriteLine(wheels.ToString().Replace(',', '.'));

            for (int i = 0; i < iterations; i++)
            {
                k_turn = KTurn(left_kol_end.Length, right_kol_end.Length, wheels.WheelsDistance, wheels.Center.X);

                if (left_kol_end > right_kol_end) wheels.ChangePosition(speed_max, speed_max * k_turn);
                else if (right_kol_end == left_kol_end) wheels.ChangePosition(speed_max, speed_max);
                else wheels.ChangePosition(speed_max * k_turn, speed_max);

                sw.WriteLine(wheels.ToString().Replace(',', '.'));

                left_kol_end = new Vector(wheels.LeftWheel, end);
                right_kol_end = new Vector(wheels.RightWheel, end);

                center_kol_end = new Vector(wheels.Center, end);

                if (center_kol_end.Length < 5)
                    break;
            }
            sw.Close();

            string[] titles = { "center", "left wheel", "right wheel" };

            GnuPlot.CreateScript("D:\\repos\\PSTU\\Robotics\\TestSystemSharp\\TestSystemSharp\\bin\\Debug\\data.txt",
                "Script.plt", 3, titles);
            GnuPlot.RunScript("Script.plt");
        }

        static void Main(string[] args)
        {
            Point end = new Point(-100, 0); //Пункт назначения
            double wheel_distance = 10; //Расстояние между колесами
            double speed_max = 10; //Максимальная скорость колеса
            double k_turn = 0.2; //Коэффицент поворота (от 0 до 1), чем больше, тем более плавный поворот, при 1 машинка всегда едет прямо
            int iterations = 1000; //Максимальное время работы

            Test(wheel_distance, end, k_turn, speed_max, iterations);

            
        }
    }
}