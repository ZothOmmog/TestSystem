using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    static class ArcRegulate
    {
        //Поиск вектора с началов в центре между колёс и концом в центре окружности проходящей через центр между 
        //колёс и цель и перпендикулярной к отрезку между левым и правым колесом
        static public Vector FindRad(Wheels wheels, Point end)
        {
            Vector center_wheels_end_vector = new Vector(end, wheels.Center);
            Point center_wheels_end = new Point((end.X + wheels.Center.X) / 2, (end.Y + wheels.Center.Y) / 2);
            Point help_point  = (new Vector(center_wheels_end, center_wheels_end_vector.Length * 2, center_wheels_end_vector)).PointEnd;

            return new Vector(wheels.Center, help_point);
            //double a_1 = wheels.RightWheel.Y - wheels.LeftWheel.Y;
            //double b_1 = wheels.RightWheel.X - wheels.LeftWheel.X;
            //double c_1 = wheels.LeftWheel.X * a_1;
            //double d_1 = wheels.LeftWheel.Y * b_1;
            //double e_1 = c_1 - d_1;

            //double a_2 = help_point.Y - center_wheels_end.Y;
            //double b_2 = help_point.X - center_wheels_end.X;
            //double c_2 = center_wheels_end.X * a_2;
            //double d_2 = center_wheels_end.Y * b_2;
            //double e_2 = c_2 - d_2;

            //double f = a_1 * b_2 - a_2 * b_1;

            //double x = (b_2 * e_1 - b_1 * e_2) / f;
            //double y = (a_2 * e_1 - a_1 * e_2) / f;

            //0.866

            //return new Vector(wheels.Center, new Point(x, y));
        }

        //Вычисление коэффицента разности скорости колёс
        static public Wheels RegulateSpeed(Wheels wheels, Point end, double speed)
        {
            Vector rad = FindRad(wheels, end);

            Vector left_center = new Vector(wheels.LeftWheel, rad.PointEnd);
            Vector right_center = new Vector(wheels.RightWheel, rad.PointEnd);

            double delta = right_center.Length - right_center.Length;
            if(left_center.Length > right_center.Length)
            {
                wheels.RightWheel = (new Vector(wheels.RightWheel, speed, new Vector(wheels.LeftWheel, wheels.RightWheel))).PointEnd;
                Vector new_right_center = new Vector(rad.PointEnd, wheels.RightWheel);
                if(new_right_center.Length < rad.Length)
                {
                    wheels.LeftWheel = (new Vector(new Vector(rad.PointEnd, wheels.RightWheel), rad.Length + (wheels.WheelsDistance - (rad.Length - new_right_center.Length)))).PointEnd;
                }
                else
                {
                    wheels.LeftWheel = new Vector(new Vector(rad.PointEnd, wheels.RightWheel), new_right_center.Length + wheels.WheelsDistance).PointEnd;
                }
            }
            else
            {
                rad.PointEnd = new Point(-rad.PointEnd.X, -rad.PointEnd.Y);
                wheels.LeftWheel = (new Vector(wheels.LeftWheel, speed, new Vector(wheels.LeftWheel, wheels.RightWheel))).PointEnd;
                Vector new_left_center = new Vector(rad.PointEnd, wheels.LeftWheel);
                if (new_left_center.Length < rad.Length)
                {
                    wheels.RightWheel = (new Vector(new_left_center, rad.Length + (wheels.WheelsDistance - (rad.Length - new_left_center.Length)))).PointEnd;
                }
                else
                {
                    wheels.RightWheel = new Vector(new_left_center, new_left_center.Length + wheels.WheelsDistance).PointEnd;
                }
            }

            return wheels;
        }

        
    }
}
