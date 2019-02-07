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
        //Написание и запуск скрипта GnuPlot
        static void RunPlot()
        {
            string[] titles = { "AxisPerpendicular", "Left wheel", "Center", "Right wheel" };
            string data = "D:\\repos\\PSTU\\Robotics\\TestSystemSharp\\TestSystemSharp\\bin\\Debug\\data.txt";
            int kol_graph = 4;

            GnuPlot.CreateScript(data,
                "Script.plt", kol_graph, titles);
            GnuPlot.RunScript("Script.plt");
        }

        static void Test(double wheel_distance, Point end, double speed_max, int iterations)
        {
            Wheels wheels = new Wheels(wheel_distance);
            TriangleRegulate triangle_regulator = new TriangleRegulate(end, wheels);

            JournalPointWheels journal = new JournalPointWheels();
            wheels.ChangePositionEvent += journal.Add;

            StreamWriter sw = new StreamWriter(new FileStream("data.txt", FileMode.Create));

            sw.Write("0 0 " + wheels.ToString().Replace(',', '.') + '\n');

            for (int i = 0; i < iterations; i++)
            {
                wheels = triangle_regulator.RegulateSpeed(wheels, speed_max);
                if (triangle_regulator.CenterEndVect.Length < speed_max / 2) break;
            }

            sw.Write(journal.ToString());
            sw.Close();

            RunPlot();
        }

        static void Main(string[] args)
        {
            Point end = new Point(100, 0); //Пункт назначения
            double wheel_distance = 5; //Расстояние между колесами
            double speed_max = 15; //Максимальная скорость колеса
            int iterations = 1000; //Максимальное время работы

            Test(wheel_distance, end, speed_max, iterations);

            //Point test = ArcRegulate.FindCenter(new Wheels(4), new Point(2,4));

            
        }
    }
}