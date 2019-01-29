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
        //Обновление расстояния от значимых точек до цели
        static void ChangeDistance(ref Vector left_kol_end, ref Vector right_kol_end, ref Vector center_kol_end, Wheels wheels, Point end)
        {
            left_kol_end = new Vector(wheels.LeftWheel, end);
            right_kol_end = new Vector(wheels.RightWheel, end);
            center_kol_end = new Vector(wheels.Center, end);
        }

        //Написание и запуск скрипта GnuPlot
        static void RunPlot()
        {
            string[] titles = { "center", "left wheel", "right wheel" };

            GnuPlot.CreateScript("D:\\repos\\PSTU\\Robotics\\TestSystemSharp\\TestSystemSharp\\bin\\Debug\\data.txt",
                "Script.plt", 3, titles);
            GnuPlot.RunScript("Script.plt");
        }

        //Запись значимых точек в текстовый файл
        static bool flag = true;
        static void DataWrite(StreamWriter sw, Wheels wheel)
        {

            if (flag)
            {
                sw.WriteLine(wheel.ToString().Replace(',', '.') + " " + wheel.LeftWheel.ToString().Replace(',', '.'));
                sw.WriteLine(wheel.ToString().Replace(',', '.') + " " + wheel.RightWheel.ToString().Replace(',', '.'));
            }
            else
            {
                sw.WriteLine(wheel.ToString().Replace(',', '.') + " " + wheel.RightWheel.ToString().Replace(',', '.'));
                sw.WriteLine(wheel.ToString().Replace(',', '.') + " " + wheel.LeftWheel.ToString().Replace(',', '.'));
            }

            flag = !flag;
        }

        static void Test(double wheel_distance, Point end, double k_turn, double speed_max, int iterations)
        {
            Wheels wheels = new Wheels(wheel_distance);
            Vector left_kol_end = new Vector(wheels.LeftWheel, end);
            Vector right_kol_end = new Vector(wheels.RightWheel, end);
            Vector center_kol_end = new Vector(wheels.Center, end);

            StreamWriter sw = new StreamWriter(new FileStream("data.txt", FileMode.Create));
            
            DataWrite(sw, wheels);

            for (int i = 0; i < iterations; i++)
            {
                //Регулировка через разность расстояний от колес до цели
                //wheels = TriangleRegulate.RegulateSpeed(left_kol_end.Length, right_kol_end.Length, wheels, speed_max);
                wheels = ArcRegulate.RegulateSpeed(wheels, end, speed_max);
                
                DataWrite(sw, wheels);
                
                ChangeDistance(ref left_kol_end, ref right_kol_end, ref center_kol_end, wheels, end);

                if (center_kol_end.Length < speed_max / 2) break;
            }
            sw.Close();

            RunPlot();
        }

        static void Main(string[] args)
        {
            Point end = new Point(0, -100); //Пункт назначения
            double wheel_distance = 5; //Расстояние между колесами
            double speed_max = 2; //Максимальная скорость колеса
            double k_turn = 0.2; //Коэффицент поворота (от 0 до 1), чем больше, тем более плавный поворот, при 1 машинка всегда едет прямо
            int iterations = 100; //Максимальное время работы

            Test(wheel_distance, end, k_turn, speed_max, iterations);

            //Point test = ArcRegulate.FindCenter(new Wheels(4), new Point(2,4));

            
        }
    }
}