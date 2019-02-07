using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemSharp
{
    delegate void ChangeHandler(object source, WheelsEventArgs args);

    class Wheels
    {
        public event ChangeHandler ChangePositionEvent;

        public double WheelsDistance { get; private set; }
        public Point LeftWheel { get; set; }
        public Point RightWheel { get; set; }
        public Point Center { get { return new Point((LeftWheel.X + RightWheel.X) / 2, (LeftWheel.Y + RightWheel.Y) / 2); } }

        public Wheels(double WheelsDistance)
        {
            this.WheelsDistance = WheelsDistance;
            this.LeftWheel = new Point(-WheelsDistance / 2, 0);
            this.RightWheel = new Point(WheelsDistance / 2, 0);
        }
         
        //Изменение положения колёс
        public void ChangePosition(double speed_left, double speed_right)
        {
            Vector left_right = new Vector(LeftWheel, RightWheel); //Вектор от левого колеса к правому

            Vector new_position_left = new Vector(LeftWheel, speed_left, left_right); //Левое колесо вперед на speed_left перпендикулярно оси колёс
            Vector new_position_right = new Vector(RightWheel, speed_right, left_right); //Правое

            //Присвоение новых позиций колёсам
            RightWheel = new_position_right.PointEnd;
            LeftWheel = new_position_left.PointEnd;

            //Изменение положения колеса, которое больше проехало для сохранения расстояния между колёсами
            if (speed_left > speed_right)
            {
                TurnRight();
                ChangePositionEvent(this, new WheelsEventArgs(LeftWheel, Center, RightWheel, new_position_left.PointBegin, new_position_right));
            }
            else if (speed_right > speed_left)
            {
                TurnLeft();
                ChangePositionEvent(this, new WheelsEventArgs(LeftWheel, Center, RightWheel, new_position_left.PointBegin, new_position_left));
            }
        }

        private void TurnLeft()
        {
            Vector new_position_right = new Vector(new Vector(LeftWheel, RightWheel), WheelsDistance);
            RightWheel = new_position_right.PointEnd;
        }

        private void TurnRight()
        {
            Vector new_position_left = new Vector(new Vector(RightWheel, LeftWheel), WheelsDistance);
            LeftWheel = new_position_left.PointEnd;
        }

        public override string ToString()
        {
            return LeftWheel.ToString() + " " +  Center.ToString() +  " " + RightWheel.ToString();
        }
    }
}