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
        static void Test(double wheel_distance, Point end, double k_turn, double speed_max, int iterations)
        {
            Wheels wheels = new Wheels(wheel_distance);
            Vector left_kol_end = new Vector(wheels.LeftWheel, end);
            Vector right_kol_end = new Vector(wheels.RightWheel, end);
            Vector center_kol_end;

            StreamWriter sw = new StreamWriter(new FileStream("data.txt", FileMode.Create));



            sw.WriteLine(wheels);

            for (int i = 0; i < iterations; i++)
            {
                if (left_kol_end > right_kol_end) wheels.ChangePosition(speed_max, speed_max * k_turn);
                else if (right_kol_end == left_kol_end) wheels.ChangePosition(speed_max, speed_max);
                else wheels.ChangePosition(speed_max * k_turn, speed_max);

                sw.WriteLine(wheels);

                left_kol_end = new Vector(wheels.LeftWheel, end);
                right_kol_end = new Vector(wheels.RightWheel, end);

                center_kol_end = new Vector(wheels.Center, end);

                if (center_kol_end.Length < 3)
                    break;
            }

            sw.Close();

            FileStream f = new FileStream("data.txt", FileMode.Open);
            StreamReader sr = new StreamReader(f);
            string str = sr.ReadToEnd();
            sr.Close();
            str = str.Replace(',', '.');
            sw = new StreamWriter(new FileStream("data.txt", FileMode.Create));
            sw.Write(str);
            sw.Close();
        }

        static void Main(string[] args)
        {
            Point end = new Point(47, 100); //Пункт назначения
            double wheel_distance = 5; //Расстояние между колесами
            double speed_max = 2; //Максимальная скорость колеса
            double k_turn = 0.00001; //Коэффицент поворота (от 0 до 1), чем больше, тем более плавный поворот, при 1 машинка всегда едет прямо

            

            int iterations = 1000;

            Test(wheel_distance, end, k_turn, speed_max, iterations);
        }
    }
}